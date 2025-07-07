namespace AdminApp
{
    partial class Habitaciones
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idHabitacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numHabitacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoHabitacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioHabitacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estadoHabitacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capacidadHabitacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.habitacionesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dbMotelHabitacionesDataSet = new AdminApp.dbMotelHabitacionesDataSet();
            this.habitacionesTableAdapter = new AdminApp.dbMotelHabitacionesDataSetTableAdapters.HabitacionesTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAgregarHabitacion = new System.Windows.Forms.Button();
            this.btnModificarHabitacion = new System.Windows.Forms.Button();
            this.btnEliminarHabitacion = new System.Windows.Forms.Button();
            this.gbAgregarHabitacion = new System.Windows.Forms.GroupBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtBoxCapacidadHabitacion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBoxPreciohabitacion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxtipohabitacion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxnumhabitacion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbModificarHabitacion = new System.Windows.Forms.GroupBox();
            this.txtModificarEstado = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.buscarNum = new System.Windows.Forms.Button();
            this.txtBoxBuscarIDhabitacion = new System.Windows.Forms.TextBox();
            this.TxtBoxBuscarNumHab = new System.Windows.Forms.TextBox();
            this.btnIdHab = new System.Windows.Forms.RadioButton();
            this.btnNumHab = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnCancelarModificar = new System.Windows.Forms.Button();
            this.txtCapacidadHabitacion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtModificarPrecio = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtModificarTipo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtModificarNum = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnCancelarEliminar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.btneliminarnumhab = new System.Windows.Forms.RadioButton();
            this.btneliminarIDhab = new System.Windows.Forms.RadioButton();
            this.txteliminarhabnum = new System.Windows.Forms.TextBox();
            this.txtEliminarhabID = new System.Windows.Forms.TextBox();
            this.gbEliminarHabitación = new System.Windows.Forms.GroupBox();
            this.btnBuscarHabitacion = new System.Windows.Forms.Button();
            this.gbBuscarHabitacion = new System.Windows.Forms.GroupBox();
            this.txtBuscarEstadoHabitacion = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnBuscarHab = new System.Windows.Forms.Button();
            this.txtBuscarIDHab = new System.Windows.Forms.TextBox();
            this.txtBuscarNumHab = new System.Windows.Forms.TextBox();
            this.btnBuscarIDHab = new System.Windows.Forms.RadioButton();
            this.btnBucarNumHab = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.txtBuscarCapacidadHabitacion = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtBuscarPrecioHabitacion = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtBuscarTipoHabitacion = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtBuscarNumeroHabitacion = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.habitacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reservasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pagosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CerrarApp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.habitacionesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbMotelHabitacionesDataSet)).BeginInit();
            this.gbAgregarHabitacion.SuspendLayout();
            this.gbModificarHabitacion.SuspendLayout();
            this.gbEliminarHabitación.SuspendLayout();
            this.gbBuscarHabitacion.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idHabitacionDataGridViewTextBoxColumn,
            this.numHabitacionDataGridViewTextBoxColumn,
            this.tipoHabitacionDataGridViewTextBoxColumn,
            this.precioHabitacionDataGridViewTextBoxColumn,
            this.estadoHabitacionDataGridViewTextBoxColumn,
            this.capacidadHabitacionDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.habitacionesBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(28, 108);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1036, 142);
            this.dataGridView1.TabIndex = 0;
            // 
            // idHabitacionDataGridViewTextBoxColumn
            // 
            this.idHabitacionDataGridViewTextBoxColumn.DataPropertyName = "IdHabitacion";
            this.idHabitacionDataGridViewTextBoxColumn.HeaderText = "IdHabitacion";
            this.idHabitacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idHabitacionDataGridViewTextBoxColumn.Name = "idHabitacionDataGridViewTextBoxColumn";
            this.idHabitacionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // numHabitacionDataGridViewTextBoxColumn
            // 
            this.numHabitacionDataGridViewTextBoxColumn.DataPropertyName = "NumHabitacion";
            this.numHabitacionDataGridViewTextBoxColumn.HeaderText = "NumHabitacion";
            this.numHabitacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.numHabitacionDataGridViewTextBoxColumn.Name = "numHabitacionDataGridViewTextBoxColumn";
            this.numHabitacionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tipoHabitacionDataGridViewTextBoxColumn
            // 
            this.tipoHabitacionDataGridViewTextBoxColumn.DataPropertyName = "TipoHabitacion";
            this.tipoHabitacionDataGridViewTextBoxColumn.HeaderText = "TipoHabitacion";
            this.tipoHabitacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.tipoHabitacionDataGridViewTextBoxColumn.Name = "tipoHabitacionDataGridViewTextBoxColumn";
            this.tipoHabitacionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // precioHabitacionDataGridViewTextBoxColumn
            // 
            this.precioHabitacionDataGridViewTextBoxColumn.DataPropertyName = "PrecioHabitacion";
            this.precioHabitacionDataGridViewTextBoxColumn.HeaderText = "PrecioHabitacion";
            this.precioHabitacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.precioHabitacionDataGridViewTextBoxColumn.Name = "precioHabitacionDataGridViewTextBoxColumn";
            this.precioHabitacionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // estadoHabitacionDataGridViewTextBoxColumn
            // 
            this.estadoHabitacionDataGridViewTextBoxColumn.DataPropertyName = "EstadoHabitacion";
            this.estadoHabitacionDataGridViewTextBoxColumn.HeaderText = "EstadoHabitacion";
            this.estadoHabitacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.estadoHabitacionDataGridViewTextBoxColumn.Name = "estadoHabitacionDataGridViewTextBoxColumn";
            this.estadoHabitacionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // capacidadHabitacionDataGridViewTextBoxColumn
            // 
            this.capacidadHabitacionDataGridViewTextBoxColumn.DataPropertyName = "CapacidadHabitacion";
            this.capacidadHabitacionDataGridViewTextBoxColumn.HeaderText = "CapacidadHabitacion";
            this.capacidadHabitacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.capacidadHabitacionDataGridViewTextBoxColumn.Name = "capacidadHabitacionDataGridViewTextBoxColumn";
            this.capacidadHabitacionDataGridViewTextBoxColumn.ReadOnly = true;
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
            // habitacionesTableAdapter
            // 
            this.habitacionesTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans Typewriter", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(492, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Habitaciones";
            // 
            // btnAgregarHabitacion
            // 
            this.btnAgregarHabitacion.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarHabitacion.Location = new System.Drawing.Point(28, 313);
            this.btnAgregarHabitacion.Name = "btnAgregarHabitacion";
            this.btnAgregarHabitacion.Size = new System.Drawing.Size(224, 43);
            this.btnAgregarHabitacion.TabIndex = 2;
            this.btnAgregarHabitacion.Text = "Agregar Habitación";
            this.btnAgregarHabitacion.UseVisualStyleBackColor = true;
            this.btnAgregarHabitacion.Click += new System.EventHandler(this.btnAgregarHabitacion_Click);
            // 
            // btnModificarHabitacion
            // 
            this.btnModificarHabitacion.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarHabitacion.Location = new System.Drawing.Point(577, 313);
            this.btnModificarHabitacion.Name = "btnModificarHabitacion";
            this.btnModificarHabitacion.Size = new System.Drawing.Size(224, 43);
            this.btnModificarHabitacion.TabIndex = 3;
            this.btnModificarHabitacion.Text = "Modificar Habitación";
            this.btnModificarHabitacion.UseVisualStyleBackColor = true;
            this.btnModificarHabitacion.Click += new System.EventHandler(this.btnModificarHabitacion_Click);
            // 
            // btnEliminarHabitacion
            // 
            this.btnEliminarHabitacion.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarHabitacion.Location = new System.Drawing.Point(840, 313);
            this.btnEliminarHabitacion.Name = "btnEliminarHabitacion";
            this.btnEliminarHabitacion.Size = new System.Drawing.Size(224, 43);
            this.btnEliminarHabitacion.TabIndex = 4;
            this.btnEliminarHabitacion.Text = "Eliminar Habitación";
            this.btnEliminarHabitacion.UseVisualStyleBackColor = true;
            this.btnEliminarHabitacion.Click += new System.EventHandler(this.btnEliminarHabitacion_Click);
            // 
            // gbAgregarHabitacion
            // 
            this.gbAgregarHabitacion.Controls.Add(this.btnAgregar);
            this.gbAgregarHabitacion.Controls.Add(this.btnCancelar);
            this.gbAgregarHabitacion.Controls.Add(this.txtBoxCapacidadHabitacion);
            this.gbAgregarHabitacion.Controls.Add(this.label6);
            this.gbAgregarHabitacion.Controls.Add(this.txtBoxPreciohabitacion);
            this.gbAgregarHabitacion.Controls.Add(this.label5);
            this.gbAgregarHabitacion.Controls.Add(this.txtBoxtipohabitacion);
            this.gbAgregarHabitacion.Controls.Add(this.label4);
            this.gbAgregarHabitacion.Controls.Add(this.txtBoxnumhabitacion);
            this.gbAgregarHabitacion.Controls.Add(this.label3);
            this.gbAgregarHabitacion.Controls.Add(this.label2);
            this.gbAgregarHabitacion.Location = new System.Drawing.Point(28, 83);
            this.gbAgregarHabitacion.Name = "gbAgregarHabitacion";
            this.gbAgregarHabitacion.Size = new System.Drawing.Size(1126, 423);
            this.gbAgregarHabitacion.TabIndex = 6;
            this.gbAgregarHabitacion.TabStop = false;
            this.gbAgregarHabitacion.Text = "Agregar Habitación";
            this.gbAgregarHabitacion.Visible = false;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(488, 269);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(145, 35);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click_1);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(248, 269);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(145, 35);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // txtBoxCapacidadHabitacion
            // 
            this.txtBoxCapacidadHabitacion.Location = new System.Drawing.Point(787, 160);
            this.txtBoxCapacidadHabitacion.Name = "txtBoxCapacidadHabitacion";
            this.txtBoxCapacidadHabitacion.Size = new System.Drawing.Size(105, 22);
            this.txtBoxCapacidadHabitacion.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(515, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(249, 19);
            this.label6.TabIndex = 7;
            this.label6.Text = "Capacidad de habitación:";
            // 
            // txtBoxPreciohabitacion
            // 
            this.txtBoxPreciohabitacion.Location = new System.Drawing.Point(288, 163);
            this.txtBoxPreciohabitacion.Name = "txtBoxPreciohabitacion";
            this.txtBoxPreciohabitacion.Size = new System.Drawing.Size(105, 22);
            this.txtBoxPreciohabitacion.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(219, 19);
            this.label5.TabIndex = 5;
            this.label5.Text = "Precio de habitación:";
            // 
            // txtBoxtipohabitacion
            // 
            this.txtBoxtipohabitacion.Location = new System.Drawing.Point(787, 97);
            this.txtBoxtipohabitacion.Name = "txtBoxtipohabitacion";
            this.txtBoxtipohabitacion.Size = new System.Drawing.Size(105, 22);
            this.txtBoxtipohabitacion.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(515, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tipo de habitación:";
            // 
            // txtBoxnumhabitacion
            // 
            this.txtBoxnumhabitacion.Location = new System.Drawing.Point(288, 84);
            this.txtBoxnumhabitacion.Name = "txtBoxnumhabitacion";
            this.txtBoxnumhabitacion.Size = new System.Drawing.Size(105, 22);
            this.txtBoxnumhabitacion.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(219, 19);
            this.label3.TabIndex = 1;
            this.label3.Text = "Número de habitación:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(348, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "Agregar Habitación";
            // 
            // gbModificarHabitacion
            // 
            this.gbModificarHabitacion.Controls.Add(this.txtModificarEstado);
            this.gbModificarHabitacion.Controls.Add(this.label13);
            this.gbModificarHabitacion.Controls.Add(this.buscarNum);
            this.gbModificarHabitacion.Controls.Add(this.txtBoxBuscarIDhabitacion);
            this.gbModificarHabitacion.Controls.Add(this.TxtBoxBuscarNumHab);
            this.gbModificarHabitacion.Controls.Add(this.btnIdHab);
            this.gbModificarHabitacion.Controls.Add(this.btnNumHab);
            this.gbModificarHabitacion.Controls.Add(this.label12);
            this.gbModificarHabitacion.Controls.Add(this.btnModificar);
            this.gbModificarHabitacion.Controls.Add(this.btnCancelarModificar);
            this.gbModificarHabitacion.Controls.Add(this.txtCapacidadHabitacion);
            this.gbModificarHabitacion.Controls.Add(this.label7);
            this.gbModificarHabitacion.Controls.Add(this.txtModificarPrecio);
            this.gbModificarHabitacion.Controls.Add(this.label8);
            this.gbModificarHabitacion.Controls.Add(this.txtModificarTipo);
            this.gbModificarHabitacion.Controls.Add(this.label9);
            this.gbModificarHabitacion.Controls.Add(this.txtModificarNum);
            this.gbModificarHabitacion.Controls.Add(this.label10);
            this.gbModificarHabitacion.Controls.Add(this.label11);
            this.gbModificarHabitacion.Location = new System.Drawing.Point(28, 77);
            this.gbModificarHabitacion.Name = "gbModificarHabitacion";
            this.gbModificarHabitacion.Size = new System.Drawing.Size(1126, 423);
            this.gbModificarHabitacion.TabIndex = 11;
            this.gbModificarHabitacion.TabStop = false;
            this.gbModificarHabitacion.Text = "Modificar Habitacion";
            this.gbModificarHabitacion.Visible = false;
            // 
            // txtModificarEstado
            // 
            this.txtModificarEstado.Location = new System.Drawing.Point(299, 305);
            this.txtModificarEstado.Name = "txtModificarEstado";
            this.txtModificarEstado.Size = new System.Drawing.Size(105, 22);
            this.txtModificarEstado.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(27, 308);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(219, 19);
            this.label13.TabIndex = 17;
            this.label13.Text = "Estado de habitación:";
            // 
            // buscarNum
            // 
            this.buscarNum.Location = new System.Drawing.Point(941, 92);
            this.buscarNum.Name = "buscarNum";
            this.buscarNum.Size = new System.Drawing.Size(75, 23);
            this.buscarNum.TabIndex = 16;
            this.buscarNum.Text = "Buscar";
            this.buscarNum.UseVisualStyleBackColor = true;
            this.buscarNum.Click += new System.EventHandler(this.buscarNum_Click);
            // 
            // txtBoxBuscarIDhabitacion
            // 
            this.txtBoxBuscarIDhabitacion.Location = new System.Drawing.Point(781, 93);
            this.txtBoxBuscarIDhabitacion.Name = "txtBoxBuscarIDhabitacion";
            this.txtBoxBuscarIDhabitacion.Size = new System.Drawing.Size(105, 22);
            this.txtBoxBuscarIDhabitacion.TabIndex = 15;
            // 
            // TxtBoxBuscarNumHab
            // 
            this.TxtBoxBuscarNumHab.Location = new System.Drawing.Point(518, 95);
            this.TxtBoxBuscarNumHab.Name = "TxtBoxBuscarNumHab";
            this.TxtBoxBuscarNumHab.Size = new System.Drawing.Size(105, 22);
            this.TxtBoxBuscarNumHab.TabIndex = 14;
            // 
            // btnIdHab
            // 
            this.btnIdHab.AutoSize = true;
            this.btnIdHab.Location = new System.Drawing.Point(650, 95);
            this.btnIdHab.Name = "btnIdHab";
            this.btnIdHab.Size = new System.Drawing.Size(125, 20);
            this.btnIdHab.TabIndex = 13;
            this.btnIdHab.TabStop = true;
            this.btnIdHab.Text = "ID de habitación";
            this.btnIdHab.UseVisualStyleBackColor = true;
            this.btnIdHab.CheckedChanged += new System.EventHandler(this.btnIdHab_CheckedChanged);
            // 
            // btnNumHab
            // 
            this.btnNumHab.AutoSize = true;
            this.btnNumHab.Location = new System.Drawing.Point(352, 95);
            this.btnNumHab.Name = "btnNumHab";
            this.btnNumHab.Size = new System.Drawing.Size(160, 20);
            this.btnNumHab.TabIndex = 12;
            this.btnNumHab.TabStop = true;
            this.btnNumHab.Text = "Número de habitación";
            this.btnNumHab.UseVisualStyleBackColor = true;
            this.btnNumHab.CheckedChanged += new System.EventHandler(this.btnNumHab_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(27, 97);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(309, 19);
            this.label12.TabIndex = 11;
            this.label12.Text = "Buscar habitación a modificar:";
            // 
            // btnModificar
            // 
            this.btnModificar.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.Location = new System.Drawing.Point(499, 353);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(145, 35);
            this.btnModificar.TabIndex = 10;
            this.btnModificar.Text = "Actualizar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnCancelarModificar
            // 
            this.btnCancelarModificar.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarModificar.Location = new System.Drawing.Point(259, 353);
            this.btnCancelarModificar.Name = "btnCancelarModificar";
            this.btnCancelarModificar.Size = new System.Drawing.Size(145, 35);
            this.btnCancelarModificar.TabIndex = 9;
            this.btnCancelarModificar.Text = "Cancelar";
            this.btnCancelarModificar.UseVisualStyleBackColor = true;
            this.btnCancelarModificar.Click += new System.EventHandler(this.btnCancelarModificar_Click);
            // 
            // txtCapacidadHabitacion
            // 
            this.txtCapacidadHabitacion.Location = new System.Drawing.Point(798, 244);
            this.txtCapacidadHabitacion.Name = "txtCapacidadHabitacion";
            this.txtCapacidadHabitacion.Size = new System.Drawing.Size(105, 22);
            this.txtCapacidadHabitacion.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(526, 247);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(249, 19);
            this.label7.TabIndex = 7;
            this.label7.Text = "Capacidad de habitación:";
            // 
            // txtModificarPrecio
            // 
            this.txtModificarPrecio.Location = new System.Drawing.Point(299, 247);
            this.txtModificarPrecio.Name = "txtModificarPrecio";
            this.txtModificarPrecio.Size = new System.Drawing.Size(105, 22);
            this.txtModificarPrecio.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(27, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(219, 19);
            this.label8.TabIndex = 5;
            this.label8.Text = "Precio de habitación:";
            // 
            // txtModificarTipo
            // 
            this.txtModificarTipo.Location = new System.Drawing.Point(798, 181);
            this.txtModificarTipo.Name = "txtModificarTipo";
            this.txtModificarTipo.Size = new System.Drawing.Size(105, 22);
            this.txtModificarTipo.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(526, 184);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(199, 19);
            this.label9.TabIndex = 3;
            this.label9.Text = "Tipo de habitación:";
            // 
            // txtModificarNum
            // 
            this.txtModificarNum.Location = new System.Drawing.Point(299, 184);
            this.txtModificarNum.Name = "txtModificarNum";
            this.txtModificarNum.Size = new System.Drawing.Size(105, 22);
            this.txtModificarNum.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(27, 187);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(219, 19);
            this.label10.TabIndex = 1;
            this.label10.Text = "Número de habitación:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(348, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(230, 22);
            this.label11.TabIndex = 0;
            this.label11.Text = "Modificar Habitación";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(348, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(219, 22);
            this.label20.TabIndex = 0;
            this.label20.Text = "Eliminar Habitación";
            // 
            // btnCancelarEliminar
            // 
            this.btnCancelarEliminar.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarEliminar.Location = new System.Drawing.Point(307, 164);
            this.btnCancelarEliminar.Name = "btnCancelarEliminar";
            this.btnCancelarEliminar.Size = new System.Drawing.Size(145, 35);
            this.btnCancelarEliminar.TabIndex = 9;
            this.btnCancelarEliminar.Text = "Cancelar";
            this.btnCancelarEliminar.UseVisualStyleBackColor = true;
            this.btnCancelarEliminar.Click += new System.EventHandler(this.btnCancelarEliminar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(547, 164);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(145, 35);
            this.btnEliminar.TabIndex = 10;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(27, 97);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(299, 19);
            this.label15.TabIndex = 11;
            this.label15.Text = "Buscar habitación a Eliminar:";
            // 
            // btneliminarnumhab
            // 
            this.btneliminarnumhab.AutoSize = true;
            this.btneliminarnumhab.Location = new System.Drawing.Point(352, 95);
            this.btneliminarnumhab.Name = "btneliminarnumhab";
            this.btneliminarnumhab.Size = new System.Drawing.Size(160, 20);
            this.btneliminarnumhab.TabIndex = 12;
            this.btneliminarnumhab.TabStop = true;
            this.btneliminarnumhab.Text = "Número de habitación";
            this.btneliminarnumhab.UseVisualStyleBackColor = true;
            this.btneliminarnumhab.CheckedChanged += new System.EventHandler(this.btneliminarnumhab_CheckedChanged);
            // 
            // btneliminarIDhab
            // 
            this.btneliminarIDhab.AutoSize = true;
            this.btneliminarIDhab.Location = new System.Drawing.Point(650, 95);
            this.btneliminarIDhab.Name = "btneliminarIDhab";
            this.btneliminarIDhab.Size = new System.Drawing.Size(125, 20);
            this.btneliminarIDhab.TabIndex = 13;
            this.btneliminarIDhab.TabStop = true;
            this.btneliminarIDhab.Text = "ID de habitación";
            this.btneliminarIDhab.UseVisualStyleBackColor = true;
            this.btneliminarIDhab.CheckedChanged += new System.EventHandler(this.btneliminarIDhab_CheckedChanged);
            // 
            // txteliminarhabnum
            // 
            this.txteliminarhabnum.Location = new System.Drawing.Point(518, 95);
            this.txteliminarhabnum.Name = "txteliminarhabnum";
            this.txteliminarhabnum.Size = new System.Drawing.Size(105, 22);
            this.txteliminarhabnum.TabIndex = 14;
            // 
            // txtEliminarhabID
            // 
            this.txtEliminarhabID.Location = new System.Drawing.Point(781, 93);
            this.txtEliminarhabID.Name = "txtEliminarhabID";
            this.txtEliminarhabID.Size = new System.Drawing.Size(105, 22);
            this.txtEliminarhabID.TabIndex = 15;
            // 
            // gbEliminarHabitación
            // 
            this.gbEliminarHabitación.Controls.Add(this.txtEliminarhabID);
            this.gbEliminarHabitación.Controls.Add(this.txteliminarhabnum);
            this.gbEliminarHabitación.Controls.Add(this.btneliminarIDhab);
            this.gbEliminarHabitación.Controls.Add(this.btneliminarnumhab);
            this.gbEliminarHabitación.Controls.Add(this.label15);
            this.gbEliminarHabitación.Controls.Add(this.btnEliminar);
            this.gbEliminarHabitación.Controls.Add(this.btnCancelarEliminar);
            this.gbEliminarHabitación.Controls.Add(this.label20);
            this.gbEliminarHabitación.Location = new System.Drawing.Point(28, 46);
            this.gbEliminarHabitación.Name = "gbEliminarHabitación";
            this.gbEliminarHabitación.Size = new System.Drawing.Size(1126, 423);
            this.gbEliminarHabitación.TabIndex = 19;
            this.gbEliminarHabitación.TabStop = false;
            this.gbEliminarHabitación.Text = "Eliminar Habitación";
            this.gbEliminarHabitación.Visible = false;
            // 
            // btnBuscarHabitacion
            // 
            this.btnBuscarHabitacion.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarHabitacion.Location = new System.Drawing.Point(300, 313);
            this.btnBuscarHabitacion.Name = "btnBuscarHabitacion";
            this.btnBuscarHabitacion.Size = new System.Drawing.Size(224, 43);
            this.btnBuscarHabitacion.TabIndex = 20;
            this.btnBuscarHabitacion.Text = "Buscar Habitación";
            this.btnBuscarHabitacion.UseVisualStyleBackColor = true;
            this.btnBuscarHabitacion.Click += new System.EventHandler(this.btnBuscarHabitacion_Click);
            // 
            // gbBuscarHabitacion
            // 
            this.gbBuscarHabitacion.Controls.Add(this.txtBuscarEstadoHabitacion);
            this.gbBuscarHabitacion.Controls.Add(this.label14);
            this.gbBuscarHabitacion.Controls.Add(this.btnBuscarHab);
            this.gbBuscarHabitacion.Controls.Add(this.txtBuscarIDHab);
            this.gbBuscarHabitacion.Controls.Add(this.txtBuscarNumHab);
            this.gbBuscarHabitacion.Controls.Add(this.btnBuscarIDHab);
            this.gbBuscarHabitacion.Controls.Add(this.btnBucarNumHab);
            this.gbBuscarHabitacion.Controls.Add(this.label16);
            this.gbBuscarHabitacion.Controls.Add(this.btnSalir);
            this.gbBuscarHabitacion.Controls.Add(this.txtBuscarCapacidadHabitacion);
            this.gbBuscarHabitacion.Controls.Add(this.label17);
            this.gbBuscarHabitacion.Controls.Add(this.txtBuscarPrecioHabitacion);
            this.gbBuscarHabitacion.Controls.Add(this.label18);
            this.gbBuscarHabitacion.Controls.Add(this.txtBuscarTipoHabitacion);
            this.gbBuscarHabitacion.Controls.Add(this.label19);
            this.gbBuscarHabitacion.Controls.Add(this.txtBuscarNumeroHabitacion);
            this.gbBuscarHabitacion.Controls.Add(this.label21);
            this.gbBuscarHabitacion.Controls.Add(this.label22);
            this.gbBuscarHabitacion.Location = new System.Drawing.Point(28, 71);
            this.gbBuscarHabitacion.Name = "gbBuscarHabitacion";
            this.gbBuscarHabitacion.Size = new System.Drawing.Size(1126, 423);
            this.gbBuscarHabitacion.TabIndex = 19;
            this.gbBuscarHabitacion.TabStop = false;
            this.gbBuscarHabitacion.Text = "Buscar Habitacion";
            this.gbBuscarHabitacion.Visible = false;
            // 
            // txtBuscarEstadoHabitacion
            // 
            this.txtBuscarEstadoHabitacion.Location = new System.Drawing.Point(299, 305);
            this.txtBuscarEstadoHabitacion.Name = "txtBuscarEstadoHabitacion";
            this.txtBuscarEstadoHabitacion.Size = new System.Drawing.Size(105, 22);
            this.txtBuscarEstadoHabitacion.TabIndex = 18;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(27, 308);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(219, 19);
            this.label14.TabIndex = 17;
            this.label14.Text = "Estado de habitación:";
            // 
            // btnBuscarHab
            // 
            this.btnBuscarHab.Location = new System.Drawing.Point(848, 95);
            this.btnBuscarHab.Name = "btnBuscarHab";
            this.btnBuscarHab.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarHab.TabIndex = 16;
            this.btnBuscarHab.Text = "Buscar";
            this.btnBuscarHab.UseVisualStyleBackColor = true;
            this.btnBuscarHab.Click += new System.EventHandler(this.btnBuscarHab_Click);
            // 
            // txtBuscarIDHab
            // 
            this.txtBuscarIDHab.Location = new System.Drawing.Point(688, 96);
            this.txtBuscarIDHab.Name = "txtBuscarIDHab";
            this.txtBuscarIDHab.Size = new System.Drawing.Size(105, 22);
            this.txtBuscarIDHab.TabIndex = 15;
            // 
            // txtBuscarNumHab
            // 
            this.txtBuscarNumHab.Location = new System.Drawing.Point(425, 98);
            this.txtBuscarNumHab.Name = "txtBuscarNumHab";
            this.txtBuscarNumHab.Size = new System.Drawing.Size(105, 22);
            this.txtBuscarNumHab.TabIndex = 14;
            // 
            // btnBuscarIDHab
            // 
            this.btnBuscarIDHab.AutoSize = true;
            this.btnBuscarIDHab.Location = new System.Drawing.Point(557, 98);
            this.btnBuscarIDHab.Name = "btnBuscarIDHab";
            this.btnBuscarIDHab.Size = new System.Drawing.Size(125, 20);
            this.btnBuscarIDHab.TabIndex = 13;
            this.btnBuscarIDHab.TabStop = true;
            this.btnBuscarIDHab.Text = "ID de habitación";
            this.btnBuscarIDHab.UseVisualStyleBackColor = true;
            this.btnBuscarIDHab.CheckedChanged += new System.EventHandler(this.btnBuscarIDHab_CheckedChanged);
            // 
            // btnBucarNumHab
            // 
            this.btnBucarNumHab.AutoSize = true;
            this.btnBucarNumHab.Location = new System.Drawing.Point(259, 98);
            this.btnBucarNumHab.Name = "btnBucarNumHab";
            this.btnBucarNumHab.Size = new System.Drawing.Size(160, 20);
            this.btnBucarNumHab.TabIndex = 12;
            this.btnBucarNumHab.TabStop = true;
            this.btnBucarNumHab.Text = "Número de habitación";
            this.btnBucarNumHab.UseVisualStyleBackColor = true;
            this.btnBucarNumHab.CheckedChanged += new System.EventHandler(this.btnBucarNumHab_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(27, 97);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(189, 19);
            this.label16.TabIndex = 11;
            this.label16.Text = "Buscar habitación:";
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(772, 342);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(145, 35);
            this.btnSalir.TabIndex = 9;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // txtBuscarCapacidadHabitacion
            // 
            this.txtBuscarCapacidadHabitacion.Location = new System.Drawing.Point(798, 244);
            this.txtBuscarCapacidadHabitacion.Name = "txtBuscarCapacidadHabitacion";
            this.txtBuscarCapacidadHabitacion.Size = new System.Drawing.Size(105, 22);
            this.txtBuscarCapacidadHabitacion.TabIndex = 8;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(526, 247);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(249, 19);
            this.label17.TabIndex = 7;
            this.label17.Text = "Capacidad de habitación:";
            // 
            // txtBuscarPrecioHabitacion
            // 
            this.txtBuscarPrecioHabitacion.Location = new System.Drawing.Point(299, 247);
            this.txtBuscarPrecioHabitacion.Name = "txtBuscarPrecioHabitacion";
            this.txtBuscarPrecioHabitacion.Size = new System.Drawing.Size(105, 22);
            this.txtBuscarPrecioHabitacion.TabIndex = 6;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(27, 250);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(219, 19);
            this.label18.TabIndex = 5;
            this.label18.Text = "Precio de habitación:";
            // 
            // txtBuscarTipoHabitacion
            // 
            this.txtBuscarTipoHabitacion.Location = new System.Drawing.Point(798, 181);
            this.txtBuscarTipoHabitacion.Name = "txtBuscarTipoHabitacion";
            this.txtBuscarTipoHabitacion.Size = new System.Drawing.Size(105, 22);
            this.txtBuscarTipoHabitacion.TabIndex = 4;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(526, 184);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(199, 19);
            this.label19.TabIndex = 3;
            this.label19.Text = "Tipo de habitación:";
            // 
            // txtBuscarNumeroHabitacion
            // 
            this.txtBuscarNumeroHabitacion.Location = new System.Drawing.Point(299, 184);
            this.txtBuscarNumeroHabitacion.Name = "txtBuscarNumeroHabitacion";
            this.txtBuscarNumeroHabitacion.Size = new System.Drawing.Size(105, 22);
            this.txtBuscarNumeroHabitacion.TabIndex = 2;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(27, 187);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(219, 19);
            this.label21.TabIndex = 1;
            this.label21.Text = "Número de habitación:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(348, 25);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(197, 22);
            this.label22.TabIndex = 0;
            this.label22.Text = "Buscar Habitación";
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
            this.pagosToolStripMenuItem,
            this.reportesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1196, 28);
            this.menuStrip1.TabIndex = 21;
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
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.clientesToolStripMenuItem.Text = "Clientes";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click);
            // 
            // reservasToolStripMenuItem
            // 
            this.reservasToolStripMenuItem.Name = "reservasToolStripMenuItem";
            this.reservasToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.reservasToolStripMenuItem.Text = "Reservas";
            this.reservasToolStripMenuItem.Click += new System.EventHandler(this.reservasToolStripMenuItem_Click);
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
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.reportesToolStripMenuItem.Text = "Reportes";
            this.reportesToolStripMenuItem.Click += new System.EventHandler(this.reportesToolStripMenuItem_Click);
            // 
            // CerrarApp
            // 
            this.CerrarApp.Location = new System.Drawing.Point(1077, 5);
            this.CerrarApp.Name = "CerrarApp";
            this.CerrarApp.Size = new System.Drawing.Size(58, 22);
            this.CerrarApp.TabIndex = 22;
            this.CerrarApp.Text = "X";
            this.CerrarApp.UseVisualStyleBackColor = true;
            this.CerrarApp.Click += new System.EventHandler(this.CerrarApp_Click);
            // 
            // Habitaciones
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1196, 659);
            this.Controls.Add(this.CerrarApp);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gbBuscarHabitacion);
            this.Controls.Add(this.gbModificarHabitacion);
            this.Controls.Add(this.gbEliminarHabitación);
            this.Controls.Add(this.gbAgregarHabitacion);
            this.Controls.Add(this.btnEliminarHabitacion);
            this.Controls.Add(this.btnModificarHabitacion);
            this.Controls.Add(this.btnAgregarHabitacion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnBuscarHabitacion);
            this.Name = "Habitaciones";
            this.Text = "Habitaciones";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Habitaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.habitacionesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbMotelHabitacionesDataSet)).EndInit();
            this.gbAgregarHabitacion.ResumeLayout(false);
            this.gbAgregarHabitacion.PerformLayout();
            this.gbModificarHabitacion.ResumeLayout(false);
            this.gbModificarHabitacion.PerformLayout();
            this.gbEliminarHabitación.ResumeLayout(false);
            this.gbEliminarHabitación.PerformLayout();
            this.gbBuscarHabitacion.ResumeLayout(false);
            this.gbBuscarHabitacion.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private dbMotelHabitacionesDataSet dbMotelHabitacionesDataSet;
        private System.Windows.Forms.BindingSource habitacionesBindingSource;
        private dbMotelHabitacionesDataSetTableAdapters.HabitacionesTableAdapter habitacionesTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idHabitacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numHabitacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoHabitacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioHabitacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn estadoHabitacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn capacidadHabitacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAgregarHabitacion;
        private System.Windows.Forms.Button btnModificarHabitacion;
        private System.Windows.Forms.Button btnEliminarHabitacion;
        private System.Windows.Forms.GroupBox gbAgregarHabitacion;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtBoxCapacidadHabitacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBoxPreciohabitacion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxtipohabitacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxnumhabitacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbModificarHabitacion;
        private System.Windows.Forms.RadioButton btnNumHab;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnCancelarModificar;
        private System.Windows.Forms.TextBox txtCapacidadHabitacion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtModificarPrecio;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtModificarTipo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtModificarNum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBoxBuscarIDhabitacion;
        private System.Windows.Forms.TextBox TxtBoxBuscarNumHab;
        private System.Windows.Forms.RadioButton btnIdHab;
        private System.Windows.Forms.Button buscarNum;
        private System.Windows.Forms.TextBox txtModificarEstado;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox gbEliminarHabitación;
        private System.Windows.Forms.TextBox txtEliminarhabID;
        private System.Windows.Forms.TextBox txteliminarhabnum;
        private System.Windows.Forms.RadioButton btneliminarIDhab;
        private System.Windows.Forms.RadioButton btneliminarnumhab;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCancelarEliminar;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnBuscarHabitacion;
        private System.Windows.Forms.GroupBox gbBuscarHabitacion;
        private System.Windows.Forms.TextBox txtBuscarEstadoHabitacion;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnBuscarHab;
        private System.Windows.Forms.TextBox txtBuscarIDHab;
        private System.Windows.Forms.TextBox txtBuscarNumHab;
        private System.Windows.Forms.RadioButton btnBuscarIDHab;
        private System.Windows.Forms.RadioButton btnBucarNumHab;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.TextBox txtBuscarCapacidadHabitacion;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtBuscarPrecioHabitacion;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtBuscarTipoHabitacion;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtBuscarNumeroHabitacion;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem habitacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reservasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviciosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pagosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.Button CerrarApp;
    }
}