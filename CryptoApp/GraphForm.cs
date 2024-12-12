using System;
using System.Windows.Forms;

namespace CryptoApp
{
    public partial class NewWindowForm : Form
    {
        public NewWindowForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "New Window";
            this.Size = new System.Drawing.Size(400, 300); // Set window size as needed
        }
    }
}
