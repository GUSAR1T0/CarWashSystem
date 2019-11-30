using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace VXDesign.Store.CarWashSystem.Server.Core.Operation
{
    public interface IOperation : IAsyncDisposable
    {
        #region ExecuteAsync

        Task<int> ExecuteAsync(string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<int> ExecuteAsync(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<int> ExecuteAsync(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null);

        #endregion

        #region QueryAsync

        Task<IEnumerable<T>> QueryAsync<T>(string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<IEnumerable<T>> QueryAsync<T>(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<IEnumerable<T>> QueryAsync<T>(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null);

        #endregion

        #region QueryFirstAsync

        Task<T> QueryFirstAsync<T>(string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<T> QueryFirstAsync<T>(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<T> QueryFirstAsync<T>(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null);

        #endregion

        #region QueryFirstOrDefaultAsync

        Task<T> QueryFirstOrDefaultAsync<T>(string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<T> QueryFirstOrDefaultAsync<T>(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<T> QueryFirstOrDefaultAsync<T>(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null);

        #endregion

        #region QuerySingleAsync

        Task<T> QuerySingleAsync<T>(string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<T> QuerySingleAsync<T>(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<T> QuerySingleAsync<T>(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null);

        #endregion

        #region QuerySingleOrDefaultAsync

        Task<T> QuerySingleOrDefaultAsync<T>(string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<T> QuerySingleOrDefaultAsync<T>(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<T> QuerySingleOrDefaultAsync<T>(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null);

        #endregion

        #region QueryMultipleAsync

        Task<SqlMapper.GridReader> QueryMultipleAsync(string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<SqlMapper.GridReader> QueryMultipleAsync(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null);
        Task<SqlMapper.GridReader> QueryMultipleAsync(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null);

        #endregion
    }
}