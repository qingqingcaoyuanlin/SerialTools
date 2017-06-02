namespace SerialTool
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_All = new System.Windows.Forms.TextBox();
            this.comboBox_Serial = new System.Windows.Forms.ComboBox();
            this.btn_open = new System.Windows.Forms.Button();
            this.textBox_Msg = new System.Windows.Forms.TextBox();
            this.textBox_Error = new System.Windows.Forms.TextBox();
            this.richTextBox_Msg = new System.Windows.Forms.RichTextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.comboBox_File = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBox_All
            // 
            this.textBox_All.Location = new System.Drawing.Point(13, 18);
            this.textBox_All.Name = "textBox_All";
            this.textBox_All.Size = new System.Drawing.Size(85, 21);
            this.textBox_All.TabIndex = 0;
            // 
            // comboBox_Serial
            // 
            this.comboBox_Serial.FormattingEnabled = true;
            this.comboBox_Serial.Location = new System.Drawing.Point(118, 18);
            this.comboBox_Serial.Name = "comboBox_Serial";
            this.comboBox_Serial.Size = new System.Drawing.Size(77, 20);
            this.comboBox_Serial.TabIndex = 1;
            this.comboBox_Serial.SelectedIndexChanged += new System.EventHandler(this.comboBox_Serial_SelectedIndexChanged);
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(234, 16);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(75, 23);
            this.btn_open.TabIndex = 2;
            this.btn_open.Text = "打开";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // textBox_Msg
            // 
            this.textBox_Msg.Location = new System.Drawing.Point(415, 17);
            this.textBox_Msg.Name = "textBox_Msg";
            this.textBox_Msg.Size = new System.Drawing.Size(100, 21);
            this.textBox_Msg.TabIndex = 3;
            // 
            // textBox_Error
            // 
            this.textBox_Error.Location = new System.Drawing.Point(530, 18);
            this.textBox_Error.Name = "textBox_Error";
            this.textBox_Error.Size = new System.Drawing.Size(100, 21);
            this.textBox_Error.TabIndex = 4;
            // 
            // richTextBox_Msg
            // 
            this.richTextBox_Msg.Location = new System.Drawing.Point(13, 80);
            this.richTextBox_Msg.Name = "richTextBox_Msg";
            this.richTextBox_Msg.Size = new System.Drawing.Size(617, 374);
            this.richTextBox_Msg.TabIndex = 5;
            this.richTextBox_Msg.Text = "";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(315, 16);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "清除";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // comboBox_File
            // 
            this.comboBox_File.FormattingEnabled = true;
            this.comboBox_File.Location = new System.Drawing.Point(118, 54);
            this.comboBox_File.Name = "comboBox_File";
            this.comboBox_File.Size = new System.Drawing.Size(77, 20);
            this.comboBox_File.TabIndex = 7;
            this.comboBox_File.SelectedIndexChanged += new System.EventHandler(this.comboBox_File_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 501);
            this.Controls.Add(this.comboBox_File);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.richTextBox_Msg);
            this.Controls.Add(this.textBox_Error);
            this.Controls.Add(this.textBox_Msg);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.comboBox_Serial);
            this.Controls.Add(this.textBox_All);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_All;
        private System.Windows.Forms.ComboBox comboBox_Serial;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.TextBox textBox_Msg;
        private System.Windows.Forms.TextBox textBox_Error;
        private System.Windows.Forms.RichTextBox richTextBox_Msg;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ComboBox comboBox_File;
    }
}

