using Microsoft.EntityFrameworkCore;
using My_App.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
       public DbSet<Value> values { get; set; }
       public DbSet<User> Users { get; set; }
       public DbSet<photo> photos { get; set; }
       public DbSet<Likes> Likes { get; set; }
       public DbSet<Message> Messages { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Likes>()
                .HasKey(X => new {X.LikerId,X.LikeeId });

            builder.Entity<Likes>()
                .HasOne(x => x.Likee)
                .WithMany(x => x.Likers)
                .HasForeignKey(x => x.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Likes>()
                .HasOne(x => x.Liker)
                .WithMany(x => x.Likees)
                .HasForeignKey(x => x.LikerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(X => X.Sender)
                .WithMany(M => M.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(X => X.Recipient)
                .WithMany(M => M.MessagesRecived)
                .OnDelete(DeleteBehavior.Restrict);

        }


  
    }
}
