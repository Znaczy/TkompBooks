using System;
using System.Collections.Generic;
using System.Linq;
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
                    @"SELECT * FROM dbo.Books 
                    INNER JOIN dbo.Authors ON dbo.Authors.AuthorId = dbo.Books.AuthorId
                    WHERE releaseDate = @rdate, title = @rtitle",
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
                // Me: filled
                var book = connection.Query<Book>(
                    @"SELECT * 
                    FROM dbo.Books 
                    INNER JOIN dbo.Authors ON dbo.Authors.AuthorId = dbo.Books.AuthorId
                    WHERE bookId = @bookId",
                    new
                    {
                        bookId = id
                    }).FirstOrDefault();

                return book;
            }
        }

        public void DeleteBook(int id)
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {
                // TODO: complete this method
                // Me: completed
                    var book = connection.Execute(
                        @"DELETE 
                        FROM dbo.Books
                        WHERE bookId = @bookid",
                        new
                        {
                            bookId = id
                        });
            }
        }

        public void UpdateBook(string firstName, string lastName, string title, DateTime releaseDate, int id)
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {
                // TODO: complete this method
                // Me: completed
                connection.Execute(
                    @"UPDATE dbo.Books 
                    SET Title = @Title, ReleaseDate = @ReleaseDate, AuthorId = (SELECT AuthorId FROM dbo.Authors WHERE FirstName = @FirstName AND LastName = @LastName)
                    WHERE BookID = @BookId",
                    new
                    {
                        Title = title,
                        ReleaseDate = releaseDate,
                        FirstName = firstName,
                        LastName = lastName,
                        BookId = id
                    });
            }
        }

        public void InsertBook(string firstName, string lastName, string title, DateTime releaseDate)
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {
                // TODO: complete this method
                // Me: completed
                connection.Execute(
                    @"INSERT INTO dbo.Books (Title, ReleaseDate, AuthorId) 
                    VALUES (@Title, @ReleaseDate, (SELECT AuthorId FROM dbo.Authors WHERE FirstName = @FirstName AND LastName = @LastName))",
                    new
                    {
                        Title = title,
                        ReleaseDate = releaseDate,
                        FirstName = firstName,
                        LastName = lastName
                    });
            }
        }
    }
}
