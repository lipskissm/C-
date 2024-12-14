using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OfficeOpenXml; // For working with Excel

namespace CryptoApp
{
    public class NewWindowForm : Form
    {
        private Dictionary<string, List<double>> _cryptoData; // Store loaded data for export

        public NewWindowForm()
        {
            // Set up the form manually
            this.Text = "Cryptocurrency Graph";
            this.Size = new System.Drawing.Size(800, 600); // Set window size

            // Add Export Button
            var exportButton = new Button
            {
                Text = "Export to Excel",
                Dock = DockStyle.Bottom
            };
            exportButton.Click += ExportToExcel;

            // Add the graph
            DisplayGraph();

            // Add the button to the form
            this.Controls.Add(exportButton);
        }

        private void DisplayGraph()
        {
            // Load crypto_rates.json data
            string filePath = "crypto_rates.json";
            if (!File.Exists(filePath))
            {
                MessageBox.Show("crypto_rates.json file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string jsonData = File.ReadAllText(filePath);
            _cryptoData = JsonConvert.DeserializeObject<Dictionary<string, List<double>>>(jsonData);

            if (_cryptoData == null || _cryptoData.Count == 0)
            {
                MessageBox.Show("No data available to display.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a PlotModel
            var plotModel = new PlotModel { Title = "Cryptocurrency Rates" };

            // Add a line series for each cryptocurrency
            foreach (var crypto in _cryptoData)
            {
                var lineSeries = new LineSeries { Title = crypto.Key, MarkerType = MarkerType.Circle };
                for (int i = 0; i < crypto.Value.Count; i++)
                {
                    lineSeries.Points.Add(new DataPoint(i + 1, crypto.Value[i])); // X is index, Y is the rate
                }
                plotModel.Series.Add(lineSeries);
            }

            // Create and configure PlotView
            var plotView = new PlotView
            {
                Model = plotModel,
                Dock = DockStyle.Fill
            };

            this.Controls.Add(plotView);
        }

        private void ExportToExcel(object sender, EventArgs e)
        {
            if (_cryptoData == null || _cryptoData.Count == 0)
            {
                MessageBox.Show("No data available to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                Title = "Save Graph Data to Excel"
            })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var package = new ExcelPackage())
                        {
                            var worksheet = package.Workbook.Worksheets.Add("Crypto Rates");

                            // Add header row
                            worksheet.Cells[1, 1].Value = "Cryptocurrency";
                            worksheet.Cells[1, 2].Value = "Rate Index";
                            worksheet.Cells[1, 3].Value = "Rate (USD)";

                            int row = 2; // Start from the second row
                            foreach (var crypto in _cryptoData)
                            {
                                for (int i = 0; i < crypto.Value.Count; i++)
                                {
                                    worksheet.Cells[row, 1].Value = crypto.Key; // Cryptocurrency name
                                    worksheet.Cells[row, 2].Value = i + 1; // Index
                                    worksheet.Cells[row, 3].Value = crypto.Value[i]; // Rate
                                    row++;
                                }
                            }

                            // Save the file
                            File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
                            MessageBox.Show("Data exported successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
