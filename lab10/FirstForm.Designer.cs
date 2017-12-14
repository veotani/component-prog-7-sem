namespace lab10
{
    partial class FirstForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.surnameLabel = new System.Windows.Forms.Label();
            this.pasportSerial = new System.Windows.Forms.Label();
            this.pasportNumber = new System.Windows.Forms.Label();
            this.surnameTextBox = new System.Windows.Forms.TextBox();
            this.pasportSerialTextBox = new System.Windows.Forms.TextBox();
            this.pasportNumberTextBox = new System.Windows.Forms.TextBox();
            this.proceedButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // surnameLabel
            // 
            this.surnameLabel.AutoSize = true;
            this.surnameLabel.Location = new System.Drawing.Point(69, 27);
            this.surnameLabel.Name = "surnameLabel";
            this.surnameLabel.Size = new System.Drawing.Size(56, 13);
            this.surnameLabel.TabIndex = 0;
            this.surnameLabel.Text = "Фамилия";
            // 
            // pasportSerial
            // 
            this.pasportSerial.AutoSize = true;
            this.pasportSerial.Location = new System.Drawing.Point(38, 63);
            this.pasportSerial.Name = "pasportSerial";
            this.pasportSerial.Size = new System.Drawing.Size(87, 13);
            this.pasportSerial.TabIndex = 0;
            this.pasportSerial.Text = "Паспорт: Серия";
            // 
            // pasportNumber
            // 
            this.pasportNumber.AutoSize = true;
            this.pasportNumber.Location = new System.Drawing.Point(201, 63);
            this.pasportNumber.Name = "pasportNumber";
            this.pasportNumber.Size = new System.Drawing.Size(18, 13);
            this.pasportNumber.TabIndex = 0;
            this.pasportNumber.Text = "№";
            // 
            // surnameTextBox
            // 
            this.surnameTextBox.Location = new System.Drawing.Point(131, 27);
            this.surnameTextBox.Name = "surnameTextBox";
            this.surnameTextBox.Size = new System.Drawing.Size(100, 20);
            this.surnameTextBox.TabIndex = 1;
            this.surnameTextBox.TextChanged += new System.EventHandler(this.surnameTextBox_TextChanged);
            // 
            // pasportSerialTextBox
            // 
            this.pasportSerialTextBox.Location = new System.Drawing.Point(131, 63);
            this.pasportSerialTextBox.MaxLength = 4;
            this.pasportSerialTextBox.Name = "pasportSerialTextBox";
            this.pasportSerialTextBox.Size = new System.Drawing.Size(38, 20);
            this.pasportSerialTextBox.TabIndex = 2;
            this.pasportSerialTextBox.TextChanged += new System.EventHandler(this.pasportSerialTextBox_TextChanged);
            // 
            // pasportNumberTextBox
            // 
            this.pasportNumberTextBox.Location = new System.Drawing.Point(225, 63);
            this.pasportNumberTextBox.MaxLength = 6;
            this.pasportNumberTextBox.Name = "pasportNumberTextBox";
            this.pasportNumberTextBox.Size = new System.Drawing.Size(100, 20);
            this.pasportNumberTextBox.TabIndex = 3;
            this.pasportNumberTextBox.TextChanged += new System.EventHandler(this.pasportNumberTextBox_TextChanged);
            // 
            // proceedButton
            // 
            this.proceedButton.Location = new System.Drawing.Point(184, 126);
            this.proceedButton.Name = "proceedButton";
            this.proceedButton.Size = new System.Drawing.Size(100, 41);
            this.proceedButton.TabIndex = 4;
            this.proceedButton.Text = "Далее";
            this.proceedButton.UseVisualStyleBackColor = true;
            this.proceedButton.Click += new System.EventHandler(this.proceedButton_Click);
            // 
            // FirstForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 207);
            this.Controls.Add(this.proceedButton);
            this.Controls.Add(this.pasportNumberTextBox);
            this.Controls.Add(this.pasportSerialTextBox);
            this.Controls.Add(this.surnameTextBox);
            this.Controls.Add(this.pasportNumber);
            this.Controls.Add(this.pasportSerial);
            this.Controls.Add(this.surnameLabel);
            this.Name = "FirstForm";
            this.Text = "Арифметика";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label surnameLabel;
        private System.Windows.Forms.Label pasportSerial;
        private System.Windows.Forms.Label pasportNumber;
        private System.Windows.Forms.TextBox surnameTextBox;
        private System.Windows.Forms.TextBox pasportSerialTextBox;
        private System.Windows.Forms.TextBox pasportNumberTextBox;
        private System.Windows.Forms.Button proceedButton;
    }
}

