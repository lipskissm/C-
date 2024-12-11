using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace CryptoApp
{
    public partial class OptionsForm : Form
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> SelectedCryptos { get; private set; }

        public OptionsForm()
        {
            InitializeComponent();
            SelectedCryptos = new List<string>();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            var cryptoList = new[] { "BTC", "ETH", "DOGE", "ADA", "XRP" };

            foreach (var crypto in cryptoList)
            {
                var checkBox = new CheckBox { Text = crypto, AutoSize = true };
                flpCryptoOptions.Controls.Add(checkBox);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SelectedCryptos = flpCryptoOptions.Controls
                .OfType<CheckBox>()
                .Where(cb => cb.Checked)
                .Select(cb => cb.Text)
                .ToList();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
