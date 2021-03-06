﻿
namespace NJFairground.Web.Data.Implementation.Base
{
    #region Required Namespace(s)
    using AutoMapper;
    using NJFairground.Web.Data.Interface.Base;
    using NJFairground.Web.Models.Base;
    using NJFairground.Web.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    #endregion

    /// <summary>
    /// Repository base class
    /// </summary>
    /// <typeparam name="TModel">The type of underlying entity in this repository</typeparam>
    public class DataRepository<TEntity, TModel> : IRepository<TModel>
        where TModel : BaseModel
        where TEntity : class
    {
        #region Members
        private readonly IQueryableUnitOfWork _UnitOfWork;
        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public DataRepository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");

            _UnitOfWork = unitOfWork;
        }
        #endregion

        #region IRepository Members
        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
        }

        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public virtual TModel Get(int id)
        {
            try
            {
                if (id > 0)
                {
                    var entity = this.GetEntity().Find(id);
                    return Mapper.Map<TEntity, TModel>(entity);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(id);
            }
            return default(TModel);
        }

        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public virtual TModel Get(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var entity = this.GetEntity().Find(id);
                    return Mapper.Map<TEntity, TModel>(entity);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(id);
            }
            return default(TModel);
        }

        #region GetCount overloaded function
        /// <summary>
        /// Gets the count of an Entity.
        /// </summary>
        /// <returns></returns>
        public virtual int GetCount()
        {
            try
            {
                return this.GetEntity().Count();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return 0;
        }

        /// <summary>
        /// Gets the count of an Entity.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual int GetCount(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                Expression<Func<TEntity, bool>> entityFilterExpression = filter.RemapForType<TModel, TEntity, bool>();
                return this.GetEntity().Where(entityFilterExpression.Compile()).Count();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(filter);
            }
            return 0;
        }
        #endregion

        #region GetList overloaded functions
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TModel> GetList()
        {
            try
            {
                var entities = this.GetEntity();
                return Mapper.Map<IQueryable<TEntity>, IEnumerable<TModel>>(entities).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return GetList();
        }

        /// <summary>
        /// Gets the filtered.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual IQueryable<TModel> GetList(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                Expression<Func<TEntity, bool>> entityFilterExpression = filter.RemapForType<TModel, TEntity, bool>();
                var entities = this.GetEntity().Where(entityFilterExpression.Compile());
                return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(filter);
            }
            return GetList();
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual IQueryable<TModel> GetList(int pageIndex, int pageCount, Expression<Func<TModel, bool>> filter)
        {
            try
            {
                Expression<Func<TEntity, bool>> entityFilterExpression = filter.RemapForType<TModel, TEntity, bool>();

                var entities = this.GetEntity().Where(entityFilterExpression.Compile())
                    .Skip(pageCount * (pageIndex - 1)).Take(pageCount);

                return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(pageIndex, pageCount, filter);
            }
            return GetList();
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="ascending">The ascending.</param>
        /// <returns></returns>
        public virtual IQueryable<TModel> GetList<KProperty>(Expression<Func<TModel, bool>> filter,
            Expression<Func<TModel, KProperty>> orderByExpression, bool ascending)
        {
            try
            {
                Expression<Func<TEntity, bool>> entityFilterExpression = filter.RemapForType<TModel, TEntity, bool>();
                Expression<Func<TEntity, KProperty>> entityOrderExpression = orderByExpression.RemapForType<TModel, TEntity, KProperty>();

                var entities = this.GetEntity().Where(entityFilterExpression.Compile());
                IEnumerable<TEntity> orderedEntities;

                if (ascending)
                    orderedEntities = entities.OrderBy(entityOrderExpression.Compile());
                else
                    orderedEntities = entities.OrderByDescending(entityOrderExpression.Compile());

                return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(orderedEntities).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(filter, orderByExpression, ascending);
            }
            return GetList();
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="ascending">The ascending.</param>
        /// <returns></returns>
        public virtual IQueryable<TModel> GetList<KProperty>(int pageIndex, int pageCount, Expression<Func<TModel, bool>> filter,
            Expression<Func<TModel, KProperty>> orderByExpression, bool ascending)
        {
            try
            {
                Expression<Func<TEntity, bool>> entityFilterExpression = filter.RemapForType<TModel, TEntity, bool>();
                Expression<Func<TEntity, KProperty>> entityOrderExpression = orderByExpression.RemapForType<TModel, TEntity, KProperty>();

                var entities = this.GetEntity().Where(entityFilterExpression.Compile());
                IEnumerable<TEntity> orderedEntities;

                if (ascending)
                    orderedEntities = entities.OrderBy(entityOrderExpression.Compile()).Skip(pageCount * (pageIndex - 1)).Take(pageCount);
                else
                    orderedEntities = entities.OrderByDescending(entityOrderExpression.Compile()).Skip(pageCount * (pageIndex - 1)).Take(pageCount);

                return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(orderedEntities).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(pageIndex, pageCount, filter, orderByExpression, ascending);
            }
            return GetList();
        }

        /// <summary>
        /// Gets the paged.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns></returns>
        public virtual IQueryable<TModel> GetList<KProperty>
            (int pageIndex, int pageCount, Expression<Func<TModel, KProperty>> orderByExpression, bool ascending)
        {
            try
            {
                Expression<Func<TEntity, KProperty>> entityOrderExpression = orderByExpression.RemapForType<TModel, TEntity, KProperty>();

                var entities = this.GetEntity();
                IEnumerable<TEntity> orderedEntities;

                if (ascending)
                    orderedEntities = entities.OrderBy(entityOrderExpression).Skip(pageCount * (pageIndex - 1)).Take(pageCount);
                else
                    orderedEntities = entities.OrderByDescending(entityOrderExpression).Skip(pageCount * (pageIndex - 1)).Take(pageCount);

                return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(orderedEntities).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(pageIndex, pageCount, orderByExpression, ascending);
            }
            return GetList();
        }
        #endregion

        #region Insert/Update/Delete operation
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="doSaveChanges">if set to <c>true</c> [do save changes].</param>
        public virtual void Insert(TModel item, bool doSaveChanges = true)
        {
            if (item != (TModel)null)
            {
                TEntity entity = Mapper.Map<TModel, TEntity>(item);
                try
                {
                    this.GetEntity().Add(entity); // add new item in this set
                    this._UnitOfWork.SetInserted(entity);

                    if (doSaveChanges)
                    {
                        this._UnitOfWork.CommitAndRefreshChanges();
                        try
                        {
                            this.SetId(entity, item);
                        }
                        catch (Exception ex)
                        {
                            ex.ExceptionValueTracker(item, doSaveChanges);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this._UnitOfWork.RollbackChanges();
                    ex.ExceptionValueTracker(item, doSaveChanges);
                }
            }
        }

        /// <summary>
        /// Adds the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        public virtual void Insert(IEnumerable<TModel> items)
        {
            try
            {
                foreach (TModel item in items)
                    this.Insert(item, false);

                this._UnitOfWork.CommitAndRefreshChanges();
            }
            catch (Exception ex)
            {
                this._UnitOfWork.RollbackChanges();
                ex.ExceptionValueTracker(items);
            }
        }

        /// <summary>
        /// Modifies the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="doSaveChanges">if set to <c>true</c> [do save changes].</param>
        public virtual void Update(TModel item, bool doSaveChanges = true)
        {
            if (item != (TModel)null)
            {
                TEntity entity = Mapper.Map<TModel, TEntity>(item);

                try
                {
                    DbEntityEntry<TEntity> entry = this.TrackEntry(entity);

                    if (entry.State == System.Data.Entity.EntityState.Detached)
                    {
                        var set = this.GetEntity();
                        TEntity attachedEntity = set.Find(GetEntityKeyValue(entity));  // You need to have access to key

                        if (attachedEntity != null)
                        {
                            var attachedEntry = this.TrackEntry(attachedEntity);
                            this._UnitOfWork.ApplyCurrentValues(attachedEntity, entity);
                        }
                        else
                            entry.State = System.Data.Entity.EntityState.Modified; // This should attach entity
                    }

                    if (doSaveChanges)
                    {
                        this._UnitOfWork.CommitAndRefreshChanges();
                    }
                }
                catch (Exception ex)
                {
                    this._UnitOfWork.RollbackChanges();
                    ex.ExceptionValueTracker(item, doSaveChanges);
                }
            }
        }

        /// <summary>
        /// Modifies the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        public virtual void Update(IEnumerable<TModel> items)
        {
            try
            {
                foreach (TModel item in items)
                    this.Update(item, false);

                this._UnitOfWork.CommitAndRefreshChanges();
            }
            catch (Exception ex)
            {
                this._UnitOfWork.RollbackChanges();
                ex.ExceptionValueTracker(items);
            }
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="doSaveChanges">if set to <c>true</c> [do save changes].</param>
        public virtual void Delete(TModel item, bool doSaveChanges = true)
        {
            if (item != (TModel)null)
            {
                var entity = Mapper.Map<TModel, TEntity>(item);
                try
                {
                    DbEntityEntry<TEntity> entry = this.TrackEntry(entity);

                    if (entry.State == System.Data.Entity.EntityState.Detached)
                    {
                        var set = this.GetEntity();
                        TEntity attachedEntity = set.Find(GetEntityKeyValue(entity));  // You need to have access to key

                        if (attachedEntity != null)
                        {
                            var attachedEntry = this.TrackEntry(attachedEntity);
                            attachedEntry.State = System.Data.Entity.EntityState.Deleted;
                            GetEntity().Remove(attachedEntity);
                        }
                        else
                            entry.State = System.Data.Entity.EntityState.Deleted; // This should attach entity
                    }

                    if (doSaveChanges)
                    {
                        this._UnitOfWork.CommitAndRefreshChanges();
                    }
                }
                catch (Exception ex)
                {
                    this._UnitOfWork.RollbackChanges();
                    ex.ExceptionValueTracker(item, doSaveChanges);
                }
            }
        }

        /// <summary>
        /// Removes the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        public virtual void Delete(IEnumerable<TModel> items)
        {
            try
            {
                foreach (TModel item in items)
                    this.Delete(item, false);

                this._UnitOfWork.CommitAndRefreshChanges();
            }
            catch (Exception ex)
            {
                this._UnitOfWork.RollbackChanges();
                ex.ExceptionValueTracker(items);
            }
        }
        #endregion

        /// <summary>
        /// Tracks the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void TrackItem(TModel item)
        {
            try
            {
                if (item != (TModel)null)
                {
                    TEntity entity = Mapper.Map<TModel, TEntity>(item);
                    this.GetEntity().Attach(entity);
                    this._UnitOfWork.SetUnchanged<TEntity>(entity);
                    this._UnitOfWork.CommitChanges();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(item);
            }
        }

        /// <summary>
        /// Merges the specified persisted.
        /// </summary>
        /// <param name="persisted">The persisted.</param>
        /// <param name="current">The current.</param>
        public virtual void Merge(TModel persisted, TModel current)
        {
            try
            {
                TEntity persistedEntity = Mapper.Map<TModel, TEntity>(persisted);
                TEntity currentEntity = Mapper.Map<TModel, TEntity>(current);
                this._UnitOfWork.ApplyCurrentValues(persistedEntity, currentEntity);
                this._UnitOfWork.CommitChanges();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(persisted, current);
            }
        }

        /// <summary>
        /// Function to execute Stored Procedure
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual IEnumerable<TModel> ExecuteProc(string sqlQuery, params object[] parameters)
        {
            try
            {
                var queryResult = this._UnitOfWork.ExecuteQuery<TEntity>(sqlQuery, parameters);
                return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(queryResult);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(sqlQuery, parameters);
            }
            return null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <returns></returns>
        private IDbSet<TEntity> GetEntity()
        {
            try
            {
                return _UnitOfWork.CreateSet<TEntity>();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return null;
        }

        /// <summary>
        /// Tracks the entry.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        private DbEntityEntry<TEntity> TrackEntry(TEntity entity)
        {
            try
            {
                return this._UnitOfWork.GetEntry(entity);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(entity);
            }
            return this._UnitOfWork.GetEntry(entity);
        }

        /// <summary>
        /// Sets the id.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="domain">The domain.</param>
        //private void SetId(TEntity entity, TModel domain)
        //{
        //    try
        //    {
        //        var keyName = this.GetEntityKayName(entity);
        //        var keyValue = GetEntityKeyValue(entity);
        //        typeof(TModel).GetProperty(keyName).SetValue(domain, keyValue, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ExceptionValueTracker(entity, domain);
        //        if (DataAccessExceptionHandle.ExceptionHandle(ref ex))
        //            throw ex;
        //    }
        //}

        /// <summary>
        /// Sets the id.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="domain">The domain.</param>
        private void SetId(TEntity entity, TModel domain)
        {
            try
            {
                var keyNames = this.GetEntityKayName(entity);
                var keyValues = GetEntityKeyValue(entity);

                foreach (var keyitem in keyNames)
                {
                    int currentPos = keyNames.IndexOf(keyitem);
                    typeof(TModel).GetProperty(keyitem).SetValue(domain, keyValues[currentPos], null);
                }
            }
            catch (Exception ex)
            {
                this._UnitOfWork.RollbackChanges();
                ex.ExceptionValueTracker(entity, domain);
            }
        }

        /// <summary>
        /// Gets the entity kay.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        //private string GetEntityKayName(TEntity entity)
        //{
        //    try
        //    {
        //        var objectContext = ((IObjectContextAdapter)_UnitOfWork).ObjectContext;
        //        var mdw = objectContext.MetadataWorkspace;

        //        var keyName = mdw.GetItems<EntityType>(DataSpace.CSpace)
        //            .FirstOrDefault(x => x.Name.Equals(typeof(TEntity).Name))
        //            .KeyMembers.FirstOrDefault().Name;

        //        return keyName.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ExceptionValueTracker(entity);
        //        if (DataAccessExceptionHandle.ExceptionHandle(ref ex))
        //            throw ex;
        //    }
        //    return GetEntityKayName(entity);
        //}

        /// <summary>
        /// Gets the entity kay.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        private IList<string> GetEntityKayName(TEntity entity)
        {
            IList<string> keyName = new List<string>();
            try
            {
                var mdw = this._UnitOfWork.GetMetadataWorkspaceFromContext();

                keyName = mdw.GetItems<EntityType>(DataSpace.CSpace)
                    .FirstOrDefault(x => typeof(TEntity).Name.Equals(x.Name))
                    .KeyMembers.Select(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                this._UnitOfWork.RollbackChanges();
                ex.ExceptionValueTracker(entity);
            }

            return keyName.ToList();
        }

        /// <summary>
        /// Gets the entity key value.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        //private object GetEntityKeyValue(TEntity entity)
        //{
        //    try
        //    {
        //        var keyName = this.GetEntityKayName(entity);
        //        var keyValue = typeof(TEntity).GetProperty(keyName).GetValue(entity, null);
        //        return keyValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ExceptionValueTracker(entity);
        //        if (DataAccessExceptionHandle.ExceptionHandle(ref ex))
        //            throw ex;
        //    }
        //    return GetEntityKeyValue(entity);
        //}

        /// <summary>
        /// Gets the entity key value.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        private object[] GetEntityKeyValue(TEntity entity)
        {
            try
            {
                var keyNames = this.GetEntityKayName(entity);
                IList<object> keyValues = new List<object>();

                foreach (var keyitem in keyNames)
                {
                    keyValues.Add(typeof(TEntity).GetProperty(keyitem).GetValue(entity, null));
                }

                return keyValues.ToArray();
            }
            catch (Exception ex)
            {
                this._UnitOfWork.RollbackChanges();
                ex.ExceptionValueTracker(entity);
            }
            return new object[] { };
        }
        #endregion
    }
}