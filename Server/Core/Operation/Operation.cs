using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace VXDesign.Store.CarWashSystem.Server.Core.Operation
{
    public class Operation : IOperation
    {
        private readonly SqlConnection connection;
        private SqlTransaction? transaction;

        private Operation(string dataStoreConnectionString)
        {
            connection = new SqlConnection(dataStoreConnectionString);
            connection.Open();
        }

        private SqlTransaction BeginTransaction()
        {
            if (transaction != null)
            {
                throw new Exception("Transaction has already started");
            }

            return transaction = connection.BeginTransaction(IsolationLevel.Snapshot);
        }

        private void EndTransaction()
        {
            transaction?.Dispose();
            transaction = null;
        }

        private async Task<T> Execute<T>(Func<string, DynamicParameters, SqlTransaction?, int?, CommandType?, Task<T>> function, DynamicParameters parameters, string command,
            CommandType? commandType = null, int? commandTimeout = null)
        {
            return await function(command, parameters, transaction, commandTimeout, commandType);
        }

        #region ExecuteAsync

        public async Task<int> ExecuteAsync(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await Execute(connection.ExecuteAsync, parameters, command, commandType, commandTimeout);
        }

        public async Task<int> ExecuteAsync(string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await ExecuteAsync(new DynamicParameters(), command, commandType, commandTimeout);
        }

        public async Task<int> ExecuteAsync(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await ExecuteAsync(new DynamicParameters(parameters), command, commandType, commandTimeout);
        }

        #endregion

        #region QueryAsync

        public async Task<IEnumerable<T>> QueryAsync<T>(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await Execute(connection.QueryAsync<T>, parameters, command, commandType, commandTimeout);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await QueryAsync<T>(new DynamicParameters(), command, commandType, commandTimeout);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await QueryAsync<T>(new DynamicParameters(parameters), command, commandType, commandTimeout);
        }

        #endregion

        #region QueryFirstAsync

        public async Task<T> QueryFirstAsync<T>(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await Execute(connection.QueryFirstAsync<T>, parameters, command, commandType, commandTimeout);
        }

        public async Task<T> QueryFirstAsync<T>(string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await QueryFirstAsync<T>(new DynamicParameters(), command, commandType, commandTimeout);
        }

        public async Task<T> QueryFirstAsync<T>(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await QueryFirstAsync<T>(new DynamicParameters(parameters), command, commandType, commandTimeout);
        }

        #endregion

        #region QueryFirstOrDefaultAsync

        public async Task<T> QueryFirstOrDefaultAsync<T>(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await Execute(connection.QueryFirstOrDefaultAsync<T>, parameters, command, commandType, commandTimeout);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await QueryFirstOrDefaultAsync<T>(new DynamicParameters(), command, commandType, commandTimeout);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await QueryFirstOrDefaultAsync<T>(new DynamicParameters(parameters), command, commandType, commandTimeout);
        }

        #endregion

        #region QuerySingleAsync

        public async Task<T> QuerySingleAsync<T>(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await Execute(connection.QuerySingleAsync<T>, parameters, command, commandType, commandTimeout);
        }

        public async Task<T> QuerySingleAsync<T>(string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await QuerySingleAsync<T>(new DynamicParameters(), command, commandType, commandTimeout);
        }

        public async Task<T> QuerySingleAsync<T>(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await QuerySingleAsync<T>(new DynamicParameters(parameters), command, commandType, commandTimeout);
        }

        #endregion

        #region QuerySingleOrDefaultAsync

        public async Task<T> QuerySingleOrDefaultAsync<T>(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await Execute(connection.QuerySingleOrDefaultAsync<T>, parameters, command, commandType, commandTimeout);
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await QuerySingleOrDefaultAsync<T>(new DynamicParameters(), command, commandType, commandTimeout);
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await QuerySingleOrDefaultAsync<T>(new DynamicParameters(parameters), command, commandType, commandTimeout);
        }

        #endregion

        #region QueryMultipleAsync

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(DynamicParameters parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await Execute(connection.QueryMultipleAsync, parameters, command, commandType, commandTimeout);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await QueryMultipleAsync(new DynamicParameters(), command, commandType, commandTimeout);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(object parameters, string command, CommandType? commandType = null, int? commandTimeout = null)
        {
            return await QueryMultipleAsync(new DynamicParameters(parameters), command, commandType, commandTimeout);
        }

        #endregion

        public ValueTask DisposeAsync()
        {
            connection?.DisposeAsync();
            transaction?.DisposeAsync();
            return new ValueTask();
        }

        public static async Task MakeAction(string dataStoreConnectionString, Func<IOperation, Task> action)
        {
            await MakeAction(dataStoreConnectionString, async operation =>
            {
                await action(operation);
                return true;
            });
        }

        public static async Task<T> MakeAction<T>(string dataStoreConnectionString, Func<IOperation, Task<T>> action)
        {
            await using var operation = new Operation(dataStoreConnectionString);
            try
            {
                await using var transaction = operation.BeginTransaction();
                try
                {
                    var result = await action(operation);
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    var retries = 5;
                    while (retries > 0)
                    {
                        try
                        {
                            transaction.Rollback();
                            break;
                        }
                        catch
                        {
                            retries--;
                            await Task.Delay(TimeSpan.FromSeconds(1));
                        }
                    }

                    throw;
                }
            }
            finally
            {
                operation.EndTransaction();
            }
        }
    }
}