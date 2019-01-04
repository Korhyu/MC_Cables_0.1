namespace MC_Cables_0._1
{
    partial class fNuevoEscenario
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
            this.btAgregar = new System.Windows.Forms.Button();
            this.btSalir = new System.Windows.Forms.Button();
            this.lbUsuario = new System.Windows.Forms.Label();
            this.lbProyecto = new System.Windows.Forms.Label();
            this.tbNombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btAgregar
            // 
            this.btAgregar.Location = new System.Drawing.Point(412, 113);
            this.btAgregar.Name = "btAgregar";
            this.btAgregar.Size = new System.Drawing.Size(100, 25);
            this.btAgregar.TabIndex = 18;
            this.btAgregar.Text = "Nuevo Escenario";
            this.btAgregar.UseVisualStyleBackColor = true;
            this.btAgregar.Click += new System.EventHandler(this.btAgregar_Click);
            // 
            // btSalir
            // 
            this.btSalir.Location = new System.Drawing.Point(525, 113);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(100, 25);
            this.btSalir.TabIndex = 17;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // lbUsuario
            // 
            this.lbUsuario.AutoSize = true;
            this.lbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsuario.ForeColor = System.Drawing.Color.White;
            this.lbUsuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbUsuario.Location = new System.Drawing.Point(10, 9);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(66, 16);
            this.lbUsuario.TabIndex = 20;
            this.lbUsuario.Text = "Usuario:";
            this.lbUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.lbProyecto.TabIndex = 19;
            this.lbProyecto.Text = "Proyecto:";
            this.lbProyecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbNombre
            // 
            this.tbNombre.Location = new System.Drawing.Point(155, 65);
            this.tbNombre.Name = "tbNombre";
            this.tbNombre.Size = new System.Drawing.Size(470, 20);
            this.tbNombre.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(10, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Nombre Escenario";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fNuevoEscenario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(640, 150);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNombre);
            this.Controls.Add(this.lbUsuario);
            this.Controls.Add(this.lbProyecto);
            this.Controls.Add(this.btAgregar);
            this.Controls.Add(this.btSalir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fNuevoEscenario";
            this.Text = "fNuevoEscenario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btAgregar;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.Label lbProyecto;
        private System.Windows.Forms.TextBox tbNombre;
        private System.Windows.Forms.Label label1;
    }
}