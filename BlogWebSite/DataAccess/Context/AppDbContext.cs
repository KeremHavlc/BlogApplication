﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Community> Communities { get; set; }
        public DbSet<CommunityPost> CommunityPosts { get; set; }
        public DbSet<CommunityComment> CommunityComments { get; set; }
        public DbSet<CommunityUser> CommunityUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommunityUser>()
                .HasKey(cu => new { cu.CommunityId, cu.UserId }); 

            modelBuilder.Entity<CommunityUser>()
                .HasOne(cu => cu.Community)
                .WithMany(c => c.CommunityUsers)
                .HasForeignKey(cu => cu.CommunityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommunityUser>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.CommunityUsers)
                .HasForeignKey(cu => cu.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            // User → Post (Cascade)
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User → Comment (Restrict - Daha güvenli)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Kullanıcı silindiğinde yorumlar silinmesin.

            // User → PostLike (Restrict - Daha güvenli)
            modelBuilder.Entity<PostLike>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Kullanıcı silindiğinde beğeniler silinmesin.

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

            // CommunityComment → CommunityPost (Cascade)
            modelBuilder.Entity<CommunityComment>()
                .HasOne(cc => cc.CommunityPost)
                .WithMany(cp => cp.CommunityComments)
                .HasForeignKey(cc => cc.CommunityPostId)
                .OnDelete(DeleteBehavior.Cascade);  // Post silindiğinde comment'lar silinsin.

            // CommunityComment → User (Restrict)
            modelBuilder.Entity<CommunityComment>()
                .HasOne(cc => cc.User)
                .WithMany(u => u.CommunityComments)
                .HasForeignKey(cc => cc.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Kullanıcı silindiğinde yorumlar silinmesin.

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasColumnType("varbinary(max)");

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordSalt)
                .HasColumnType("varbinary(max)");

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
