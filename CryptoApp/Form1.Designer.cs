using System.ComponentModel;
using System.Windows.Forms;
namespace CryptoApp
{
    partial class MainForm
    {
         private DataGridView dgvRates;
         private System.Windows.Forms.Button btnOpenOptions;
          private System.Windows.Forms.Button btnOpenNewWindow;

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
            this.ClientSize = new System.Drawing.Size(850, 500);
            this.Controls.Add(this.dgvRates);
            this.Name = "MainForm";
            this.Text = "Crypto Rates";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRates)).EndInit();
            this.ResumeLayout(false);
            
            //
            //Filtering
            //
            this.btnOpenOptions = new System.Windows.Forms.Button();
            this.btnOpenOptions.Location = new System.Drawing.Point(12, 420);
            this.btnOpenOptions.Name = "btnOpenOptions";
            this.btnOpenOptions.Size = new System.Drawing.Size(120, 30);
            this.btnOpenOptions.Text = "Filter Options";
            this.btnOpenOptions.Click += new System.EventHandler(this.btnOpenOptions_Click);
            this.Controls.Add(this.btnOpenOptions);


            // this.btnRefresh = new System.Windows.Forms.Button();
            // this.btnRefresh.Location = new System.Drawing.Point(150, 420); // Adjust location as needed
            // this.btnRefresh.Name = "btnRefresh";
            // this.btnRefresh.Size = new System.Drawing.Size(120, 30);
            // this.btnRefresh.Text = "Refresh";
            // this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // this.Controls.Add(this.btnRefresh);
            // this.btnOpenGraphForm = new System.Windows.Forms.Button();

            //
            //Graphform
            //
            this.btnOpenNewWindow = new System.Windows.Forms.Button();
            this.btnOpenNewWindow.Location = new System.Drawing.Point(290, 420); // Adjust location as needed
            this.btnOpenNewWindow.Name = "btnOpenNewWindow";
            this.btnOpenNewWindow.Size = new System.Drawing.Size(120, 30);
            this.btnOpenNewWindow.Text = "Open Window";
            this.btnOpenNewWindow.Click += new System.EventHandler(this.btnOpenNewWindow_Click);
            this.Controls.Add(this.btnOpenNewWindow);

        }
    }
}