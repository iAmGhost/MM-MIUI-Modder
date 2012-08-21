namespace MM
{
    partial class Form_CheckFiles
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
            this.modTitleLabel = new System.Windows.Forms.Label();
            this.debugList = new System.Windows.Forms.ListBox();
            this.button_Check = new System.Windows.Forms.Button();
            this.button_GetFromPhone = new System.Windows.Forms.Button();
            this.button_OpenWorkspace = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modTitleLabel
            // 
            this.modTitleLabel.AutoSize = true;
            this.modTitleLabel.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.modTitleLabel.Location = new System.Drawing.Point(12, 9);
            this.modTitleLabel.Name = "modTitleLabel";
            this.modTitleLabel.Size = new System.Drawing.Size(206, 21);
            this.modTitleLabel.TabIndex = 0;
            this.modTitleLabel.Text = "Mod Title Goes Here";
            // 
            // debugList
            // 
            this.debugList.FormattingEnabled = true;
            this.debugList.ItemHeight = 12;
            this.debugList.Location = new System.Drawing.Point(12, 47);
            this.debugList.Name = "debugList";
            this.debugList.Size = new System.Drawing.Size(572, 232);
            this.debugList.TabIndex = 1;
            // 
            // button_Check
            // 
            this.button_Check.Location = new System.Drawing.Point(12, 295);
            this.button_Check.Name = "button_Check";
            this.button_Check.Size = new System.Drawing.Size(167, 53);
            this.button_Check.TabIndex = 2;
            this.button_Check.Text = "파일 체크";
            this.button_Check.UseVisualStyleBackColor = true;
            this.button_Check.Click += new System.EventHandler(this.button_Check_Click);
            // 
            // button_GetFromPhone
            // 
            this.button_GetFromPhone.Location = new System.Drawing.Point(214, 295);
            this.button_GetFromPhone.Name = "button_GetFromPhone";
            this.button_GetFromPhone.Size = new System.Drawing.Size(167, 53);
            this.button_GetFromPhone.TabIndex = 3;
            this.button_GetFromPhone.Text = "핸드폰에서 가져오기";
            this.button_GetFromPhone.UseVisualStyleBackColor = true;
            this.button_GetFromPhone.Click += new System.EventHandler(this.button_GetFromPhone_Click);
            // 
            // button_OpenWorkspace
            // 
            this.button_OpenWorkspace.Location = new System.Drawing.Point(416, 295);
            this.button_OpenWorkspace.Name = "button_OpenWorkspace";
            this.button_OpenWorkspace.Size = new System.Drawing.Size(167, 53);
            this.button_OpenWorkspace.TabIndex = 4;
            this.button_OpenWorkspace.Text = "작업 폴더 열기";
            this.button_OpenWorkspace.UseVisualStyleBackColor = true;
            this.button_OpenWorkspace.Click += new System.EventHandler(this.button_OpenWorkspace_Click);
            // 
            // Form_CheckFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.button_OpenWorkspace);
            this.Controls.Add(this.button_GetFromPhone);
            this.Controls.Add(this.button_Check);
            this.Controls.Add(this.debugList);
            this.Controls.Add(this.modTitleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_CheckFiles";
            this.Text = "Form_CheckFiles";
            this.Load += new System.EventHandler(this.Form_CheckFiles_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label modTitleLabel;
        private System.Windows.Forms.ListBox debugList;
        private System.Windows.Forms.Button button_Check;
        private System.Windows.Forms.Button button_GetFromPhone;
        private System.Windows.Forms.Button button_OpenWorkspace;
    }
}