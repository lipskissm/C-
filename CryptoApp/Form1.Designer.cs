using System.ComponentModel;
using System.Windows.Forms;
namespace CryptoApp
{
    partial class MainForm
    {
        private IContainer components = null;
        private DataGridView dgvRates;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvRates = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRates)).BeginInit();
            this.SuspendLayout();
            
            // 
            // dgvRates
            // 
            this.dgvRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRates.Location = new System.Drawing.Point(12, 12); // Adjust position as needed
            this.dgvRates.Name = "dgvRates";
            this.dgvRates.Size = new System.Drawing.Size(760, 400); // Adjust size as needed
            this.dgvRates.TabIndex = 0;

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvRates);
            this.Name = "MainForm";
            this.Text = "Crypto Rates";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRates)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
