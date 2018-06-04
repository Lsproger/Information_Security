namespace LZ77
{
    partial class Decompress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Decompress));
            this.messageBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.decompressButton = new System.Windows.Forms.Button();
            this.informBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // messageBox
            // 
            this.messageBox.BackColor = System.Drawing.Color.White;
            this.messageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageBox.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messageBox.Location = new System.Drawing.Point(11, 28);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(359, 23);
            this.messageBox.TabIndex = 3;
            this.messageBox.TextChanged += new System.EventHandler(this.messageBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Результат сжатия исходного сообщения:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // decompressButton
            // 
            this.decompressButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.decompressButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.decompressButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.decompressButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.decompressButton.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.decompressButton.Location = new System.Drawing.Point(273, 57);
            this.decompressButton.Name = "decompressButton";
            this.decompressButton.Size = new System.Drawing.Size(97, 23);
            this.decompressButton.TabIndex = 4;
            this.decompressButton.Text = "Восстановить";
            this.decompressButton.UseVisualStyleBackColor = false;
            this.decompressButton.Click += new System.EventHandler(this.decompressButton_Click);
            // 
            // informBox
            // 
            this.informBox.BackColor = System.Drawing.Color.White;
            this.informBox.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.informBox.Location = new System.Drawing.Point(11, 57);
            this.informBox.Name = "informBox";
            this.informBox.Size = new System.Drawing.Size(256, 295);
            this.informBox.TabIndex = 9;
            this.informBox.Text = "";
            this.informBox.TextChanged += new System.EventHandler(this.informBox_TextChanged);
            // 
            // Decompress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(391, 359);
            this.Controls.Add(this.informBox);
            this.Controls.Add(this.decompressButton);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Decompress";
            this.Text = "Decompress";
            this.Load += new System.EventHandler(this.Decompress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button decompressButton;
        private System.Windows.Forms.RichTextBox informBox;
    }
}