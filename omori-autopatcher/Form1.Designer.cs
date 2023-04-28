namespace omori_autopatcher
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dumpBtn = new System.Windows.Forms.Button();
            this.statusLbl = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.dumpExeBtn = new System.Windows.Forms.Button();
            this.patchBtn = new System.Windows.Forms.Button();
            this.unpatchBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dumpBtn
            // 
            this.dumpBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dumpBtn.Location = new System.Drawing.Point(280, 12);
            this.dumpBtn.Name = "dumpBtn";
            this.dumpBtn.Size = new System.Drawing.Size(107, 29);
            this.dumpBtn.TabIndex = 0;
            this.dumpBtn.Text = "Dump Game";
            this.dumpBtn.UseVisualStyleBackColor = true;
            this.dumpBtn.Click += new System.EventHandler(this.dumpBtn_Click);
            // 
            // statusLbl
            // 
            this.statusLbl.Location = new System.Drawing.Point(12, 76);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(683, 36);
            this.statusLbl.TabIndex = 4;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 50);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(683, 23);
            this.progressBar.TabIndex = 3;
            // 
            // dumpExeBtn
            // 
            this.dumpExeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dumpExeBtn.Location = new System.Drawing.Point(128, 12);
            this.dumpExeBtn.Name = "dumpExeBtn";
            this.dumpExeBtn.Size = new System.Drawing.Size(146, 29);
            this.dumpExeBtn.TabIndex = 5;
            this.dumpExeBtn.Text = "Dump Executable";
            this.dumpExeBtn.UseVisualStyleBackColor = true;
            this.dumpExeBtn.Click += new System.EventHandler(this.dumpExeBtn_Click);
            // 
            // patchBtn
            // 
            this.patchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.patchBtn.Location = new System.Drawing.Point(393, 12);
            this.patchBtn.Name = "patchBtn";
            this.patchBtn.Size = new System.Drawing.Size(107, 29);
            this.patchBtn.TabIndex = 6;
            this.patchBtn.Text = "Patch Game";
            this.patchBtn.UseVisualStyleBackColor = true;
            this.patchBtn.Click += new System.EventHandler(this.patchBtn_Click);
            // 
            // unpatchBtn
            // 
            this.unpatchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.unpatchBtn.Location = new System.Drawing.Point(506, 12);
            this.unpatchBtn.Name = "unpatchBtn";
            this.unpatchBtn.Size = new System.Drawing.Size(107, 29);
            this.unpatchBtn.TabIndex = 7;
            this.unpatchBtn.Text = "Unpatch Game";
            this.unpatchBtn.UseVisualStyleBackColor = true;
            this.unpatchBtn.Click += new System.EventHandler(this.unpatchBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(707, 118);
            this.Controls.Add(this.unpatchBtn);
            this.Controls.Add(this.patchBtn);
            this.Controls.Add(this.dumpExeBtn);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.dumpBtn);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button unpatchBtn;

        private System.Windows.Forms.Button dumpExeBtn;
        private System.Windows.Forms.Button patchBtn;

        private System.Windows.Forms.Label statusLbl;
        private System.Windows.Forms.ProgressBar progressBar;

        private System.Windows.Forms.Button dumpBtn;

        #endregion
    }
}