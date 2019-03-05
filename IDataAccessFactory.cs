using Microsoft.EntityFrameworkCore;

namespace Evalx.Core.DataAccess
{
    /// <summary>
    /// Provides object for data access.
    /// </summary>
    public interface IDataAccessFactory
    {
        /// <summary>
        /// Instantiates a new UnitOfWork.
        /// </summary>
        IUnitOfWork CreateUnitOfWork();

        DbContext GetDbContext();
    }
}
