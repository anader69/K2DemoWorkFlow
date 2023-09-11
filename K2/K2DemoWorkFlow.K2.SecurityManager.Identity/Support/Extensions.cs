// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuidExtensions.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Support
{
    #region

    using System;
    using System.Linq;
    using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// The guid extensions.
    /// </summary>
    public static class Extensions
    {
        // http://stackoverflow.com/questions/211498/is-there-a-net-equalent-to-sql-servers-newsequentialid
        // NHibernate.Id.GuidCombGenerator

        /// <summary>
        /// The as sequential guid.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/>.
        /// </returns>
        public static Guid AsSequentialGuid(this Guid guid)
        {
            var guidArray = guid.ToByteArray();

            var baseDate = new DateTime(1900, 1, 1);
            var now = DateTime.Now;

            // Get the days and milliseconds which will be used to build the byte string 
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            var msecs = now.TimeOfDay;

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            var daysArray = BitConverter.GetBytes(days.Days);
            var msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }


        /// <summary>
        /// The get paged.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TOrderBy">
        /// The type of the order by.
        /// </typeparam>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="orderBy">
        /// The order by.
        /// </param>
        /// <param name="isDescending">
        /// The is descending.
        /// </param>
        /// <param name="pageNum">
        /// The page num.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="PagedList"/>.
        /// </returns>
        /// <exception cref="System.Exception">
        /// To do Paging you MUST provide valid OrderBy value
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        public static PagedList<T> GetPaged<T, TOrderBy>(
            this IQueryable<T> query,
            Expression<Func<T, TOrderBy>> orderBy,
            bool isDescending,
            int pageNum,
            int pageSize)
        {
            if (orderBy != null)
            {
                query = isDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            }
            else
            {
                throw new Exception("To do Paging you MUST provide valid OrderBy value");
            }

            return new PagedList<T>(query, pageNum, 5000);
        }
    }
}