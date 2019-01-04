namespace MC_Cables_0._1
{
    partial class LogIn
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogIn));
            this.cbUsuario = new System.Windows.Forms.ComboBox();
            this.btEquipos = new System.Windows.Forms.Button();
            this.cbProyecto = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btCargas = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btMCCables = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbUsuario
            // 
            this.cbUsuario.DropDownHeight = 110;
            this.cbUsuario.DropDownWidth = 400;
            this.cbUsuario.FormattingEnabled = true;
            this.cbUsuario.IntegralHeight = false;
            this.cbUsuario.Location = new System.Drawing.Point(50, 100);
            this.cbUsuario.Name = "cbUsuario";
            this.cbUsuario.Size = new System.Drawing.Size(382, 21);
            this.cbUsuario.TabIndex = 0;
            // 
            // btEquipos
            // 
            this.btEquipos.Location = new System.Drawing.Point(49, 205);
            this.btEquipos.Name = "btEquipos";
            this.btEquipos.Size = new System.Drawing.Size(120, 25);
            this.btEquipos.TabIndex = 2;
            this.btEquipos.Text = "Lista Equipos";
            this.btEquipos.UseVisualStyleBackColor = true;
            this.btEquipos.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbProyecto
            // 
            this.cbProyecto.DropDownHeight = 110;
            this.cbProyecto.DropDownWidth = 400;
            this.cbProyecto.FormattingEnabled = true;
            this.cbProyecto.IntegralHeight = false;
            this.cbProyecto.Location = new System.Drawing.Point(50, 165);
            this.cbProyecto.Name = "cbProyecto";
            this.cbProyecto.Size = new System.Drawing.Size(382, 21);
            this.cbProyecto.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(45, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(45, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Proyecto";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(413, 365);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Salir";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(13, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 42);
            this.label3.TabIndex = 7;
            this.label3.Text = "Thor";
            // 
            // btCargas
            // 
            this.btCargas.Location = new System.Drawing.Point(180, 205);
            this.btCargas.Name = "btCargas";
            this.btCargas.Size = new System.Drawing.Size(120, 25);
            this.btCargas.TabIndex = 3;
            this.btCargas.Text = "Lista de Cargas";
            this.btCargas.UseVisualStyleBackColor = true;
            this.btCargas.Click += new System.EventHandler(this.btCargas_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = global::MC_Cables_0._1.Properties.Resources.SEIBO;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(286, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(185, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btMCCables
            // 
            this.btMCCables.Location = new System.Drawing.Point(311, 205);
            this.btMCCables.Name = "btMCCables";
            this.btMCCables.Size = new System.Drawing.Size(120, 25);
            this.btMCCables.TabIndex = 4;
            this.btMCCables.Text = "MC Cables";
            this.btMCCables.UseVisualStyleBackColor = true;
            this.btMCCables.Click += new System.EventHandler(this.btMCCables_Click);
            // 
            // LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.btMCCables);
            this.Controls.Add(this.btCargas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbProyecto);
            this.Controls.Add(this.btEquipos);
            this.Controls.Add(this.cbUsuario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Thor_LogIn";
            this.Text = "LogIn";
            this.Load += new System.EventHandler(this.LogIn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbUsuario;
        private System.Windows.Forms.Button btEquipos;
        private System.Windows.Forms.ComboBox cbProyecto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btCargas;
        private System.Windows.Forms.Button btMCCables;
    }
}

