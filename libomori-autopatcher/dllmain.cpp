#include <windows.h>

void main()
{
    HANDLE pipe = CreateFileW(L"\\\\.\\pipe\\omori-autopatcher-pipe", GENERIC_READ | GENERIC_WRITE, FILE_SHARE_WRITE, NULL, OPEN_EXISTING, 0, NULL);
    if (pipe == nullptr)
    {
        MessageBoxW(nullptr, L"Failed to connect to named pipe", L"Error", MB_OK | MB_ICONERROR);
        return;
    }

    auto* msg = "test\r\n";
    WriteFile(pipe, msg, strlen(msg), nullptr, nullptr);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
    if (ul_reason_for_call == DLL_PROCESS_ATTACH) main();
	
    return TRUE;
}

