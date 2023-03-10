namespace ElectronicJournal.Формы
{
    partial class RegistrationForm
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
            this.textBox3 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.textBox2 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.textBox1 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.button1 = new MaterialSkin.Controls.MaterialFlatButton();
            this.label4 = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            // 
            // textBox3
            // 
            this.textBox3.Depth = 0;
            this.textBox3.Hint = "Уровень";
            this.textBox3.Location = new System.Drawing.Point(74, 203);
            this.textBox3.MouseState = MaterialSkin.MouseState.HOVER;
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '\0';
            this.textBox3.SelectedText = "";
            this.textBox3.SelectionLength = 0;
            this.textBox3.SelectionStart = 0;
            this.textBox3.Size = new System.Drawing.Size(283, 23);
            this.textBox3.TabIndex = 18;
            this.textBox3.UseSystemPasswordChar = false;
            // 
            // textBox2
            // 
            this.textBox2.Depth = 0;
            this.textBox2.Hint = "Пароль";
            this.textBox2.Location = new System.Drawing.Point(74, 150);
            this.textBox2.MouseState = MaterialSkin.MouseState.HOVER;
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '\0';
            this.textBox2.SelectedText = "";
            this.textBox2.SelectionLength = 0;
            this.textBox2.SelectionStart = 0;
            this.textBox2.Size = new System.Drawing.Size(283, 23);
            this.textBox2.TabIndex = 17;
            this.textBox2.UseSystemPasswordChar = false;
            // 
            // textBox1
            // 
            this.textBox1.Depth = 0;
            this.textBox1.Hint = "Логин";
            this.textBox1.Location = new System.Drawing.Point(74, 93);
            this.textBox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '\0';
            this.textBox1.SelectedText = "";
            this.textBox1.SelectionLength = 0;
            this.textBox1.SelectionStart = 0;
            this.textBox1.Size = new System.Drawing.Size(283, 23);
            this.textBox1.TabIndex = 16;
            this.textBox1.UseSystemPasswordChar = false;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Depth = 0;
            this.button1.Location = new System.Drawing.Point(130, 255);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.button1.MouseState = MaterialSkin.MouseState.HOVER;
            this.button1.Name = "button1";
            this.button1.Primary = false;
            this.button1.Size = new System.Drawing.Size(170, 36);
            this.button1.TabIndex = 19;
            this.button1.Text = "Зарегистрироваться";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Depth = 0;
            this.label4.Font = new System.Drawing.Font("Roboto", 11F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(152, 332);
            this.label4.MouseState = MaterialSkin.MouseState.HOVER;
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 19);
            this.label4.TabIndex = 20;
            this.label4.Text = "Уже есть аккаунт";
            this.label4.Click += new System.EventHandler(this.label4_Click_1);
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 360);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RegistrationForm";
            this.Text = "Форма регистрации";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField textBox3;
        private MaterialSkin.Controls.MaterialSingleLineTextField textBox2;
        private MaterialSkin.Controls.MaterialSingleLineTextField textBox1;
        private MaterialSkin.Controls.MaterialFlatButton button1;
        private MaterialSkin.Controls.MaterialLabel label4;
    }
}