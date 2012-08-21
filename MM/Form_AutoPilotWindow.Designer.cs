namespace MM
{
    partial class Form_AutoPilotWindow
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
            this.modList = new System.Windows.Forms.ListBox();
            this.modExcludeList = new System.Windows.Forms.ListBox();
            this.button_IncludeExclude = new System.Windows.Forms.Button();
            this.label_IncludeModList = new System.Windows.Forms.Label();
            this.label_ExcludeModList = new System.Windows.Forms.Label();
            this.label_Autopilot_Info = new System.Windows.Forms.Label();
            this.button_SetAutopilotMode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modList
            // 
            this.modList.FormattingEnabled = true;
            this.modList.ItemHeight = 12;
            this.modList.Location = new System.Drawing.Point(12, 45);
            this.modList.Name = "modList";
            this.modList.Size = new System.Drawing.Size(251, 316);
            this.modList.TabIndex = 0;
            this.modList.SelectedIndexChanged += new System.EventHandler(this.modList_SelectedIndexChanged);
            // 
            // modExcludeList
            // 
            this.modExcludeList.FormattingEnabled = true;
            this.modExcludeList.ItemHeight = 12;
            this.modExcludeList.Location = new System.Drawing.Point(315, 45);
            this.modExcludeList.Name = "modExcludeList";
            this.modExcludeList.Size = new System.Drawing.Size(273, 316);
            this.modExcludeList.TabIndex = 1;
            this.modExcludeList.SelectedIndexChanged += new System.EventHandler(this.modExcludeList_SelectedIndexChanged);
            // 
            // button_IncludeExclude
            // 
            this.button_IncludeExclude.Location = new System.Drawing.Point(269, 184);
            this.button_IncludeExclude.Name = "button_IncludeExclude";
            this.button_IncludeExclude.Size = new System.Drawing.Size(40, 34);
            this.button_IncludeExclude.TabIndex = 3;
            this.button_IncludeExclude.Text = "<->";
            this.button_IncludeExclude.UseVisualStyleBackColor = true;
            this.button_IncludeExclude.Click += new System.EventHandler(this.button_IncludeExclude_Click);
            // 
            // label_IncludeModList
            // 
            this.label_IncludeModList.AutoSize = true;
            this.label_IncludeModList.Location = new System.Drawing.Point(96, 19);
            this.label_IncludeModList.Name = "label_IncludeModList";
            this.label_IncludeModList.Size = new System.Drawing.Size(69, 12);
            this.label_IncludeModList.TabIndex = 4;
            this.label_IncludeModList.Text = "패치할 모드";
            // 
            // label_ExcludeModList
            // 
            this.label_ExcludeModList.AutoSize = true;
            this.label_ExcludeModList.Location = new System.Drawing.Point(408, 19);
            this.label_ExcludeModList.Name = "label_ExcludeModList";
            this.label_ExcludeModList.Size = new System.Drawing.Size(69, 12);
            this.label_ExcludeModList.TabIndex = 5;
            this.label_ExcludeModList.Text = "제외할 모드";
            // 
            // label_Autopilot_Info
            // 
            this.label_Autopilot_Info.AutoSize = true;
            this.label_Autopilot_Info.Location = new System.Drawing.Point(12, 376);
            this.label_Autopilot_Info.Name = "label_Autopilot_Info";
            this.label_Autopilot_Info.Size = new System.Drawing.Size(565, 12);
            this.label_Autopilot_Info.TabIndex = 6;
            this.label_Autopilot_Info.Text = "주의: 패치 과정중 사용자에게 입력을 묻는 모드가 있는 경우 자동 비행 모드가 잠시 중단될 수 있습니다.";
            // 
            // button_SetAutopilotMode
            // 
            this.button_SetAutopilotMode.Location = new System.Drawing.Point(12, 393);
            this.button_SetAutopilotMode.Name = "button_SetAutopilotMode";
            this.button_SetAutopilotMode.Size = new System.Drawing.Size(576, 33);
            this.button_SetAutopilotMode.TabIndex = 7;
            this.button_SetAutopilotMode.Text = "확인";
            this.button_SetAutopilotMode.UseVisualStyleBackColor = true;
            this.button_SetAutopilotMode.Click += new System.EventHandler(this.button_SetAutopilotMode_Click);
            // 
            // Form_AutoPilotWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 438);
            this.Controls.Add(this.button_SetAutopilotMode);
            this.Controls.Add(this.label_Autopilot_Info);
            this.Controls.Add(this.label_ExcludeModList);
            this.Controls.Add(this.label_IncludeModList);
            this.Controls.Add(this.button_IncludeExclude);
            this.Controls.Add(this.modExcludeList);
            this.Controls.Add(this.modList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form_AutoPilotWindow";
            this.Text = "Form_AutoPilotSettings";
            this.Load += new System.EventHandler(this.Form_AutoPilotWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox modList;
        private System.Windows.Forms.ListBox modExcludeList;
        private System.Windows.Forms.Button button_IncludeExclude;
        private System.Windows.Forms.Label label_IncludeModList;
        private System.Windows.Forms.Label label_ExcludeModList;
        private System.Windows.Forms.Label label_Autopilot_Info;
        private System.Windows.Forms.Button button_SetAutopilotMode;
    }
}