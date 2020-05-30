using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class frmItem : Form
    {
        #region ##### VARIABLES #####   
        /// <summary>
        /// Using var as comparing image as either Images or Byte[]'s doesn't seem to work. 
        /// Ensure set to false when form is hidden. - Hide()
        /// </summary>
        private bool uploadedImage = false;

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

            if (Item.Name != null)
                txtName.Enabled = false;
            else
                txtName.Enabled = true;

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
                MessageBox.Show(ex.GetBaseException().Message);
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
                MessageBox.Show(ex.GetBaseException().Message);
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
                    uploadedImage = true;
                }
            }
        }

        private byte[] ConvertImageToBinary(Image prImage)
        {
            using(MemoryStream lcMemoryStream = new MemoryStream())
            {
                prImage.Save(lcMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return lcMemoryStream.ToArray();
            }
        }

        private Image ConvertBinaryToImage(byte[] prImageData)
        {
            using(MemoryStream lcMemoryStream = new MemoryStream(prImageData))
            {
                return Image.FromStream(lcMemoryStream);
            }
        }

        private async void BtnSaveAndClose_Click(object sender, EventArgs e)
        {
            // TODO: Should only save if changed as modified date is getting updated even when no changes are made.
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
                    frmCategory.Instance.UpdateForm();

                    uploadedImage = false;
                    Hide();
                }
            }
            else
            {
                MessageBox.Show("No changes made.");
                uploadedImage = false;
                Hide();
            }

            
        }

        private void BtnCloseWithoutSaving_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close without saving? Any changes you have made will not be saved.", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                uploadedImage = false;
                Hide();
            }
        }

        private void FrmItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                uploadedImage = false;
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
                                    return true;
                                }
                            }
                        }
                    }
                }               
            }           
            return false;
        }

        private bool HasChanged()
        {
            if (
                txtName.Text != Item.Name ||
                txtDescription.Text != Item.Description ||
                txtMotor.Text != Item.Motor ||
                txtBattery.Text != Item.Battery ||
                Convert.ToInt32(txtQuantity.Text) != Item.Quantity ||
                float.Parse(txtPrice.Text) != Item.Price ||

                uploadedImage
            )
            {
                MessageBox.Show("Changed");
                return true;
            }
            MessageBox.Show("No change");
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

            try
            {
                picImage.Image = ConvertBinaryToImage(Item.Image);
            }
            catch (Exception)
            {
                // pass
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


    }
}
