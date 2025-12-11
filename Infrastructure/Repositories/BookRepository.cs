using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly MongoDbContext _context;

        public BookRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _context.Books.InsertOneAsync(book);
            return book;
        }

        public async Task DeleteBookAsync(int id)
        {
            await _context.Books.DeleteOneAsync(b => b.Id == id);
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            // FindAsync returns IAsyncCursor<Book>, need ToListAsync
            return await _context.Books.Find(_ => true).ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _context.Books.ReplaceOneAsync(b => b.Id == book.Id, book);
        }
    }
}
