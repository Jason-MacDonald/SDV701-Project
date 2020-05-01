using System;
using System.Collections.Generic;

namespace SDV701_Project
{
    [Serializable()]
    public class clsCategoryList : SortedList<string, clsCategory>
    {
        private const string _FileName = "inventory.xml";

        public void EditCategory(clsCategory prCategory)
        {
            prCategory.EditDetails();
        }

        public void Save()
        {
            try
            {
                System.IO.FileStream lcFileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Create);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter lcFormatter =
                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                lcFormatter.Serialize(lcFileStream, this);
                lcFileStream.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public static clsCategoryList Retrieve()
        {
            clsCategoryList lcCategoryList = new clsCategoryList();

            try
            {
                System.IO.FileStream lcFileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter lcFormatter =
                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                lcCategoryList = (clsCategoryList)lcFormatter.Deserialize(lcFileStream);
                lcFileStream.Close();
            }
            catch (Exception ex)
            {
                lcCategoryList = new clsCategoryList();

                System.Windows.Forms.MessageBox.Show(ex.Message);

                // ##### DODGY STUFF #####
                lcCategoryList.Add("Mountain E-Bikes", new clsCategory("Mountain E-bikes", "This is the description for a Mountain E-Bikes."));
                lcCategoryList.Add("Commuter E-Bikes", new clsCategory("Commuter E-Bikes", "This is the description for a Commuter E-Bikes."));
                lcCategoryList.Add("Leisure and Trail E-Bikes", new clsCategory("Leisure and Trail E-Bikes", "This is the description for Leisure and Trail E-Bikes."));
                lcCategoryList.Add("Specialty, Folding E-Bikes and Scooters", new clsCategory("Specialty, Folding E-Bikes and Scooters", "This is the description for Specialty, Folding E-Bikes and Scooters."));
            }
            return lcCategoryList;
        }
    }
}
