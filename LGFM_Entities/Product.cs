namespace LGFM_Entities
{
    public class Product
    {
        public int ProductoID { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public string Category { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PricePurchase { get; set; }
        public int Stock { get; set; }
        public string UnidadMedida { get; set; }
        public string Supplier { get; set; }
        public bool Estatus { get; set; }
        public DateTime CreationDateProduct { get; set; }
        public DateTime? DateModifiedProduct { get; set; }
    }
}
