namespace Warehouse
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.Button btnStockReport;
        private System.Windows.Forms.Button btnPopularReport;
        private System.Windows.Forms.Button btnExportXlsx;
        private System.Windows.Forms.Button btnExportDocx;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.btnStockReport = new System.Windows.Forms.Button();
            this.btnPopularReport = new System.Windows.Forms.Button();
            this.btnExportXlsx = new System.Windows.Forms.Button();
            this.btnExportDocx = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.AllowUserToAddRows = false;
            this.dataGridViewReport.AllowUserToDeleteRows = false;
            this.dataGridViewReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.Location = new System.Drawing.Point(12, 10);
            this.dataGridViewReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.ReadOnly = true;
            this.dataGridViewReport.RowHeadersWidth = 51;
            this.dataGridViewReport.RowTemplate.Height = 29;
            this.dataGridViewReport.Size = new System.Drawing.Size(658, 224);
            this.dataGridViewReport.TabIndex = 0;
            // 
            // btnStockReport
            // 
            this.btnStockReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStockReport.Location = new System.Drawing.Point(12, 248);
            this.btnStockReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStockReport.Name = "btnStockReport";
            this.btnStockReport.Size = new System.Drawing.Size(120, 24);
            this.btnStockReport.TabIndex = 1;
            this.btnStockReport.Text = "Остатки";
            this.btnStockReport.UseVisualStyleBackColor = true;
            this.btnStockReport.Click += new System.EventHandler(this.btnStockReport_Click);
            // 
            // btnPopularReport
            // 
            this.btnPopularReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPopularReport.Location = new System.Drawing.Point(138, 248);
            this.btnPopularReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPopularReport.Name = "btnPopularReport";
            this.btnPopularReport.Size = new System.Drawing.Size(120, 24);
            this.btnPopularReport.TabIndex = 2;
            this.btnPopularReport.Text = "Популярные";
            this.btnPopularReport.UseVisualStyleBackColor = true;
            this.btnPopularReport.Click += new System.EventHandler(this.btnPopularReport_Click);
            // 
            // btnExportXlsx
            // 
            this.btnExportXlsx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportXlsx.Location = new System.Drawing.Point(332, 248);
            this.btnExportXlsx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportXlsx.Name = "btnExportXlsx";
            this.btnExportXlsx.Size = new System.Drawing.Size(80, 24);
            this.btnExportXlsx.TabIndex = 3;
            this.btnExportXlsx.Text = "XLSX";
            this.btnExportXlsx.UseVisualStyleBackColor = true;
            this.btnExportXlsx.Click += new System.EventHandler(this.btnExportXlsx_Click);
            // 
            // btnExportDocx
            // 
            this.btnExportDocx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportDocx.Location = new System.Drawing.Point(418, 248);
            this.btnExportDocx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportDocx.Name = "btnExportDocx";
            this.btnExportDocx.Size = new System.Drawing.Size(80, 24);
            this.btnExportDocx.TabIndex = 4;
            this.btnExportDocx.Text = "DOCX";
            this.btnExportDocx.UseVisualStyleBackColor = true;
            this.btnExportDocx.Click += new System.EventHandler(this.btnExportDocx_Click);
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportPdf.Location = new System.Drawing.Point(504, 248);
            this.btnExportPdf.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(50, 24);
            this.btnExportPdf.TabIndex = 5;
            this.btnExportPdf.Text = "PDF";
            this.btnExportPdf.UseVisualStyleBackColor = true;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(560, 248);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 24);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 281);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExportPdf);
            this.Controls.Add(this.btnExportDocx);
            this.Controls.Add(this.btnExportXlsx);
            this.Controls.Add(this.btnPopularReport);
            this.Controls.Add(this.btnStockReport);
            this.Controls.Add(this.dataGridViewReport);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ReportsForm";
            this.Text = "Отчёты";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
