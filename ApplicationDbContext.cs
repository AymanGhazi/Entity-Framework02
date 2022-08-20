using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Entity_Framework.Configrations;
using Entity_Framework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Entity_Framework;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options) =>

        options.UseLazyLoadingProxies().UseSqlServer(@"Server=localhost;Database=EFCore;Trusted_Connection=True;", o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BlogImage> BlogImages { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderTest> OrderTests { get; set; }
    public DbSet<BookDto> BookDto { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        new BlogEntitytypeConfigrations().Configure(modelBuilder.Entity<Blog>());
        // DTO from Db Stored procedure
        // modelBuilder.Entity<BookDto>(e => { e.HasNoKey().ToView(null); });
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogEntitytypeConfigrations).Assembly);


        // public DbSet<AuditEntry> AuditEntries { get; set; }
        modelBuilder.Entity<AuditEntry>();


        //Ignore Like [Notmapped] Annotations
        // modelBuilder.Ignore<Post>();

        //Exclude from Future Migrations

        // modelBuilder.Entity<Blog>()
        // .ToTable("Blog", b => b.ExcludeFromMigrations());

        //changing the Name of table in the Database
        //1-[Table("Posts")]
        //2- modelBuilder.Entity<Post>().ToTable("Posts");


        //Changing the Schema dbo
        // 1-[Table("Posts", Schema = "Blogging")]
        // 2-  modelBuilder.Entity<Post>().ToTable("Posts", schema: "Blogging");


        //Make the schema is the Primary or default Schema
        // modelBuilder.HasDefaultSchema("Blogging"); 

        //maping to the View
        // 1- modelBuilder.Entity<Post>().ToView("SelectPosts",schema:"blogging");

        //Skip a properity
        //  1-  modelBuilder.Entity<Blog>().Ignore(b => b.AddedOn);
        // 2-    [NotMapped]

        //Rename a Field
        //1-     [Column("BlogUrl")]
        // 2- modelBuilder.Entity<Blog>().Property(b => b.Url).HasColumnName("BlogUrl");

        //changing the Type of Field
        // 1-    [Column(TypeName = "nvarchar(200)")]
        // 2-    [Column(TypeName = "decimal(5,2)")]
        // 3- modelBuilder.Entity<Blog>(eb =>
        // {
        //     eb.Property(b => b.Url).HasColumnType("nvarchar(200)");
        //     eb.Property(b => b.Rating).HasColumnType("decimal(5,2)");

        // });

        //max Length

        // 1- [MaxLength(200)]
        // 2-   modelBuilder.Entity<Blog>().Property(b => b.Url).HasMaxLength(200);

        //Comment the field
        // 1-    [Comment("the Url of the Blog")]
        // 2-  modelBuilder.Entity<Blog>().Property(b => b.Url).HasComment("Url of the blog");
        //Primary Key
        // 1-     [Key]
        //2-  modelBuilder.Entity<Book>().HasKey(b => b.BookKey);
        // the PK name 
        //3-  modelBuilder.Entity<Book>().HasKey(b => b.BookKey).HasName        ("PK_BookName");

        //Composite Key
        //  modelBuilder.Entity<Book>().HasKey(b => new { b.Name, b.Author });

        //Default values
        modelBuilder.Entity<Blog>()
        .Property(b => b.rating).HasDefaultValue(2);
        modelBuilder.Entity<Blog>()
     .Property(b => b.CreatedOn).HasDefaultValueSql("GETDATE()");

        //Computed Column
        modelBuilder.Entity<Author>().Property(a => a.DisplayName).HasComputedColumnSql("[FirtsName]+' , '+[LastName]");


        //Idenity Annotation
        //1- [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        modelBuilder.Entity<Category>().Property(c => c.Id).ValueGeneratedOnAdd();

        //One to one RelationShip

        modelBuilder.Entity<Blog>()
        .HasOne(b => b.BlogImage)
        .WithOne(b => b.Blog)
        .HasForeignKey<BlogImage>(b => b.BlogForignKey);

        //One to Many

        modelBuilder.Entity<Blog>()
        .HasMany(b => b.Posts)
        .WithOne(p => p.Blog)
        .OnDelete(DeleteBehavior.SetNull);

        // modelBuilder.Entity<Post>()
        // .HasOne(b => b.Blog)
        // .WithMany(b => b.Posts);

        // modelBuilder.Entity<Post>()
        // .HasOne<Blog>()
        // .WithMany()
        // .HasForeignKey(p => p.BlogId)
        // .HasConstraintName("FK_Posts_Test");

        //Specific prop ro make the one to may relationShip
        // modelBuilder.Entity<RecordOfSale>()
        // .HasOne(c => c.Car)
        // .WithMany(c => c.SaleHsitory)
        // .HasForeignKey(s => s.CarLicencePlate) child
        // .HasPrincipalKey(c => c.LicencePlate); 

        modelBuilder.Entity<RecordOfSale>()
       .HasOne(c => c.Car)
       .WithMany(c => c.SaleHsitory)
       .HasForeignKey(s => new { s.CarLicencePlate, s.CarState })
       .HasPrincipalKey(c => new { c.LicencePlate, c.State });

        //Many To Many
        //  1-
        // modelBuilder.Entity<Post>()
        // .HasMany(p => p.Tags)
        // .WithMany(T => T.Posts)
        // .UsingEntity(p => p.ToTable("PostTagsTest"));
        //2-
        // modelBuilder.Entity<Post>()
        // .HasMany(p => p.Tags)
        // .WithMany(t => t.Posts)
        // .UsingEntity<PostTags>(
        //     j => j.HasOne(Pt => Pt.Tag)
        //          .WithMany(T => T.PostTags)
        //          .HasForeignKey(Pt => Pt.TagId),

        //     j => j.HasOne(Pt => Pt.Post)
        //     .WithMany(P => P.PostTags)
        //     .HasForeignKey(pt => pt.PostId),
        //     j =>
        //         {
        //             j.Property(pt => pt.AddedOn).HasDefaultValueSql("GETDATE()");
        //             j.HasKey(t => new { t.PostId, t.TagId });
        //         }

        // );
        //3-
        //Indirect Many To Many
        modelBuilder.Entity<PostTags>()
        .HasKey(t => new { t.PostId, t.TagId });

        modelBuilder.Entity<PostTags>()
            .HasOne(pt => pt.Post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.PostId);

        modelBuilder.Entity<PostTags>()
           .HasOne(pt => pt.Tag)
           .WithMany(p => p.PostTags)
           .HasForeignKey(pt => pt.TagId);

        //Indexies
        // 1- [Index(nameof(Url))]
        //2-
        // modelBuilder.Entity<Blog>()
        // .HasIndex(b => b.Url);
        //Composite
        // modelBuilder.Entity<Person>()
        // .HasIndex(p => new { p.FirstName, p.lastName });

        //Unique Index
        // [Index(nameof(Url), IsUnique = true)]
        // modelBuilder.Entity<Blog>()
        // .HasIndex(b => b.Url)
        // .IsUnique();

        //Name
        //  [Index(nameof(Url), IsUnique = true, Name = "URL_Index")]
        // modelBuilder.Entity<Blog>()
        // .HasIndex(b => b.Url)
        // .HasDatabaseName("Index_Url_Test");

        //IndexFilter  or cluster
        // modelBuilder.Entity<Blog>()
        // .HasIndex(b => b.Url)
        // //.HasFilter("[Url] is not null")
        // .IsUnique()
        // .HasFilter(null); // will not filter nulls

        //SeQuence
        // modelBuilder.HasSequence<int>("OrderNumber", schema: "Shared")
        // .StartsAt(10)
        // .IncrementsBy(2);
        // modelBuilder.Entity<Order>()
        // .Property(o => o.OrderNo)
        // .HasDefaultValueSql("NEXT VALUE FOR Shared.OrderNumber");
        // modelBuilder.Entity<OrderTest>()
        // .Property(o => o.OrderNo)
        // .HasDefaultValueSql("NEXT VALUE FOR Shared.OrderNumber");

        ///seeding 

        //     modelBuilder.Entity<Blog>()
        //     .HasData(new Blog { Id = 1, Url = "www.google.com" });
        //     modelBuilder.Entity<Post>()
        //    .HasData(new Post { Id = 2, BlogId = 1, Title = "Post1", Content = "Post1" });
        //     modelBuilder.Entity<Post>()
        //     .HasData(new Post { Id = 1, BlogId = 1, Title = "Post2", Content = "Post2" });

        modelBuilder.Entity<Post>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Post>().HasQueryFilter(p => p.Tags.Count > 0);

    }
}
