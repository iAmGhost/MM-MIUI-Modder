namespace MM
{
    partial class Form_ZipPackaging
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
            this.components = new System.ComponentModel.Container();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label_Included_Files = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Button_Save = new System.Windows.Forms.Button();
            this.fileListBox = new System.Windows.Forms.CheckedListBox();
            this.label_Patch_Desc = new System.Windows.Forms.Label();
            this.textBox_PatchInfo = new System.Windows.Forms.TextBox();
            this.button_UncheckAll = new System.Windows.Forms.Button();
            this.button_CheckAll = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "zip";
            this.saveFileDialog1.Filter = "Zip 파일|*.zip";
            // 
            // label_Included_Files
            // 
            this.label_Included_Files.AutoSize = true;
            this.label_Included_Files.Location = new System.Drawing.Point(14, 167);
            this.label_Included_Files.Name = "label_Included_Files";
            this.label_Included_Files.Size = new System.Drawing.Size(69, 12);
            this.label_Included_Files.TabIndex = 6;
            this.label_Included_Files.Text = "포함할 파일";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 306);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(316, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // Button_Save
            // 
            this.Button_Save.Location = new System.Drawing.Point(12, 335);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(317, 48);
            this.Button_Save.TabIndex = 4;
            this.Button_Save.Text = "저장!";
            this.Button_Save.UseVisualStyleBackColor = true;
            this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // fileListBox
            // 
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.Location = new System.Drawing.Point(12, 182);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.Size = new System.Drawing.Size(318, 84);
            this.fileListBox.TabIndex = 3;
            // 
            // label_Patch_Desc
            // 
            this.label_Patch_Desc.AutoSize = true;
            this.label_Patch_Desc.Location = new System.Drawing.Point(12, 9);
            this.label_Patch_Desc.Name = "label_Patch_Desc";
            this.label_Patch_Desc.Size = new System.Drawing.Size(57, 12);
            this.label_Patch_Desc.TabIndex = 2;
            this.label_Patch_Desc.Text = "패치 정보";
            // 
            // textBox_PatchInfo
            // 
            this.textBox_PatchInfo.Location = new System.Drawing.Point(12, 24);
            this.textBox_PatchInfo.Multiline = true;
            this.textBox_PatchInfo.Name = "textBox_PatchInfo";
            this.textBox_PatchInfo.Size = new System.Drawing.Size(318, 140);
            this.textBox_PatchInfo.TabIndex = 1;
            // 
            // button_UncheckAll
            // 
            this.button_UncheckAll.Location = new System.Drawing.Point(173, 272);
            this.button_UncheckAll.Name = "button_UncheckAll";
            this.button_UncheckAll.Size = new System.Drawing.Size(158, 29);
            this.button_UncheckAll.TabIndex = 8;
            this.button_UncheckAll.Text = "모두 선택 해제";
            this.button_UncheckAll.UseVisualStyleBackColor = true;
            this.button_UncheckAll.Click += new System.EventHandler(this.button_UncheckAll_Click);
            // 
            // button_CheckAll
            // 
            this.button_CheckAll.Location = new System.Drawing.Point(12, 271);
            this.button_CheckAll.Name = "button_CheckAll";
            this.button_CheckAll.Size = new System.Drawing.Size(155, 29);
            this.button_CheckAll.TabIndex = 7;
            this.button_CheckAll.Text = "모두 선택";
            this.button_CheckAll.UseVisualStyleBackColor = true;
            this.button_CheckAll.Click += new System.EventHandler(this.button_CheckAll_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form_ZipPackaging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 387);
            this.Controls.Add(this.button_UncheckAll);
            this.Controls.Add(this.button_CheckAll);
            this.Controls.Add(this.label_Included_Files);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Button_Save);
            this.Controls.Add(this.fileListBox);
            this.Controls.Add(this.label_Patch_Desc);
            this.Controls.Add(this.textBox_PatchInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form_ZipPackaging";
            this.Text = "ZIP 패키지 만들기";
            this.Load += new System.EventHandler(this.Form_ZipPackaging_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_PatchInfo;
        private System.Windows.Forms.Label label_Patch_Desc;
        private System.Windows.Forms.CheckedListBox fileListBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button Button_Save;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label_Included_Files;
        private System.Windows.Forms.Button button_UncheckAll;
        private System.Windows.Forms.Button button_CheckAll;
        private System.Windows.Forms.Timer timer1;
    }
}