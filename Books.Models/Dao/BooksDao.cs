using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Books.Models.Dao
{
    // TODO: add/include this file to project
    // Me: included
    public class BooksDao
    {
        private readonly DbConnectionHolder _dbConnectionHolder;

        public BooksDao(DbConnectionHolder dbConnectionHolder)
        {
            _dbConnectionHolder = dbConnectionHolder;
        }

        public List<Book> GetBooks()
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {
                var books = connection.Query<Book>("SELECT * FROM dbo.Books INNER JOIN dbo.Authors ON dbo.Authors.AuthorId = dbo.Books.AuthorId").ToList();
                return books;
            }
        }

        public List<Book> GetBooks(string title, DateTime releaseDate)
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {
                // TODO: fill with query parameters
                // Me: filled
                var books = connection.Query<Book>(
                    @"SELECT * FROM dbo.Books " +
                    "INNER JOIN dbo.Authors ON dbo.Authors.AuthorId = dbo.Books.AuthorId " +
                    "Where releaseDate = @rdate, title = @rtitle",
                    new
                    {
                        rdate = releaseDate,
                        rtitle = title
                    }).ToList();

                return books;
            }
        }

        public Book GetBook(int id)
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {
                // TODO:fill with query parameters
                var book = connection.Query<Book>("SELECT * FROM dbo.Books " +
                    "INNER JOIN dbo.Authors ON dbo.Authors.AuthorId = dbo.Books.AuthorId",
                    new
                    {
                        bookId = id
                    }).FirstOrDefault();

                return book;
            }
        }

        public Book DeleteBook(int id)
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {
                // TODO: complete this method
                    var book = connection.Query<Book>("DELETE FROM dbo.Books",
                        new
                        {
                            
                        }).FirstOrDefault();
                    return book;
            }
        }

        public Book UpdateBook(string firstName, string lastName, string title, string releaseDate, int id)
        {

            using (var connection = _dbConnectionHolder.GetConnection())
            {
                // TODO: complete this method
                var book = connection.Query<Book>("UPDATE dbo.Books SET dbo.Books.Title = @title, dbo.Books.ReleaseDate = @relDate, dbo.Books.AuthorId = (SELECT dbo.Authors.AuthorId FROM dbo.Authors WHERE dbo.Authors.FirstName = @fname AND dbo.Authors.LastName = @lname) Where dbo.Books.BookID = @bookId",
                    new
                    {
                        
                    }).FirstOrDefault();
                return book;
            }
        }

        public Book InsertBook(string firstName, string lastName, string title, string releaseDate)
        {

            using (var connection = _dbConnectionHolder.GetConnection())
            {
                // TODO: complete this method
                var book = connection.Query<Book>("INSERT INTO dbo.Books (dbo.Books.Title, dbo.Books.ReleaseDate, dbo.Books.AuthorId) VALUES (@title, @relDate, (SELECT dbo.Authors.AuthorId FROM dbo.Authors WHERE dbo.Authors.FirstName = @fname AND dbo.Authors.LastName = @lname))",
                    new
                    {
                        Title = title
                    }).FirstOrDefault();
                    return book;
            }
        }
    }
}
