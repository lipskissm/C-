using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoLibrary;

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

            var fileManager = new FileManager();
            string filePath = "crypto_rates.json"; // Path to save the file
            fileManager.SaveToJson(filePath, cryptoRates);

            foreach (var rate in cryptoRates)
            {
                dgvRates.Rows.Add(rate.Key, rate.Value);
            }
        }

        private async void btnOpenOptions_Click(object sender, EventArgs e)
        {
            var optionsForm = new OptionsForm();
            if (optionsForm.ShowDialog() == DialogResult.OK)
            {
                var selectedCryptos = optionsForm.SelectedCryptos;
                var selectedDate = optionsForm.SelectedDate;

                if (selectedCryptos.Count > 0)
                {
                    try
                    {
                        var cryptoRates = await _cryptoApi.FetchCryptoPricesByDate(selectedCryptos, selectedDate);

                        dgvRates.Rows.Clear();
                        foreach (var rate in cryptoRates)
                        {
                            dgvRates.Rows.Add(rate.Key, rate.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnOpenNewWindow_Click(object sender, EventArgs e)
    {
    var newWindow = new NewWindowForm();
    newWindow.ShowDialog(); // This will open the new window as a modal
    }

       
    }
}