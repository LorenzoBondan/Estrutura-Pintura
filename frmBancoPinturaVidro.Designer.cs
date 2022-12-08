namespace Concatenar
{
    partial class frmBancoPinturaVidro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBancoPinturaVidro));
            this.label1 = new System.Windows.Forms.Label();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnImportarCSVPinturas = new System.Windows.Forms.Button();
            this.datagridBancoPintura = new System.Windows.Forms.DataGridView();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.datagridBancoPintura)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(623, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 54);
            this.label1.TabIndex = 14;
            this.label1.Text = "A importação de CSV deve conter cabeçalho e o formato de:\r\ncodigo | cor | descric" +
    "ao";
            // 
            // btnExcluir
            // 
            this.btnExcluir.Location = new System.Drawing.Point(626, 34);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(162, 23);
            this.btnExcluir.TabIndex = 13;
            this.btnExcluir.Text = "Excluir Código";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnImportarCSVPinturas
            // 
            this.btnImportarCSVPinturas.Location = new System.Drawing.Point(626, 63);
            this.btnImportarCSVPinturas.Name = "btnImportarCSVPinturas";
            this.btnImportarCSVPinturas.Size = new System.Drawing.Size(162, 23);
            this.btnImportarCSVPinturas.TabIndex = 11;
            this.btnImportarCSVPinturas.Text = "Importar CSV Pinturas";
            this.btnImportarCSVPinturas.UseVisualStyleBackColor = true;
            this.btnImportarCSVPinturas.Click += new System.EventHandler(this.btnImportarCSVPinturas_Click);
            // 
            // datagridBancoPintura
            // 
            this.datagridBancoPintura.AllowUserToAddRows = false;
            this.datagridBancoPintura.AllowUserToDeleteRows = false;
            this.datagridBancoPintura.AllowUserToResizeRows = false;
            this.datagridBancoPintura.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.datagridBancoPintura.BackgroundColor = System.Drawing.Color.White;
            this.datagridBancoPintura.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.datagridBancoPintura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridBancoPintura.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.cor,
            this.descricao});
            this.datagridBancoPintura.Location = new System.Drawing.Point(13, 122);
            this.datagridBancoPintura.Name = "datagridBancoPintura";
            this.datagridBancoPintura.ReadOnly = true;
            this.datagridBancoPintura.RowHeadersVisible = false;
            this.datagridBancoPintura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagridBancoPintura.Size = new System.Drawing.Size(775, 316);
            this.datagridBancoPintura.TabIndex = 10;
            // 
            // codigo
            // 
            this.codigo.HeaderText = "Código";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 65;
            // 
            // cor
            // 
            this.cor.HeaderText = "Cor";
            this.cor.Name = "cor";
            this.cor.ReadOnly = true;
            this.cor.Width = 48;
            // 
            // descricao
            // 
            this.descricao.HeaderText = "Descrição";
            this.descricao.Name = "descricao";
            this.descricao.ReadOnly = true;
            this.descricao.Width = 80;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(12, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(765, 43);
            this.label10.TabIndex = 12;
            this.label10.Text = "label1";
            this.label10.Visible = false;
            // 
            // frmBancoPinturaVidro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnImportarCSVPinturas);
            this.Controls.Add(this.datagridBancoPintura);
            this.Controls.Add(this.label10);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmBancoPinturaVidro";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Banco Pintura Acetinada Vidros";
            this.Load += new System.EventHandler(this.frmBancoPinturaVidro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagridBancoPintura)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnImportarCSVPinturas;
        private System.Windows.Forms.DataGridView datagridBancoPintura;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cor;
        private System.Windows.Forms.DataGridViewTextBoxColumn descricao;
        private System.Windows.Forms.Label label10;
    }
}