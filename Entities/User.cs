
namespace yunchanbackproject.Entities
{
    public partial class User
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string password { get; set; }
        public int perfil { get; set; }
    }
}