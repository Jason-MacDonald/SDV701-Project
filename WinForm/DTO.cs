namespace WinForm
{
    public class clsCategory
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class clsItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string ModifiedDate { get; set; }
        public int Quantity { get; set; }
        public string Motor { get; set; }
        public string Battery { get; set; }
        public int WarrantyPeriod { get; set; }
        public string Condition { get; set; }
        public string Type { get; set; }       
    }

    public class clsOrder
    {
        public int InvoiceNumber { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

