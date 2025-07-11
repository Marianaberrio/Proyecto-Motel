namespace AdminApp
{
    partial class Reportes
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
            this.reportePagos = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnReportePagos = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.habitacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reservasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviciosPorReservaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.habitacionesPorReservaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pagosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReporteClientes = new System.Windows.Forms.Button();
            this.ReporteClientes = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnReporteHabitaciones = new System.Windows.Forms.Button();
            this.ReporteHabitaciones = new Microsoft.Reporting.WinForms.ReportViewer();
            this.buttonbtnReporteHabitacionesReserva = new System.Windows.Forms.Button();
            this.reporteReservaHabitacion = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnReporteReservas = new System.Windows.Forms.Button();
            this.reporteReservas = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnServiciosReserva = new System.Windows.Forms.Button();
            this.reporteReservaServicio = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnReporteServicios = new System.Windows.Forms.Button();
            this.reporteServicios = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnReporteUsuarios = new System.Windows.Forms.Button();
            this.reporteUsuarios = new Microsoft.Reporting.WinForms.ReportViewer();
            this.CerrarApp = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportePagos
            // 
            this.reportePagos.AutoScroll = true;
            this.reportePagos.AutoSize = true;
            this.reportePagos.DocumentMapWidth = 1;
            this.reportePagos.IsDocumentMapWidthFixed = true;
            this.reportePagos.LocalReport.ReportEmbeddedResource = "AdminApp.RptPagos.rdlc";
            this.reportePagos.Location = new System.Drawing.Point(74, 326);
            this.reportePagos.Name = "reportePagos";
            this.reportePagos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.reportePagos.ServerReport.BearerToken = null;
            this.reportePagos.ShowBackButton = false;
            this.reportePagos.Size = new System.Drawing.Size(1232, 610);
            this.reportePagos.TabIndex = 0;
            this.reportePagos.Visible = false;
            this.reportePagos.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // btnReportePagos
            // 
            this.btnReportePagos.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportePagos.Location = new System.Drawing.Point(91, 116);
            this.btnReportePagos.Name = "btnReportePagos";
            this.btnReportePagos.Size = new System.Drawing.Size(231, 73);
            this.btnReportePagos.TabIndex = 1;
            this.btnReportePagos.Text = "Generar Reporte de Pagos";
            this.btnReportePagos.UseVisualStyleBackColor = true;
            this.btnReportePagos.Click += new System.EventHandler(this.btnReportePagos_Click);
            this.btnReportePagos.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnReportePagos_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans Typewriter", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(635, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Reportes";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.habitacionesToolStripMenuItem,
            this.clientesToolStripMenuItem,
            this.reservasToolStripMenuItem,
            this.serviciosToolStripMenuItem,
            this.usuariosToolStripMenuItem,
            this.pagosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1294, 28);
            this.menuStrip1.TabIndex = 46;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // habitacionesToolStripMenuItem
            // 
            this.habitacionesToolStripMenuItem.Name = "habitacionesToolStripMenuItem";
            this.habitacionesToolStripMenuItem.Size = new System.Drawing.Size(121, 24);
            this.habitacionesToolStripMenuItem.Text = "Menú Principal";
            this.habitacionesToolStripMenuItem.Click += new System.EventHandler(this.habitacionesToolStripMenuItem_Click);
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.clientesToolStripMenuItem.Text = "Habitaciones";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click);
            // 
            // reservasToolStripMenuItem
            // 
            this.reservasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviciosPorReservaToolStripMenuItem,
            this.habitacionesPorReservaToolStripMenuItem});
            this.reservasToolStripMenuItem.Name = "reservasToolStripMenuItem";
            this.reservasToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.reservasToolStripMenuItem.Text = "Reservas";
            this.reservasToolStripMenuItem.Click += new System.EventHandler(this.reservasToolStripMenuItem_Click);
            // 
            // serviciosPorReservaToolStripMenuItem
            // 
            this.serviciosPorReservaToolStripMenuItem.Name = "serviciosPorReservaToolStripMenuItem";
            this.serviciosPorReservaToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
            this.serviciosPorReservaToolStripMenuItem.Text = "Servicios por Reserva";
            this.serviciosPorReservaToolStripMenuItem.Click += new System.EventHandler(this.serviciosPorReservaToolStripMenuItem_Click);
            // 
            // habitacionesPorReservaToolStripMenuItem
            // 
            this.habitacionesPorReservaToolStripMenuItem.Name = "habitacionesPorReservaToolStripMenuItem";
            this.habitacionesPorReservaToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
            this.habitacionesPorReservaToolStripMenuItem.Text = "Habitaciones por Reserva";
            this.habitacionesPorReservaToolStripMenuItem.Click += new System.EventHandler(this.habitacionesPorReservaToolStripMenuItem_Click);
            // 
            // serviciosToolStripMenuItem
            // 
            this.serviciosToolStripMenuItem.Name = "serviciosToolStripMenuItem";
            this.serviciosToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.serviciosToolStripMenuItem.Text = "Servicios";
            this.serviciosToolStripMenuItem.Click += new System.EventHandler(this.serviciosToolStripMenuItem_Click);
            // 
            // usuariosToolStripMenuItem
            // 
            this.usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            this.usuariosToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.usuariosToolStripMenuItem.Text = "Usuarios";
            this.usuariosToolStripMenuItem.Click += new System.EventHandler(this.usuariosToolStripMenuItem_Click);
            // 
            // pagosToolStripMenuItem
            // 
            this.pagosToolStripMenuItem.Name = "pagosToolStripMenuItem";
            this.pagosToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.pagosToolStripMenuItem.Text = "Pagos";
            this.pagosToolStripMenuItem.Click += new System.EventHandler(this.pagosToolStripMenuItem_Click);
            // 
            // btnReporteClientes
            // 
            this.btnReporteClientes.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporteClientes.Location = new System.Drawing.Point(405, 116);
            this.btnReporteClientes.Name = "btnReporteClientes";
            this.btnReporteClientes.Size = new System.Drawing.Size(231, 73);
            this.btnReporteClientes.TabIndex = 47;
            this.btnReporteClientes.Text = "Generar Reporte de Clientes";
            this.btnReporteClientes.UseVisualStyleBackColor = true;
            this.btnReporteClientes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnReporteClientes_MouseClick);
            // 
            // ReporteClientes
            // 
            this.ReporteClientes.AutoScroll = true;
            this.ReporteClientes.AutoSize = true;
            this.ReporteClientes.DocumentMapWidth = 1;
            this.ReporteClientes.IsDocumentMapWidthFixed = true;
            this.ReporteClientes.LocalReport.ReportEmbeddedResource = "AdminApp.RptClientes.rdlc";
            this.ReporteClientes.Location = new System.Drawing.Point(74, 326);
            this.ReporteClientes.Name = "ReporteClientes";
            this.ReporteClientes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ReporteClientes.ServerReport.BearerToken = null;
            this.ReporteClientes.ShowBackButton = false;
            this.ReporteClientes.Size = new System.Drawing.Size(1232, 610);
            this.ReporteClientes.TabIndex = 48;
            this.ReporteClientes.Visible = false;
            this.ReporteClientes.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // btnReporteHabitaciones
            // 
            this.btnReporteHabitaciones.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporteHabitaciones.Location = new System.Drawing.Point(733, 116);
            this.btnReporteHabitaciones.Name = "btnReporteHabitaciones";
            this.btnReporteHabitaciones.Size = new System.Drawing.Size(231, 73);
            this.btnReporteHabitaciones.TabIndex = 49;
            this.btnReporteHabitaciones.Text = "Generar Reporte de Habitaciones";
            this.btnReporteHabitaciones.UseVisualStyleBackColor = true;
            this.btnReporteHabitaciones.Click += new System.EventHandler(this.btnReporteHabitaciones_Click);
            // 
            // ReporteHabitaciones
            // 
            this.ReporteHabitaciones.AutoScroll = true;
            this.ReporteHabitaciones.AutoSize = true;
            this.ReporteHabitaciones.DocumentMapWidth = 1;
            this.ReporteHabitaciones.IsDocumentMapWidthFixed = true;
            this.ReporteHabitaciones.LocalReport.ReportEmbeddedResource = "AdminApp.RptHabitaciones.rdlc";
            this.ReporteHabitaciones.Location = new System.Drawing.Point(74, 326);
            this.ReporteHabitaciones.Name = "ReporteHabitaciones";
            this.ReporteHabitaciones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ReporteHabitaciones.ServerReport.BearerToken = null;
            this.ReporteHabitaciones.ShowBackButton = false;
            this.ReporteHabitaciones.Size = new System.Drawing.Size(1232, 610);
            this.ReporteHabitaciones.TabIndex = 50;
            this.ReporteHabitaciones.Visible = false;
            this.ReporteHabitaciones.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // buttonbtnReporteHabitacionesReserva
            // 
            this.buttonbtnReporteHabitacionesReserva.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonbtnReporteHabitacionesReserva.Location = new System.Drawing.Point(1051, 116);
            this.buttonbtnReporteHabitacionesReserva.Name = "buttonbtnReporteHabitacionesReserva";
            this.buttonbtnReporteHabitacionesReserva.Size = new System.Drawing.Size(231, 73);
            this.buttonbtnReporteHabitacionesReserva.TabIndex = 51;
            this.buttonbtnReporteHabitacionesReserva.Text = "Generar Reporte de Habitaciones por Reserva";
            this.buttonbtnReporteHabitacionesReserva.UseVisualStyleBackColor = true;
            this.buttonbtnReporteHabitacionesReserva.Click += new System.EventHandler(this.buttonbtnReporteHabitacionesReserva_Click);
            // 
            // reporteReservaHabitacion
            // 
            this.reporteReservaHabitacion.AutoScroll = true;
            this.reporteReservaHabitacion.AutoSize = true;
            this.reporteReservaHabitacion.DocumentMapWidth = 1;
            this.reporteReservaHabitacion.IsDocumentMapWidthFixed = true;
            this.reporteReservaHabitacion.LocalReport.ReportEmbeddedResource = "AdminApp.RptReservaHabitacion.rdlc";
            this.reporteReservaHabitacion.Location = new System.Drawing.Point(74, 326);
            this.reporteReservaHabitacion.Name = "reporteReservaHabitacion";
            this.reporteReservaHabitacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.reporteReservaHabitacion.ServerReport.BearerToken = null;
            this.reporteReservaHabitacion.ShowBackButton = false;
            this.reporteReservaHabitacion.Size = new System.Drawing.Size(1232, 610);
            this.reporteReservaHabitacion.TabIndex = 52;
            this.reporteReservaHabitacion.Visible = false;
            this.reporteReservaHabitacion.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // btnReporteReservas
            // 
            this.btnReporteReservas.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporteReservas.Location = new System.Drawing.Point(91, 233);
            this.btnReporteReservas.Name = "btnReporteReservas";
            this.btnReporteReservas.Size = new System.Drawing.Size(231, 73);
            this.btnReporteReservas.TabIndex = 53;
            this.btnReporteReservas.Text = "Generar Reporte de Reservas";
            this.btnReporteReservas.UseVisualStyleBackColor = true;
            this.btnReporteReservas.Click += new System.EventHandler(this.btnReporteReservas_Click);
            // 
            // reporteReservas
            // 
            this.reporteReservas.AutoScroll = true;
            this.reporteReservas.AutoSize = true;
            this.reporteReservas.DocumentMapWidth = 1;
            this.reporteReservas.IsDocumentMapWidthFixed = true;
            this.reporteReservas.LocalReport.ReportEmbeddedResource = "AdminApp.RptReservas.rdlc";
            this.reporteReservas.Location = new System.Drawing.Point(74, 326);
            this.reporteReservas.Name = "reporteReservas";
            this.reporteReservas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.reporteReservas.ServerReport.BearerToken = null;
            this.reporteReservas.ShowBackButton = false;
            this.reporteReservas.Size = new System.Drawing.Size(1232, 610);
            this.reporteReservas.TabIndex = 54;
            this.reporteReservas.Visible = false;
            this.reporteReservas.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // btnServiciosReserva
            // 
            this.btnServiciosReserva.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServiciosReserva.Location = new System.Drawing.Point(405, 233);
            this.btnServiciosReserva.Name = "btnServiciosReserva";
            this.btnServiciosReserva.Size = new System.Drawing.Size(231, 73);
            this.btnServiciosReserva.TabIndex = 55;
            this.btnServiciosReserva.Text = "Generar Reporte de Servicios por Reserva";
            this.btnServiciosReserva.UseVisualStyleBackColor = true;
            this.btnServiciosReserva.Click += new System.EventHandler(this.btnServiciosReserva_Click);
            // 
            // reporteReservaServicio
            // 
            this.reporteReservaServicio.AutoScroll = true;
            this.reporteReservaServicio.AutoSize = true;
            this.reporteReservaServicio.DocumentMapWidth = 1;
            this.reporteReservaServicio.IsDocumentMapWidthFixed = true;
            this.reporteReservaServicio.LocalReport.ReportEmbeddedResource = "AdminApp.RptReservaServicio.rdlc";
            this.reporteReservaServicio.Location = new System.Drawing.Point(74, 326);
            this.reporteReservaServicio.Name = "reporteReservaServicio";
            this.reporteReservaServicio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.reporteReservaServicio.ServerReport.BearerToken = null;
            this.reporteReservaServicio.ShowBackButton = false;
            this.reporteReservaServicio.Size = new System.Drawing.Size(1232, 610);
            this.reporteReservaServicio.TabIndex = 56;
            this.reporteReservaServicio.Visible = false;
            this.reporteReservaServicio.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // btnReporteServicios
            // 
            this.btnReporteServicios.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporteServicios.Location = new System.Drawing.Point(733, 233);
            this.btnReporteServicios.Name = "btnReporteServicios";
            this.btnReporteServicios.Size = new System.Drawing.Size(231, 73);
            this.btnReporteServicios.TabIndex = 57;
            this.btnReporteServicios.Text = "Generar Reporte de Servicios";
            this.btnReporteServicios.UseVisualStyleBackColor = true;
            this.btnReporteServicios.Click += new System.EventHandler(this.btnReporteServicios_Click);
            // 
            // reporteServicios
            // 
            this.reporteServicios.AutoScroll = true;
            this.reporteServicios.AutoSize = true;
            this.reporteServicios.DocumentMapWidth = 1;
            this.reporteServicios.IsDocumentMapWidthFixed = true;
            this.reporteServicios.LocalReport.ReportEmbeddedResource = "AdminApp.RptServicios.rdlc";
            this.reporteServicios.Location = new System.Drawing.Point(74, 326);
            this.reporteServicios.Name = "reporteServicios";
            this.reporteServicios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.reporteServicios.ServerReport.BearerToken = null;
            this.reporteServicios.ShowBackButton = false;
            this.reporteServicios.Size = new System.Drawing.Size(1232, 610);
            this.reporteServicios.TabIndex = 58;
            this.reporteServicios.Visible = false;
            this.reporteServicios.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // btnReporteUsuarios
            // 
            this.btnReporteUsuarios.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporteUsuarios.Location = new System.Drawing.Point(1051, 233);
            this.btnReporteUsuarios.Name = "btnReporteUsuarios";
            this.btnReporteUsuarios.Size = new System.Drawing.Size(231, 73);
            this.btnReporteUsuarios.TabIndex = 59;
            this.btnReporteUsuarios.Text = "Generar Reporte de Usuarios";
            this.btnReporteUsuarios.UseVisualStyleBackColor = true;
            this.btnReporteUsuarios.Click += new System.EventHandler(this.btnReporteUsuarios_Click);
            // 
            // reporteUsuarios
            // 
            this.reporteUsuarios.AutoScroll = true;
            this.reporteUsuarios.AutoSize = true;
            this.reporteUsuarios.DocumentMapWidth = 1;
            this.reporteUsuarios.IsDocumentMapWidthFixed = true;
            this.reporteUsuarios.LocalReport.ReportEmbeddedResource = "AdminApp.RptUsuarios.rdlc";
            this.reporteUsuarios.Location = new System.Drawing.Point(74, 326);
            this.reporteUsuarios.Name = "reporteUsuarios";
            this.reporteUsuarios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.reporteUsuarios.ServerReport.BearerToken = null;
            this.reporteUsuarios.ShowBackButton = false;
            this.reporteUsuarios.Size = new System.Drawing.Size(1232, 610);
            this.reporteUsuarios.TabIndex = 60;
            this.reporteUsuarios.Visible = false;
            this.reporteUsuarios.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // CerrarApp
            // 
            this.CerrarApp.Location = new System.Drawing.Point(618, 474);
            this.CerrarApp.Name = "CerrarApp";
            this.CerrarApp.Size = new System.Drawing.Size(58, 22);
            this.CerrarApp.TabIndex = 61;
            this.CerrarApp.Text = "X";
            this.CerrarApp.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1207, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 62;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1294, 971);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CerrarApp);
            this.Controls.Add(this.reporteUsuarios);
            this.Controls.Add(this.reporteServicios);
            this.Controls.Add(this.reporteReservaServicio);
            this.Controls.Add(this.reporteReservas);
            this.Controls.Add(this.reporteReservaHabitacion);
            this.Controls.Add(this.ReporteHabitaciones);
            this.Controls.Add(this.ReporteClientes);
            this.Controls.Add(this.reportePagos);
            this.Controls.Add(this.btnReporteUsuarios);
            this.Controls.Add(this.btnReporteServicios);
            this.Controls.Add(this.btnServiciosReserva);
            this.Controls.Add(this.btnReporteReservas);
            this.Controls.Add(this.buttonbtnReporteHabitacionesReserva);
            this.Controls.Add(this.btnReporteHabitaciones);
            this.Controls.Add(this.btnReporteClientes);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReportePagos);
            this.Name = "Reportes";
            this.Text = "Reportes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Reportes_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportePagos;
        private System.Windows.Forms.Button btnReportePagos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem habitacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reservasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviciosPorReservaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem habitacionesPorReservaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviciosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pagosToolStripMenuItem;
        private System.Windows.Forms.Button btnReporteClientes;
        private Microsoft.Reporting.WinForms.ReportViewer ReporteClientes;
        private System.Windows.Forms.Button btnReporteHabitaciones;
        private Microsoft.Reporting.WinForms.ReportViewer ReporteHabitaciones;
        private System.Windows.Forms.Button buttonbtnReporteHabitacionesReserva;
        private Microsoft.Reporting.WinForms.ReportViewer reporteReservaHabitacion;
        private System.Windows.Forms.Button btnReporteReservas;
        private Microsoft.Reporting.WinForms.ReportViewer reporteReservas;
        private System.Windows.Forms.Button btnServiciosReserva;
        private Microsoft.Reporting.WinForms.ReportViewer reporteReservaServicio;
        private System.Windows.Forms.Button btnReporteServicios;
        private Microsoft.Reporting.WinForms.ReportViewer reporteServicios;
        private System.Windows.Forms.Button btnReporteUsuarios;
        private Microsoft.Reporting.WinForms.ReportViewer reporteUsuarios;
        private System.Windows.Forms.Button CerrarApp;
        private System.Windows.Forms.Button button1;
    }
}