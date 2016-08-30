namespace DedeAccount
{
    partial class FrmStrG
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxUStr = new System.Windows.Forms.CheckBox();
            this.cbxLStr = new System.Windows.Forms.CheckBox();
            this.cbxNumStr = new System.Windows.Forms.CheckBox();
            this.cbxTStr = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxFirstY = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStrLength = new System.Windows.Forms.TextBox();
            this.btnG = new System.Windows.Forms.Button();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "所用字符：";
            // 
            // cbxUStr
            // 
            this.cbxUStr.AutoSize = true;
            this.cbxUStr.Location = new System.Drawing.Point(78, 19);
            this.cbxUStr.Name = "cbxUStr";
            this.cbxUStr.Size = new System.Drawing.Size(42, 16);
            this.cbxUStr.TabIndex = 1;
            this.cbxUStr.Text = "A-Z";
            this.cbxUStr.UseVisualStyleBackColor = true;
            // 
            // cbxLStr
            // 
            this.cbxLStr.AutoSize = true;
            this.cbxLStr.Location = new System.Drawing.Point(126, 18);
            this.cbxLStr.Name = "cbxLStr";
            this.cbxLStr.Size = new System.Drawing.Size(42, 16);
            this.cbxLStr.TabIndex = 1;
            this.cbxLStr.Text = "a-z";
            this.cbxLStr.UseVisualStyleBackColor = true;
            // 
            // cbxNumStr
            // 
            this.cbxNumStr.AutoSize = true;
            this.cbxNumStr.Location = new System.Drawing.Point(174, 18);
            this.cbxNumStr.Name = "cbxNumStr";
            this.cbxNumStr.Size = new System.Drawing.Size(42, 16);
            this.cbxNumStr.TabIndex = 1;
            this.cbxNumStr.Text = "0-9";
            this.cbxNumStr.UseVisualStyleBackColor = true;
            // 
            // cbxTStr
            // 
            this.cbxTStr.AutoSize = true;
            this.cbxTStr.Location = new System.Drawing.Point(222, 18);
            this.cbxTStr.Name = "cbxTStr";
            this.cbxTStr.Size = new System.Drawing.Size(66, 16);
            this.cbxTStr.TabIndex = 1;
            this.cbxTStr.Text = "!@#$%^&*";
            this.cbxTStr.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "特别选项：";
            // 
            // cbxFirstY
            // 
            this.cbxFirstY.AutoSize = true;
            this.cbxFirstY.Location = new System.Drawing.Point(78, 50);
            this.cbxFirstY.Name = "cbxFirstY";
            this.cbxFirstY.Size = new System.Drawing.Size(120, 16);
            this.cbxFirstY.TabIndex = 2;
            this.cbxFirstY.Text = "第一个字符是英文";
            this.cbxFirstY.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "字符长度：";
            // 
            // txtStrLength
            // 
            this.txtStrLength.Location = new System.Drawing.Point(78, 81);
            this.txtStrLength.Name = "txtStrLength";
            this.txtStrLength.Size = new System.Drawing.Size(100, 21);
            this.txtStrLength.TabIndex = 3;
            // 
            // btnG
            // 
            this.btnG.Location = new System.Drawing.Point(231, 79);
            this.btnG.Name = "btnG";
            this.btnG.Size = new System.Drawing.Size(75, 23);
            this.btnG.TabIndex = 4;
            this.btnG.Text = "生成";
            this.btnG.UseVisualStyleBackColor = true;
            this.btnG.Click += new System.EventHandler(this.btnG_Click);
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(12, 109);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(294, 111);
            this.txtContent.TabIndex = 6;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(231, 226);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 7;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // FrmStrG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 261);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.btnG);
            this.Controls.Add(this.txtStrLength);
            this.Controls.Add(this.cbxFirstY);
            this.Controls.Add(this.cbxTStr);
            this.Controls.Add(this.cbxNumStr);
            this.Controls.Add(this.cbxLStr);
            this.Controls.Add(this.cbxUStr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmStrG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " 字符随机生成";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbxUStr;
        private System.Windows.Forms.CheckBox cbxLStr;
        private System.Windows.Forms.CheckBox cbxNumStr;
        private System.Windows.Forms.CheckBox cbxTStr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbxFirstY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStrLength;
        private System.Windows.Forms.Button btnG;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Button btnCopy;
    }
}