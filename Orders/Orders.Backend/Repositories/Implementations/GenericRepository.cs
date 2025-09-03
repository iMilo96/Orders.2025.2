using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.Responses;
using System;

namespace Orders.Backend.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;

    public GenericRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ActionResponse<T>> AddAsync(T entity)
    {
        _context.Add(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    public Task<ActionResponse<T>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResponse<T>> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResponse<IEnumerable<T>>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ActionResponse<T>> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    private ActionResponse<T> ExceptionActionResponse(Exception exception) => new ActionResponse<T>
    {
        Message = exception.Message
    };

    private ActionResponse<T> DbUpdateExceptionActionResponse() => new ActionResponse<T>
    {
        Message = "Ya existe el registro."
    };
}