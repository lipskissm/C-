
using System.ComponentModel;


namespace CryptoApp
{
    public partial class OptionsForm : Form
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> SelectedCryptos { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? SelectedDate { get; private set; }

        public OptionsForm()
        {
            InitializeComponent();
            SelectedCryptos = new List<string>();
            SelectedDate = null; // Initialize SelectedDate
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

            SelectedDate = dtpDate.Value.Date; // Capture the selected date

            DialogResult = DialogResult.OK;
            Close();
        }
        
    }
}