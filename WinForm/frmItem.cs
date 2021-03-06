﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForm.Properties;

namespace WinForm
{
    public partial class frmItem : Form
    {
        #region ##### VARIABLES #####   
        private bool imageChanged;

        private clsItem item;
        protected clsItem Item { get => item; set => item = value; }
        #endregion

        #region ##### CONSTRUCTOR #####
        public frmItem()
        {
            InitializeComponent();
        }
        #endregion

        #region ##### METHODS ######
        public void SetDetails(clsItem prItem)
        {
            Item = prItem;
            txtName.Enabled = Item.Name != null ? false : true;

            UpdateForm();
            ShowDialog();
        }

        private async Task UpdateItemInDatabase()
        {
            try
            {
                MessageBox.Show((await ServiceClient.UpdateItemAsync(Item)).Trim('"'));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 001: " + ex.GetBaseException().Message);
            }
        }

        private async Task InsertIntoDatabase()
        {
            try
            {
                MessageBox.Show((await ServiceClient.InsertItemAsync(Item)).Trim('"'));
                txtName.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 002: " + ex.GetBaseException().Message);
            }
        }
        #endregion

        #region ##### BUTTONS #####
        private void BtnUploadImage_Click(object sender, EventArgs e)
        {
            // https://www.youtube.com/watch?v=W_cOlBBlFGM
            using(OpenFileDialog fileDialog = new OpenFileDialog() { Filter = "JPG|*.jpg", ValidateNames = true, Multiselect = false })
            {
                if(fileDialog.ShowDialog() == DialogResult.OK)
                {
                    picImage.Image = Image.FromFile(fileDialog.FileName);
                    imageChanged = true;
                }
            }
        }

        private byte[] ConvertImageToBinary(Image prImage)
        {
            MemoryStream lcMemoryStream = new MemoryStream();
            prImage.Save(lcMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return lcMemoryStream.ToArray();
        }

        private Image ConvertBinaryToImage(byte[] prImageData)
        {
            MemoryStream lcMemoryStream = new MemoryStream(prImageData);
            return Image.FromStream(lcMemoryStream);
        }

        private async void BtnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (HasChanged())
            {
                if (PushData())
                {
                    if (txtName.Enabled)
                    {
                        await InsertIntoDatabase();
                    }
                    else
                    {
                        await UpdateItemInDatabase();
                    }
                    //frmItems.Instance.UpdateForm();
                    Hide();
                }
            }
            else
            {
                MessageBox.Show("No changes made.");

                Hide();
            }         
        }

