using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Books.Models.Dao
{
    public class AuthorsDAO
    {
        private readonly DbConnectionHolder _dbConnectionHolder;

        public AuthorsDAO(DbConnectionHolder dbConnectionHolder)
        {
            _dbConnectionHolder = dbConnectionHolder;
        }

        public List<Author> GetAuthors()
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {
                var authors = connection.Query<Author>("SELECT * FROM dbo.Authors").ToList();
                
                // TODO: map properties Books using JOIN
                return authors;
            }
        }

        public List<Author> GetAuthors(string firstName, string lastName)
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {
                var authors = connection.Query<Author>("SELECT * FROM dbo.Authors Where dbo.Authors.LastName = @lname, dbo.Authors.FirstName = @fname", 
                    new
                    {
                        lname = lastName,
                        fname = firstName
                    }).ToList();
                // TODO: map properties Books using JOIN

                return authors;
            }
        }

        public Author GetAuthor(int id)
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {
                var author = connection.Query<Author>("SELECT * FROM dbo.Authors Where dbo.Authors.AuthorID = @authorId",
                    new
                    {
                        authorId = id
                    }).FirstOrDefault();
                // TODO: map properties Books using JOIN
                return author;
            }
        }

        // TODO: see BooksDao.cs - Add it to project
        public Author DeleteAuthor(int id)
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {     
                var author = connection.Query<Author>("DELETE FROM dbo.Authors Where dbo.Authors.AuthorID = @authorId",
                    new
                    {
                        authorId = id
                    }).FirstOrDefault();
                return author;
            }
        }

        public Author UpdateAuthor(string firstName, string lastName, int id)
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {
                var author = connection.Query<Author>("UPDATE dbo.Authors SET dbo.Authors.FirstName = @fname, dbo.Authors.LastName = @lname Where dbo.Authors.AuthorID = @authorId",
                    new
                    {
                        lname = lastName,
                        fname = firstName,
                        authorId = id
                    }).FirstOrDefault();
                return author;
            }
        }

        public Author InsertAuthor(string firstName, string lastName)
        {
            using (var connection = _dbConnectionHolder.GetConnection())
            {
                var author = connection.Query<Author>("INSERT INTO dbo.Authors (dbo.Authors.FirstName, dbo.Authors.LastName) VALUES (@fname, @lname)",
                    new
                    {
                        lname = lastName,
                        fname = firstName,
                    }).FirstOrDefault();
                return author;
            }
        }

    }
}
