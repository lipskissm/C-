using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OfficeOpenXml;
using OxyPlot.Axes;


namespace CryptoApp
{
    public class NewWindowForm : Form
    {
        private Dictionary<string, List<CryptoEntry>> _cryptoData; // Store loaded data for export

        public NewWindowForm()
        {
            this.Text = "Cryptocurrency Graph";
            this.Size = new System.Drawing.Size(800, 600);

            // Add Export Button
            var exportButton = new Button
            {
                Text = "Export to Excel",
                Dock = DockStyle.Bottom
            };
            exportButton.Click += ExportToExcel;

            // Display Graph
            DisplayGraph();

            this.Controls.Add(exportButton);
        }

        private void DisplayGraph()
{
    string filePath = "crypto_rates.json";
    if (!File.Exists(filePath))
    {
        MessageBox.Show("crypto_rates.json file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    string jsonData = File.ReadAllText(filePath);
    var cryptoData = JsonConvert.DeserializeObject<Dictionary<string, List<CryptoEntry>>>(jsonData);

    if (cryptoData == null || cryptoData.Count == 0)
    {
        MessageBox.Show("No data available to display.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    // Create a PlotModel
    var plotModel = new PlotModel { Title = "Cryptocurrency Rates" };

    // Add a DateTime axis for X (dates) and a linear axis for Y (prices)
    plotModel.Axes.Add(new DateTimeAxis
    {
        Position = AxisPosition.Bottom,
        StringFormat = "yyyy-MM-dd",
        Title = "Date",
        MinorIntervalType = DateTimeIntervalType.Days,
        IntervalType = DateTimeIntervalType.Days,
        MajorGridlineStyle = LineStyle.Solid,
        MinorGridlineStyle = LineStyle.Dot
    });

    plotModel.Axes.Add(new LinearAxis
    {
        Position = AxisPosition.Left,
        Title = "Price (USD)",
        MajorGridlineStyle = LineStyle.Solid,
        MinorGridlineStyle = LineStyle.Dot
    });

    // Add a line series for each cryptocurrency
    foreach (var crypto in cryptoData)
    {
        // Sort the data by date before plotting
        var sortedData = crypto.Value
            .OrderBy(entry => DateTime.Parse(entry.Date))
            .ToList();

        var lineSeries = new LineSeries
        {
            Title = crypto.Key,
            MarkerType = MarkerType.Circle
        };

        foreach (var entry in sortedData)
        {
            var date = DateTime.Parse(entry.Date);
            lineSeries.Points.Add(DateTimeAxis.CreateDataPoint(date, (double)entry.Price));
        }

        plotModel.Series.Add(lineSeries);
    }

    // Display the graph in a PlotView
    var plotView = new PlotView
    {
        Model = plotModel,
        Dock = DockStyle.Fill
    };

    this.Controls.Add(plotView);
}
        private void ExportToExcel(object sender, EventArgs e)
        {
            string filePath = "crypto_rates.json";
            string jsonData = File.ReadAllText(filePath);
            var _cryptoData = JsonConvert.DeserializeObject<Dictionary<string, List<CryptoEntry>>>(jsonData);
            if (_cryptoData == null || _cryptoData.Count == 0)
            {
                MessageBox.Show("No data available to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Export logic remains unchanged
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

                            worksheet.Cells[1, 1].Value = "Cryptocurrency";
                            worksheet.Cells[1, 2].Value = "Date";
                            worksheet.Cells[1, 3].Value = "Price";

                            int row = 2;
                            foreach (var crypto in _cryptoData)
                            {
                                foreach (var entry in crypto.Value)
                                {
                                    worksheet.Cells[row, 1].Value = crypto.Key;
                                    worksheet.Cells[row, 2].Value = entry.Date;
                                    worksheet.Cells[row, 3].Value = entry.Price;
                                    row++;
                                }
                            }

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

    public class CryptoEntry
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
