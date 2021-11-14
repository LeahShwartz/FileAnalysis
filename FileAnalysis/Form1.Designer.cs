
namespace FileAnalysis
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.textPath = new System.Windows.Forms.TextBox();
            this.openOnline = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(207, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "open file from this device\r\n";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.Location = new System.Drawing.Point(36, 87);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(933, 441);
            this.richTextBox.TabIndex = 1;
            this.richTextBox.Text = "";
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(751, 38);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(189, 27);
            this.textPath.TabIndex = 2;
            this.textPath.TextChanged += new System.EventHandler(this.textPath_TextChanged);
            // 
            // openOnline
            // 
            this.openOnline.Enabled = false;
            this.openOnline.Location = new System.Drawing.Point(493, 33);
            this.openOnline.Name = "openOnline";
            this.openOnline.Size = new System.Drawing.Size(222, 32);
            this.openOnline.TabIndex = 3;
            this.openOnline.Text = "open file by path";
            this.openOnline.UseVisualStyleBackColor = true;
            this.openOnline.Click += new System.EventHandler(this.openOnline_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 549);
            this.Controls.Add(this.openOnline);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Button openOnline;
    }
}

