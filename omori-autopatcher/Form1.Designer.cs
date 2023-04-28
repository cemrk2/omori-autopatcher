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
            this.statusLbl = new System.Windows.Forms.Label();
            this.dumpExeBtn = new MaterialSkin.Controls.MaterialButton();
            this.dumpBtn = new MaterialSkin.Controls.MaterialButton();
            this.patchBtn = new MaterialSkin.Controls.MaterialButton();
            this.unpatchBtn = new MaterialSkin.Controls.MaterialButton();
            this.progressBar = new MaterialSkin.Controls.MaterialProgressBar();
            this.SuspendLayout();
            // 
            // statusLbl
            // 
            this.statusLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLbl.Location = new System.Drawing.Point(0, 91);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(543, 36);
            this.statusLbl.TabIndex = 4;
            // 
            // dumpExeBtn
            // 
            this.dumpExeBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dumpExeBtn.BackColor = System.Drawing.SystemColors.Control;
            this.dumpExeBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.dumpExeBtn.Depth = 0;
            this.dumpExeBtn.HighEmphasis = true;
            this.dumpExeBtn.Icon = null;
            this.dumpExeBtn.Location = new System.Drawing.Point(7, 133);
            this.dumpExeBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.dumpExeBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.dumpExeBtn.Name = "dumpExeBtn";
            this.dumpExeBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.dumpExeBtn.Size = new System.Drawing.Size(156, 36);
            this.dumpExeBtn.TabIndex = 8;
            this.dumpExeBtn.Text = "Dump Executable";
            this.dumpExeBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.dumpExeBtn.UseAccentColor = false;
            this.dumpExeBtn.UseVisualStyleBackColor = false;
            this.dumpExeBtn.Click += new System.EventHandler(this.dumpExeBtn_Click);
            // 
            // dumpBtn
            // 
            this.dumpBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dumpBtn.BackColor = System.Drawing.SystemColors.Control;
            this.dumpBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.dumpBtn.Depth = 0;
            this.dumpBtn.HighEmphasis = true;
            this.dumpBtn.Icon = null;
            this.dumpBtn.Location = new System.Drawing.Point(171, 133);
            this.dumpBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.dumpBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.dumpBtn.Name = "dumpBtn";
            this.dumpBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.dumpBtn.Size = new System.Drawing.Size(108, 36);
            this.dumpBtn.TabIndex = 9;
            this.dumpBtn.Text = "Dump Game";
            this.dumpBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.dumpBtn.UseAccentColor = false;
            this.dumpBtn.UseVisualStyleBackColor = false;
            this.dumpBtn.Click += new System.EventHandler(this.dumpBtn_Click);
            // 
            // patchBtn
            // 
            this.patchBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.patchBtn.BackColor = System.Drawing.SystemColors.Control;
            this.patchBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.patchBtn.Depth = 0;
            this.patchBtn.HighEmphasis = true;
            this.patchBtn.Icon = null;
            this.patchBtn.Location = new System.Drawing.Point(287, 132);
            this.patchBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.patchBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.patchBtn.Name = "patchBtn";
            this.patchBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.patchBtn.Size = new System.Drawing.Size(114, 36);
            this.patchBtn.TabIndex = 10;
            this.patchBtn.Text = "Patch Game";
            this.patchBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.patchBtn.UseAccentColor = false;
            this.patchBtn.UseVisualStyleBackColor = false;
            this.patchBtn.Click += new System.EventHandler(this.patchBtn_Click);
            // 
            // unpatchBtn
            // 
            this.unpatchBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.unpatchBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.unpatchBtn.BackColor = System.Drawing.SystemColors.Control;
            this.unpatchBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.unpatchBtn.Depth = 0;
            this.unpatchBtn.HighEmphasis = true;
            this.unpatchBtn.Icon = null;
            this.unpatchBtn.Location = new System.Drawing.Point(409, 133);
            this.unpatchBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.unpatchBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.unpatchBtn.Name = "unpatchBtn";
            this.unpatchBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.unpatchBtn.Size = new System.Drawing.Size(134, 36);
            this.unpatchBtn.TabIndex = 11;
            this.unpatchBtn.Text = "Unpatch Game";
            this.unpatchBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.unpatchBtn.UseAccentColor = false;
            this.unpatchBtn.UseVisualStyleBackColor = false;
            this.unpatchBtn.Click += new System.EventHandler(this.unpatchBtn_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Depth = 0;
            this.progressBar.Location = new System.Drawing.Point(-3, 77);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar.MouseState = MaterialSkin.MouseState.HOVER;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(554, 5);
            this.progressBar.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(550, 176);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.unpatchBtn);
            this.Controls.Add(this.patchBtn);
            this.Controls.Add(this.dumpBtn);
            this.Controls.Add(this.dumpExeBtn);
            this.Controls.Add(this.statusLbl);
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(3, 64, 3, 2);
            this.Text = "OMORI AutoPatcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private MaterialSkin.Controls.MaterialProgressBar progressBar;

        private MaterialSkin.Controls.MaterialButton dumpBtn;
        private MaterialSkin.Controls.MaterialButton patchBtn;
        private MaterialSkin.Controls.MaterialButton unpatchBtn;

        private MaterialSkin.Controls.MaterialButton dumpExeBtn;

        private System.Windows.Forms.Label statusLbl;

        #endregion
    }
}