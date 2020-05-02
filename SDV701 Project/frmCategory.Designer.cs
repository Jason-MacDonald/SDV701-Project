namespace SDV701_Project
{
    partial class frmCategory
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.cbChoice = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.txtDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(16, 326);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(212, 28);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete Selected E-Bike";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(236, 326);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(208, 28);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add E-Bike";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(669, 324);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(208, 28);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Edit Selected E-Bike";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // cbChoice
            // 
            this.cbChoice.FormattingEnabled = true;
            this.cbChoice.Items.AddRange(new object[] {
            "New",
            "Used"});
            this.cbChoice.Location = new System.Drawing.Point(452, 326);
            this.cbChoice.Margin = new System.Windows.Forms.Padding(4);
            this.cbChoice.Name = "cbChoice";
            this.cbChoice.Size = new System.Drawing.Size(207, 24);
            this.cbChoice.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(664, 372);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(212, 28);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close Window";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // lstCategories
            // 
            this.lstItems.FormattingEnabled = true;
            this.lstItems.ItemHeight = 16;
            this.lstItems.Location = new System.Drawing.Point(16, 44);
            this.lstItems.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstItems.Name = "lstCategories";
            this.lstItems.Size = new System.Drawing.Size(859, 260);
            this.lstItems.TabIndex = 8;
            this.lstItems.DoubleClick += new System.EventHandler(this.LstCategories_DoubleClick);
            // 
            // txtDescription
            // 
            this.txtDescription.AutoSize = true;
            this.txtDescription.Location = new System.Drawing.Point(13, 9);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(45, 16);
            this.txtDescription.TabIndex = 9;
            this.txtDescription.Text = "label1";
            // 
            // frmCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 415);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lstItems);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cbChoice);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmCategory";
            this.Text = "Mountain E-Bikes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ComboBox cbChoice;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox lstItems;
        private System.Windows.Forms.Label txtDescription;
    }
}