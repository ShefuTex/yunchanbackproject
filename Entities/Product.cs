namespace yunchanbackproject.Entities
{
    public partial class Product
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public string descriopcion { get; set; }
        public decimal precio { get; set; }
        public string imagen { get; set; }
        public int stok { get; set; }
    }
}