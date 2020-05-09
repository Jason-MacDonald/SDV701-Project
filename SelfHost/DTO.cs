namespace SelfHost
{
    public class clsCategory
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class clsItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string ModifiedDate { get; set; }
        public int Quantity { get; set; }
        public string Motor { get; set; }
        public int WarrantyPeriod { get; set; }
        public string Condition { get; set; }
        public string Type { get; set; }

    }

}

