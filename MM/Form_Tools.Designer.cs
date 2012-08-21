namespace MM
{
    partial class Form_Tools
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
            this.label_Open_Workspace_Desc = new System.Windows.Forms.Label();
            this.button_ShowWorkspace = new System.Windows.Forms.Button();
            this.label_AdbFileWindow_Batch_Desc = new System.Windows.Forms.Label();
            this.button_AdbAllPush = new System.Windows.Forms.Button();
            this.label_ZipPackaging_Batch_Desc = new System.Windows.Forms.Label();
            this.button_PackageAll = new System.Windows.Forms.Button();
            this.label_Tools = new System.Windows.Forms.Label();
            this.button_SaveFiles = new System.Windows.Forms.Button();
            this.label_SaveFilesBatch_Desc = new System.Windows.Forms.Label();
            this.button_AutoPilot = new System.Windows.Forms.Button();
            this.label_Autopilot = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Open_Workspace_Desc
            // 
            this.label_Open_Workspace_Desc.AutoSize = true;
            this.label_Open_Workspace_Desc.Location = new System.Drawing.Point(14, 188);
            this.label_Open_Workspace_Desc.Name = "label_Open_Workspace_Desc";
            this.label_Open_Workspace_Desc.Size = new System.Drawing.Size(137, 12);
            this.label_Open_Workspace_Desc.TabIndex = 8;
            this.label_Open_Workspace_Desc.Text = "작업 폴더를 보여줍니다.";
            // 
            // button_ShowWorkspace
            // 
            this.button_ShowWorkspace.Location = new System.Drawing.Point(382, 164);
            this.button_ShowWorkspace.Name = "button_ShowWorkspace";
            this.button_ShowWorkspace.Size = new System.Drawing.Size(206, 55);
            this.button_ShowWorkspace.TabIndex = 7;
            this.button_ShowWorkspace.Text = "작업 폴더 열기";
            this.button_ShowWorkspace.UseVisualStyleBackColor = true;
            this.button_ShowWorkspace.Click += new System.EventHandler(this.button_ShowWorkspace_Click);
            // 
            // label_AdbFileWindow_Batch_Desc
            // 
            this.label_AdbFileWindow_Batch_Desc.AutoSize = true;
            this.label_AdbFileWindow_Batch_Desc.Location = new System.Drawing.Point(14, 127);
            this.label_AdbFileWindow_Batch_Desc.Name = "label_AdbFileWindow_Batch_Desc";
            this.label_AdbFileWindow_Batch_Desc.Size = new System.Drawing.Size(245, 12);
            this.label_AdbFileWindow_Batch_Desc.TabIndex = 6;
            this.label_AdbFileWindow_Batch_Desc.Text = "작업 폴더의 모든 파일을 폰으로 전송합니다.";
            // 
            // button_AdbAllPush
            // 
            this.button_AdbAllPush.Location = new System.Drawing.Point(382, 103);
            this.button_AdbAllPush.Name = "button_AdbAllPush";
            this.button_AdbAllPush.Size = new System.Drawing.Size(206, 55);
            this.button_AdbAllPush.TabIndex = 5;
            this.button_AdbAllPush.Text = "작업폴더의 파일 한꺼번에 보내기";
            this.button_AdbAllPush.UseVisualStyleBackColor = true;
            this.button_AdbAllPush.Click += new System.EventHandler(this.button_AdbAllPush_Click);
            // 
            // label_ZipPackaging_Batch_Desc
            // 
            this.label_ZipPackaging_Batch_Desc.AutoSize = true;
            this.label_ZipPackaging_Batch_Desc.Location = new System.Drawing.Point(14, 66);
            this.label_ZipPackaging_Batch_Desc.Name = "label_ZipPackaging_Batch_Desc";
            this.label_ZipPackaging_Batch_Desc.Size = new System.Drawing.Size(296, 12);
            this.label_ZipPackaging_Batch_Desc.TabIndex = 4;
            this.label_ZipPackaging_Batch_Desc.Text = "작업 폴더의 모든 파일을 ZIP 설치 패키지로 만듭니다.";
            // 
            // button_PackageAll
            // 
            this.button_PackageAll.Location = new System.Drawing.Point(382, 42);
            this.button_PackageAll.Name = "button_PackageAll";
            this.button_PackageAll.Size = new System.Drawing.Size(206, 55);
            this.button_PackageAll.TabIndex = 3;
            this.button_PackageAll.Text = "통합 ZIP 패키지 만들기";
            this.button_PackageAll.UseVisualStyleBackColor = true;
            this.button_PackageAll.Click += new System.EventHandler(this.button_PackageAll_Click);
            // 
            // label_Tools
            // 
            this.label_Tools.AutoSize = true;
            this.label_Tools.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Tools.Location = new System.Drawing.Point(12, 9);
            this.label_Tools.Name = "label_Tools";
            this.label_Tools.Size = new System.Drawing.Size(54, 21);
            this.label_Tools.TabIndex = 2;
            this.label_Tools.Text = "도구";
            // 
            // button_SaveFiles
            // 
            this.button_SaveFiles.Location = new System.Drawing.Point(382, 225);
            this.button_SaveFiles.Name = "button_SaveFiles";
            this.button_SaveFiles.Size = new System.Drawing.Size(206, 53);
            this.button_SaveFiles.TabIndex = 9;
            this.button_SaveFiles.Text = "작업 내역 저장하기";
            this.button_SaveFiles.UseVisualStyleBackColor = true;
            this.button_SaveFiles.Click += new System.EventHandler(this.button_SaveFiles_Click);
            // 
            // label_SaveFilesBatch_Desc
            // 
            this.label_SaveFilesBatch_Desc.AutoSize = true;
            this.label_SaveFilesBatch_Desc.Location = new System.Drawing.Point(14, 248);
            this.label_SaveFilesBatch_Desc.Name = "label_SaveFilesBatch_Desc";
            this.label_SaveFilesBatch_Desc.Size = new System.Drawing.Size(189, 12);
            this.label_SaveFilesBatch_Desc.TabIndex = 10;
            this.label_SaveFilesBatch_Desc.Text = "이전에 작업한 내역을 저장합니다.";
            // 
            // button_AutoPilot
            // 
            this.button_AutoPilot.Location = new System.Drawing.Point(382, 284);
            this.button_AutoPilot.Name = "button_AutoPilot";
            this.button_AutoPilot.Size = new System.Drawing.Size(206, 53);
            this.button_AutoPilot.TabIndex = 11;
            this.button_AutoPilot.Text = "자동 비행 모드";
            this.button_AutoPilot.UseVisualStyleBackColor = true;
            this.button_AutoPilot.Click += new System.EventHandler(this.button_AutoPilot_Click);
            // 
            // label_Autopilot
            // 
            this.label_Autopilot.AutoSize = true;
            this.label_Autopilot.Location = new System.Drawing.Point(14, 304);
            this.label_Autopilot.Name = "label_Autopilot";
            this.label_Autopilot.Size = new System.Drawing.Size(253, 12);
            this.label_Autopilot.TabIndex = 12;
            this.label_Autopilot.Text = "지정한 모드를 한꺼번에 자동으로 패치합니다.";
            // 
            // Form_Tools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.label_Autopilot);
            this.Controls.Add(this.button_AutoPilot);
            this.Controls.Add(this.label_SaveFilesBatch_Desc);
            this.Controls.Add(this.button_SaveFiles);
            this.Controls.Add(this.label_Open_Workspace_Desc);
            this.Controls.Add(this.button_ShowWorkspace);
            this.Controls.Add(this.label_AdbFileWindow_Batch_Desc);
            this.Controls.Add(this.button_AdbAllPush);
            this.Controls.Add(this.label_ZipPackaging_Batch_Desc);
            this.Controls.Add(this.button_PackageAll);
            this.Controls.Add(this.label_Tools);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Tools";
            this.Text = "Form_Tools";
            this.Load += new System.EventHandler(this.Form_Tools_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Tools;
        private System.Windows.Forms.Button button_PackageAll;
        private System.Windows.Forms.Label label_ZipPackaging_Batch_Desc;
        private System.Windows.Forms.Label label_AdbFileWindow_Batch_Desc;
        private System.Windows.Forms.Button button_AdbAllPush;
        private System.Windows.Forms.Label label_Open_Workspace_Desc;
        private System.Windows.Forms.Button button_ShowWorkspace;
        private System.Windows.Forms.Button button_SaveFiles;
        private System.Windows.Forms.Label label_SaveFilesBatch_Desc;
        private System.Windows.Forms.Button button_AutoPilot;
        private System.Windows.Forms.Label label_Autopilot;
    }
}