using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Evalx.Core.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dataContext;

        public UnitOfWork(DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region Implementation of IUnitOfWork

        /// <summary>
        /// Return a IQueryable of all data objects of the specified type.
        /// </summary>
        public virtual IQueryable<T> Get<T>()
            where T : class
        {
            return _dataContext.Set<T>();
        }

        /// <summary>
        /// Marks object as added in the unit of work.
        /// </summary>
        public void Add<T>(T entity)
            where T : class
        {
            _dataContext.Set<T>().Add(entity);
        }
        /// <summary>
        /// Marks object as removed in the unit of work.
        /// </summary>
        public void Remove<T>(T entity)
            where T : class
        {
            _dataContext.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Marks objects as removed in the unit of work.
        /// </summary>
        public void RemoveRange<T>(IEnumerable<T> entities)
            where T : class
        {
            _dataContext.Set<T>().RemoveRange(entities);
        }

        /// <summary>
        /// Commits current changes.
        /// </summary>
        public void Commit()
        {
            try
            {
                _dataContext.SaveChanges();

            }
            catch (Exception ex)
            {
                //TODO: log db error. 
                throw;
            }
            
        }

        public DbContext GetContext()
        {
            return _dataContext;
        }

        /// <summary>
        /// Executes raw sql query and returns elements of the given generic type.
        /// </summary>
        public virtual IEnumerable<T> SqlQuery<T>(string sql, params object[] objects)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region SP
        public DataSet ExecuteStoredProcedure(string name, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
            //SqlConnection sqlConnection;
            //var dataSet = new DataSet();
            //SqlDataAdapter sqlDataAdapter;

            //using (sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            //{

            //    sqlConnection.Open();

            //    using (sqlDataAdapter = new SqlDataAdapter(name, sqlConnection))
            //    {
            //        sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //        sqlDataAdapter.SelectCommand.Parameters.AddRange(parameters);

            //        sqlDataAdapter.Fill(dataSet);
            //        sqlDataAdapter.Dispose();
            //    }
            //}
            //return dataSet;
        }
        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Cleans up external resources.
        /// </summary>
        public void Dispose()
        {
            _dataContext.Dispose();
        }

        #endregion
    }
}
