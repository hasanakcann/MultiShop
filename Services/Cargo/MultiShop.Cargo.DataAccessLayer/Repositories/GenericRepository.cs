using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;

namespace MultiShop.Cargo.DataAccessLayer.Repositories;

public class GenericRepository<T> : IGenericDal<T> where T : class
{
    private readonly CargoContext _context;

    public GenericRepository(CargoContext context)
    {
        _context = context;
    }

    public void Delete(int id)
    {
        try
        {
            var existingEntity = _context.Set<T>().Find(id);
            if (existingEntity == null)
                throw new InvalidOperationException($"No entity found with ID {id}.");

            _context.Set<T>().Remove(existingEntity);
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("A database update error occurred while deleting the entity.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while deleting the entity.", ex);
        }
    }

    public List<T> GetAll()
    {
        try
        {
            return _context.Set<T>().ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving all entities.", ex);
        }
    }

    public T GetById(int id)
    {
        try
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
                throw new InvalidOperationException($"No entity found with ID {id}.");

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the entity by ID.", ex);
        }
    }

    public void Insert(T entity)
    {
        try
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity to insert cannot be null.");

            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("A database update error occurred while inserting the entity.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while inserting the entity.", ex);
        }
    }

    public void Update(T entity)
    {
        try
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity to update cannot be null.");

            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("A database update error occurred while updating the entity.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while updating the entity.", ex);
        }
    }
}
