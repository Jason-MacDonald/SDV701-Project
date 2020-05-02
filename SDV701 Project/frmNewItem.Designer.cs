namespace SDV701_Project
{
    partial class frmNewItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label9 = new System.Windows.Forms.Label();
            this.txtWarrantyPeriod = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(293, 264);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "Waranty";
            // 
            // txtWarrantyPeriod
            // 
            this.txtWarrantyPeriod.Location = new System.Drawing.Point(372, 263);
            this.txtWarrantyPeriod.Name = "txtWarrantyPeriod";
            this.txtWarrantyPeriod.Size = new System.Drawing.Size(179, 20);
            this.txtWarrantyPeriod.TabIndex = 18;
            this.txtWarrantyPeriod.Text = "6 Months";
            // 
            // frmNewItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(563, 382);
            this.Controls.Add(this.txtWarrantyPeriod);
            this.Controls.Add(this.label9);
            this.Name = "frmNewItem";
            this.Text = "New Item Details";
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.txtWarrantyPeriod, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWarrantyPeriod;
    }
}
