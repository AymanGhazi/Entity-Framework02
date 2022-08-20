// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using Entity_Framework;
using Entity_Framework.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var _context = new ApplicationDbContext();
        // var author = _context.Authors.Find(1);
        // author.LastName = "Ali";
        // var Author = new Author
        // {
        //     FirtsName = "Ayman",
        //     LastName = "Ghazi"
        // };
        //_context.Authors.Add(Author);
        // var Category = new Category
        // {
        //     Name = "action"
        // };
        var order = new OrderTest
        {
            Amount = 222558
        };

        _context.OrderTests.Add(order);
        _context.SaveChanges();

        seedData();

        // var books = (from b in _context.Books
        //              join A in _context.Authors
        //              on b.BookKey equals A.Id
        //              join n in _context.Blogs
        //              on b.BookKey equals n.Id into AuthorNationality Inner Join
        //              from an in AuthorNationality.DefaultIfEmpty() 
        //Left Join
        // select new { bookId = b.BookKey, BookName = b.Name }).ToList();
        // var Books = _context.Blogs.FromSqlRaw("SELECT * FROM Books").ToList();
        // var Books = _context.Blogs.FromSqlRaw("Exec Sp").ToList();
        //var Books = _context.BookDto.FromSqlRaw($"Exec Sp id").ToList();
        var Blog = _context.Blogs.IgnoreQueryFilters().ToList();
        using var transactions = _context.Database.BeginTransaction(); ;
        try
        {
            _context.Blogs.Add(new Blog { Url = "Test from transactions 1" });
            _context.SaveChanges();
            transactions.CreateSavepoint("First Row Inserted");
            _context.Blogs.Add(new Blog { Id = 9, Url = "Test from transactions 2" });

            _context.SaveChanges();
            transactions.Commit();
        }
        catch (System.Exception)
        {

            transactions.RollbackToSavepoint("First Row Inserted");
            transactions.Commit();
        }


        _context.Database.ExecuteSqlRaw("INSERT INTO Blogs VAlues('Tests')");

        var Name = "ايمن";
        _context.Database.ExecuteSqlRaw($"EXEC procAddBlog @Name=N '{Name}' ");
    }

    public static void seedData()
    {
        using (var context = new ApplicationDbContext())
        {
            context.Database.EnsureCreated();
            var blog = context.Blogs.FirstOrDefault(b => b.Url == "www.google.com");
            if (blog == null)
            {
                context.Blogs.Add(new Blog { Url = "www.google.com" });
            }
            context.SaveChanges();
        }

    }
}