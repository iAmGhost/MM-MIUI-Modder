namespace MM
{
    partial class Form_AdbFileWindow
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
            this.debugTextBox = new System.Windows.Forms.TextBox();
            this.button_PullPushFiles = new System.Windows.Forms.Button();
            this.label_Caution = new System.Windows.Forms.Label();
            this.button_UncheckAll = new System.Windows.Forms.Button();
            this.button_CheckAll = new System.Windows.Forms.Button();
            this.fileListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // debugTextBox
            // 
            this.debugTextBox.Location = new System.Drawing.Point(14, 197);
            this.debugTextBox.Multiline = true;
            this.debugTextBox.Name = "debugTextBox";
            this.debugTextBox.Size = new System.Drawing.Size(435, 176);
            this.debugTextBox.TabIndex = 5;
            // 
            // button_PullPushFiles
            // 
            this.button_PullPushFiles.Location = new System.Drawing.Point(225, 162);
            this.button_PullPushFiles.Name = "button_PullPushFiles";
            this.button_PullPushFiles.Size = new System.Drawing.Size(221, 29);
            this.button_PullPushFiles.TabIndex = 4;
            this.button_PullPushFiles.Text = "가져오기";
            this.button_PullPushFiles.UseVisualStyleBackColor = true;
            this.button_PullPushFiles.Click += new System.EventHandler(this.button_PullFiles_Click);
            // 
            // label_Caution
            // 
            this.label_Caution.AutoSize = true;
            this.label_Caution.Location = new System.Drawing.Point(12, 147);
            this.label_Caution.Name = "label_Caution";
            this.label_Caution.Size = new System.Drawing.Size(389, 12);
            this.label_Caution.TabIndex = 3;
            this.label_Caution.Text = "주의: 이미 존재하는 파일을 체크하면 핸드폰으로부터 새로 받아옵니다.";
            // 
            // button_UncheckAll
            // 
            this.button_UncheckAll.Location = new System.Drawing.Point(113, 162);
            this.button_UncheckAll.Name = "button_UncheckAll";
            this.button_UncheckAll.Size = new System.Drawing.Size(106, 29);
            this.button_UncheckAll.TabIndex = 2;
            this.button_UncheckAll.Text = "모두 선택 해제";
            this.button_UncheckAll.UseVisualStyleBackColor = true;
            this.button_UncheckAll.Click += new System.EventHandler(this.button_UncheckAll_Click);
            // 
            // button_CheckAll
            // 
            this.button_CheckAll.Location = new System.Drawing.Point(12, 162);
            this.button_CheckAll.Name = "button_CheckAll";
            this.button_CheckAll.Size = new System.Drawing.Size(95, 29);
            this.button_CheckAll.TabIndex = 1;
            this.button_CheckAll.Text = "모두 선택";
            this.button_CheckAll.UseVisualStyleBackColor = true;
            this.button_CheckAll.Click += new System.EventHandler(this.button_CheckAll_Click);
            // 
            // fileListBox
            // 
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.Location = new System.Drawing.Point(12, 12);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.Size = new System.Drawing.Size(434, 132);
            this.fileListBox.TabIndex = 0;
            // 
            // Form_AdbFileWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 413);
            this.Controls.Add(this.debugTextBox);
            this.Controls.Add(this.button_PullPushFiles);
            this.Controls.Add(this.label_Caution);
            this.Controls.Add(this.button_UncheckAll);
            this.Controls.Add(this.button_CheckAll);
            this.Controls.Add(this.fileListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form_AdbFileWindow";
            this.Text = "파일 관리자";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_AdbFileGetWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_AdbFileGetWindow_FormClosed);
            this.Load += new System.EventHandler(this.Form_AdbFileGetWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox fileListBox;
        private System.Windows.Forms.Button button_CheckAll;
        private System.Windows.Forms.Button button_UncheckAll;
        private System.Windows.Forms.Label label_Caution;
        private System.Windows.Forms.Button button_PullPushFiles;
        private System.Windows.Forms.TextBox debugTextBox;
    }
}