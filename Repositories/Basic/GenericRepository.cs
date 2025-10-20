using Microsoft.EntityFrameworkCore;

namespace Repositories.Basic
{
    public class GenericRepository<T> where T : class
    {
        protected DemoDbContext _context;

        //public GenericRepository(ApplicationDbContext context)
        //{
        //    _context ??= new ApplicationDbContext();
        //}

        public GenericRepository(DemoDbContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public async Task<int> CreateAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            //// Turning off Tracking for UpdateAsync in Entity Framework
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            try
            {
                // Get primary key dynamically
                var keyValues = _context.Model.FindEntityType(typeof(T))
                                ?.FindPrimaryKey()
                                ?.Properties
                                ?.Select(p => p.PropertyInfo.GetValue(entity))
                                .ToArray();

                if (keyValues == null || keyValues.Length == 0)
                    throw new InvalidOperationException("No primary key defined for entity.");

                // Fetch existing entity without tracking
                var existingEntity = await _context.Set<T>().FindAsync(keyValues);

                if (existingEntity == null) return 0;

                _context.Entry(existingEntity).State = EntityState.Detached; // ✅ Prevent tracking conflicts
                _context.Entry(entity).State = EntityState.Modified; // ✅ Mark for update

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool Remove(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        //public async Task<bool> RemoveAsync(T entity)
        //{
        //    _context.Remove(entity);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}
        public async Task<bool> RemoveAsync(T entity)
        {
            var tracked = _context.ChangeTracker.Entries<T>()
                                  .FirstOrDefault(e => e.Entity.Equals(entity));

            if (tracked != null)
            {
                if (!ReferenceEquals(tracked.Entity, entity))
                {
                    tracked.State = EntityState.Detached;
                }
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }


        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T GetById(string code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(string code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        public T GetById(Guid code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(Guid code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        #region Separating asigned entity and save operators        

        public void PrepareCreate(T entity)
        {
            _context.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _context.Remove(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion Separating asign entity and save operators
    }
}
