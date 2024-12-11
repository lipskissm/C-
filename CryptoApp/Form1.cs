using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoLibrary; // Assuming CryptoApi and Crypto classes are in this library

namespace CryptoApp
{
    public partial class MainForm : Form
    {
        private readonly CryptoApi _cryptoApi = new CryptoApi();

        public MainForm()
        {
            InitializeComponent();
        }
        
        private void btnRefresh_Click(object sender, EventArgs e)
{
    RefreshCryptoPrices();
}
        

       private async void MainForm_Load(object sender, EventArgs e)
{
    dgvRates.Columns.Clear();
    dgvRates.Rows.Clear();

    dgvRates.Columns.Add("Cryptocurrency", "Cryptocurrency");
    dgvRates.Columns.Add("Rate (USD)", "Rate (USD)");

    var cryptoRates = await _cryptoApi.FetchCurrentCryptoPrices();

    // Save to JSON
    var fileManager = new FileManager();
    string filePath = "crypto_rates.json"; // Path to save the file
    fileManager.SaveToJson(filePath, cryptoRates);

    foreach (var rate in cryptoRates)
    {
        dgvRates.Rows.Add(rate.Key, rate.Value);
    }
}
private void btnOpenOptions_Click(object sender, EventArgs e)
{
    var optionsForm = new OptionsForm();
    if (optionsForm.ShowDialog() == DialogResult.OK)
    {
        var selectedCryptos = optionsForm.SelectedCryptos;

        if (selectedCryptos.Count > 0)
        {
            try
            {
                // Load data from JSON file
                var fileManager = new FileManager();
                string filePath = "crypto_rates.json";
                var allRates = fileManager.LoadFromJson<Dictionary<string, decimal>>(filePath);

                // Filter data based on selected cryptocurrencies
                var filteredRates = allRates
                    .Where(rate => selectedCryptos.Contains(rate.Key))
                    .ToDictionary(rate => rate.Key, rate => rate.Value);

                // Display filtered data in DataGridView
                dgvRates.Rows.Clear();
                foreach (var rate in filteredRates)
                {
                    dgvRates.Rows.Add(rate.Key, rate.Value);
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("The data file 'crypto_rates.json' was not found. Please fetch the data first.", 
                                "File Not Found", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", 
                                "Error", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Error);
            }
        }
    }
}

private async void RefreshCryptoPrices()
{
    var cryptoRates = await _cryptoApi.FetchCurrentCryptoPrices();
    var fileManager = new FileManager();
    string filePath = "crypto_rates.json";

    fileManager.SaveToJson(filePath, cryptoRates);

    dgvRates.Rows.Clear();
    foreach (var rate in cryptoRates)
    {
        dgvRates.Rows.Add(rate.Key, rate.Value);
    }
}



        

    }
    
}
