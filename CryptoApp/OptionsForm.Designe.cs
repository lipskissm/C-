namespace CryptoApp
{
    partial class OptionsForm
    {
        private System.Windows.Forms.FlowLayoutPanel flpCryptoOptions;
        private System.Windows.Forms.Button btnApply;

        private void InitializeComponent()
        {
            this.flpCryptoOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnApply = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // flpCryptoOptions
            this.flpCryptoOptions.AutoScroll = true;
            this.flpCryptoOptions.Location = new System.Drawing.Point(12, 12);
            this.flpCryptoOptions.Name = "flpCryptoOptions";
            this.flpCryptoOptions.Size = new System.Drawing.Size(260, 200);

            // btnApply
            this.btnApply.Location = new System.Drawing.Point(197, 230);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            // OptionsForm
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.flpCryptoOptions);
            this.Controls.Add(this.btnApply);
            this.Name = "OptionsForm";
            this.Text = "Filter Options";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.ResumeLayout(false);
        }
    }
}
