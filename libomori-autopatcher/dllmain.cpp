#include <fcntl.h>
#include <cstdio>
#include <iostream>
#include <codecvt>
#include <io.h>
#include <windows.h>

void BindCrtHandlesToStdHandles(bool bindStdIn, bool bindStdOut, bool bindStdErr)
{
    // Re-initialize the C runtime "FILE" handles with clean handles bound to "nul". We do this because it has been
    // observed that the file number of our standard handle file objects can be assigned internally to a value of -2
    // when not bound to a valid target, which represents some kind of unknown internal invalid state. In this state our
    // call to "_dup2" fails, as it specifically tests to ensure that the target file number isn't equal to this value
    // before allowing the operation to continue. We can resolve this issue by first "re-opening" the target files to
    // use the "nul" device, which will place them into a valid state, after which we can redirect them to our target
    // using the "_dup2" function.
    if (bindStdIn)
    {
        FILE* dummyFile;
        freopen_s(&dummyFile, "nul", "r", stdin);
    }
    if (bindStdOut)
    {
        FILE* dummyFile;
        freopen_s(&dummyFile, "nul", "w", stdout);
    }
    if (bindStdErr)
    {
        FILE* dummyFile;
        freopen_s(&dummyFile, "nul", "w", stderr);
    }

    // Redirect unbuffered stdin from the current standard input handle
    if (bindStdIn)
    {
        HANDLE stdHandle = GetStdHandle(STD_INPUT_HANDLE);
        if (stdHandle != INVALID_HANDLE_VALUE)
        {
            int fileDescriptor = _open_osfhandle((intptr_t)stdHandle, _O_TEXT);
            if (fileDescriptor != -1)
            {
                FILE* file = _fdopen(fileDescriptor, "r");
                if (file != nullptr)
                {
                    int dup2Result = _dup2(_fileno(file), _fileno(stdin));
                    if (dup2Result == 0)
                    {
                        setvbuf(stdin, nullptr, _IONBF, 0);
                    }
                }
            }
        }
    }

    // Redirect unbuffered stdout to the current standard output handle
    if (bindStdOut)
    {
        HANDLE stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
        if (stdHandle != INVALID_HANDLE_VALUE)
        {
            int fileDescriptor = _open_osfhandle((intptr_t)stdHandle, _O_TEXT);
            if (fileDescriptor != -1)
            {
                FILE* file = _fdopen(fileDescriptor, "w");
                if (file != nullptr)
                {
                    int dup2Result = _dup2(_fileno(file), _fileno(stdout));
                    if (dup2Result == 0)
                    {
                        setvbuf(stdout, nullptr, _IONBF, 0);
                    }
                }
            }
        }
    }

    // Redirect unbuffered stderr to the current standard error handle
    if (bindStdErr)
    {
        HANDLE stdHandle = GetStdHandle(STD_ERROR_HANDLE);
        if (stdHandle != INVALID_HANDLE_VALUE)
        {
            int fileDescriptor = _open_osfhandle((intptr_t)stdHandle, _O_TEXT);
            if (fileDescriptor != -1)
            {
                FILE* file = _fdopen(fileDescriptor, "w");
                if (file != nullptr)
                {
                    int dup2Result = _dup2(_fileno(file), _fileno(stderr));
                    if (dup2Result == 0)
                    {
                        setvbuf(stderr, nullptr, _IONBF, 0);
                    }
                }
            }
        }
    }

    // Clear the error state for each of the C++ standard stream objects. We need to do this, as attempts to access the
    // standard streams before they refer to a valid target will cause the iostream objects to enter an error state. In
    // versions of Visual Studio after 2005, this seems to always occur during startup regardless of whether anything
    // has been read from or written to the targets or not.
    if (bindStdIn)
    {
        std::wcin.clear();
        std::cin.clear();
    }
    if (bindStdOut)
    {
        std::wcout.clear();
        std::cout.clear();
    }
    if (bindStdErr)
    {
        std::wcerr.clear();
        std::cerr.clear();
    }
}

struct FileData {
    BYTE* data;
    size_t size;
};

FileData ReadFileData(const char* filename)
{
    OFSTRUCT finfo;
    auto handle = reinterpret_cast<HANDLE>(OpenFile(filename, &finfo, OF_READ));
    if (handle == nullptr)
    {
        printf("Failed to open file for reading: %s\n", filename);
        return {
            nullptr,
            0
        };
    }

    DWORD size = GetFileSize(handle, nullptr);
    void* buffer = malloc(size);

    if (!ReadFile(handle, buffer, size, nullptr, nullptr))
    {
        printf("Failed to read file: %s\n", filename);
        return {
            nullptr,
            size
    };
    }
    CloseHandle(handle);

    return {
        (BYTE*)buffer,
        size
    };
}

void ReadString(HANDLE fileHandle, char* output) {
    ULONG read = 0;
    int index = 0;
    do {
        ReadFile(fileHandle, output + index++, 1, &read, nullptr);
    } while (read > 0 && *(output + index - 1) != 0);
}

bool processFile(HANDLE pipe, std::string filePath)
{
    std::cout << "cmd: " << filePath << "\n";
    // really hacky ipc
    if (filePath == ":freeLibrary")
    {
        bool msg = true;
        WriteFile(pipe, &msg, 1, nullptr, nullptr);
        std::cout << "FreeLibraryAndExitThread\n";
        FreeLibraryAndExitThread(GetModuleHandleA("libomori-autopatcher.dll"), 0);
    }
    
    auto colonPos = filePath.find(std::string(":"));
    auto inputPath = filePath.substr(0, colonPos);
    auto outputPath = filePath.substr(colonPos+1, filePath.length());

    std::cout << inputPath << "\n";
    std::cout << outputPath << "\n";

    auto data = ReadFileData(inputPath.c_str());
    std::cout << "Read: " << data.size << " bytes\n";

    std::cout << "Writing...\n";
    auto handle = CreateFileA(outputPath.c_str(), FILE_GENERIC_WRITE, false, nullptr, CREATE_NEW, FILE_ATTRIBUTE_NORMAL, nullptr);
    std::cout << GetLastError() << "\n";
    WriteFile(handle, data.data, data.size, nullptr, nullptr);
    CloseHandle(handle);
    std::cout << "Ok.\n";
    
    bool msg = true;
    WriteFile(pipe, &msg, 1, nullptr, nullptr);

    return true;
}

void main()
{
    AllocConsole();
    BindCrtHandlesToStdHandles(true, true, true);
    HANDLE pipe = CreateFileW(L"\\\\.\\pipe\\omori-autopatcher-pipe", GENERIC_READ | GENERIC_WRITE, FILE_SHARE_WRITE, NULL, OPEN_EXISTING, 0, NULL);
    if (pipe == nullptr)
    {
        MessageBoxW(nullptr, L"Failed to connect to named pipe", L"Error", MB_OK | MB_ICONERROR);
        return;
    }

    // file1:file2, max path length is 255
    auto* filePathBuffer = static_cast<char*>(malloc(255 + 255 + 1));
    ReadString(pipe, filePathBuffer);
    std::string filePath(filePathBuffer);
    free(filePathBuffer);

    while (processFile(pipe, filePath))
    {
        filePathBuffer = static_cast<char*>(malloc(255 + 255 + 1));
        ReadString(pipe, filePathBuffer);
        filePath= std::string(filePathBuffer);
        free(filePathBuffer);
    }
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
    if (ul_reason_for_call == DLL_PROCESS_ATTACH) main();
	
    return TRUE;
}

