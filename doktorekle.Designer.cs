namespace hastane
{
    partial class doktorekle
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
            dataGridView1 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            textBox0 = new TextBox();
            button3 = new Button();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(176, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(775, 228);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(267, 299);
            label1.Name = "label1";
            label1.Size = new Size(111, 20);
            label1.TabIndex = 1;
            label1.Text = "DOKTOR İSİM : ";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(240, 338);
            label2.Name = "label2";
            label2.Size = new Size(138, 20);
            label2.TabIndex = 2;
            label2.Text = "DOKTOR SOYİSİM : ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(274, 372);
            label3.Name = "label3";
            label3.Size = new Size(104, 20);
            label3.TabIndex = 3;
            label3.Text = "DOKTOR NO : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(249, 407);
            label4.Name = "label4";
            label4.Size = new Size(129, 20);
            label4.TabIndex = 4;
            label4.Text = "DOKTOR BRANŞ : ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(222, 439);
            label5.Name = "label5";
            label5.Size = new Size(156, 20);
            label5.TabIndex = 5;
            label5.Text = "DOKTOR POLİKLİNİK : ";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(384, 296);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(384, 335);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(384, 369);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 8;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(384, 402);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(218, 28);
            comboBox1.TabIndex = 9;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(384, 436);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(218, 28);
            comboBox2.TabIndex = 10;
            // 
            // button1
            // 
            button1.Location = new Point(656, 294);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 11;
            button1.Text = "EKLE";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(756, 294);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 12;
            button2.Text = "SİL";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox0
            // 
            textBox0.Location = new Point(384, 263);
            textBox0.Name = "textBox0";
            textBox0.Size = new Size(125, 27);
            textBox0.TabIndex = 13;
            textBox0.Visible = false;
            // 
            // button3
            // 
            button3.Location = new Point(711, 329);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 14;
            button3.Text = "GÜNCELLE";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(656, 380);
            button4.Name = "button4";
            button4.Size = new Size(194, 84);
            button4.TabIndex = 15;
            button4.Text = "GERİ";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // doktorekle
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1147, 526);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(textBox0);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Name = "doktorekle";
            Text = "doktorekle";
            Load += doktorekle_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Button button1;
        private Button button2;
        private TextBox textBox0;
        private Button button3;
        private Button button4;
    }
}