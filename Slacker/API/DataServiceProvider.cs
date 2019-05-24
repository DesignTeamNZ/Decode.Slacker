using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Slacker {
    
    /// <summary>
    /// Reduces DataService implementation by pre-mapping common method calls to a single abstract implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DataServiceProvider<T> : IDataService<T> where T : DataModel, new() {
        #region Insert
        /// <inheritdoc />
        public async Task InsertAsync(T model, bool loadGeneratedKeys = true, long batchId = -1) {
            await Task.Run(new Action(() => { Insert(model, loadGeneratedKeys, batchId); }));
        }
        /// <inheritdoc />
        public async Task InsertAsync(T[] models, bool loadGeneratedKeys = true, long batchId = -1) {
            await Task.Run(new Action(() => { Insert(models, loadGeneratedKeys, batchId); }));
        }
        /// <inheritdoc />
        public void Insert(T model, bool loadGeneratedKeys = true, long batchId = -1) {
            Insert(new[] { model }, loadGeneratedKeys, batchId);
        }
        /// <inheritdoc />
        public abstract void Insert(T[] models, bool loadGeneratedKeys = true, long batchId = -1);
        #endregion

        #region Select
        /// <inheritdoc />
        public async Task<IEnumerable<T>> SelectAllAsync(long batchId = -1) {
            return await Task.Run(() => { return SelectAll(batchId); });
        }
        /// <inheritdoc />
        public async Task<IEnumerable<T>> FindAsync(object whereParam, long batchId = -1) {
            return await Task.Run(() => { return Find(whereParam, batchId); });
        }
        /// <inheritdoc />
        public async Task<IEnumerable<T>> SelectAsync(string where, object whereParam, long batchId = -1) {
            return await Task.Run(() => { return Select(where, whereParam, batchId); });
        }
        /// <inheritdoc />
        public async Task<IEnumerable<T>> SelectAsync(QueryProps queryProps, long batchId = -1) {
            return await Task.Run(() => { return Select(queryProps, batchId); });
        }
        /// <inheritdoc />
        public async Task<int> CountAsync(QueryProps queryProps, long batchId = -1) {
            return await Task.Run(() => { return Count(queryProps, batchId); });
        }
        /// <inheritdoc />
        public IEnumerable<T> SelectAll(long batchId = -1) {
            return Select(new QueryProps(), batchId);
        }
        /// <inheritdoc />
        public IEnumerable<T> Select(string whereSql, object whereParams, long batchId = -1) {
            return Select(new QueryProps { WhereSql = whereSql, WhereParams = whereParams }, batchId);
        }
        /// <inheritdoc />
        public abstract IEnumerable<T> Find(object whereParams, long batchId = -1);
        /// <inheritdoc />
        public abstract IEnumerable<T> Select(QueryProps queryProps, long batchId = -1);
        /// <inheritdoc />
        public abstract int Count(QueryProps queryProps, long batchId = -1);
        #endregion

        #region Update
        /// <inheritdoc />
        public async Task UpdateAsync(T model, bool updateOnlyChangedProperties = true, long batchId = -1) {
            await Task.Run(() => { Update(model, updateOnlyChangedProperties, batchId); });
        }
        /// <inheritdoc />
        public async Task UpdateAsync(object model, IEnumerable<string> updateFields = null,
            string where = null, object whereObj = null, long batchId = -1) {

            await Task.Run(() => { Update(model, updateFields, where, whereObj, batchId); });
        }
        /// <inheritdoc />
        public void Update(T model, bool updateOnlyChangedProperties = true, long batchId = -1) {
            if (updateOnlyChangedProperties) {
                var changedProperties = model.ChangedProperties;
                if (changedProperties.Count < 1) {
                    return;
                }

                Update(model, changedProperties, null, null, batchId);
                return;
            }

            Update(model, null, null, null, batchId);
        }
        /// <inheritdoc />
        public abstract void Update(object model, IEnumerable<string> updateFields = null,
            string where = null, object whereObj = null, long batchId = -1);
        #endregion

        #region Delete
        /// <inheritdoc />
        public async Task DeleteAllAsync(long batchId = -1) {
            await Task.Run(() => { DeleteAll(batchId); });
        }
        /// <inheritdoc />
        public async Task DeleteAsync(T model, long batchId = -1) {
            await Task.Run(() => { Delete(model, batchId); });
        }
        /// <inheritdoc />
        public async Task DeleteAsync(string where, object whereParam, long batchId = -1) {
            await Task.Run(() => { Delete(where, whereParam, batchId); });
        }
        /// <inheritdoc />
        public async Task DeleteAsync(DeleteProps deleteProps, long batchId = -1) {
            await Task.Run(() => { Delete(deleteProps, batchId); });
        }
        /// <inheritdoc />
        public void DeleteAll(long batchId = -1) {
            Delete((DeleteProps)null, batchId);
        }
        /// <inheritdoc />
        public void Delete(string whereSql, object whereParams, long batchId = -1) {
            Delete(new DeleteProps { WhereSql = whereSql, WhereParams = whereParams }, batchId);
        }
        /// <inheritdoc />
        public abstract void Delete(T model, long batchId = -1);
        /// <inheritdoc />
        public abstract void Delete(DeleteProps deleteProps, long batchId = -1);
        #endregion

        #region Batches
        /// <inheritdoc />
        public abstract void StartBatch(long batchId = -1, string createTransactionAs = null);
        /// <inheritdoc />
        public abstract void EndBatch(long batchId = -1);
        /// <inheritdoc />
        public abstract void RollbackBatch(long batchId = -1, bool closeConnection = true);
        #endregion

    }
}
