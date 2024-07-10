using BookStore.Domain.Entities;
using BookStore.Infrastructure.Context;
using BookStore.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly BookStoreContext _context;

    public BaseRepository(BookStoreContext context)
    {
        _context = context;
    }

    
    // Add the CreateAsync method
    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    
    // Add the DeleteAsync method
    public async Task<T> DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    
    // Add the SearchBooksAsync  by it  name    

    public async Task<IEnumerable<T>> SearchBooksAsync(string searchTerm)
    {
        if (typeof(T) == typeof(Book))
        {
            var books = await _context.Set<Book>()
                .Where(b => EF.Functions.Like(b.Name, $"%{searchTerm}%"))
                .ToListAsync();
            return books as IEnumerable<T>;
        }
        else
        {
            throw new NotImplementedException("Search is only implemented for books.");
        }
    }
    // Add the GetAllAsync method
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        if (typeof(T) == typeof(Book))
        {
            return await _context.Set<T>()
                .Include("Category")
                .AsNoTracking()
                .ToListAsync() as IEnumerable<T>;
        }
        else
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        return entity;
    }

    
    // Add the UpdateAsync method
    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    
    // Add the UpdateAsync method
    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}