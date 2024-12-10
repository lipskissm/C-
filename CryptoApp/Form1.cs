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

        private async void MainForm_Load(object sender, EventArgs e)
        {
            dgvRates.Columns.Clear();
            dgvRates.Rows.Clear();

            dgvRates.Columns.Add("Cryptocurrency", "Cryptocurrency");
            dgvRates.Columns.Add("Rate (USD)", "Rate (USD)");

            var cryptoRates = await _cryptoApi.FetchCurrentCryptoPrices();

            foreach (var rate in cryptoRates)
            {
                dgvRates.Rows.Add(rate.Key, rate.Value);
            }
        }

    }
}
