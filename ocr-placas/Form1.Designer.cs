namespace ocr_placas
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.original_pic = new System.Windows.Forms.PictureBox();
            this.placa_pic = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ocr_label = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.worker_ocr = new System.ComponentModel.BackgroundWorker();
            this.timer_ocr = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.original_pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.placa_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(604, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(345, 47);
            this.button1.TabIndex = 0;
            this.button1.Text = "Realizar OCR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // original_pic
            // 
            this.original_pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.original_pic.Location = new System.Drawing.Point(12, 27);
            this.original_pic.Name = "original_pic";
            this.original_pic.Size = new System.Drawing.Size(571, 450);
            this.original_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.original_pic.TabIndex = 1;
            this.original_pic.TabStop = false;
            // 
            // placa_pic
            // 
            this.placa_pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.placa_pic.Location = new System.Drawing.Point(604, 110);
            this.placa_pic.Name = "placa_pic";
            this.placa_pic.Size = new System.Drawing.Size(319, 115);
            this.placa_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.placa_pic.TabIndex = 1;
            this.placa_pic.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(606, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Placa Encontrada";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Imagem Original";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(606, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Resultado OCR";
            // 
            // ocr_label
            // 
            this.ocr_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ocr_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ocr_label.Location = new System.Drawing.Point(606, 258);
            this.ocr_label.Name = "ocr_label";
            this.ocr_label.Size = new System.Drawing.Size(317, 73);
            this.ocr_label.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(604, 368);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(345, 47);
            this.button2.TabIndex = 0;
            this.button2.Text = "Varrer na Pasta";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // worker_ocr
            // 
            this.worker_ocr.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // timer_ocr
            // 
            this.timer_ocr.Interval = 1500;
            this.timer_ocr.Tick += new System.EventHandler(this.timer_ocr_Tick_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 490);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ocr_label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.placa_pic);
            this.Controls.Add(this.original_pic);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.original_pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.placa_pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox original_pic;
        private System.Windows.Forms.PictureBox placa_pic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ocr_label;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker worker_ocr;
        private System.Windows.Forms.Timer timer_ocr;
    }
}

