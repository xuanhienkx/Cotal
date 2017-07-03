using System.IO;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cotal.App.Data.Contexts
{
  public class CotalContex : EntityContextBase<CotalContex>
  {
    public CotalContex() : base(new DbContextOptions<CotalContex>())
    {
    }

    public CotalContex(DbContextOptions<CotalContex> options) : base(options)
    {
    }

    public DbSet<Footer> Footers { set; get; }

    //public DbSet<Order> Orders { set; get; }
    //public DbSet<OrderDetail> OrderDetails { set; get; }
    public DbSet<Page> Pages { set; get; }

    public DbSet<Post> Posts { set; get; }
    public DbSet<PostCategory> PostCategories { set; get; }

    public DbSet<PostTag> PostTags { set; get; }
    //public DbSet<Product> Products { set; get; }

    //public DbSet<ProductCategory> ProductCategories { set; get; }
    //public DbSet<ProductTag> ProductTags { set; get; }
    public DbSet<Slide> Slides { set; get; }

    public DbSet<SupportOnline> SupportOnlines { set; get; }
    public DbSet<SystemConfig> SystemConfigs { set; get; }

    public DbSet<Tag> Tags { set; get; }

    public DbSet<VisitorStatistic> VisitorStatistics { set; get; }
    public DbSet<Error> Errors { set; get; }
    public DbSet<ContactDetail> ContactDetails { set; get; }
    public DbSet<Feedback> Feedbacks { set; get; }

    public DbSet<Function> Functions { set; get; }
    public DbSet<Permission> Permissions { set; get; }


    //public DbSet<Color> Colors { set; get; }
    //public DbSet<Size> Sizes { set; get; }
    public DbSet<ProductQuantity> ProductQuantities { set; get; }

    public DbSet<ProductImage> ProductImages { set; get; }

    public DbSet<Announcement> Announcements { set; get; }
    public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      // get the configuration from the app settings
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      // define the database to use
      optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
    }
  }
}