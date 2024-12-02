using System;
using System.Collections.Generic;
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

        private async void MainForm_Load(object sender, EventArgs e)
{
    // Clear existing columns and rows just in case
    dgvRates.Columns.Clear();
    dgvRates.Rows.Clear();

    // Add necessary columns to the DataGridView
    dgvRates.Columns.Add("Cryptocurrency", "Cryptocurrency");
    dgvRates.Columns.Add("Rate", "Rate");

    // Fetch cryptocurrency rates
    var symbols = new List<string> { "bitcoin", "ethereum", "dogecoin" };
    var rates = await FetchCryptoRates(symbols);

    // Add rows with fetched data
    foreach (var rate in rates)
    {
        dgvRates.Rows.Add(rate.Key, rate.Value);
    }
}

        private async Task<Dictionary<string, decimal>> FetchCryptoRates(List<string> symbols)
{
    
    var rates = new Dictionary<string, decimal>();
    // Mock implementation (replace with actual API call logic)
    foreach (var symbol in symbols)
    {
        rates[symbol] = new Random().Next(100, 50000); // Random rates for demonstration
    }
    await Task.Delay(500); // Simulate network delay
    return rates;
}
    
   }
}
