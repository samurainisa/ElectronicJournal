namespace ElectronicJournal.Формы.Формы_для_добавления
{
    partial class AddInstructForm
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
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox4 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.button1 = new MaterialSkin.Controls.MaterialFlatButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(54, 141);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(79, 19);
            this.materialLabel1.TabIndex = 24;
            this.materialLabel1.Text = "Описание";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Location = new System.Drawing.Point(55, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(251, 176);
            this.panel1.TabIndex = 23;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(245, 170);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = "";
            // 
            // textBox4
            // 
            this.textBox4.Depth = 0;
            this.textBox4.Hint = "Название инструктажа";
            this.textBox4.Location = new System.Drawing.Point(55, 95);
            this.textBox4.MouseState = MaterialSkin.MouseState.HOVER;
            this.textBox4.Name = "textBox4";
            this.textBox4.PasswordChar = '\0';
            this.textBox4.SelectedText = "";
            this.textBox4.SelectionLength = 0;
            this.textBox4.SelectionStart = 0;
            this.textBox4.Size = new System.Drawing.Size(251, 23);
            this.textBox4.TabIndex = 28;
            this.textBox4.UseSystemPasswordChar = false;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Depth = 0;
            this.button1.Location = new System.Drawing.Point(92, 368);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.button1.MouseState = MaterialSkin.MouseState.HOVER;
            this.button1.Name = "button1";
            this.button1.Primary = false;
            this.button1.Size = new System.Drawing.Size(182, 36);
            this.button1.TabIndex = 25;
            this.button1.Text = "Добавить инструктаж";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddInstructForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 433);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.panel1);
            this.Name = "AddInstructForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление инструктажа";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddInstructForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private MaterialSkin.Controls.MaterialSingleLineTextField textBox4;
        private MaterialSkin.Controls.MaterialFlatButton button1;
    }
}