using System;
using System.Collections.Generic;

namespace SDV701_Project
{
    [Serializable()]
    public class clsCategoryList : SortedList<string, clsCategory>
    {
        private const string _FileName = "inventory.xml";

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
            catch { }
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
            catch
            {
                lcCategoryList = new clsCategoryList();
                System.Windows.Forms.MessageBox.Show("Test");
            }
            return lcCategoryList;
        }
    }
}
