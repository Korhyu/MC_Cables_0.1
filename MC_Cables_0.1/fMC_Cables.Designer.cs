namespace MC_Cables_0._1
{
    partial class fMC_Cables
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
            this.dgvMC = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lbProyecto = new System.Windows.Forms.Label();
            this.lbUsuario = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recalcularCorrientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxOrigenDestinoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarCableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMC
            // 
            this.dgvMC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMC.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvMC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMC.Location = new System.Drawing.Point(0, 0);
            this.dgvMC.Name = "dgvMC";
            this.dgvMC.Size = new System.Drawing.Size(964, 559);
            this.dgvMC.TabIndex = 0;
            this.dgvMC.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMC_CellValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvMC);
            this.splitContainer1.Size = new System.Drawing.Size(964, 588);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lbProyecto);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lbUsuario);
            this.splitContainer2.Size = new System.Drawing.Size(964, 25);
            this.splitContainer2.SplitterDistance = 482;
            this.splitContainer2.SplitterWidth = 2;
            this.splitContainer2.TabIndex = 0;
            // 
            // lbProyecto
            // 
            this.lbProyecto.AutoSize = true;
            this.lbProyecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProyecto.ForeColor = System.Drawing.Color.White;
            this.lbProyecto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbProyecto.Location = new System.Drawing.Point(-3, 0);
            this.lbProyecto.Name = "lbProyecto";
            this.lbProyecto.Size = new System.Drawing.Size(81, 18);
            this.lbProyecto.TabIndex = 2;
            this.lbProyecto.Text = "Proyecto:";
            this.lbProyecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbUsuario
            // 
            this.lbUsuario.AutoSize = true;
            this.lbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsuario.ForeColor = System.Drawing.Color.White;
            this.lbUsuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbUsuario.Location = new System.Drawing.Point(3, 0);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(72, 18);
            this.lbUsuario.TabIndex = 1;
            this.lbUsuario.Text = "Usuario:";
            this.lbUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(964, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recalcularCorrientesToolStripMenuItem,
            this.comboBoxOrigenDestinoToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // recalcularCorrientesToolStripMenuItem
            // 
            this.recalcularCorrientesToolStripMenuItem.Name = "recalcularCorrientesToolStripMenuItem";
            this.recalcularCorrientesToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.recalcularCorrientesToolStripMenuItem.Text = "RecalcularCorrientes";
            this.recalcularCorrientesToolStripMenuItem.Click += new System.EventHandler(this.recalcularCorrientesToolStripMenuItem_Click);
            // 
            // comboBoxOrigenDestinoToolStripMenuItem
            // 
            this.comboBoxOrigenDestinoToolStripMenuItem.Name = "comboBoxOrigenDestinoToolStripMenuItem";
            this.comboBoxOrigenDestinoToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.comboBoxOrigenDestinoToolStripMenuItem.Text = "ComboBoxOrigenDestino";
            this.comboBoxOrigenDestinoToolStripMenuItem.Click += new System.EventHandler(this.comboBoxOrigenDestinoToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarCableToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 26);
            // 
            // eliminarCableToolStripMenuItem
            // 
            this.eliminarCableToolStripMenuItem.Name = "eliminarCableToolStripMenuItem";
            this.eliminarCableToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.eliminarCableToolStripMenuItem.Text = "Eliminar Cable";
            this.eliminarCableToolStripMenuItem.Click += new System.EventHandler(this.eliminarCableToolStripMenuItem_Click);
            // 
            // fMC_Cables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(964, 612);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "fMC_Cables";
            this.Text = "MC_Cables";
            this.Shown += new System.EventHandler(this.fMC_Cables_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMC)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMC;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lbProyecto;
        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recalcularCorrientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comboBoxOrigenDestinoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eliminarCableToolStripMenuItem;
    }
}