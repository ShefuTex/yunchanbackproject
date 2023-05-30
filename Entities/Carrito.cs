namespace yunchanbackproject.Entities
{
    public partial class Carrito
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int precio { get; set; }
        public string imagen { get; set; }
        public int cantidad { get; set; }
        public int usuarioId { get; set; }
        public string nombreUsuario { get; set; }
        public int comprado { get; set; }

        
    }
}