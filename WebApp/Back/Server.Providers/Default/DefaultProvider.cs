using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Entities;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace System.Provider
{
    public class DefaultProvider
    {
        private readonly ApplicationDbContext _context;

        public DefaultProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetAsync<T>(int id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            return await _context.Set<T>().IncludeNavigations().ToListAsync();
        }



        public async Task<T> CreateAsync<T>(T entity) where T : class
        {
            await ValidateEntity(entity);

            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : class
        {
            await ValidateEntity(entity);

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync<T>(int id) where T : class
        {

            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
                throw new Exception($"{typeof(T).Name} não encontrado!");

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync<T>(int id) where T : class
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
                throw new Exception($"{typeof(T).Name} não encontrado!");

            var softDeletableEntity = entity as DefaultDb;
            if (softDeletableEntity != null)
            {
                softDeletableEntity.IsDeleted = true;
                softDeletableEntity.DeleteDate = DateTime.Now;
                _context.Entry(softDeletableEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"{typeof(T).Name} não suporta exclusão lógica!");
            }
        }


        private async Task IsDataBaseObject<T>(T entity) where T : class
        {
            if (!typeof(DefaultDb).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidDataException("This object is not Data Base Object.");
            }
        }

        private async Task ValidateEntity<T>(T entity) where T : class
        {
            await this.IsDataBaseObject(entity);

            var validateMethod = typeof(T).GetMethod("Validate");
            if (validateMethod != null)
            {
                validateMethod.Invoke(entity, null);
            }
            else
            {
                throw new InvalidOperationException($"A entidade do tipo {typeof(T).Name} não possui um método Validate.");
            }
        }
    }

}
