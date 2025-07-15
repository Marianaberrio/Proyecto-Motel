namespace caja3
{
    partial class Facturar
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
            this.montoapagartxt = new System.Windows.Forms.Label();
            this.numreserva = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.montototaltxt = new System.Windows.Forms.TextBox();
            this.numreservacombo = new System.Windows.Forms.ComboBox();
            this.metodopagoCombo = new System.Windows.Forms.ComboBox();
            this.facturarbtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // montoapagartxt
            // 
            this.montoapagartxt.AutoSize = true;
            this.montoapagartxt.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.montoapagartxt.Location = new System.Drawing.Point(271, 254);
            this.montoapagartxt.Name = "montoapagartxt";
            this.montoapagartxt.Size = new System.Drawing.Size(149, 19);
            this.montoapagartxt.TabIndex = 0;
            this.montoapagartxt.Text = "Monto a pagar:";
            // 
            // numreserva
            // 
            this.numreserva.AutoSize = true;
            this.numreserva.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numreserva.Location = new System.Drawing.Point(241, 151);
            this.numreserva.Name = "numreserva";
            this.numreserva.Size = new System.Drawing.Size(179, 19);
            this.numreserva.TabIndex = 1;
            this.numreserva.Text = "Numero de Reseva:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(261, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Metodo de Pago:";
            // 
            // montototaltxt
            // 
            this.montototaltxt.Location = new System.Drawing.Point(426, 254);
            this.montototaltxt.Name = "montototaltxt";
            this.montototaltxt.Size = new System.Drawing.Size(100, 22);
            this.montototaltxt.TabIndex = 6;
            this.montototaltxt.TextChanged += new System.EventHandler(this.montototaltxt_TextChanged);
            // 
            // numreservacombo
            // 
            this.numreservacombo.FormattingEnabled = true;
            this.numreservacombo.Location = new System.Drawing.Point(426, 151);
            this.numreservacombo.Name = "numreservacombo";
            this.numreservacombo.Size = new System.Drawing.Size(121, 24);
            this.numreservacombo.TabIndex = 7;
            this.numreservacombo.SelectedIndexChanged += new System.EventHandler(this.numreservacombo_SelectedIndexChanged);
            // 
            // metodopagoCombo
            // 
            this.metodopagoCombo.FormattingEnabled = true;
            this.metodopagoCombo.Location = new System.Drawing.Point(426, 210);
            this.metodopagoCombo.Name = "metodopagoCombo";
            this.metodopagoCombo.Size = new System.Drawing.Size(121, 24);
            this.metodopagoCombo.TabIndex = 8;
            this.metodopagoCombo.SelectedIndexChanged += new System.EventHandler(this.metodopagoCombo_SelectedIndexChanged);
            // 
            // facturarbtn
            // 
            this.facturarbtn.Location = new System.Drawing.Point(636, 382);
            this.facturarbtn.Name = "facturarbtn";
            this.facturarbtn.Size = new System.Drawing.Size(125, 33);
            this.facturarbtn.TabIndex = 10;
            this.facturarbtn.Text = "Pagar";
            this.facturarbtn.UseVisualStyleBackColor = false;
            this.facturarbtn.Click += new System.EventHandler(this.facturarbtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Sans Typewriter", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(313, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 26);
            this.label2.TabIndex = 11;
            this.label2.Text = "Factura aqui:";
            // 
            // Facturar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.facturarbtn);
            this.Controls.Add(this.metodopagoCombo);
            this.Controls.Add(this.numreservacombo);
            this.Controls.Add(this.montototaltxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numreserva);
            this.Controls.Add(this.montoapagartxt);
            this.Name = "Facturar";
            this.Text = "Facturar";
            this.Load += new System.EventHandler(this.Facturar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label montoapagartxt;
        private System.Windows.Forms.Label numreserva;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox montototaltxt;
        private System.Windows.Forms.ComboBox numreservacombo;
        private System.Windows.Forms.ComboBox metodopagoCombo;
        private System.Windows.Forms.Button facturarbtn;
        private System.Windows.Forms.Label label2;
    }
}