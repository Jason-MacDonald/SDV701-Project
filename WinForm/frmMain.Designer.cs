namespace WinForm
{
    partial class frmMain
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
            this.lstCategories = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenSelectedCategory = new System.Windows.Forms.Button();
            this.btnOpenCurrentOrdersForm = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstCategories
            // 
            this.lstCategories.FormattingEnabled = true;
            this.lstCategories.ItemHeight = 16;
            this.lstCategories.Location = new System.Drawing.Point(16, 89);
            this.lstCategories.Margin = new System.Windows.Forms.Padding(4);
            this.lstCategories.Name = "lstCategories";
            this.lstCategories.Size = new System.Drawing.Size(299, 132);
            this.lstCategories.TabIndex = 0;
            this.lstCategories.DoubleClick += new System.EventHandler(this.LstCategories_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Categories";
            // 
            // btnOpenSelectedCategory
            // 
            this.btnOpenSelectedCategory.Location = new System.Drawing.Point(16, 229);
            this.btnOpenSelectedCategory.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenSelectedCategory.Name = "btnOpenSelectedCategory";
            this.btnOpenSelectedCategory.Size = new System.Drawing.Size(300, 28);
            this.btnOpenSelectedCategory.TabIndex = 2;
            this.btnOpenSelectedCategory.Text = "Open Selected Category";
            this.btnOpenSelectedCategory.UseVisualStyleBackColor = true;
            this.btnOpenSelectedCategory.Click += new System.EventHandler(this.BtnOpenSelectedCategory_Click);
            // 
            // btnOpenCurrentOrdersForm
            // 
            this.btnOpenCurrentOrdersForm.Location = new System.Drawing.Point(19, 15);
            this.btnOpenCurrentOrdersForm.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenCurrentOrdersForm.Name = "btnOpenCurrentOrdersForm";
            this.btnOpenCurrentOrdersForm.Size = new System.Drawing.Size(300, 28);
            this.btnOpenCurrentOrdersForm.TabIndex = 3;
            this.btnOpenCurrentOrdersForm.Text = "Open Current Orders";
            this.btnOpenCurrentOrdersForm.UseVisualStyleBackColor = true;
            this.btnOpenCurrentOrdersForm.Click += new System.EventHandler(this.BtnOpenCurrentOrdersForm_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(16, 277);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(300, 28);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 324);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpenCurrentOrdersForm);
            this.Controls.Add(this.btnOpenSelectedCategory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstCategories);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "Electrify NZ";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstCategories;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenSelectedCategory;
        private System.Windows.Forms.Button btnOpenCurrentOrdersForm;
        private System.Windows.Forms.Button btnClose;
    }
}

