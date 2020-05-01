namespace SDV701_Project
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
            this.lstCategories.Location = new System.Drawing.Point(12, 72);
            this.lstCategories.Name = "lstCategories";
            this.lstCategories.Size = new System.Drawing.Size(225, 108);
            this.lstCategories.TabIndex = 0;
            this.lstCategories.DoubleClick += new System.EventHandler(this.LstCategories_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Categories";
            // 
            // btnOpenSelectedCategory
            // 
            this.btnOpenSelectedCategory.Location = new System.Drawing.Point(12, 186);
            this.btnOpenSelectedCategory.Name = "btnOpenSelectedCategory";
            this.btnOpenSelectedCategory.Size = new System.Drawing.Size(225, 23);
            this.btnOpenSelectedCategory.TabIndex = 2;
            this.btnOpenSelectedCategory.Text = "Open Selected Category";
            this.btnOpenSelectedCategory.UseVisualStyleBackColor = true;
            this.btnOpenSelectedCategory.Click += new System.EventHandler(this.BtnOpenSelectedCategory_Click);
            // 
            // btnOpenCurrentOrdersForm
            // 
            this.btnOpenCurrentOrdersForm.Location = new System.Drawing.Point(14, 12);
            this.btnOpenCurrentOrdersForm.Name = "btnOpenCurrentOrdersForm";
            this.btnOpenCurrentOrdersForm.Size = new System.Drawing.Size(225, 23);
            this.btnOpenCurrentOrdersForm.TabIndex = 3;
            this.btnOpenCurrentOrdersForm.Text = "Open Current Orders";
            this.btnOpenCurrentOrdersForm.UseVisualStyleBackColor = true;
            this.btnOpenCurrentOrdersForm.Click += new System.EventHandler(this.BtnOpenCurrentOrdersForm_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 225);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(225, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 263);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpenCurrentOrdersForm);
            this.Controls.Add(this.btnOpenSelectedCategory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstCategories);
            this.Name = "frmMain";
            this.Text = "Electrify NZ";
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

