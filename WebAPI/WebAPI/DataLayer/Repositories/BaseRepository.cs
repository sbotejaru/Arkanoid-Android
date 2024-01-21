// <copyright file="BaseDataServices.cs" company="University Transilvania of Brasov">
// Copyright (c) Stefan Andrei Botejaru. All rights reserved.
// </copyright>

namespace DataLayer.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataLayer.Entities;

    /// <summary>
    /// BaseDataServices class.
    /// </summary>
    /// <typeparam name="T">The class that the base should work with.</typeparam>
    public class BaseRepository<T>
        where T : class
    {
        /// <summary>
        /// Adds the given entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(T entity)
        {
            using (var ctx = new GameContext())
            {
                var dbSet = ctx.Set<T>();
                dbSet.Add(entity);

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the given entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(T entity)
        {
            using (var ctx = new GameContext())
            {
                var dbSet = ctx.Set<T>();
                dbSet.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the entity with the given identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            this.Delete(this.GetById(id));
        }

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            using (var ctx = new GameContext())
            {
                var dbSet = ctx.Set<T>();

                if (ctx.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }

                dbSet.Remove(entity);

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity with the given identifier.</returns>
        public T GetById(int id)
        {
            using (var ctx = new GameContext())
            {
                return ctx.Set<T>().Find(id);
            }
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>The list of entities.</returns>
        public IList<T> GetAll()
        {
            using (var ctx = new GameContext())
            {
                return ctx.Set<T>().ToList();
            }
        }
    }
}
