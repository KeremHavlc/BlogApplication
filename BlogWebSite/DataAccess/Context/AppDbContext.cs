using Microsoft.EntityFrameworkCore;
using Entity.Concrete;

namespace DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-HEK9VG2;Database=BlogAppDb;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<FriendShip> FriendShips { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User → Post (Cascade)
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User → Comment (Cascade)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            // User → PostLike (Cascade)
            modelBuilder.Entity<PostLike>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            // Post → Comment (Cascade)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Post → PostLike (Cascade)
            modelBuilder.Entity<PostLike>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // User → FriendShip.Sender (Cascade)
            modelBuilder.Entity<FriendShip>()
                .HasOne(f => f.Sender)
                .WithMany(u => u.SentFriendships)
                .HasForeignKey(f => f.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            // User → FriendShip.Receiver (Restrict)
            modelBuilder.Entity<FriendShip>()
                .HasOne(f => f.Receiver)
                .WithMany(u => u.ReceivedFriendships)
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

            //Cascade / Resctrict / ClientCascade
            
            //Cascade: Silinen verinin bağlı olduğu verileri de siler.
            
            //Restrict: Silinen verinin bağlı olduğu verileri silmez, hata verir.
            
            //ClientCascade: Silinen verinin bağlı olduğu verileri siler, ancak veritabanında hata verir.
            //Bu durumda, veritabanı işlemi başarısız olur, ancak uygulama tarafında işlem devam eder.
            //(Uygulama Tarafında dediği kısım EfCore bu işi halleder.)
            //Bu nedenle, bu tür bir silme işlemi genellikle önerilmez ve dikkatli kullanılmalıdır.
        }
    }
    
}
