﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Interfaces
{
    public interface IGnereicRepository<TContext,TEntity>
        where TContext:DbContext,new()
        where TEntity : class ,new()
    {
        List<TEntity> GetAll(TContext context,Expression<Func<TEntity,bool>> filter=null,params string[] tbl);//Filtre Null ise Tüm Listeyi Getir
        TEntity GetByFilter(TContext context,Expression<Func<TEntity,bool>> filter,params string[] tbl); //Tek Kayıt Getir

        TEntity GetById(TContext context, int? id);

        void InsertorUpdate(TContext context,TEntity entity);

        void Delete(TContext context, Expression<Func<TEntity, bool>> filter);

        void Save(TContext context);
                                                                                          
    }
}