        private void BtnCloseWithoutSaving_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close without saving? Any changes you have made will not be saved.", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Hide();
            }
        }

        private void FrmItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                Hide();
            }
        }
        #endregion

        #region ##### UPDATES #####
        protected virtual bool PushData()
        {
            if (ValidName())
            {
                if (ValidDescription())
                {
                    if (ValidMotor())
                    {
                        if (ValidBattery())
                        {
                            if (ValidQuantity())
                            {
                                if (ValidPrice())
                                {
                                    Item.Name = txtName.Text;
                                    Item.Description = txtDescription.Text;
                                    Item.ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Immediately formates date for database.
                                    Item.Motor = txtMotor.Text;
                                    Item.Battery = txtBattery.Text;
                                    Item.Quantity = Convert.ToInt32(txtQuantity.Text);
                                    Item.Price = float.Parse(txtPrice.Text);
                                    Item.Image = ConvertImageToBinary(picImage.Image);

                                    imageChanged = false;
                                    return true;
                                }
                            }
                        }
                    }
                }               
            }           
            return false;
        }

        protected virtual bool HasChanged()
        {
            if 
            (
                txtName.Text != Item.Name ||
                txtDescription.Text != Item.Description ||
                txtMotor.Text != Item.Motor ||
                txtBattery.Text != Item.Battery ||
                txtQuantity.Text != Item.Quantity.ToString() ||
                txtPrice.Text != Item.Price.ToString() ||
                imageChanged
            )
            {
                return true;
            }
            return false;
        }

        protected virtual void UpdateForm()
        {
            txtName.Text = Item.Name;
            txtDescription.Text = Item.Description;
            txtMotor.Text = Item.Motor;
            txtBattery.Text = Item.Battery;
            txtQuantity.Text = Item.Quantity.ToString();
            txtPrice.Text = Item.Price.ToString();
            lblModifiedDate.Text = Item.ModifiedDate;

            if(Item.Image == null)
            {
                picImage.Image = Resources.noimage;
            }
            else
            {
                picImage.Image = ConvertBinaryToImage(Item.Image);
            }
        }
        #endregion

        #region ##### VALIDATION #####

        #region ### NAME ###
        private bool ValidName()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter the items name.");
                return false;
            }

            if (!txtName.Text.All(name => char.IsLetterOrDigit(name) || char.IsWhiteSpace(name)))
            {
                MessageBox.Show("The item Name has invalid characters.");
                return false;
            }

            if (txtName.Text.Length >= 40)
            {
                MessageBox.Show("The item name cannot exceed 40 characters.");
                return false;
            }
            return true;
        }
        #endregion

        #region ### DESCRIPTION ###
        private bool ValidDescription()
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please enter the items description.");
                return false;
            }

            if (!txtDescription.Text.All(description => char.IsLetterOrDigit(description) || char.IsWhiteSpace(description) || char.IsPunctuation(description)))
            {
                MessageBox.Show("The items description has invalid characters.");
                return false;
            }

            if (txtDescription.Text.Length >= 256)
            {
                MessageBox.Show("The items description cannot exceed 256 characters.");
                return false;
            }
            return true;
        }
        #endregion

        #region ### MOTOR ###
        private bool ValidMotor()
        {
            if (string.IsNullOrWhiteSpace(txtMotor.Text))
            {
                MessageBox.Show("Please enter the motor.");
                return false;
            }

            if (!txtMotor.Text.All(description => char.IsLetterOrDigit(description) || char.IsWhiteSpace(description) || char.IsPunctuation(description)))
            {
                MessageBox.Show("The motor has invalid characters.");
                return false;
            }

            if (txtMotor.Text.Length >= 256)
            {
                MessageBox.Show("The motor cannot exceed 40 characters.");
                return false;
            }
            return true;
        }
        #endregion

        #region ### BATTERY ###
        private bool ValidBattery()
        {
            if (string.IsNullOrWhiteSpace(txtBattery.Text))
            {
                MessageBox.Show("Please enter the battery.");
                return false;
            }

            if (!txtDescription.Text.All(battery => char.IsLetterOrDigit(battery) || char.IsWhiteSpace(battery) || char.IsPunctuation(battery)))
            {
                MessageBox.Show("The battery has invalid characters.");
                return false;
            }

            if (txtDescription.Text.Length >= 256)
            {
                MessageBox.Show("The battery cannot exceed 40 characters.");
                return false;
            }
            return true;
        }
        #endregion

        #region ### QUANTITY ###
        private bool ValidQuantity()
        {
            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("Please enter the quantity.");
                return false;
            }

            if (!txtQuantity.Text.Trim().All(char.IsDigit))
            {
                MessageBox.Show("The order quantity contains invalid characters.");
                return false;
            }

            if (Convert.ToInt16(txtQuantity.Text) <= 0)
            {
                MessageBox.Show("Please enter a valid order quantity.");
                return false;
            }
            return true;
        }
        #endregion

        #region ### PRICE ###
        private bool ValidPrice()
        {
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Please enter the price.");
                return false;
            }

            try
            {
                Convert.ToSingle(txtPrice.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("The price contains invalid characters.");
                return false;
            }

            if (Convert.ToSingle(txtPrice.Text) <= 0)
            {
                MessageBox.Show("Please enter a valid price.");
                return false;
            }
            return true;
        }
        #endregion

        #endregion

        private void BtnDeleteImage_Click(object sender, EventArgs e)
        {
            //TODO: Delete image.
            MessageBox.Show("Not implemented!");
        }
    }
}
