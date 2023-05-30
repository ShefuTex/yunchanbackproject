using Microsoft.EntityFrameworkCore;

namespace yunchanbackproject.Entities
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Carrito> Carrito { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.nombre)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.apellido)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.correo)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.telefono)
                    .IsRequired()
                    .HasMaxLength(45);
                entity.Property(e => e.password)
                .IsRequired()
                .HasMaxLength(45);
                entity.Property(e => e.perfil)
                .IsRequired()
                .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("productos");
                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.nombre)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.descriopcion)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.precio)
                    .IsRequired()
                    .HasColumnType("decimal");

                entity.Property(e => e.imagen)
                    .IsRequired()
                    .HasMaxLength(45);
                entity.Property(e => e.stok)
                .IsRequired()
                .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Carrito>(entity =>
            {
                entity.ToTable("carrito");
                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.nombre)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.precio)
                    .IsRequired()
                    .HasColumnType("decimal");

                entity.Property(e => e.imagen)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.cantidad)
                .IsRequired()
                .HasColumnType("int(11)");

                entity.Property(e => e.usuarioId)
                .IsRequired()
                .HasColumnType("int(11)");

                entity.Property(e => e.nombreUsuario)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.comprado)
            .IsRequired()
            .HasColumnType("int(1)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}