using Microsoft.EntityFrameworkCore;
using ProjectEmploee.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Data.Repository
{

        public class Repository<T> : IRepository<T> where T : class
        {
            protected readonly DbSet<T> _dbSet;

            public Repository(DataContext context)
            {
                _dbSet = context.Set<T>();
            }
            public async Task<T> PostAsync(T entity)
            {
                await _dbSet.AddAsync(entity);
                return entity;
            }



            public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null)
            {
                IQueryable<T> query = _dbSet;

                if (include != null)
                {
                    query = include(query);  // אם יש פונקציית Include, נבצע אותה
                }

                return query.ToList();  // מחזירים את התוצאה כרשימה
            }

            public async Task<T?> GetByIdAsync(int id)
            {
                var t = await _dbSet.FindAsync(id);
                return t;
            }

            public async Task<T> PutAsync(int id, T entity)
            {
                var tUpdete = await _dbSet.FindAsync(id);  // חיפוש ישות לפי ID
                if (tUpdete == null)
                {
                    throw new Exception($"Entity with ID {id} not found.");  // זריקת שגיאה אם לא נמצא
                }

                // עדכון כל הערכים של הישות הקיימת עם הערכים של הישות החדשה
                _dbSet.Entry(tUpdete).CurrentValues.SetValues(entity);
            
            return tUpdete;
            }
            public async Task DeleteAsync(int id)
            {
                var tDelete = await GetByIdAsync(id);
                if (tDelete == null)
                {
                    throw new Exception($"ID {id} not found.");
                }
                _dbSet.Remove(tDelete);
            }
        }
    }

