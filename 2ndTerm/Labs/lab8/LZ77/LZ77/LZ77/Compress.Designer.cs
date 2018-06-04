namespace LZ77
{
    partial class Compress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Compress));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.informBox = new System.Windows.Forms.RichTextBox();
            this.compressButton = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.dictionarySizeBox = new System.Windows.Forms.TextBox();
            this.bufferSizeBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Constantia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(19, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите сообщение:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Constantia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(141, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Длина словаря:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Constantia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(320, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "Длина буфера:";
            // 
            // informBox
            // 
            this.informBox.BackColor = System.Drawing.Color.White;
            this.informBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.informBox.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.informBox.Location = new System.Drawing.Point(13, 113);
            this.informBox.Name = "informBox";
            this.informBox.Size = new System.Drawing.Size(450, 270);
            this.informBox.TabIndex = 7;
            this.informBox.Text = "";
            this.informBox.TextChanged += new System.EventHandler(this.informBox_TextChanged);
            // 
            // compressButton
            // 
            this.compressButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.compressButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.compressButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.compressButton.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.compressButton.Location = new System.Drawing.Point(312, 84);
            this.compressButton.Name = "compressButton";
            this.compressButton.Size = new System.Drawing.Size(151, 23);
            this.compressButton.TabIndex = 8;
            this.compressButton.Text = "Выполнить сжатие";
            this.compressButton.UseVisualStyleBackColor = false;
            this.compressButton.Click += new System.EventHandler(this.compressButton_Click);
            // 
            // messageBox
            // 
            this.messageBox.BackColor = System.Drawing.Color.White;
            this.messageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageBox.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messageBox.Location = new System.Drawing.Point(135, 24);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(328, 23);
            this.messageBox.TabIndex = 1;
            // 
            // dictionarySizeBox
            // 
            this.dictionarySizeBox.BackColor = System.Drawing.Color.White;
            this.dictionarySizeBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dictionarySizeBox.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dictionarySizeBox.Location = new System.Drawing.Point(253, 53);
            this.dictionarySizeBox.Name = "dictionarySizeBox";
            this.dictionarySizeBox.Size = new System.Drawing.Size(44, 23);
            this.dictionarySizeBox.TabIndex = 4;
            this.dictionarySizeBox.TextChanged += new System.EventHandler(this.dictionarySizeBox_TextChanged);
            // 
            // bufferSizeBox
            // 
            this.bufferSizeBox.BackColor = System.Drawing.Color.White;
            this.bufferSizeBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bufferSizeBox.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bufferSizeBox.Location = new System.Drawing.Point(419, 55);
            this.bufferSizeBox.Name = "bufferSizeBox";
            this.bufferSizeBox.Size = new System.Drawing.Size(44, 23);
            this.bufferSizeBox.TabIndex = 5;
            // 
            // Compress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(480, 393);
            this.Controls.Add(this.compressButton);
            this.Controls.Add(this.informBox);
            this.Controls.Add(this.bufferSizeBox);
            this.Controls.Add(this.dictionarySizeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Compress";
            this.Text = "Compress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox informBox;
        private System.Windows.Forms.Button compressButton;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.TextBox dictionarySizeBox;
        private System.Windows.Forms.TextBox bufferSizeBox;
    }
}

