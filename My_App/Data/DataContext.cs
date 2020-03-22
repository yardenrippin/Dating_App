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
    }
}
