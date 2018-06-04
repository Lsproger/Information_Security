namespace EDSElGamal
{
    partial class Form1
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
            this.inP = new System.Windows.Forms.TextBox();
            this.inG = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.inX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.inY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.inputMsg = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.generateEDS = new System.Windows.Forms.Button();
            this.outM = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.outB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.outA = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.outY = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.outG = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.outP = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.checkEDS = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "P = ";
            // 
            // inP
            // 
            this.inP.BackColor = System.Drawing.Color.Lime;
            this.inP.Location = new System.Drawing.Point(38, 12);
            this.inP.Name = "inP";
            this.inP.ReadOnly = true;
            this.inP.Size = new System.Drawing.Size(76, 20);
            this.inP.TabIndex = 1;
            // 
            // inG
            // 
            this.inG.BackColor = System.Drawing.Color.Lime;
            this.inG.Location = new System.Drawing.Point(38, 38);
            this.inG.Name = "inG";
            this.inG.ReadOnly = true;
            this.inG.Size = new System.Drawing.Size(76, 20);
            this.inG.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Lime;
            this.label2.Location = new System.Drawing.Point(6, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "G = ";
            // 
            // inX
            // 
            this.inX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.inX.Location = new System.Drawing.Point(38, 92);
            this.inX.Name = "inX";
            this.inX.ReadOnly = true;
            this.inX.Size = new System.Drawing.Size(76, 20);
            this.inX.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label3.Location = new System.Drawing.Point(6, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "X = ";
            // 
            // inY
            // 
            this.inY.BackColor = System.Drawing.Color.Lime;
            this.inY.Location = new System.Drawing.Point(38, 66);
            this.inY.Name = "inY";
            this.inY.ReadOnly = true;
            this.inY.Size = new System.Drawing.Size(76, 20);
            this.inY.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Lime;
            this.label4.Location = new System.Drawing.Point(6, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Y = ";
            // 
            // inputMsg
            // 
            this.inputMsg.Location = new System.Drawing.Point(9, 148);
            this.inputMsg.Name = "inputMsg";
            this.inputMsg.Size = new System.Drawing.Size(94, 20);
            this.inputMsg.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Message";
            // 
            // generateEDS
            // 
            this.generateEDS.Location = new System.Drawing.Point(2, 174);
            this.generateEDS.Name = "generateEDS";
            this.generateEDS.Size = new System.Drawing.Size(112, 23);
            this.generateEDS.TabIndex = 10;
            this.generateEDS.Text = "Получить ключи";
            this.generateEDS.UseVisualStyleBackColor = true;
            this.generateEDS.Click += new System.EventHandler(this.generateEDS_Click);
            // 
            // outM
            // 
            this.outM.Location = new System.Drawing.Point(42, 19);
            this.outM.Name = "outM";
            this.outM.Size = new System.Drawing.Size(260, 20);
            this.outM.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "М = ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.outB);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.outA);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.outM);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(288, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 100);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ЭЦП";
            // 
            // outB
            // 
            this.outB.Location = new System.Drawing.Point(42, 71);
            this.outB.Name = "outB";
            this.outB.Size = new System.Drawing.Size(260, 20);
            this.outB.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "b = ";
            // 
            // outA
            // 
            this.outA.Location = new System.Drawing.Point(42, 45);
            this.outA.Name = "outA";
            this.outA.Size = new System.Drawing.Size(260, 20);
            this.outA.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "a = ";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Lime;
            this.groupBox2.Controls.Add(this.outY);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.outG);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.outP);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(288, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(308, 71);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Передаваемые ключи";
            // 
            // outY
            // 
            this.outY.Location = new System.Drawing.Point(165, 30);
            this.outY.Name = "outY";
            this.outY.Size = new System.Drawing.Size(76, 20);
            this.outY.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(133, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Y = ";
            // 
            // outG
            // 
            this.outG.Location = new System.Drawing.Point(43, 43);
            this.outG.Name = "outG";
            this.outG.Size = new System.Drawing.Size(76, 20);
            this.outG.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "G = ";
            // 
            // outP
            // 
            this.outP.Location = new System.Drawing.Point(43, 17);
            this.outP.Name = "outP";
            this.outP.Size = new System.Drawing.Size(76, 20);
            this.outP.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "P = ";
            // 
            // checkEDS
            // 
            this.checkEDS.Location = new System.Drawing.Point(405, 198);
            this.checkEDS.Name = "checkEDS";
            this.checkEDS.Size = new System.Drawing.Size(108, 23);
            this.checkEDS.TabIndex = 15;
            this.checkEDS.Text = "Сверить";
            this.checkEDS.UseVisualStyleBackColor = true;
            this.checkEDS.Click += new System.EventHandler(this.checkEDS_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 322);
            this.Controls.Add(this.checkEDS);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.generateEDS);
            this.Controls.Add(this.inputMsg);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.inY);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.inX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inG);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inP);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inP;
        private System.Windows.Forms.TextBox inG;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox inX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox inY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox inputMsg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button generateEDS;
        private System.Windows.Forms.TextBox outM;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox outB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox outA;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox outY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox outG;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox outP;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button checkEDS;
    }
}

