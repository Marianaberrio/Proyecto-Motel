namespace AdminApp
{
    partial class Reservas
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
            this.gbBuscarReserva = new System.Windows.Forms.GroupBox();
            this.btnBuscarReserv = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.btnSalirBuscarReserva = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.clientesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dbMotelDataSet = new AdminApp.dbMotelDataSet();
            this.gbModificarReserva = new System.Windows.Forms.GroupBox();
            this.btnModificarUser = new System.Windows.Forms.Button();
            this.btnCancelarModificar = new System.Windows.Forms.Button();
            this.usuariosTableAdapter = new AdminApp.dbMotelUsuariosDataSet1TableAdapters.UsuariosTableAdapter();
            this.habitacionesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dbMotelHabitacionesDataSet = new AdminApp.dbMotelHabitacionesDataSet();
            this.btnEliminarNombreUsuario = new System.Windows.Forms.RadioButton();
            this.txtEliminarNombreUsuario = new System.Windows.Forms.TextBox();
            this.btnEliminarIdUsuario = new System.Windows.Forms.RadioButton();
            this.txtEliminarIDUsuario = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.gbEliminarReserva = new System.Windows.Forms.GroupBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCancelarEliminar = new System.Windows.Forms.Button();
            this.gbAgregarReserva = new System.Windows.Forms.GroupBox();
            this.dateTimePickerFechaSalida = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFechaEntrada = new System.Windows.Forms.DateTimePicker();
            this.txtComentarioReserva = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtBoxTotalReserva = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmBoxTipoHabitacion = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNumHabitaciones = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmBoxNumCliente = new System.Windows.Forms.ComboBox();
            this.btnAgregarNumCliente = new System.Windows.Forms.RadioButton();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCancelarAgregar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.habitacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reservasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pagosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAgregarReserva = new System.Windows.Forms.Button();
            this.menuprincipalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBuscarReserva = new System.Windows.Forms.Button();
            this.btnEliminarReserva = new System.Windows.Forms.Button();
            this.habitacionesTableAdapter = new AdminApp.dbMotelHabitacionesDataSetTableAdapters.HabitacionesTableAdapter();
            this.serviciosTableAdapter = new AdminApp.dbMotelServiciosDataSetTableAdapters.ServiciosTableAdapter();
            this.btnModificarReserva = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.reservasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.serviciosPorReservaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.habitacionesPorReservaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbMotelServiciosDataSet = new AdminApp.dbMotelServiciosDataSet();
            this.serviciosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dbMotelUsuariosDataSet1 = new AdminApp.dbMotelUsuariosDataSet1();
            this.usuariosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewClientes = new System.Windows.Forms.DataGridView();
            this.numReservaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numClienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaReservaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaEntradaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaSalidaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estadoReservaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalReservaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comentarioReservaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reservasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dbMotelDataSet1 = new AdminApp.dbMotelDataSet1();
            this.clientesTableAdapter = new AdminApp.dbMotelDataSetTableAdapters.ClientesTableAdapter();
            this.CerrarApp = new System.Windows.Forms.Button();
            this.reservasTableAdapter = new AdminApp.dbMotelDataSet1TableAdapters.ReservasTableAdapter();
            this.cmBoxIdsReserva = new System.Windows.Forms.ComboBox();
            this.txtBuscarComentarioReserva = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtBuscarTotalReserva = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtFechaCreada = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.dtPickerFechaSalida = new System.Windows.Forms.DateTimePicker();
            this.dtPickerFechaEntrada = new System.Windows.Forms.DateTimePicker();
            this.txtEstadoReserva = new System.Windows.Forms.TextBox();
            this.txtBoxBuscarIDCliente = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtModificarIdCliente = new System.Windows.Forms.TextBox();
            this.txtModificarEstadoReserva = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtPickerModificarFechaSalida = new System.Windows.Forms.DateTimePicker();
            this.dtPickerModificarFechaEntrada = new System.Windows.Forms.DateTimePicker();
            this.txtModificarComentarioReserva = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotalReserva = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtModificarFechaCreada = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.cmBoxModificarIdReserva = new System.Windows.Forms.ComboBox();
            this.btnBuscarModificarReserva = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.gbBuscarReserva.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbMotelDataSet)).BeginInit();
            this.gbModificarReserva.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.habitacionesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbMotelHabitacionesDataSet)).BeginInit();
            this.gbEliminarReserva.SuspendLayout();
            this.gbAgregarReserva.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbMotelServiciosDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviciosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbMotelUsuariosDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usuariosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reservasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbMotelDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbBuscarReserva
            // 
            this.gbBuscarReserva.Controls.Add(this.label25);
            this.gbBuscarReserva.Controls.Add(this.txtBoxBuscarIDCliente);
            this.gbBuscarReserva.Controls.Add(this.txtEstadoReserva);
            this.gbBuscarReserva.Controls.Add(this.label23);
            this.gbBuscarReserva.Controls.Add(this.dtPickerFechaSalida);
            this.gbBuscarReserva.Controls.Add(this.dtPickerFechaEntrada);
            this.gbBuscarReserva.Controls.Add(this.txtBuscarComentarioReserva);
            this.gbBuscarReserva.Controls.Add(this.label13);
            this.gbBuscarReserva.Controls.Add(this.txtBuscarTotalReserva);
            this.gbBuscarReserva.Controls.Add(this.label14);
            this.gbBuscarReserva.Controls.Add(this.label16);
            this.gbBuscarReserva.Controls.Add(this.txtFechaCreada);
            this.gbBuscarReserva.Controls.Add(this.label19);
            this.gbBuscarReserva.Controls.Add(this.label21);
            this.gbBuscarReserva.Controls.Add(this.label22);
            this.gbBuscarReserva.Controls.Add(this.cmBoxIdsReserva);
            this.gbBuscarReserva.Controls.Add(this.btnBuscarReserv);
            this.gbBuscarReserva.Controls.Add(this.label24);
            this.gbBuscarReserva.Controls.Add(this.btnSalirBuscarReserva);
            this.gbBuscarReserva.Controls.Add(this.label29);
            this.gbBuscarReserva.Location = new System.Drawing.Point(12, 78);
            this.gbBuscarReserva.Name = "gbBuscarReserva";
            this.gbBuscarReserva.Size = new System.Drawing.Size(1126, 511);
            this.gbBuscarReserva.TabIndex = 71;
            this.gbBuscarReserva.TabStop = false;
            this.gbBuscarReserva.Text = "Buscar Reserva";
            this.gbBuscarReserva.Visible = false;
            // 
            // btnBuscarReserv
            // 
            this.btnBuscarReserv.Location = new System.Drawing.Point(493, 82);
            this.btnBuscarReserv.Name = "btnBuscarReserv";
            this.btnBuscarReserv.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarReserv.TabIndex = 16;
            this.btnBuscarReserv.Text = "Buscar";
            this.btnBuscarReserv.UseVisualStyleBackColor = true;
            this.btnBuscarReserv.Click += new System.EventHandler(this.btnBuscarReserv_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(23, 82);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(159, 19);
            this.label24.TabIndex = 11;
            this.label24.Text = "Buscar Reserva:";
            // 
            // btnSalirBuscarReserva
            // 
            this.btnSalirBuscarReserva.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalirBuscarReserva.Location = new System.Drawing.Point(774, 327);
            this.btnSalirBuscarReserva.Name = "btnSalirBuscarReserva";
            this.btnSalirBuscarReserva.Size = new System.Drawing.Size(145, 35);
            this.btnSalirBuscarReserva.TabIndex = 9;
            this.btnSalirBuscarReserva.Text = "Salir";
            this.btnSalirBuscarReserva.UseVisualStyleBackColor = true;
            this.btnSalirBuscarReserva.Click += new System.EventHandler(this.btnSalirBuscarReserva_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(464, 23);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(164, 22);
            this.label29.TabIndex = 0;
            this.label29.Text = "Buscar Reserva";
            // 
            // clientesBindingSource
            // 
            this.clientesBindingSource.DataMember = "Clientes";
            this.clientesBindingSource.DataSource = this.dbMotelDataSet;
            // 
            // dbMotelDataSet
            // 
            this.dbMotelDataSet.DataSetName = "dbMotelDataSet";
            this.dbMotelDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gbModificarReserva
            // 
            this.gbModificarReserva.Controls.Add(this.label6);
            this.gbModificarReserva.Controls.Add(this.txtModificarIdCliente);
            this.gbModificarReserva.Controls.Add(this.txtModificarEstadoReserva);
            this.gbModificarReserva.Controls.Add(this.label7);
            this.gbModificarReserva.Controls.Add(this.dtPickerModificarFechaSalida);
            this.gbModificarReserva.Controls.Add(this.dtPickerModificarFechaEntrada);
            this.gbModificarReserva.Controls.Add(this.txtModificarComentarioReserva);
            this.gbModificarReserva.Controls.Add(this.label8);
            this.gbModificarReserva.Controls.Add(this.txtTotalReserva);
            this.gbModificarReserva.Controls.Add(this.label9);
            this.gbModificarReserva.Controls.Add(this.label10);
            this.gbModificarReserva.Controls.Add(this.txtModificarFechaCreada);
            this.gbModificarReserva.Controls.Add(this.label11);
            this.gbModificarReserva.Controls.Add(this.label26);
            this.gbModificarReserva.Controls.Add(this.label27);
            this.gbModificarReserva.Controls.Add(this.cmBoxModificarIdReserva);
            this.gbModificarReserva.Controls.Add(this.btnBuscarModificarReserva);
            this.gbModificarReserva.Controls.Add(this.label28);
            this.gbModificarReserva.Controls.Add(this.label30);
            this.gbModificarReserva.Controls.Add(this.btnModificarUser);
            this.gbModificarReserva.Controls.Add(this.btnCancelarModificar);
            this.gbModificarReserva.Location = new System.Drawing.Point(12, 78);
            this.gbModificarReserva.Name = "gbModificarReserva";
            this.gbModificarReserva.Size = new System.Drawing.Size(1126, 505);
            this.gbModificarReserva.TabIndex = 66;
            this.gbModificarReserva.TabStop = false;
            this.gbModificarReserva.Text = "Modificar Reserva";
            this.gbModificarReserva.Visible = false;
            // 
            // btnModificarUser
            // 
            this.btnModificarUser.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarUser.Location = new System.Drawing.Point(594, 443);
            this.btnModificarUser.Name = "btnModificarUser";
            this.btnModificarUser.Size = new System.Drawing.Size(145, 35);
            this.btnModificarUser.TabIndex = 64;
            this.btnModificarUser.Text = "Modificar";
            this.btnModificarUser.UseVisualStyleBackColor = true;
            this.btnModificarUser.Click += new System.EventHandler(this.btnModificarUser_Click);
            // 
            // btnCancelarModificar
            // 
            this.btnCancelarModificar.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarModificar.Location = new System.Drawing.Point(347, 443);
            this.btnCancelarModificar.Name = "btnCancelarModificar";
            this.btnCancelarModificar.Size = new System.Drawing.Size(145, 35);
            this.btnCancelarModificar.TabIndex = 63;
            this.btnCancelarModificar.Text = "Cancelar";
            this.btnCancelarModificar.UseVisualStyleBackColor = true;
            this.btnCancelarModificar.Click += new System.EventHandler(this.btnCancelarModificar_Click);
            // 
            // usuariosTableAdapter
            // 
            this.usuariosTableAdapter.ClearBeforeFill = true;
            // 
            // habitacionesBindingSource
            // 
            this.habitacionesBindingSource.DataMember = "Habitaciones";
            this.habitacionesBindingSource.DataSource = this.dbMotelHabitacionesDataSet;
            // 
            // dbMotelHabitacionesDataSet
            // 
            this.dbMotelHabitacionesDataSet.DataSetName = "dbMotelHabitacionesDataSet";
            this.dbMotelHabitacionesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnEliminarNombreUsuario
            // 
            this.btnEliminarNombreUsuario.AutoSize = true;
            this.btnEliminarNombreUsuario.Location = new System.Drawing.Point(515, 94);
            this.btnEliminarNombreUsuario.Name = "btnEliminarNombreUsuario";
            this.btnEliminarNombreUsuario.Size = new System.Drawing.Size(127, 20);
            this.btnEliminarNombreUsuario.TabIndex = 82;
            this.btnEliminarNombreUsuario.TabStop = true;
            this.btnEliminarNombreUsuario.Text = "Nombre Usuario";
            this.btnEliminarNombreUsuario.UseVisualStyleBackColor = true;
            // 
            // txtEliminarNombreUsuario
            // 
            this.txtEliminarNombreUsuario.Location = new System.Drawing.Point(670, 92);
            this.txtEliminarNombreUsuario.Name = "txtEliminarNombreUsuario";
            this.txtEliminarNombreUsuario.Size = new System.Drawing.Size(105, 22);
            this.txtEliminarNombreUsuario.TabIndex = 81;
            // 
            // btnEliminarIdUsuario
            // 
            this.btnEliminarIdUsuario.AutoSize = true;
            this.btnEliminarIdUsuario.Location = new System.Drawing.Point(231, 97);
            this.btnEliminarIdUsuario.Name = "btnEliminarIdUsuario";
            this.btnEliminarIdUsuario.Size = new System.Drawing.Size(91, 20);
            this.btnEliminarIdUsuario.TabIndex = 80;
            this.btnEliminarIdUsuario.TabStop = true;
            this.btnEliminarIdUsuario.Text = "ID Usuario";
            this.btnEliminarIdUsuario.UseVisualStyleBackColor = true;
            // 
            // txtEliminarIDUsuario
            // 
            this.txtEliminarIDUsuario.Location = new System.Drawing.Point(348, 95);
            this.txtEliminarIDUsuario.Name = "txtEliminarIDUsuario";
            this.txtEliminarIDUsuario.Size = new System.Drawing.Size(105, 22);
            this.txtEliminarIDUsuario.TabIndex = 78;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(31, 94);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(159, 19);
            this.label15.TabIndex = 77;
            this.label15.Text = "Buscar Usuario:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(416, 30);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(186, 22);
            this.label20.TabIndex = 0;
            this.label20.Text = "Eliminar Cliente";
            // 
            // gbEliminarReserva
            // 
            this.gbEliminarReserva.Controls.Add(this.btnEliminarNombreUsuario);
            this.gbEliminarReserva.Controls.Add(this.txtEliminarNombreUsuario);
            this.gbEliminarReserva.Controls.Add(this.btnEliminarIdUsuario);
            this.gbEliminarReserva.Controls.Add(this.txtEliminarIDUsuario);
            this.gbEliminarReserva.Controls.Add(this.label15);
            this.gbEliminarReserva.Controls.Add(this.btnEliminar);
            this.gbEliminarReserva.Controls.Add(this.btnCancelarEliminar);
            this.gbEliminarReserva.Controls.Add(this.label20);
            this.gbEliminarReserva.Location = new System.Drawing.Point(45, 662);
            this.gbEliminarReserva.Name = "gbEliminarReserva";
            this.gbEliminarReserva.Size = new System.Drawing.Size(1126, 423);
            this.gbEliminarReserva.TabIndex = 67;
            this.gbEliminarReserva.TabStop = false;
            this.gbEliminarReserva.Text = "Eliminar Cliente";
            this.gbEliminarReserva.Visible = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(521, 165);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(145, 35);
            this.btnEliminar.TabIndex = 10;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnCancelarEliminar
            // 
            this.btnCancelarEliminar.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarEliminar.Location = new System.Drawing.Point(320, 165);
            this.btnCancelarEliminar.Name = "btnCancelarEliminar";
            this.btnCancelarEliminar.Size = new System.Drawing.Size(145, 35);
            this.btnCancelarEliminar.TabIndex = 9;
            this.btnCancelarEliminar.Text = "Cancelar";
            this.btnCancelarEliminar.UseVisualStyleBackColor = true;
            // 
            // gbAgregarReserva
            // 
            this.gbAgregarReserva.Controls.Add(this.dateTimePickerFechaSalida);
            this.gbAgregarReserva.Controls.Add(this.dateTimePickerFechaEntrada);
            this.gbAgregarReserva.Controls.Add(this.txtComentarioReserva);
            this.gbAgregarReserva.Controls.Add(this.label18);
            this.gbAgregarReserva.Controls.Add(this.txtBoxTotalReserva);
            this.gbAgregarReserva.Controls.Add(this.label17);
            this.gbAgregarReserva.Controls.Add(this.cmBoxTipoHabitacion);
            this.gbAgregarReserva.Controls.Add(this.label12);
            this.gbAgregarReserva.Controls.Add(this.txtNumHabitaciones);
            this.gbAgregarReserva.Controls.Add(this.label5);
            this.gbAgregarReserva.Controls.Add(this.label4);
            this.gbAgregarReserva.Controls.Add(this.label3);
            this.gbAgregarReserva.Controls.Add(this.cmBoxNumCliente);
            this.gbAgregarReserva.Controls.Add(this.btnAgregarNumCliente);
            this.gbAgregarReserva.Controls.Add(this.btnAgregar);
            this.gbAgregarReserva.Controls.Add(this.btnCancelarAgregar);
            this.gbAgregarReserva.Controls.Add(this.label2);
            this.gbAgregarReserva.Location = new System.Drawing.Point(12, 83);
            this.gbAgregarReserva.Name = "gbAgregarReserva";
            this.gbAgregarReserva.Size = new System.Drawing.Size(1126, 512);
            this.gbAgregarReserva.TabIndex = 65;
            this.gbAgregarReserva.TabStop = false;
            this.gbAgregarReserva.Text = "Agregar Reserva";
            this.gbAgregarReserva.Visible = false;
            // 
            // dateTimePickerFechaSalida
            // 
            this.dateTimePickerFechaSalida.Location = new System.Drawing.Point(695, 135);
            this.dateTimePickerFechaSalida.Name = "dateTimePickerFechaSalida";
            this.dateTimePickerFechaSalida.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerFechaSalida.TabIndex = 79;
            // 
            // dateTimePickerFechaEntrada
            // 
            this.dateTimePickerFechaEntrada.Location = new System.Drawing.Point(172, 137);
            this.dateTimePickerFechaEntrada.Name = "dateTimePickerFechaEntrada";
            this.dateTimePickerFechaEntrada.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerFechaEntrada.TabIndex = 78;
            // 
            // txtComentarioReserva
            // 
            this.txtComentarioReserva.Location = new System.Drawing.Point(154, 289);
            this.txtComentarioReserva.Multiline = true;
            this.txtComentarioReserva.Name = "txtComentarioReserva";
            this.txtComentarioReserva.Size = new System.Drawing.Size(372, 129);
            this.txtComentarioReserva.TabIndex = 77;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(20, 289);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(119, 19);
            this.label18.TabIndex = 76;
            this.label18.Text = "Comentario:";
            // 
            // txtBoxTotalReserva
            // 
            this.txtBoxTotalReserva.Location = new System.Drawing.Point(889, 273);
            this.txtBoxTotalReserva.Name = "txtBoxTotalReserva";
            this.txtBoxTotalReserva.Size = new System.Drawing.Size(105, 22);
            this.txtBoxTotalReserva.TabIndex = 75;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(715, 276);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(149, 19);
            this.label17.TabIndex = 74;
            this.label17.Text = "Total Reserva:";
            // 
            // cmBoxTipoHabitacion
            // 
            this.cmBoxTipoHabitacion.FormattingEnabled = true;
            this.cmBoxTipoHabitacion.Location = new System.Drawing.Point(870, 207);
            this.cmBoxTipoHabitacion.Name = "cmBoxTipoHabitacion";
            this.cmBoxTipoHabitacion.Size = new System.Drawing.Size(121, 24);
            this.cmBoxTipoHabitacion.TabIndex = 73;
            this.cmBoxTipoHabitacion.SelectedIndexChanged += new System.EventHandler(this.cmBoxTipoHabitacion_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(665, 212);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(199, 19);
            this.label12.TabIndex = 72;
            this.label12.Text = "Tipo de Habitación:";
            // 
            // txtNumHabitaciones
            // 
            this.txtNumHabitaciones.Location = new System.Drawing.Point(265, 209);
            this.txtNumHabitaciones.Name = "txtNumHabitaciones";
            this.txtNumHabitaciones.Size = new System.Drawing.Size(105, 22);
            this.txtNumHabitaciones.TabIndex = 71;
            this.txtNumHabitaciones.TextChanged += new System.EventHandler(this.txtNumHabitaciones_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(239, 19);
            this.label5.TabIndex = 70;
            this.label5.Text = "Número de Habitaciones:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(537, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 19);
            this.label4.TabIndex = 68;
            this.label4.Text = "Fecha salida:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 19);
            this.label3.TabIndex = 66;
            this.label3.Text = "Fecha entrada:";
            // 
            // cmBoxNumCliente
            // 
            this.cmBoxNumCliente.Enabled = false;
            this.cmBoxNumCliente.FormattingEnabled = true;
            this.cmBoxNumCliente.Location = new System.Drawing.Point(124, 67);
            this.cmBoxNumCliente.Name = "cmBoxNumCliente";
            this.cmBoxNumCliente.Size = new System.Drawing.Size(121, 24);
            this.cmBoxNumCliente.TabIndex = 14;
            // 
            // btnAgregarNumCliente
            // 
            this.btnAgregarNumCliente.AutoSize = true;
            this.btnAgregarNumCliente.Location = new System.Drawing.Point(21, 68);
            this.btnAgregarNumCliente.Name = "btnAgregarNumCliente";
            this.btnAgregarNumCliente.Size = new System.Drawing.Size(100, 20);
            this.btnAgregarNumCliente.TabIndex = 13;
            this.btnAgregarNumCliente.TabStop = true;
            this.btnAgregarNumCliente.Text = "Num Cliente";
            this.btnAgregarNumCliente.UseVisualStyleBackColor = true;
            this.btnAgregarNumCliente.CheckedChanged += new System.EventHandler(this.btnAgregarNumCliente_CheckedChanged);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(557, 447);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(145, 35);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCancelarAgregar
            // 
            this.btnCancelarAgregar.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarAgregar.Location = new System.Drawing.Point(354, 447);
            this.btnCancelarAgregar.Name = "btnCancelarAgregar";
            this.btnCancelarAgregar.Size = new System.Drawing.Size(145, 35);
            this.btnCancelarAgregar.TabIndex = 9;
            this.btnCancelarAgregar.Text = "Cancelar";
            this.btnCancelarAgregar.UseVisualStyleBackColor = true;
            this.btnCancelarAgregar.Click += new System.EventHandler(this.btnCancelarAgregar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(421, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "Agregar Reserva";
            // 
            // habitacionesToolStripMenuItem
            // 
            this.habitacionesToolStripMenuItem.Name = "habitacionesToolStripMenuItem";
            this.habitacionesToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.habitacionesToolStripMenuItem.Text = "Habitaciones";
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.clientesToolStripMenuItem.Text = "Clientes";
            // 
            // reservasToolStripMenuItem
            // 
            this.reservasToolStripMenuItem.Name = "reservasToolStripMenuItem";
            this.reservasToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.reservasToolStripMenuItem.Text = "Servicios";
            // 
            // serviciosToolStripMenuItem
            // 
            this.serviciosToolStripMenuItem.Name = "serviciosToolStripMenuItem";
            this.serviciosToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.serviciosToolStripMenuItem.Text = "Usuarios";
            // 
            // pagosToolStripMenuItem
            // 
            this.pagosToolStripMenuItem.Name = "pagosToolStripMenuItem";
            this.pagosToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.pagosToolStripMenuItem.Text = "Pagos";
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // btnAgregarReserva
            // 
            this.btnAgregarReserva.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarReserva.Location = new System.Drawing.Point(12, 349);
            this.btnAgregarReserva.Name = "btnAgregarReserva";
            this.btnAgregarReserva.Size = new System.Drawing.Size(224, 43);
            this.btnAgregarReserva.TabIndex = 62;
            this.btnAgregarReserva.Text = "Agregar Reserva";
            this.btnAgregarReserva.UseVisualStyleBackColor = true;
            this.btnAgregarReserva.Click += new System.EventHandler(this.btnAgregarReserva_Click);
            // 
            // menuprincipalToolStripMenuItem
            // 
            this.menuprincipalToolStripMenuItem.Name = "menuprincipalToolStripMenuItem";
            this.menuprincipalToolStripMenuItem.Size = new System.Drawing.Size(121, 24);
            this.menuprincipalToolStripMenuItem.Text = "Menú Principal";
            // 
            // btnBuscarReserva
            // 
            this.btnBuscarReserva.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarReserva.Location = new System.Drawing.Point(280, 349);
            this.btnBuscarReserva.Name = "btnBuscarReserva";
            this.btnBuscarReserva.Size = new System.Drawing.Size(224, 43);
            this.btnBuscarReserva.TabIndex = 68;
            this.btnBuscarReserva.Text = "Buscar Reserva";
            this.btnBuscarReserva.UseVisualStyleBackColor = true;
            this.btnBuscarReserva.Click += new System.EventHandler(this.btnBuscarReserva_Click);
            // 
            // btnEliminarReserva
            // 
            this.btnEliminarReserva.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarReserva.Location = new System.Drawing.Point(824, 349);
            this.btnEliminarReserva.Name = "btnEliminarReserva";
            this.btnEliminarReserva.Size = new System.Drawing.Size(224, 43);
            this.btnEliminarReserva.TabIndex = 64;
            this.btnEliminarReserva.Text = "Eliminar Reserva";
            this.btnEliminarReserva.UseVisualStyleBackColor = true;
            this.btnEliminarReserva.Click += new System.EventHandler(this.btnEliminarReserva_Click);
            // 
            // habitacionesTableAdapter
            // 
            this.habitacionesTableAdapter.ClearBeforeFill = true;
            // 
            // serviciosTableAdapter
            // 
            this.serviciosTableAdapter.ClearBeforeFill = true;
            // 
            // btnModificarReserva
            // 
            this.btnModificarReserva.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarReserva.Location = new System.Drawing.Point(555, 349);
            this.btnModificarReserva.Name = "btnModificarReserva";
            this.btnModificarReserva.Size = new System.Drawing.Size(224, 43);
            this.btnModificarReserva.TabIndex = 63;
            this.btnModificarReserva.Text = "Modificar Reserva";
            this.btnModificarReserva.UseVisualStyleBackColor = true;
            this.btnModificarReserva.Click += new System.EventHandler(this.btnModificarReserva_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans Typewriter", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(475, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 26);
            this.label1.TabIndex = 61;
            this.label1.Text = "Reservas";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuprincipalToolStripMenuItem,
            this.habitacionesToolStripMenuItem,
            this.clientesToolStripMenuItem,
            this.reservasToolStripMenuItem,
            this.serviciosToolStripMenuItem,
            this.pagosToolStripMenuItem,
            this.reportesToolStripMenuItem,
            this.reservasToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1251, 28);
            this.menuStrip1.TabIndex = 69;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // reservasToolStripMenuItem1
            // 
            this.reservasToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviciosPorReservaToolStripMenuItem,
            this.habitacionesPorReservaToolStripMenuItem});
            this.reservasToolStripMenuItem1.Name = "reservasToolStripMenuItem1";
            this.reservasToolStripMenuItem1.Size = new System.Drawing.Size(80, 24);
            this.reservasToolStripMenuItem1.Text = "Reservas";
            // 
            // serviciosPorReservaToolStripMenuItem
            // 
            this.serviciosPorReservaToolStripMenuItem.Name = "serviciosPorReservaToolStripMenuItem";
            this.serviciosPorReservaToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
            this.serviciosPorReservaToolStripMenuItem.Text = "Servicios por Reserva";
            // 
            // habitacionesPorReservaToolStripMenuItem
            // 
            this.habitacionesPorReservaToolStripMenuItem.Name = "habitacionesPorReservaToolStripMenuItem";
            this.habitacionesPorReservaToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
            this.habitacionesPorReservaToolStripMenuItem.Text = "Habitaciones por Reserva";
            // 
            // dbMotelServiciosDataSet
            // 
            this.dbMotelServiciosDataSet.DataSetName = "dbMotelServiciosDataSet";
            this.dbMotelServiciosDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // serviciosBindingSource
            // 
            this.serviciosBindingSource.DataMember = "Servicios";
            this.serviciosBindingSource.DataSource = this.dbMotelServiciosDataSet;
            // 
            // dbMotelUsuariosDataSet1
            // 
            this.dbMotelUsuariosDataSet1.DataSetName = "dbMotelUsuariosDataSet1";
            this.dbMotelUsuariosDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // usuariosBindingSource
            // 
            this.usuariosBindingSource.DataMember = "Usuarios";
            this.usuariosBindingSource.DataSource = this.dbMotelUsuariosDataSet1;
            // 
            // dataGridViewClientes
            // 
            this.dataGridViewClientes.AllowUserToAddRows = false;
            this.dataGridViewClientes.AllowUserToDeleteRows = false;
            this.dataGridViewClientes.AllowUserToOrderColumns = true;
            this.dataGridViewClientes.AutoGenerateColumns = false;
            this.dataGridViewClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewClientes.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numReservaDataGridViewTextBoxColumn,
            this.numClienteDataGridViewTextBoxColumn,
            this.fechaReservaDataGridViewTextBoxColumn,
            this.fechaEntradaDataGridViewTextBoxColumn,
            this.fechaSalidaDataGridViewTextBoxColumn,
            this.estadoReservaDataGridViewTextBoxColumn,
            this.totalReservaDataGridViewTextBoxColumn,
            this.comentarioReservaDataGridViewTextBoxColumn});
            this.dataGridViewClientes.DataSource = this.reservasBindingSource;
            this.dataGridViewClientes.Location = new System.Drawing.Point(12, 138);
            this.dataGridViewClientes.Name = "dataGridViewClientes";
            this.dataGridViewClientes.ReadOnly = true;
            this.dataGridViewClientes.RowHeadersWidth = 51;
            this.dataGridViewClientes.RowTemplate.Height = 24;
            this.dataGridViewClientes.Size = new System.Drawing.Size(1036, 142);
            this.dataGridViewClientes.TabIndex = 60;
            // 
            // numReservaDataGridViewTextBoxColumn
            // 
            this.numReservaDataGridViewTextBoxColumn.DataPropertyName = "NumReserva";
            this.numReservaDataGridViewTextBoxColumn.HeaderText = "NumReserva";
            this.numReservaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.numReservaDataGridViewTextBoxColumn.Name = "numReservaDataGridViewTextBoxColumn";
            this.numReservaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // numClienteDataGridViewTextBoxColumn
            // 
            this.numClienteDataGridViewTextBoxColumn.DataPropertyName = "NumCliente";
            this.numClienteDataGridViewTextBoxColumn.HeaderText = "NumCliente";
            this.numClienteDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.numClienteDataGridViewTextBoxColumn.Name = "numClienteDataGridViewTextBoxColumn";
            this.numClienteDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fechaReservaDataGridViewTextBoxColumn
            // 
            this.fechaReservaDataGridViewTextBoxColumn.DataPropertyName = "FechaReserva";
            this.fechaReservaDataGridViewTextBoxColumn.HeaderText = "FechaReserva";
            this.fechaReservaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fechaReservaDataGridViewTextBoxColumn.Name = "fechaReservaDataGridViewTextBoxColumn";
            this.fechaReservaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fechaEntradaDataGridViewTextBoxColumn
            // 
            this.fechaEntradaDataGridViewTextBoxColumn.DataPropertyName = "FechaEntrada";
            this.fechaEntradaDataGridViewTextBoxColumn.HeaderText = "FechaEntrada";
            this.fechaEntradaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fechaEntradaDataGridViewTextBoxColumn.Name = "fechaEntradaDataGridViewTextBoxColumn";
            this.fechaEntradaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fechaSalidaDataGridViewTextBoxColumn
            // 
            this.fechaSalidaDataGridViewTextBoxColumn.DataPropertyName = "FechaSalida";
            this.fechaSalidaDataGridViewTextBoxColumn.HeaderText = "FechaSalida";
            this.fechaSalidaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fechaSalidaDataGridViewTextBoxColumn.Name = "fechaSalidaDataGridViewTextBoxColumn";
            this.fechaSalidaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // estadoReservaDataGridViewTextBoxColumn
            // 
            this.estadoReservaDataGridViewTextBoxColumn.DataPropertyName = "EstadoReserva";
            this.estadoReservaDataGridViewTextBoxColumn.HeaderText = "EstadoReserva";
            this.estadoReservaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.estadoReservaDataGridViewTextBoxColumn.Name = "estadoReservaDataGridViewTextBoxColumn";
            this.estadoReservaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // totalReservaDataGridViewTextBoxColumn
            // 
            this.totalReservaDataGridViewTextBoxColumn.DataPropertyName = "TotalReserva";
            this.totalReservaDataGridViewTextBoxColumn.HeaderText = "TotalReserva";
            this.totalReservaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.totalReservaDataGridViewTextBoxColumn.Name = "totalReservaDataGridViewTextBoxColumn";
            this.totalReservaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // comentarioReservaDataGridViewTextBoxColumn
            // 
            this.comentarioReservaDataGridViewTextBoxColumn.DataPropertyName = "ComentarioReserva";
            this.comentarioReservaDataGridViewTextBoxColumn.HeaderText = "ComentarioReserva";
            this.comentarioReservaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.comentarioReservaDataGridViewTextBoxColumn.Name = "comentarioReservaDataGridViewTextBoxColumn";
            this.comentarioReservaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // reservasBindingSource
            // 
            this.reservasBindingSource.DataMember = "Reservas";
            this.reservasBindingSource.DataSource = this.dbMotelDataSet1;
            // 
            // dbMotelDataSet1
            // 
            this.dbMotelDataSet1.DataSetName = "dbMotelDataSet1";
            this.dbMotelDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // clientesTableAdapter
            // 
            this.clientesTableAdapter.ClearBeforeFill = true;
            // 
            // CerrarApp
            // 
            this.CerrarApp.Location = new System.Drawing.Point(1009, 31);
            this.CerrarApp.Name = "CerrarApp";
            this.CerrarApp.Size = new System.Drawing.Size(58, 22);
            this.CerrarApp.TabIndex = 70;
            this.CerrarApp.Text = "X";
            this.CerrarApp.UseVisualStyleBackColor = true;
            this.CerrarApp.Click += new System.EventHandler(this.CerrarApp_Click);
            // 
            // reservasTableAdapter
            // 
            this.reservasTableAdapter.ClearBeforeFill = true;
            // 
            // cmBoxIdsReserva
            // 
            this.cmBoxIdsReserva.FormattingEnabled = true;
            this.cmBoxIdsReserva.Location = new System.Drawing.Point(339, 81);
            this.cmBoxIdsReserva.Name = "cmBoxIdsReserva";
            this.cmBoxIdsReserva.Size = new System.Drawing.Size(121, 24);
            this.cmBoxIdsReserva.TabIndex = 27;
            // 
            // txtBuscarComentarioReserva
            // 
            this.txtBuscarComentarioReserva.Location = new System.Drawing.Point(157, 288);
            this.txtBuscarComentarioReserva.Multiline = true;
            this.txtBuscarComentarioReserva.Name = "txtBuscarComentarioReserva";
            this.txtBuscarComentarioReserva.Size = new System.Drawing.Size(372, 129);
            this.txtBuscarComentarioReserva.TabIndex = 89;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(23, 288);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(119, 19);
            this.label13.TabIndex = 88;
            this.label13.Text = "Comentario:";
            // 
            // txtBuscarTotalReserva
            // 
            this.txtBuscarTotalReserva.Enabled = false;
            this.txtBuscarTotalReserva.Location = new System.Drawing.Point(840, 259);
            this.txtBuscarTotalReserva.Name = "txtBuscarTotalReserva";
            this.txtBuscarTotalReserva.Size = new System.Drawing.Size(105, 22);
            this.txtBuscarTotalReserva.TabIndex = 87;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(600, 262);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(149, 19);
            this.label14.TabIndex = 86;
            this.label14.Text = "Total Reserva:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(600, 198);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(159, 19);
            this.label16.TabIndex = 84;
            this.label16.Text = "Estado Reserva:";
            // 
            // txtFechaCreada
            // 
            this.txtFechaCreada.Enabled = false;
            this.txtFechaCreada.Location = new System.Drawing.Point(269, 198);
            this.txtFechaCreada.Name = "txtFechaCreada";
            this.txtFechaCreada.Size = new System.Drawing.Size(105, 22);
            this.txtFechaCreada.TabIndex = 83;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(20, 198);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(139, 19);
            this.label19.TabIndex = 82;
            this.label19.Text = "Fecha Creada:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(600, 135);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(139, 19);
            this.label21.TabIndex = 81;
            this.label21.Text = "Fecha salida:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(20, 136);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(149, 19);
            this.label22.TabIndex = 80;
            this.label22.Text = "Fecha entrada:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(234, 84);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(81, 18);
            this.label23.TabIndex = 93;
            this.label23.Text = "ID Reserva";
            // 
            // dtPickerFechaSalida
            // 
            this.dtPickerFechaSalida.Enabled = false;
            this.dtPickerFechaSalida.Location = new System.Drawing.Point(745, 133);
            this.dtPickerFechaSalida.Name = "dtPickerFechaSalida";
            this.dtPickerFechaSalida.Size = new System.Drawing.Size(200, 22);
            this.dtPickerFechaSalida.TabIndex = 91;
            // 
            // dtPickerFechaEntrada
            // 
            this.dtPickerFechaEntrada.Enabled = false;
            this.dtPickerFechaEntrada.Location = new System.Drawing.Point(175, 136);
            this.dtPickerFechaEntrada.Name = "dtPickerFechaEntrada";
            this.dtPickerFechaEntrada.Size = new System.Drawing.Size(200, 22);
            this.dtPickerFechaEntrada.TabIndex = 90;
            // 
            // txtEstadoReserva
            // 
            this.txtEstadoReserva.Enabled = false;
            this.txtEstadoReserva.Location = new System.Drawing.Point(840, 198);
            this.txtEstadoReserva.Name = "txtEstadoReserva";
            this.txtEstadoReserva.Size = new System.Drawing.Size(105, 22);
            this.txtEstadoReserva.TabIndex = 94;
            // 
            // txtBoxBuscarIDCliente
            // 
            this.txtBoxBuscarIDCliente.Enabled = false;
            this.txtBoxBuscarIDCliente.Location = new System.Drawing.Point(840, 84);
            this.txtBoxBuscarIDCliente.Name = "txtBoxBuscarIDCliente";
            this.txtBoxBuscarIDCliente.Size = new System.Drawing.Size(105, 22);
            this.txtBoxBuscarIDCliente.TabIndex = 96;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(600, 86);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(119, 19);
            this.label25.TabIndex = 97;
            this.label25.Text = "ID Cliente:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(625, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 19);
            this.label6.TabIndex = 117;
            this.label6.Text = "ID Cliente:";
            // 
            // txtModificarIdCliente
            // 
            this.txtModificarIdCliente.Enabled = false;
            this.txtModificarIdCliente.Location = new System.Drawing.Point(865, 79);
            this.txtModificarIdCliente.Name = "txtModificarIdCliente";
            this.txtModificarIdCliente.Size = new System.Drawing.Size(105, 22);
            this.txtModificarIdCliente.TabIndex = 116;
            // 
            // txtModificarEstadoReserva
            // 
            this.txtModificarEstadoReserva.Location = new System.Drawing.Point(865, 193);
            this.txtModificarEstadoReserva.Name = "txtModificarEstadoReserva";
            this.txtModificarEstadoReserva.Size = new System.Drawing.Size(105, 22);
            this.txtModificarEstadoReserva.TabIndex = 115;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(259, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 18);
            this.label7.TabIndex = 114;
            this.label7.Text = "ID Reserva";
            // 
            // dtPickerModificarFechaSalida
            // 
            this.dtPickerModificarFechaSalida.Location = new System.Drawing.Point(770, 128);
            this.dtPickerModificarFechaSalida.Name = "dtPickerModificarFechaSalida";
            this.dtPickerModificarFechaSalida.Size = new System.Drawing.Size(200, 22);
            this.dtPickerModificarFechaSalida.TabIndex = 113;
            // 
            // dtPickerModificarFechaEntrada
            // 
            this.dtPickerModificarFechaEntrada.Location = new System.Drawing.Point(200, 131);
            this.dtPickerModificarFechaEntrada.Name = "dtPickerModificarFechaEntrada";
            this.dtPickerModificarFechaEntrada.Size = new System.Drawing.Size(200, 22);
            this.dtPickerModificarFechaEntrada.TabIndex = 112;
            // 
            // txtModificarComentarioReserva
            // 
            this.txtModificarComentarioReserva.Location = new System.Drawing.Point(182, 283);
            this.txtModificarComentarioReserva.Multiline = true;
            this.txtModificarComentarioReserva.Name = "txtModificarComentarioReserva";
            this.txtModificarComentarioReserva.Size = new System.Drawing.Size(372, 129);
            this.txtModificarComentarioReserva.TabIndex = 111;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(48, 283);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 19);
            this.label8.TabIndex = 110;
            this.label8.Text = "Comentario:";
            // 
            // txtTotalReserva
            // 
            this.txtTotalReserva.Location = new System.Drawing.Point(865, 254);
            this.txtTotalReserva.Name = "txtTotalReserva";
            this.txtTotalReserva.Size = new System.Drawing.Size(105, 22);
            this.txtTotalReserva.TabIndex = 109;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(625, 257);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 19);
            this.label9.TabIndex = 108;
            this.label9.Text = "Total Reserva:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(625, 193);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(159, 19);
            this.label10.TabIndex = 107;
            this.label10.Text = "Estado Reserva:";
            // 
            // txtModificarFechaCreada
            // 
            this.txtModificarFechaCreada.Enabled = false;
            this.txtModificarFechaCreada.Location = new System.Drawing.Point(294, 193);
            this.txtModificarFechaCreada.Name = "txtModificarFechaCreada";
            this.txtModificarFechaCreada.Size = new System.Drawing.Size(105, 22);
            this.txtModificarFechaCreada.TabIndex = 106;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(45, 193);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(139, 19);
            this.label11.TabIndex = 105;
            this.label11.Text = "Fecha Creada:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(625, 130);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(139, 19);
            this.label26.TabIndex = 104;
            this.label26.Text = "Fecha salida:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(45, 131);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(149, 19);
            this.label27.TabIndex = 103;
            this.label27.Text = "Fecha entrada:";
            // 
            // cmBoxModificarIdReserva
            // 
            this.cmBoxModificarIdReserva.FormattingEnabled = true;
            this.cmBoxModificarIdReserva.Location = new System.Drawing.Point(364, 76);
            this.cmBoxModificarIdReserva.Name = "cmBoxModificarIdReserva";
            this.cmBoxModificarIdReserva.Size = new System.Drawing.Size(121, 24);
            this.cmBoxModificarIdReserva.TabIndex = 102;
            // 
            // btnBuscarModificarReserva
            // 
            this.btnBuscarModificarReserva.Location = new System.Drawing.Point(518, 77);
            this.btnBuscarModificarReserva.Name = "btnBuscarModificarReserva";
            this.btnBuscarModificarReserva.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarModificarReserva.TabIndex = 101;
            this.btnBuscarModificarReserva.Text = "Buscar";
            this.btnBuscarModificarReserva.UseVisualStyleBackColor = true;
            this.btnBuscarModificarReserva.Click += new System.EventHandler(this.btnBuscarModificarReserva_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(48, 77);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(159, 19);
            this.label28.TabIndex = 100;
            this.label28.Text = "Buscar Reserva:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(489, 18);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(164, 22);
            this.label30.TabIndex = 98;
            this.label30.Text = "Buscar Reserva";
            // 
            // Reservas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 1055);
            this.Controls.Add(this.gbModificarReserva);
            this.Controls.Add(this.gbBuscarReserva);
            this.Controls.Add(this.gbAgregarReserva);
            this.Controls.Add(this.gbEliminarReserva);
            this.Controls.Add(this.btnAgregarReserva);
            this.Controls.Add(this.btnBuscarReserva);
            this.Controls.Add(this.btnEliminarReserva);
            this.Controls.Add(this.btnModificarReserva);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dataGridViewClientes);
            this.Controls.Add(this.CerrarApp);
            this.Name = "Reservas";
            this.Text = "Reservas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Reservas_Load);
            this.gbBuscarReserva.ResumeLayout(false);
            this.gbBuscarReserva.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbMotelDataSet)).EndInit();
            this.gbModificarReserva.ResumeLayout(false);
            this.gbModificarReserva.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.habitacionesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbMotelHabitacionesDataSet)).EndInit();
            this.gbEliminarReserva.ResumeLayout(false);
            this.gbEliminarReserva.PerformLayout();
            this.gbAgregarReserva.ResumeLayout(false);
            this.gbAgregarReserva.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbMotelServiciosDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviciosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbMotelUsuariosDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usuariosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reservasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbMotelDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbBuscarReserva;
        private System.Windows.Forms.Button btnBuscarReserv;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnSalirBuscarReserva;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.BindingSource clientesBindingSource;
        private dbMotelDataSet dbMotelDataSet;
        private System.Windows.Forms.GroupBox gbModificarReserva;
        private System.Windows.Forms.Button btnModificarUser;
        private System.Windows.Forms.Button btnCancelarModificar;
        private dbMotelUsuariosDataSet1TableAdapters.UsuariosTableAdapter usuariosTableAdapter;
        private System.Windows.Forms.BindingSource habitacionesBindingSource;
        private dbMotelHabitacionesDataSet dbMotelHabitacionesDataSet;
        private System.Windows.Forms.RadioButton btnEliminarNombreUsuario;
        private System.Windows.Forms.TextBox txtEliminarNombreUsuario;
        private System.Windows.Forms.RadioButton btnEliminarIdUsuario;
        private System.Windows.Forms.TextBox txtEliminarIDUsuario;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox gbEliminarReserva;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCancelarEliminar;
        private System.Windows.Forms.GroupBox gbAgregarReserva;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCancelarAgregar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem habitacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reservasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviciosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pagosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.Button btnAgregarReserva;
        private System.Windows.Forms.ToolStripMenuItem menuprincipalToolStripMenuItem;
        private System.Windows.Forms.Button btnBuscarReserva;
        private System.Windows.Forms.Button btnEliminarReserva;
        private dbMotelHabitacionesDataSetTableAdapters.HabitacionesTableAdapter habitacionesTableAdapter;
        private dbMotelServiciosDataSetTableAdapters.ServiciosTableAdapter serviciosTableAdapter;
        private System.Windows.Forms.Button btnModificarReserva;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private dbMotelServiciosDataSet dbMotelServiciosDataSet;
        private System.Windows.Forms.BindingSource serviciosBindingSource;
        private dbMotelUsuariosDataSet1 dbMotelUsuariosDataSet1;
        private System.Windows.Forms.BindingSource usuariosBindingSource;
        private System.Windows.Forms.DataGridView dataGridViewClientes;
        private dbMotelDataSetTableAdapters.ClientesTableAdapter clientesTableAdapter;
        private System.Windows.Forms.Button CerrarApp;
        private dbMotelDataSet1 dbMotelDataSet1;
        private System.Windows.Forms.BindingSource reservasBindingSource;
        private dbMotelDataSet1TableAdapters.ReservasTableAdapter reservasTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn numReservaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numClienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaReservaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaEntradaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaSalidaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn estadoReservaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalReservaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn comentarioReservaDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem reservasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem serviciosPorReservaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem habitacionesPorReservaToolStripMenuItem;
        private System.Windows.Forms.RadioButton btnAgregarNumCliente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmBoxNumCliente;
        private System.Windows.Forms.TextBox txtComentarioReserva;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtBoxTotalReserva;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmBoxTipoHabitacion;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNumHabitaciones;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaSalida;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaEntrada;
        private System.Windows.Forms.ComboBox cmBoxIdsReserva;
        private System.Windows.Forms.TextBox txtBuscarComentarioReserva;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtBuscarTotalReserva;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtFechaCreada;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtEstadoReserva;
        private System.Windows.Forms.DateTimePicker dtPickerFechaSalida;
        private System.Windows.Forms.DateTimePicker dtPickerFechaEntrada;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtBoxBuscarIDCliente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtModificarIdCliente;
        private System.Windows.Forms.TextBox txtModificarEstadoReserva;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtPickerModificarFechaSalida;
        private System.Windows.Forms.DateTimePicker dtPickerModificarFechaEntrada;
        private System.Windows.Forms.TextBox txtModificarComentarioReserva;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTotalReserva;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtModificarFechaCreada;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cmBoxModificarIdReserva;
        private System.Windows.Forms.Button btnBuscarModificarReserva;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
    }
}