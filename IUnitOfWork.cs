using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Evalx.Core.DataAccess
{
    /// <summary>
    /// Contract for classes providing unit of work pattern implementation for data access and editing.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Returns a IQueriable of all data objects of the specified type.
        /// </summary>
        IQueryable<T> Get<T>()
            where T : class;

        /// <summary>
        /// Marks object as added in the unit of work.
        /// </summary>
        void Add<T>(T entity)
            where T : class;

        /// <summary>
        /// Marks object as removed in the unit of work.
        /// </summary>
        void Remove<T>(T entity)
            where T : class;

        /// <summary>
        /// Marks objects as removed in the unit of work.
        /// </summary>
        void RemoveRange<T>(IEnumerable<T> entities)
            where T : class;

        /// <summary>
        /// Commits current changes.
        /// </summary>
        void Commit();

        /// <summary>
        /// Returns context for the batch operations.
        /// </summary>
        DbContext GetContext();

        /// <summary>
        /// Executes raw sql query and returns elements of the given generic type.
        /// </summary>
        IEnumerable<T> SqlQuery<T>(string sql, params object[] objects);


        DataSet ExecuteStoredProcedure(string name, SqlParameter[] parameters);
    }
}
