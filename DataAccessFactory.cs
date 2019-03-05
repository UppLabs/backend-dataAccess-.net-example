using System;
using Microsoft.EntityFrameworkCore;

namespace Evalx.Core.DataAccess
{
    /// <summary>
    /// Implements IDataAccessFactory for working with EntityFramework DbContext.
    /// </summary>
    /// <typeparam name="T">Specific DbContext used to access the DB. Must have a constructor taking a connection string as the single parameter.</typeparam>
    public class DataAccessFactory<T> : IDataAccessFactory
        where T : DbContext
    {
        private readonly string _connectionString;

        public DataAccessFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Implementation of IDataAccessFactory

        /// <summary>
        /// Instantiates a new UnitOfWork.
        /// </summary>
        public IUnitOfWork CreateUnitOfWork()
        {
            var context = GetDbContext();
            return new UnitOfWork(context);
        }

        public DbContext GetDbContext()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<T>();
            dbOptionsBuilder.UseSqlServer(_connectionString);
            var context = (T)Activator.CreateInstance(typeof(T), dbOptionsBuilder.Options);
            return context;
        }

        #endregion
    }
}
