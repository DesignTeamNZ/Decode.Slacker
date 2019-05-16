using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Slacker {
    
    /// <summary>
    /// Used as a marker for reflection. Cast to IDataService<T> to use.
    /// </summary>
    public interface IDataService { }
    /// <summary>
    /// Represents a Slacker DataService manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataService<T> : IDataService where T : DataModel, new() {
        #region Insert
        /// <summary>
        /// Perform an insert query using data model
        /// </summary>
        /// <param name="loadGeneratedKeys">Should generated keys (ID fields) be loaded to models on insert.</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        void Insert(T model, bool loadGeneratedKeys = true, long batchId = -1);
        /// <summary>
        /// Perform an insert query using data model(s)
        /// </summary>
        /// <param name="loadGeneratedKeys">Should generated keys (ID fields) be loaded to models on insert.</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        void Insert(T[] models, bool loadGeneratedKeys = true, long batchId = -1);
        /// <summary>
        /// Perform an async insert query using data model
        /// </summary>
        /// <param name="loadGeneratedKeys">Should generated keys (ID fields) be loaded to models on insert.</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        Task InsertAsync(T model, bool loadGeneratedKeys = true, long batchId = -1);
        /// <summary>
        /// Perform an async insert query using data model(s)
        /// </summary>
        /// <param name="loadGeneratedKeys">Should generated keys (ID fields) be loaded to models on insert.</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        Task InsertAsync(T[] models, bool loadGeneratedKeys = true, long batchId = -1);
        #endregion
        #region Select
        /// <summary>
        /// Select all records
        /// </summary>
        /// <returns>IEnumerable<typeparamref name="T"/> results</returns>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        IEnumerable<T> SelectAll(long batchId = -1);
        /// <summary>
        /// Perform an async select using a default condition (Primary Key) with parameter object
        /// </summary>
        /// <param name="whereParam"></param>
        /// <returns>IEnumerable<typeparamref name="T"/> results</returns>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        IEnumerable<T> Find(object whereParam, long batchId = -1);
        /// <summary>
        /// Perform an select query with Condition
        /// </summary>
        /// <param name="where">Condition query</param>
        /// <param name="whereParam">Condition parameter</param>
        /// <returns>IEnumerable<typeparamref name="T"/>results</returns>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        IEnumerable<T> Select(string where, object whereParam, long batchId = -1);
        /// <summary>
        /// Perform a select query
        /// </summary>
        /// <param name="queryProps">Query properties</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against.</param>
        /// <returns></returns>
        IEnumerable<T> Select(QueryProps queryProps, long batchId = -1);
        /// <summary>
        /// Select all records async
        /// </summary>
        /// <returns>IEnumerable<typeparamref name="T"/> results</returns>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        Task<IEnumerable<T>> SelectAllAsync(long batchId = -1);
        /// <summary>
        /// Perform an async select using a default condition (Primary Key) with parameter object
        /// </summary>
        /// <param name="whereParam"></param>
        /// <returns>IEnumerable<typeparamref name="T"/> results</returns>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        Task<IEnumerable<T>> FindAsync(object whereParam, long batchId = -1);
        /// <summary>
        /// Perform an async select query with Condition
        /// </summary>
        /// <param name="where">Condition query</param>
        /// <param name="whereParam">Condition parameter</param>
        /// <returns>IEnumerable<typeparamref name="T"/>results</returns>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        Task<IEnumerable<T>> SelectAsync(string where, object whereParam, long batchId = -1);
        /// <summary>
        /// Perform an async select query
        /// </summary>
        /// <param name="queryProps">Query properties</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> SelectAsync(QueryProps queryProps, long batchId = -1);
        /// <summary>
        /// Performs a count query on dataservice table with optional condition params
        /// </summary>
        /// <param name="queryProps">Sql props</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against.</param>
        /// <returns></returns>
        int Count(QueryProps queryProps, long batchId = -1);
        /// <summary>
        /// Performs a count query on dataservice table with optional condition params
        /// </summary>
        /// <param name="queryProps">Sql props</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against.</param>
        /// <returns></returns>
        Task<int> CountAsync(QueryProps queryProps, long batchId = -1);
        #endregion
        #region Update
        /// Performs an update on data model using default primary key based condition
        /// </summary>
        /// <param name="model"></param>
        /// <param name="onlyChanged">Only update changed fields on the model</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        void Update(T model, bool updateOnlyChangedProperties = true, long batchId = -1);
        /// <summary>
        /// Performs an update on object model
        /// </summary>
        /// <param name="model">T or anonymous Object (Uses reflection unless updateFields is set)</param>
        /// <param name="updateFields">The fields to be updated or null for all</param>
        /// <param name="where">The condition or null for all models (Requires UpdateAll Flag)</param>
        /// <param name="whereObj">Provides an additional object for where condition specifically.</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        void Update(object model, IEnumerable<string> updateFields = null, string where = null, object whereObj = null, long batchId = -1);
        /// Performs an async update on data model using default primary key based condition
        /// </summary>
        /// <param name="model"></param>
        /// <param name="onlyChanged">Only update changed fields on the model</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        Task UpdateAsync(T model, bool updateOnlyChangedProperties = true, long batchId = -1);
        /// <summary>
        /// Performs an async update on object model
        /// </summary>
        /// <param name="model">T or anonymous Object (Uses reflection unless updateFields is set)</param>
        /// <param name="updateFields">The fields to be updated or null for all</param>
        /// <param name="where">The condition or null for all models (Requires UpdateAll Flag)</param>
        /// <param name="whereObj">Provides an additional object for where condition specifically.</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        Task UpdateAsync(object model, IEnumerable<string> updateFields = null,
            string where = null, object whereObj = null, long batchId = -1);
        #endregion
        #region Delete
        /// <summary>
        /// Global Delete on Table
        /// </summary>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        void DeleteAll(long batchId = -1);
        /// <summary>
        /// Delete a model by primary key
        /// </summary>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        void Delete(T model, long batchId = -1);
        /// <summary>
        /// Delete records based on condition and condition parameter
        /// </summary>
        /// <param name="where">Condition query</param>
        /// <param name="whereParam">Condition parameter</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        void Delete(string where, object whereParam, long batchId = -1);
        /// <summary>
        /// Delete records based on deleteProps conditions
        /// </summary>
        /// <param name="deleteProps">Query props</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        void Delete(DeleteProps deleteProps, long batchId = -1);
        /// <summary>
        /// Async Global Delete on Table
        /// </summary>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        Task DeleteAllAsync(long batchId = -1);
        /// <summary>
        /// Async Delete a model by primary key
        /// </summary>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        Task DeleteAsync(T model, long batchId = -1);
        /// <summary>
        /// Async Delete records based on condition and condition parameter
        /// </summary>
        /// <param name="where">Condition query</param>
        /// <param name="whereParam">Condition parameter</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        Task DeleteAsync(string where, object whereParam, long batchId = -1);
        /// <summary>
        /// Delete records based on deleteProps conditions
        /// </summary>
        /// <param name="deleteProps">Sql props</param>
        /// <param name="batchId">Specifies which associated batch this method should be called against. Defaults to thread batch</param>
        Task DeleteAsync(DeleteProps deleteProps, long batchId = -1);
        #endregion

        #region Batches
        /// <summary>
        /// Marks the connection manager to treat all updates as part of the same batch / connection.
        /// </summary>
        /// <param name="batchId">A identification number (batch number). If null thread id will be used</param>
        /// <param name="createTransactionAs">Optinal transaction name to enable transaction support for this batch</param>
        void StartBatch(long batchId = -1, string createTransactionAs = null);
        /// <summary>
        /// Ends the current batch and closes the batch connection, if transaction has been created for this batch it will be commited
        /// </summary>
        /// <param name="batchId">A identification number (batch number). If null thread id will be used</param>
        void EndBatch(long batchId = -1);
        /// <summary>
        /// Rolls back the current transaction associated with this batch (Requires StartBatch:createTransactionAs).
        /// </summary>
        /// <param name="batchId">A identification number (batch number). If null thread id will be used</param>
        /// <param name="closeConnection">Specifies if 'EndBatch' be called after rollback.</param>
        void RollbackBatch(long batchId = -1, bool closeConnection = true);
        #endregion
    }

}
