using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;

namespace Slacker {
    /// <summary>
    /// DataService Connection Manager for managing multiple connections / batches / transactions 
    /// </summary>
    public interface ISqlConnectionService {
        /// <summary>
        /// Specifies the connection string used by this Connection Manager
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Marks the connection manager to treat all updates as part of the same batch / connection.
        /// </summary>
        /// <param name="threadOrBatchId">A identification number (Either thread id or batch number) to associate queries with a batch</param>
        /// <param name="transactionName">Optinal transaction name to enable transaction support</param>
        void StartBatch(long threadOrBatchId, string transactionName = null);
        /// <summary>
        /// Ends the current batch, commiting the associated transaction (if exists) and closing the batch associated connection
        /// </summary>
        /// <param name="threadOrBatchId">A identification number (Either thread id or batch number) to associate queries with a batch</param>
        void EndBatch(long threadOrBatchId);
        /// <summary>
        /// Rolls back the current batch associated transaction
        /// </summary>
        /// <param name="threadOrBatchId">A identification number (Either thread id or batch number) to associate queries with a batch</param>
        /// <param name="closeConnection">Should the current batch connection be closed after rollback</param>
        void RollbackBatch(long threadId, bool closeConnection = true);

        /// <summary>
        /// Executes a query based on current batch/thread id. If no batch has been started at specified id a new connection will be spawned for only this query
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="paramObject">Dapper supported Parameter Object</param>
        /// <param name="threadOrBatchId">A identification number (Either thread id or batch number) to associate queries with a batch</param>
        /// <returns></returns>
        IEnumerable<T> ExecuteQuery<T>(string query, object paramObject, long threadId);
        void ExecuteUpdate(string update, object paramObject, long threadId);
    }

    /// <inheritdoc/>
    public class SqlConnectionService : ISqlConnectionService {

        /// <inheritdoc />
        public string ConnectionString { get; set; }

        /// <summary>
        /// Current state of Batch Connections: batchId -> connection
        /// </summary>
        protected Dictionary<long, SqlConnection> BatchConnections { get; set; }
        /// <summary>
        /// Current state of Batch Transactions: batchId -> transaction
        /// </summary>
        protected Dictionary<long, SqlTransaction> Transactions { get; set; }

        protected SqlConnectionService() {
            this.BatchConnections = new Dictionary<long, SqlConnection>();
            this.Transactions = new Dictionary<long, SqlTransaction>();
        }

        /// <inheritdoc />
        public void StartBatch(long threadOrBatchId, string transactionName = null) {
            if (this.BatchConnections.ContainsKey(threadOrBatchId)) {
                throw new Exception("Batch is already running on this thread/batch id.");
            }

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            BatchConnections.Add(threadOrBatchId, connection);
            if (transactionName != null) {
                this.Transactions.Add(threadOrBatchId, connection.BeginTransaction(transactionName));
            }
        }

        /// <inheritdoc />
        public void EndBatch(long threadOrBatchId) {
            if (!this.BatchConnections.ContainsKey(threadOrBatchId)) {
                return; // no batch 
            }
            
            var connection = BatchConnections[threadOrBatchId];
            BatchConnections.Remove(threadOrBatchId);

            if (Transactions.ContainsKey(threadOrBatchId)) {
                Transactions[threadOrBatchId].Commit();
                Transactions.Remove(threadOrBatchId);
            }
            
            connection.Dispose();
        }

        /// <inheritdoc />
        public void RollbackBatch(long threadOrBatchId, bool closeConnection = true) {
            if (!this.Transactions.ContainsKey(threadOrBatchId)) {
                throw new Exception("Failed to rollback transaction on batch. Transaction was never initialized.");
            }
            this.Transactions[threadOrBatchId].Rollback();
            this.Transactions.Remove(threadOrBatchId);
            if(closeConnection) this.EndBatch(threadOrBatchId);
        }

        /// <inheritdoc />
        public IEnumerable<T> ExecuteQuery<T>(string query, object paramObject, long threadOrBatchId) {

            // If no batch is defined, build connection for query
            if (!this.BatchConnections.ContainsKey(threadOrBatchId)) {
                var connection = new SqlConnection(ConnectionString);

                using (connection) {
                    return connection.Query<T>(query, paramObject);
                }
            }

            // Use batch connection and transaction (if exists) 
            return this.BatchConnections[threadOrBatchId].Query<T>(query, paramObject);
        }

        /// <inheritdoc />
        public void ExecuteUpdate(string update, object paramObject, long threadOrBatchId) {
            if (!this.BatchConnections.ContainsKey(threadOrBatchId)) {
                var connection = new SqlConnection(ConnectionString);

                using (connection) {
                    connection.Execute(update, paramObject);
                    return;
                }
            }

            // Use batch connection and transaction (if exists) 
            this.BatchConnections[threadOrBatchId].Query(update, paramObject,
                this.Transactions.TryGetValue(threadOrBatchId, out SqlTransaction trans) ? trans : null
            );
        }

        /// <inheritdoc />
        public static SqlConnectionService FromConnectionString(string connStr) {
            return new SqlConnectionService() {
                ConnectionString = connStr
            };
        }

        /// <inheritdoc />
        //public static SqlConnectionService FromConfig(string key, Assembly configAssembly = null) {
        //    if (configAssembly == null) {
        //        configAssembly = Assembly.GetCallingAssembly();
        //    }

        //    var config = ConfigurationManager.OpenExeConfiguration(configAssembly.Location);

        //    var connStr = config?.ConnectionStrings?.ConnectionStrings[key]?.ConnectionString;
        //    if(connStr == null) { 
        //        throw new Exception(
        //            $"Could not find connection string '{key}' in assembly config '{configAssembly.GetName()}"
        //        );
        //    }

        //    return FromConnectionString(connStr);
        //}

    }

}
