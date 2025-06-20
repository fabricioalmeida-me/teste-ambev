﻿using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of UserRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
    
    /// <summary>
    /// Get all users from the repository
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task<(IEnumerable<User> Users, int TotalItems, int TotalPages)> GetAllAsync(
        int page,
        int size,
        string? orderBy,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Users.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            var orderParams = orderBy.Split(',', StringSplitOptions.RemoveEmptyEntries);
            bool first = true;
            foreach (var param in orderParams)
            {
                var trimmed = param.Trim();
                var descending = trimmed.EndsWith(" desc", StringComparison.OrdinalIgnoreCase);
                var property = trimmed.Replace(" desc", "", StringComparison.OrdinalIgnoreCase)
                    .Replace(" asc", "", StringComparison.OrdinalIgnoreCase);

                if (first)
                {
                    query = descending ? query.OrderByDescendingDynamic(property)
                        : query.OrderByDynamic(property);
                    first = false;
                }
                else
                {
                    query = descending ? ((IOrderedQueryable<User>)query).ThenByDescendingDynamic(property)
                        : ((IOrderedQueryable<User>)query).ThenByDynamic(property);
                }
            }
        }

        var totalItems = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalItems / (double)size);

        var users = await query.Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return (users, totalItems, totalPages);
    }

    
    /// <summary>
    /// Update a user from the repository
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task<User?> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id, cancellationToken);
        if (existing is null)
            return null;

        _context.Entry(existing).CurrentValues.SetValues(user);
        
        existing.Username = user.Username;
        existing.Email = user.Email;
        existing.Phone = user.Phone;
        existing.Status = user.Status;
        existing.Role = user.Role;
        existing.UpdatedAt = DateTime.UtcNow;
        existing.Password = user.Password;
        existing.Address = user.Address;
        existing.Name = user.Name;

        await _context.SaveChangesAsync(cancellationToken);
        return existing;
    }
}
