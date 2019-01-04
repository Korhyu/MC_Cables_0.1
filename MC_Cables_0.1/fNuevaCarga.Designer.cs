namespace MC_Cables_0._1
{
    partial class fNuevaCarga
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
            this.lbProyecto = new System.Windows.Forms.Label();
            this.lbUsuario = new System.Windows.Forms.Label();
            this.cbEquipos = new System.Windows.Forms.ComboBox();
            this.lbPotencia = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbTension = new System.Windows.Forms.Label();
            this.lbRev = new System.Windows.Forms.Label();
            this.lbCorriente = new System.Windows.Forms.Label();
            this.lbIArranque = new System.Windows.Forms.Label();
            this.lbCosfi = new System.Windows.Forms.Label();
            this.lbCosFiArr = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btSalir = new System.Windows.Forms.Button();
            this.btAgregar = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbProyecto
            // 
            this.lbProyecto.AutoSize = true;
            this.lbProyecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProyecto.ForeColor = System.Drawing.Color.White;
            this.lbProyecto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbProyecto.Location = new System.Drawing.Point(311, 9);
            this.lbProyecto.Name = "lbProyecto";
            this.lbProyecto.Size = new System.Drawing.Size(74, 16);
            this.lbProyecto.TabIndex = 3;
            this.lbProyecto.Text = "Proyecto:";
            this.lbProyecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbUsuario
            // 
            this.lbUsuario.AutoSize = true;
            this.lbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsuario.ForeColor = System.Drawing.Color.White;
            this.lbUsuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbUsuario.Location = new System.Drawing.Point(12, 9);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(66, 16);
            this.lbUsuario.TabIndex = 4;
            this.lbUsuario.Text = "Usuario:";
            this.lbUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbEquipos
            // 
            this.cbEquipos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEquipos.FormattingEnabled = true;
            this.cbEquipos.Location = new System.Drawing.Point(61, 50);
            this.cbEquipos.Name = "cbEquipos";
            this.cbEquipos.Size = new System.Drawing.Size(288, 21);
            this.cbEquipos.TabIndex = 5;
            this.cbEquipos.SelectedIndexChanged += new System.EventHandler(this.cbEquipos_SelectedIndexChanged);
            // 
            // lbPotencia
            // 
            this.lbPotencia.AutoSize = true;
            this.lbPotencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPotencia.ForeColor = System.Drawing.Color.White;
            this.lbPotencia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbPotencia.Location = new System.Drawing.Point(3, 24);
            this.lbPotencia.Name = "lbPotencia";
            this.lbPotencia.Size = new System.Drawing.Size(124, 16);
            this.lbPotencia.TabIndex = 6;
            this.lbPotencia.Text = "Potencia Activa: ";
            this.lbPotencia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "TAG:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTension
            // 
            this.lbTension.AutoSize = true;
            this.lbTension.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTension.ForeColor = System.Drawing.Color.White;
            this.lbTension.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbTension.Location = new System.Drawing.Point(309, 24);
            this.lbTension.Name = "lbTension";
            this.lbTension.Size = new System.Drawing.Size(68, 16);
            this.lbTension.TabIndex = 8;
            this.lbTension.Text = "Tension:";
            this.lbTension.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRev
            // 
            this.lbRev.AutoSize = true;
            this.lbRev.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRev.ForeColor = System.Drawing.Color.White;
            this.lbRev.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbRev.Location = new System.Drawing.Point(3, 0);
            this.lbRev.Name = "lbRev";
            this.lbRev.Size = new System.Drawing.Size(77, 16);
            this.lbRev.TabIndex = 9;
            this.lbRev.Text = "Revision: ";
            this.lbRev.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCorriente
            // 
            this.lbCorriente.AutoSize = true;
            this.lbCorriente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCorriente.ForeColor = System.Drawing.Color.White;
            this.lbCorriente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbCorriente.Location = new System.Drawing.Point(309, 48);
            this.lbCorriente.Name = "lbCorriente";
            this.lbCorriente.Size = new System.Drawing.Size(75, 16);
            this.lbCorriente.TabIndex = 10;
            this.lbCorriente.Text = "Corriente:";
            this.lbCorriente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbIArranque
            // 
            this.lbIArranque.AutoSize = true;
            this.lbIArranque.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIArranque.ForeColor = System.Drawing.Color.White;
            this.lbIArranque.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbIArranque.Location = new System.Drawing.Point(3, 72);
            this.lbIArranque.Name = "lbIArranque";
            this.lbIArranque.Size = new System.Drawing.Size(146, 16);
            this.lbIArranque.TabIndex = 11;
            this.lbIArranque.Text = "Corriente Arranque: ";
            this.lbIArranque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCosfi
            // 
            this.lbCosfi.AutoSize = true;
            this.lbCosfi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCosfi.ForeColor = System.Drawing.Color.White;
            this.lbCosfi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbCosfi.Location = new System.Drawing.Point(3, 48);
            this.lbCosfi.Name = "lbCosfi";
            this.lbCosfi.Size = new System.Drawing.Size(86, 16);
            this.lbCosfi.TabIndex = 12;
            this.lbCosfi.Text = "Coseno Fi: ";
            this.lbCosfi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCosFiArr
            // 
            this.lbCosFiArr.AutoSize = true;
            this.lbCosFiArr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCosFiArr.ForeColor = System.Drawing.Color.White;
            this.lbCosFiArr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbCosFiArr.Location = new System.Drawing.Point(309, 72);
            this.lbCosFiArr.Name = "lbCosFiArr";
            this.lbCosFiArr.Size = new System.Drawing.Size(153, 16);
            this.lbCosFiArr.TabIndex = 13;
            this.lbCosFiArr.Text = "Coseno Fi Arranque: ";
            this.lbCosFiArr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lbCosFiArr, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbCorriente, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbCosfi, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbTension, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbRev, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbIArranque, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbPotencia, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 91);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(612, 96);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // btSalir
            // 
            this.btSalir.Location = new System.Drawing.Point(528, 203);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(100, 25);
            this.btSalir.TabIndex = 15;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // btAgregar
            // 
            this.btAgregar.Location = new System.Drawing.Point(415, 203);
            this.btAgregar.Name = "btAgregar";
            this.btAgregar.Size = new System.Drawing.Size(100, 25);
            this.btAgregar.TabIndex = 16;
            this.btAgregar.Text = "Agregar Carga";
            this.btAgregar.UseVisualStyleBackColor = true;
            this.btAgregar.Click += new System.EventHandler(this.btAgregar_Click);
            // 
            // fNuevaCarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(640, 240);
            this.Controls.Add(this.btAgregar);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbEquipos);
            this.Controls.Add(this.lbUsuario);
            this.Controls.Add(this.lbProyecto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fNuevaCarga";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nueva Carga";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbProyecto;
        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.ComboBox cbEquipos;
        private System.Windows.Forms.Label lbPotencia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbTension;
        private System.Windows.Forms.Label lbRev;
        private System.Windows.Forms.Label lbCorriente;
        private System.Windows.Forms.Label lbIArranque;
        private System.Windows.Forms.Label lbCosfi;
        private System.Windows.Forms.Label lbCosFiArr;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Button btAgregar;
    }
}