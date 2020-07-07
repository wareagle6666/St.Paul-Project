using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public static class QueryMonitor
    {
        public static Func<string> GetCurrentUserName = () => null;

        public class Log : IDisposable
        {
            public static bool Enabled = false;
            public static string MachineName = Environment.MachineName;
            private IDbConnection _cnn;
            private string _sql;
            private object _params;
            private readonly IDbTransaction _transaction;
            private Stopwatch _stopwatch;

            public Log(IDbConnection cnn, string sql, IDbTransaction transaction, object param = null)
            {
                if (!Enabled)
                    return;

                _cnn = cnn;
                _sql = sql;
                _params = param;
                _transaction = transaction;
                _stopwatch = Stopwatch.StartNew();
            }

            public void Dispose()
            {
                try
                {
                    const String EmsysDatabase = "EmSys";
                    if (!Enabled || _cnn.Database != EmsysDatabase)
                        return;

                    _stopwatch.Stop();

                    SqlMapper.Execute(_cnn, "s1inc_AddQueryLog", new
                    {
                        MachineName,
                        UserName = GetCurrentUserName(),
                        Sql = _sql,
                        @params = _params != null ? _params.ToString() : null,
                        Seconds = _stopwatch.Elapsed.TotalSeconds
                    }, commandType: CommandType.StoredProcedure, transaction: _transaction);
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                    throw;
                }
                finally
                {
                    _cnn = null;
                    _sql = null;
                    _params = null;
                    _stopwatch = null;
                }
            }
        }

        /// <summary>
        /// Execute parameterized SQL
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Number of rows affected
        /// </returns>
        public static int Execute(this IDbConnection cnn, string sql, object param = null,
                                  IDbTransaction transaction = null,
                                  int? commandTimeout = null, CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.Execute(cnn, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// The first cell selected
        /// </returns>
        public static object ExecuteScalar(this IDbConnection cnn, string sql, object param = null,
                                           IDbTransaction transaction = null, int? commandTimeout = null,
                                           CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.ExecuteScalar(cnn, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// The first cell selected
        /// </returns>
        public static T ExecuteScalar<T>(this IDbConnection cnn, string sql, object param = null,
                                         IDbTransaction transaction = null, int? commandTimeout = null,
                                         CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.ExecuteScalar<T>(cnn, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute parameterized SQL and return an <see cref="T:System.Data.IDataReader"/>
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Data.IDataReader"/> that can be used to iterate over the results of the SQL query.
        /// </returns>
        /// 
        /// <remarks>
        /// This is typically used when the results of a query are not processed by Dapper, for example, used to fill a <see cref="T:System.Data.DataTable"/>
        ///             or <see cref="T:System.Data.DataSet"/>.
        /// 
        /// </remarks>
        /// 
        /// <example>
        /// 
        /// <code>
        /// <![CDATA[
        ///             DataTable table = new DataTable("MyTable");
        ///             using (var reader = ExecuteReader(cnn, sql, param))
        ///             {
        ///                 table.Load(reader);
        ///             }
        ///             ]]>
        /// </code>
        /// 
        /// </example>
        public static IDataReader ExecuteReader(this IDbConnection cnn, string sql, object param = null,
                                                IDbTransaction transaction = null, int? commandTimeout = null,
                                                CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.ExecuteReader(cnn, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Return a list of dynamic objects, reader is closed after the call
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Note: each row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;
        /// </remarks>
        public static IEnumerable<dynamic> Query(this IDbConnection cnn, string sql, object param = null,
                                                IDbTransaction transaction = null, bool buffered = true,
                                                int? commandTimeout = null, CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.Query(cnn, sql, param, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes a query, returning the data typed as per T
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// the dynamic param may seem a bit odd, but this works around a major usability issue in vs, if it is Object vs completion gets annoying. Eg type new [space] get new object
        /// </remarks>
        /// 
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        ///             created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// 
        /// </returns>
        public static IEnumerable<T> Query<T>(this IDbConnection cnn, string sql, object param = null,
                                              IDbTransaction transaction = null, bool buffered = true,
                                              int? commandTimeout = null, CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.Query<T>(cnn, sql, param, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes a query, returning the data typed as per the Type suggested
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        ///             created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// 
        /// </returns>
        public static IEnumerable<object> Query(this IDbConnection cnn, Type type, string sql, object param = null,
                                                IDbTransaction transaction = null, bool buffered = true,
                                                int? commandTimeout = null, CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.Query(cnn, type, sql, param, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn
        /// 
        /// </summary>
        public static SqlMapper.GridReader QueryMultiple(this IDbConnection cnn, string sql, object param = null,
                                                                IDbTransaction transaction = null,
                                                                int? commandTimeout = null,
                                                                CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.QueryMultiple(cnn, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Maps a query to objects
        /// 
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset</typeparam><typeparam name="TSecond">The second type in the recordset</typeparam><typeparam name="TReturn">The return type</typeparam><param name="cnn"/><param name="sql"/><param name="map"/><param name="param"/><param name="transaction"/><param name="buffered"/><param name="splitOn">The Field we should split and read the second object from (default: id)</param><param name="commandTimeout">Number of seconds before command execution timeout</param><param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns/>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(this IDbConnection cnn, string sql,
                                                                           Func<TFirst, TSecond, TReturn> map,
                                                                           object param = null,
                                                                           IDbTransaction transaction = null,
                                                                           bool buffered = true, string splitOn = "Id",
                                                                           int? commandTimeout = null,
                                                                           CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.Query<TFirst, TSecond, TReturn>(cnn, sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <summary>
        /// Maps a query to objects
        /// 
        /// </summary>
        /// <typeparam name="TFirst"/><typeparam name="TSecond"/><typeparam name="TThird"/><typeparam name="TReturn"/><param name="cnn"/><param name="sql"/><param name="map"/><param name="param"/><param name="transaction"/><param name="buffered"/><param name="splitOn">The Field we should split and read the second object from (default: id)</param><param name="commandTimeout">Number of seconds before command execution timeout</param><param name="commandType"/>
        /// <returns/>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(this IDbConnection cnn, string sql,
                                                                                   Func<TFirst, TSecond, TThird, TReturn>
                                                                                       map, object param = null,
                                                                                   IDbTransaction transaction = null,
                                                                                   bool buffered = true,
                                                                                   string splitOn = "Id",
                                                                                   int? commandTimeout = null,
                                                                                   CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.Query<TFirst, TSecond, TThird, TReturn>(cnn, sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <summary>
        /// Perform a multi mapping query with 4 input parameters
        /// 
        /// </summary>
        /// <typeparam name="TFirst"/><typeparam name="TSecond"/><typeparam name="TThird"/><typeparam name="TFourth"/><typeparam name="TReturn"/><param name="cnn"/><param name="sql"/><param name="map"/><param name="param"/><param name="transaction"/><param name="buffered"/><param name="splitOn"/><param name="commandTimeout"/><param name="commandType"/>
        /// <returns/>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection cnn,
                                                                                            string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map,
                                                                                            object param = null, IDbTransaction transaction = null, bool buffered = true,
                                                                                            string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TReturn>(cnn, sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <summary>
        /// Perform a multi mapping query with 5 input parameters
        /// 
        /// </summary>
        /// <typeparam name="TFirst"/><typeparam name="TSecond"/><typeparam name="TThird"/><typeparam name="TFourth"/><typeparam name="TFifth"/><typeparam name="TReturn"/><param name="cnn"/><param name="sql"/><param name="map"/><param name="param"/><param name="transaction"/><param name="buffered"/><param name="splitOn"/><param name="commandTimeout"/><param name="commandType"/>
        /// <returns/>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map,
            object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(cnn, sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <summary>
        /// Perform a multi mapping query with 6 input parameters
        /// 
        /// </summary>
        /// <typeparam name="TFirst"/><typeparam name="TSecond"/><typeparam name="TThird"/><typeparam name="TFourth"/><typeparam name="TFifth"/><typeparam name="TSixth"/><typeparam name="TReturn"/><param name="cnn"/><param name="sql"/><param name="map"/><param name="param"/><param name="transaction"/><param name="buffered"/><param name="splitOn"/><param name="commandTimeout"/><param name="commandType"/>
        /// <returns/>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(
            this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map,
            object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id",
            int? commandTimeout = null, CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(cnn, sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <summary>
        /// Perform a multi mapping query with 7 input parameters
        /// 
        /// </summary>
        /// <typeparam name="TFirst"/><typeparam name="TSecond"/><typeparam name="TThird"/><typeparam name="TFourth"/><typeparam name="TFifth"/><typeparam name="TSixth"/><typeparam name="TSeventh"/><typeparam name="TReturn"/><param name="cnn"/><param name="sql"/><param name="map"/><param name="param"/><param name="transaction"/><param name="buffered"/><param name="splitOn"/><param name="commandTimeout"/><param name="commandType"/>
        /// <returns/>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(cnn, sql, map, param,
                                                                                                 transaction, buffered,
                                                                                                 splitOn, commandTimeout,
                                                                                                 commandType);
        }

        /// <summary>
        /// Perform a multi mapping query with arbitrary input parameters
        /// 
        /// </summary>
        /// <typeparam name="TReturn">The return type</typeparam><param name="cnn"/><param name="sql"/><param name="types">array of types in the recordset</param><param name="map"/><param name="param"/><param name="transaction"/><param name="buffered"/><param name="splitOn">The Field we should split and read the second object from (default: id)</param><param name="commandTimeout">Number of seconds before command execution timeout</param><param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns/>
        public static IEnumerable<TReturn> Query<TReturn>(this IDbConnection cnn, string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            using (new Log(cnn, sql, transaction, param))
                return SqlMapper.Query<TReturn>(cnn, sql, types, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }
    }
}