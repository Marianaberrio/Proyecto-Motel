namespace caja3
{
    partial class DetallePago
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numpagotxt = new System.Windows.Forms.TextBox();
            this.numreservatxt = new System.Windows.Forms.TextBox();
            this.montopagadotxt = new System.Windows.Forms.TextBox();
            this.fechapagotxt = new System.Windows.Forms.TextBox();
            this.metodopagotxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numero de pago:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Numero de reserva:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(98, 355);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Monto pago:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(426, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Fecha de pago:";
            // 
            // numpagotxt
            // 
            this.numpagotxt.Location = new System.Drawing.Point(211, 203);
            this.numpagotxt.Name = "numpagotxt";
            this.numpagotxt.Size = new System.Drawing.Size(145, 22);
            this.numpagotxt.TabIndex = 4;
            // 
            // numreservatxt
            // 
            this.numreservatxt.Location = new System.Drawing.Point(211, 275);
            this.numreservatxt.Name = "numreservatxt";
            this.numreservatxt.Size = new System.Drawing.Size(145, 22);
            this.numreservatxt.TabIndex = 5;
            // 
            // montopagadotxt
            // 
            this.montopagadotxt.Location = new System.Drawing.Point(211, 350);
            this.montopagadotxt.Name = "montopagadotxt";
            this.montopagadotxt.Size = new System.Drawing.Size(145, 22);
            this.montopagadotxt.TabIndex = 6;
            // 
            // fechapagotxt
            // 
            this.fechapagotxt.Location = new System.Drawing.Point(566, 206);
            this.fechapagotxt.Name = "fechapagotxt";
            this.fechapagotxt.Size = new System.Drawing.Size(169, 22);
            this.fechapagotxt.TabIndex = 7;
            // 
            // metodopagotxt
            // 
            this.metodopagotxt.Location = new System.Drawing.Point(566, 280);
            this.metodopagotxt.Name = "metodopagotxt";
            this.metodopagotxt.Size = new System.Drawing.Size(161, 22);
            this.metodopagotxt.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(417, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Metodo de pago:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Lucida Sans Typewriter", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(296, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(236, 26);
            this.label7.TabIndex = 12;
            this.label7.Text = "Detalles de Pago";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(566, 395);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 34);
            this.button1.TabIndex = 13;
            this.button1.Text = "Generar Reporte";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DetallePago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.metodopagotxt);
            this.Controls.Add(this.fechapagotxt);
            this.Controls.Add(this.montopagadotxt);
            this.Controls.Add(this.numreservatxt);
            this.Controls.Add(this.numpagotxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DetallePago";
            this.Text = "DetallePago";
            this.Load += new System.EventHandler(this.DetallePago_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox numpagotxt;
        private System.Windows.Forms.TextBox numreservatxt;
        private System.Windows.Forms.TextBox montopagadotxt;
        private System.Windows.Forms.TextBox fechapagotxt;
        private System.Windows.Forms.TextBox metodopagotxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
    }
}