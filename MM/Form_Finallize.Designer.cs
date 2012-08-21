namespace MM
{
    partial class Form_Finallize
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
            this.label_ModdingFinished = new System.Windows.Forms.Label();
            this.button_sendWithAdb = new System.Windows.Forms.Button();
            this.button_Packaging = new System.Windows.Forms.Button();
            this.label_ModdingFinishedCaption = new System.Windows.Forms.Label();
            this.label_AdbFileWindow_Desc = new System.Windows.Forms.Label();
            this.label_ZipPackaging_Desc = new System.Windows.Forms.Label();
            this.label_Workspace_Save_Desc = new System.Windows.Forms.Label();
            this.button_SaveFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_ModdingFinished
            // 
            this.label_ModdingFinished.AutoSize = true;
            this.label_ModdingFinished.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_ModdingFinished.Location = new System.Drawing.Point(12, 9);
            this.label_ModdingFinished.Name = "label_ModdingFinished";
            this.label_ModdingFinished.Size = new System.Drawing.Size(114, 21);
            this.label_ModdingFinished.TabIndex = 1;
            this.label_ModdingFinished.Text = "패치 완료!";
            // 
            // button_sendWithAdb
            // 
            this.button_sendWithAdb.Location = new System.Drawing.Point(372, 78);
            this.button_sendWithAdb.Name = "button_sendWithAdb";
            this.button_sendWithAdb.Size = new System.Drawing.Size(202, 80);
            this.button_sendWithAdb.TabIndex = 2;
            this.button_sendWithAdb.Text = "폰으로 파일 보내기";
            this.button_sendWithAdb.UseVisualStyleBackColor = true;
            this.button_sendWithAdb.Click += new System.EventHandler(this.button_sendWithAdb_Click);
            // 
            // button_Packaging
            // 
            this.button_Packaging.Location = new System.Drawing.Point(372, 169);
            this.button_Packaging.Name = "button_Packaging";
            this.button_Packaging.Size = new System.Drawing.Size(202, 80);
            this.button_Packaging.TabIndex = 3;
            this.button_Packaging.Text = "ZIP 패키지 만들기";
            this.button_Packaging.UseVisualStyleBackColor = true;
            this.button_Packaging.Click += new System.EventHandler(this.button_Packaging_Click);
            // 
            // label_ModdingFinishedCaption
            // 
            this.label_ModdingFinishedCaption.AutoSize = true;
            this.label_ModdingFinishedCaption.Location = new System.Drawing.Point(14, 46);
            this.label_ModdingFinishedCaption.Name = "label_ModdingFinishedCaption";
            this.label_ModdingFinishedCaption.Size = new System.Drawing.Size(189, 12);
            this.label_ModdingFinishedCaption.TabIndex = 4;
            this.label_ModdingFinishedCaption.Text = "패치 작업이 모두 완료되었습니다.";
            // 
            // label_AdbFileWindow_Desc
            // 
            this.label_AdbFileWindow_Desc.AutoSize = true;
            this.label_AdbFileWindow_Desc.Location = new System.Drawing.Point(14, 112);
            this.label_AdbFileWindow_Desc.Name = "label_AdbFileWindow_Desc";
            this.label_AdbFileWindow_Desc.Size = new System.Drawing.Size(305, 12);
            this.label_AdbFileWindow_Desc.TabIndex = 5;
            this.label_AdbFileWindow_Desc.Text = "연결된 스마트폰으로 패치된 파일들을 바로 전송합니다.";
            // 
            // label_ZipPackaging_Desc
            // 
            this.label_ZipPackaging_Desc.AutoSize = true;
            this.label_ZipPackaging_Desc.Location = new System.Drawing.Point(14, 203);
            this.label_ZipPackaging_Desc.Name = "label_ZipPackaging_Desc";
            this.label_ZipPackaging_Desc.Size = new System.Drawing.Size(276, 12);
            this.label_ZipPackaging_Desc.TabIndex = 6;
            this.label_ZipPackaging_Desc.Text = "리커버리에서 설치 가능한 ZIP 패키지를 만듭니다.";
            // 
            // label_Workspace_Save_Desc
            // 
            this.label_Workspace_Save_Desc.AutoSize = true;
            this.label_Workspace_Save_Desc.Location = new System.Drawing.Point(14, 294);
            this.label_Workspace_Save_Desc.Name = "label_Workspace_Save_Desc";
            this.label_Workspace_Save_Desc.Size = new System.Drawing.Size(301, 24);
            this.label_Workspace_Save_Desc.TabIndex = 8;
            this.label_Workspace_Save_Desc.Text = "만들어진 파일들을 작업 폴더로 복사합니다.\r\n같은 파일에 여러 패치를 적용하고 싶을 때 유용합니다.";
            // 
            // button_SaveFiles
            // 
            this.button_SaveFiles.Location = new System.Drawing.Point(372, 260);
            this.button_SaveFiles.Name = "button_SaveFiles";
            this.button_SaveFiles.Size = new System.Drawing.Size(202, 80);
            this.button_SaveFiles.TabIndex = 7;
            this.button_SaveFiles.Text = "작업 내역 저장하기";
            this.button_SaveFiles.UseVisualStyleBackColor = true;
            this.button_SaveFiles.Click += new System.EventHandler(this.button_SaveFiles_Click);
            // 
            // Form_Finallize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.label_Workspace_Save_Desc);
            this.Controls.Add(this.button_SaveFiles);
            this.Controls.Add(this.label_ZipPackaging_Desc);
            this.Controls.Add(this.label_AdbFileWindow_Desc);
            this.Controls.Add(this.label_ModdingFinishedCaption);
            this.Controls.Add(this.button_Packaging);
            this.Controls.Add(this.button_sendWithAdb);
            this.Controls.Add(this.label_ModdingFinished);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Finallize";
            this.Text = "Form_Finallize";
            this.Load += new System.EventHandler(this.Form_Finallize_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_ModdingFinished;
        private System.Windows.Forms.Button button_sendWithAdb;
        private System.Windows.Forms.Button button_Packaging;
        private System.Windows.Forms.Label label_ModdingFinishedCaption;
        private System.Windows.Forms.Label label_AdbFileWindow_Desc;
        private System.Windows.Forms.Label label_ZipPackaging_Desc;
        private System.Windows.Forms.Label label_Workspace_Save_Desc;
        private System.Windows.Forms.Button button_SaveFiles;
    }
}