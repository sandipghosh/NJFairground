﻿
namespace NJFairground.Web.Data.Implementation.Base
{
    #region Required Namespace(s)
    using NJFairground.Web.Data.Interface.Base;
    using NJFairground.Web.Models.Base;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    #endregion

    public class QueryDataRepository<TEntityModel> : IQueryDataRepository
        where TEntityModel : DbContext, new()
    {
        private readonly TEntityModel _dbContext;
        public QueryDataRepository()
        {
            this._dbContext = new TEntityModel();
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public IEnumerable<TModel> ExecuteQuery<TModel>(string sqlQuery, params object[] parameters)
            where TModel : BaseModel
        {
            int commandTimeoutAppSetting = int.Parse(ConfigurationManager.AppSettings["CommandTimeout"].ToString());
            ((IObjectContextAdapter)_dbContext).ObjectContext.CommandTimeout = commandTimeoutAppSetting;
            var entities = this._dbContext.Database.SqlQuery<TModel>(sqlQuery, parameters);
            return entities;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            int commandTimeoutAppSetting = int.Parse(ConfigurationManager.AppSettings["CommandTimeout"].ToString());
            ((IObjectContextAdapter)_dbContext).ObjectContext.CommandTimeout = commandTimeoutAppSetting;
            return this._dbContext.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }
    }
}