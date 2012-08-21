namespace MM
{
    partial class Form_SelectMod
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
            this.modInfoText = new System.Windows.Forms.RichTextBox();
            this.modList = new System.Windows.Forms.ListBox();
            this.label_SelectMod = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // modInfoText
            // 
            this.modInfoText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.modInfoText.Location = new System.Drawing.Point(280, 43);
            this.modInfoText.Name = "modInfoText";
            this.modInfoText.ReadOnly = true;
            this.modInfoText.Size = new System.Drawing.Size(308, 305);
            this.modInfoText.TabIndex = 2;
            this.modInfoText.Text = "";
            this.modInfoText.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.modInfoText_LinkClicked);
            // 
            // modList
            // 
            this.modList.FormattingEnabled = true;
            this.modList.ItemHeight = 12;
            this.modList.Location = new System.Drawing.Point(12, 43);
            this.modList.Name = "modList";
            this.modList.Size = new System.Drawing.Size(262, 304);
            this.modList.TabIndex = 0;
            this.modList.SelectedIndexChanged += new System.EventHandler(this.modList_SelectedIndexChanged);
            // 
            // label_SelectMod
            // 
            this.label_SelectMod.AutoSize = true;
            this.label_SelectMod.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_SelectMod.Location = new System.Drawing.Point(12, 9);
            this.label_SelectMod.Name = "label_SelectMod";
            this.label_SelectMod.Size = new System.Drawing.Size(106, 21);
            this.label_SelectMod.TabIndex = 3;
            this.label_SelectMod.Text = "모드 선택";
            // 
            // Form_SelectMod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.label_SelectMod);
            this.Controls.Add(this.modInfoText);
            this.Controls.Add(this.modList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_SelectMod";
            this.Text = "Form_SelectMod";
            this.Load += new System.EventHandler(this.Form_SelectMod_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox modList;
        private System.Windows.Forms.RichTextBox modInfoText;
        private System.Windows.Forms.Label label_SelectMod;
    }
}