using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WarehouseProject;

namespace Warehouse
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void btnStockReport_Click(object sender, EventArgs e)
        {
            DataTable dt = DatabaseHelper.GetStockReport();
            dataGridViewReport.DataSource = dt;
        }

        private void btnPopularReport_Click(object sender, EventArgs e)
        {
            DataTable dt = DatabaseHelper.GetPopularProductsReport();
            dataGridViewReport.DataSource = dt;
        }

        // ==========================
        //  1) Экспорт в XLSX (EPPlus)
        // ==========================
        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            if (!(dataGridViewReport.DataSource is DataTable dt)) return;

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel File|*.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sfd.FileName;
                    try
                    {
                        ExportToExcel(dt, filePath);
                        MessageBox.Show("Отчет сохранён: " + filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка экспорта в XLSX:\n" + ex.Message);
                    }
                }
            }
        }

        private void ExportToExcel(DataTable dt, string filePath)
        {
            // Указываем, что используем EPPlus в некоммерческом режиме
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Отчет");
                worksheet.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.Light1);
                worksheet.Cells.AutoFitColumns();

                package.SaveAs(new FileInfo(filePath));
            }
        }

        // ==========================
        //  2) Экспорт в DOCX (OpenXML)
        // ==========================
        private void btnExportDocx_Click(object sender, EventArgs e)
        {
            if (!(dataGridViewReport.DataSource is DataTable dt)) return;

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Word Document|*.docx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sfd.FileName;
                    try
                    {
                        ExportToDocx(dt, filePath);
                        MessageBox.Show("Отчет сохранён: " + filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка экспорта в DOCX:\n" + ex.Message);
                    }
                }
            }
        }

        private void ExportToDocx(DataTable dt, string filePath)
        {
            // Создаем DOCX-файл с помощью OpenXML
            using (WordprocessingDocument wordDoc =
                   WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                // Добавляем MainDocumentPart
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();

                Body body = new Body();

                // Создадим параграф для заголовка
                DocumentFormat.OpenXml.Wordprocessing.Paragraph titlePar =
                    new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                        new DocumentFormat.OpenXml.Wordprocessing.Run(
                            new DocumentFormat.OpenXml.Wordprocessing.Text("Отчет")));

                body.Append(titlePar);

                // Создаем таблицу
                Table table = new Table();

                // Шапка таблицы (первая строка)
                TableRow headerRow = new TableRow();
                foreach (DataColumn col in dt.Columns)
                {
                    TableCell cell = new TableCell(
                        new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                            new DocumentFormat.OpenXml.Wordprocessing.Run(
                                new DocumentFormat.OpenXml.Wordprocessing.Text(col.ColumnName)))
                    );
                    headerRow.Append(cell);
                }
                table.Append(headerRow);

                // Данные
                foreach (DataRow row in dt.Rows)
                {
                    TableRow dataRow = new TableRow();
                    foreach (var cellVal in row.ItemArray)
                    {
                        TableCell cell = new TableCell(
                            new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                                new DocumentFormat.OpenXml.Wordprocessing.Run(
                                    new DocumentFormat.OpenXml.Wordprocessing.Text(cellVal?.ToString() ?? "")))
                        );
                        dataRow.Append(cell);
                    }
                    table.Append(dataRow);
                }

                // Добавляем таблицу в документ
                body.Append(table);

                // Сохраняем
                mainPart.Document.Append(body);
                mainPart.Document.Save();
            }
        }

        // ==========================
        //  3) Экспорт в PDF (iTextSharp)
        // ==========================
        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (!(dataGridViewReport.DataSource is DataTable dt)) return;

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF File|*.pdf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sfd.FileName;
                    try
                    {
                        ExportToPdf(dt, filePath);
                        MessageBox.Show("Отчет сохранён: " + filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка экспорта в PDF:\n" + ex.Message);
                    }
                }
            }
        }

        private void ExportToPdf(DataTable dt, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Указываем, что используем iTextSharp.text.Document
                iTextSharp.text.Document doc = new iTextSharp.text.Document(
                    iTextSharp.text.PageSize.A4, 20f, 20f, 20f, 20f);

                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                // Создаем таблицу
                PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);

                // Заголовки
                foreach (DataColumn col in dt.Columns)
                {
                    var cell = new PdfPCell(new Phrase(col.ColumnName))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY
                    };
                    pdfTable.AddCell(cell);
                }

                // Данные
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var cellObj in row.ItemArray)
                    {
                        pdfTable.AddCell(cellObj?.ToString() ?? "");
                    }
                }

                doc.Add(pdfTable);

                doc.Close();
                writer.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
