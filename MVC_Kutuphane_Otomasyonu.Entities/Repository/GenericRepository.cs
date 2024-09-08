using MVC_Kutuphane_Otomasyonu.Entities.DAL;
using MVC_Kutuphane_Otomasyonu.Entities.Interfaces;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using MVC_Kutuphane_Otomasyonu.Entities.Validations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Repository
{
    public class GenericRepository<TContext, TEntity> : IGnereicRepository<TContext, TEntity>
        where TContext : DbContext, new()
        where TEntity : class, new()
    {
        public void Delete(TContext context, Expression<Func<TEntity, bool>> filter)
        {
            var Model = context.Set<TEntity>().FirstOrDefault(filter);
             context.Set<TEntity>().Remove(Model);
        }
        public List<TEntity> GetAll(TContext context, Expression<Func<TEntity, bool>> filter = null ,params string[] tbl) 
        {

            IQueryable<TEntity> query = context.Set<TEntity>();

            foreach (var item in tbl)//Nesne başvurusu bir nesnenin örneğine ayarlanmadı.' Local1 was null.

            {
                if (filter != null)
                {
                    query = query.Where(filter).Include(item);

                }
                else
                {
                    query = query.Include(item);

                }
            }
            return query.ToList();

            
        }  
        public TEntity GetByFilter(TContext context, Expression<Func<TEntity, bool>> filter,params string[] tbl)
        {

            IQueryable<TEntity> query = context.Set<TEntity>();
            foreach (var item in tbl)
            {
                query = query.Include(item);
            }
            return query.FirstOrDefault(filter);

        }
        public TEntity GetById(TContext context, int? id)
        {
            return context.Set<TEntity>().Find(id);
        }
        public void InsertorUpdate(TContext context, TEntity entity)
        {
            context.Set<TEntity>().AddOrUpdate(entity);
        }
        public void Save(TContext context)
        {
            context.SaveChanges();
        }
    }
}
