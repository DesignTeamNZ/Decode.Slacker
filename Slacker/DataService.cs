using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Slacker.Helpers;
using Slacker.Helpers.Attributes;
using System.Linq.Expressions;
using System.Data;
using System.Dynamic;
using FastMember;
using System.Threading.Tasks;
using Slacker.Exceptions;
using Slacker.Connection;
using System.Threading;

namespace Slacker {

    public class SqlProps {
        public string WhereSql { get; set; }
        public object WhereParams { get; set; }

        public Func<string, string> PostEditSQL { get; set; } = sql => sql;
    }

    public class DeleteProps : SqlProps {
        public int? Top { get; set; }
    }

    public class QueryProps : SqlProps {
        public int? Top { get; set; }
        public string OrderBy { get; set; }
        public int? Offset { get; set; }
        public int? Limit { get; set; }
    }

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

    public abstract class DataServiceProvider<T> : IDataService<T> where T: DataModel, new() {
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
                var changedProperties = model.GetChangedPropertiesList();
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
            Delete((DeleteProps) null, batchId);
        }
        /// <inheritdoc />
        public void Delete(string whereSql, object whereParams, long batchId = -1) {
            Delete(new DeleteProps {WhereSql = whereSql, WhereParams = whereParams}, batchId);
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

    public abstract class DataService<T> : DataServiceProvider<T> where T : DataModel, new() {
        
        private static ServiceRegistry _serviceRegistry;
        /// <summary>
        /// Stores all singleton type instances of IDataService
        /// </summary>
        public static ServiceRegistry SERVICE_REGISTRY {
            get {
                if (_serviceRegistry == null) {
                    _serviceRegistry = new ServiceRegistry();
                }
                return _serviceRegistry;
            }
        }


        private TypeAccessor _typeAccessor;
        /// <summary>
        /// FastMember TypeAccessor for T
        /// </summary>
        public TypeAccessor TypeAccessor {
            get {
                if (_typeAccessor == null) {
                    _typeAccessor = TypeAccessor.Create(typeof(T));
                }
                return _typeAccessor;
            }
        }

        private string _queryFieldCols;
        /// <summary>
        /// Returns a pre-generated field string for table fields
        /// </summary>
        public string QueryFieldCols {
            get {
                if (_queryFieldCols == null) {
                    _queryFieldCols = string.Join(",", Fields.Select(
                            field => field.TableFieldSql
                    ));
                }
                return _queryFieldCols;
            }
        }

        private string _queryNonKeyFieldCols;
        /// <summary>
        /// Returns a pre-generated field string for table non-key fields
        /// </summary>
        public string QueryNonKeyFieldCols {
            get {
                if (_queryNonKeyFieldCols == null) {
                    _queryNonKeyFieldCols = string.Join(",",
                        NonGeneratedFields.Select(
                            field => field.TableFieldSql
                        )
                    );
                }
                return _queryNonKeyFieldCols;
            }
        }

        private string _queryModelRefs;
        /// <summary>
        /// Returns a pre-generated field string for table fields
        /// </summary>
        public string QueryModelRefs {
            get {
                if (_queryModelRefs == null) {
                    _queryModelRefs = string.Join(",", Fields.Select(
                        field => $"@{field.ModelField}"
                    ));
                }
                return _queryModelRefs;
            }
        }

        private string _queryNonKeyModelRefs;
        /// <summary>
        /// Returns a pre-generated field string for table non-key fields
        /// </summary>
        public string QueryNonKeyGeneratedModelRefs {
            get {
                if (_queryNonKeyModelRefs == null) {
                    _queryNonKeyModelRefs = string.Join(",",
                        NonGeneratedFields.Select(
                            field => $"@{field.ModelField}"
                        )
                    );
                }
                return _queryNonKeyModelRefs;
            }
        }


        private string _querySelects;
        /// <summary>
        /// Returns query selects for model fields
        /// </summary>
        public string QuerySelects {
            get {
                if (_querySelects == null) {
                    _querySelects = string.Join(",",
                        Fields.Select(
                            field => $"{AliasSql}.{field.TableFieldSql} AS [{field.ModelField}]"
                        )
                    );
                }
                return _querySelects;
            }
        }

        private string _queryDefaultUpdateRefs;
        /// <summary>
        /// Returns query updates for all model fields
        /// </summary>
        public string QueryDefaultUpdateRefs {
            get {
                if (_queryDefaultUpdateRefs == null) {
                    _queryDefaultUpdateRefs = string.Join(",",
                        NonGeneratedFields.Select(
                            field => $@"{AliasSql}.{field.TableFieldSql} = @{field.ModelField}"
                        )
                    );
                }
                return _queryDefaultUpdateRefs;
            }
        }


        private bool _tableAttributeSearched;
        public TableAttribute _tableAttribute;
        /// <summary>
        /// Returns the Table Attribute if defined for this table
        /// </summary>
        public TableAttribute TableAttribute {
            get {
                if (!_tableAttributeSearched) {
                    _tableAttribute = typeof(T).GetCustomAttribute<TableAttribute>();
                    _tableAttributeSearched = true;
                }

                return _tableAttribute;
            }
        }


        private string _table;
        /// <summary>
        /// Database Table
        /// </summary>
        public string Table {
            get {
                if(_table == null) {
                    _table = (TableAttribute?.Name ?? typeof(T).Name);
                }
                return _table;
            }
            set {
                _table = value;
            }
        }
        /// <summary>
        /// Database Table with SQL Formatting
        /// </summary>
        public string TableSql {
            get => $"[{Table.Replace(".", "].[")}]";
        }

        private string _alias;
        /// <summary>
        /// Database Alias
        /// </summary>
        public string Alias {
            get {
                if (_alias == null) {
                    _alias = TableAttribute?.Alias ?? 
                        typeof(T).Name.PadRight(3, '0').Substring(3);
                }
                return _alias;
            }
            set {
                _alias = value;
            }
        }
        /// <summary>
        /// Database Table Alias with SQL FOrmatting
        /// </summary>
        public string AliasSql {
            get => $"[{Alias.Replace(".", "].[")}]";
        }
        private string _defaultCondition;
        /// <summary>
        /// Default condition based on Primary Key
        /// </summary>
        public string DefaultCondition {
            get {
                if (_defaultCondition == null) {
                    _defaultCondition = string.Join(" AND ", PrimaryKey.Select(
                        field => $@"({AliasSql}.{field.TableFieldSql} = @{field.ModelField})"
                    ));
                }
                return _defaultCondition;
            }
        }

        private List<DataModelField> _primaryKey;
        /// <summary>
        /// Return Primary Key Fields 
        /// </summary>
        public List<DataModelField> PrimaryKey {
            get {
                if (_primaryKey == null) {
                    _primaryKey = Fields.Where(
                        field => field.IsPrimary
                    ).ToList();
                }
                return _primaryKey;
            }
        }

        private List<DataModelField> _nonGeneratedFields;
        /// <summary>
        /// Return NonKey Fields
        /// </summary>
        public List<DataModelField> NonGeneratedFields {
            get {
                if (_nonGeneratedFields == null) {
                    _nonGeneratedFields = Fields.Where(
                        field => !field.IsGenerated
                    ).ToList();
                }
                return _nonGeneratedFields;
            }
        }



        /// <summary>
        /// Enable this setting to allow this model to use delete
        /// </summary>
        public bool AllowDelete { get; set; }

        /// <summary>
        /// Enable this setting to allow this model to use delete all
        /// </summary>
        public bool AllowGlobalDelete { get; set; }

        /// <summary>
        /// Enable this setting to allow global updates on this service
        /// </summary>
        public bool AllowGlobalUpdates { get; set; }

        /// <summary>
        /// The SQLConnection for this DataService
        /// </summary>
        public IDataServiceConnectionManager ConnectionManager { get; set; }

        /// <summary>
        /// Contains DataField info
        /// </summary>
        public List<DataModelField> Fields { get; protected set; }
        
        /// <summary>
        /// Initializes a new DataService with a given connection
        /// </summary>
        /// <param name="connectionManager">Connection manager</param>
        public DataService(DataServiceConnectionManager connectionManager = null) {
            this.ConnectionManager = connectionManager;

            // Register Fields/Properties
            // Potential TODO: Replace with FastMember
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            this.Fields = typeof(T).GetProperties(bindingFlags).Select(
                memberInfo => new DataModelField(memberInfo)
            ).Where(
                dataField => !dataField.IsIgnored
            ).ToList();
            
            // Add DataService as managing service for model (T)
            SERVICE_REGISTRY.Register(typeof(T), this);
        }

        [Obsolete]
        /// <summary>
        /// Initializes a new DataService with a given connection
        /// </summary>
        /// <param name="connectionManager">The SqlConnection</param>
        public DataService(SqlConnection conn = null) 
            : this(conn == null ? null : 
                  DataServiceConnectionManager.FromConnectionString(conn.ConnectionString)
            ) {
        }
        
        #region CRUD Functions
        private string _insertQuery;
        /// <inheritdoc />
        public override void Insert(T[] models, bool loadGeneratedKeys = true, long batchId = -1) {

            // Build Insert Query
            if(_insertQuery == null) { 
                _insertQuery = $@"
                    INSERT INTO {TableSql} ({QueryNonKeyFieldCols}) 
                    VALUES ({QueryNonKeyGeneratedModelRefs});";
            }

            var autoIncField = PrimaryKey.FirstOrDefault(
                pk => pk.FieldAttribute.IsGenerated
            );

            // Do Insert
            foreach (var model in models) {
                
                if (autoIncField == null || !loadGeneratedKeys) {
                    Execute(_insertQuery, model, batchId);
                    continue;
                }

                // Update and save generated id to model
                var results = Query<int>(
                    _insertQuery + @"SELECT CAST(SCOPE_IDENTITY() as int)",
                    model,
                    batchId
                );


                TypeAccessor[model, autoIncField.ModelField] = results.Single();
                model.ClearChangedPropertiesList();
            }
        }

        /// <inheritdoc />
        public override IEnumerable<T> Find(object whereParam, long batchId = -1) {
            return Select(DefaultCondition, whereParam, batchId);
        }
        
        private string _selectQuery;
        private string _selectTopQuery;
        /// <inheritdoc />
        public override IEnumerable<T> Select(QueryProps queryProps, long batchId = -1) {

            // Build base query

            string query = null;
            if (queryProps.Top != null && queryProps.Top > 0) {
                query = _selectTopQuery ?? (
                    _selectTopQuery = $@"SELECT TOP({queryProps.Limit}) {QuerySelects} FROM {TableSql} {AliasSql}"
                );
            } else {
                query = _selectQuery ?? (
                    _selectQuery = $@"SELECT {QuerySelects} FROM {TableSql} {AliasSql}"
                );
            }

            // Where
            if (!string.IsNullOrWhiteSpace(queryProps?.WhereSql)) {
                query += " WHERE " + queryProps.WhereSql;
            }

            // Order By
            if (queryProps?.OrderBy != null) {
                query += " ORDER BY " + queryProps.OrderBy;
            } else {
                query += " ORDER BY (SELECT NULL)";
            }

            // Offset
            bool offsetDefined = (queryProps?.Offset != null && queryProps?.Offset > 0);
            if (offsetDefined) {
                query += " OFFSET " + queryProps.Offset + " ROWS";
            }
            
            // Fetch
            if (queryProps?.Limit != null && queryProps?.Limit > 0) {
                // Limit requires offset, top should be used here instead but meh
                if (!offsetDefined) {
                    query += " OFFSET 0 ROWS"; 
                }

                query += " FETCH NEXT " + queryProps.Limit + " ROWS ONLY";
            }


            // Select with condition
            var results = Query<T>(queryProps.PostEditSQL(query), queryProps?.WhereParams, batchId);

            // Clear model changes
            results.ToList().ForEach(
                res => res.ClearChangedPropertiesList()
            );

            return results;
        }
        

        private string _countQuery;
        /// <inheritdoc />
        public override int Count(QueryProps queryProps, long batchId = -1) {
            // Build Query
            var query = _countQuery ?? (
                _countQuery = $@"SELECT COUNT({Fields.First().TableFieldSql}) AS Count FROM {TableSql} {AliasSql}"
            );
            // Where
            if (!string.IsNullOrWhiteSpace(queryProps?.WhereSql)) {
                query += " WHERE " + queryProps.WhereSql;
            }

            // Offset
            if (queryProps?.Offset != null) {
                query += " OFFSET " + queryProps.Offset + " Rows";
            }
            
            // Query Result
            var result = Query<dynamic>(
                queryProps.PostEditSQL(query), 
                queryProps?.WhereParams, 
                batchId
            ).First();

            return result.Count;
        }

        /// <inheritdoc />
        public override void Update(object model, IEnumerable<string> updateFields = null, 
            string where = null, object whereObj = null, long batchId = -1) {
            
            // Build combined parameter object
            var param = new DynamicParameters(model);
            if(whereObj != null) { 
                param.AddDynamicParams(whereObj);
            }

            // If fields is null and model is anonymous object, lookup properties.
            if (updateFields == null && !(model is T)) {
                var bindingFlags = BindingFlags.Instance | BindingFlags.Public;
                updateFields = model.GetType().GetProperties(bindingFlags).Select(
                    m => m.Name
                );
            }

            // If fields is null, use all NonKeyFields else map Fields by tableFieldsToUpdate
            var updateFieldsInfo = updateFields == null ? NonGeneratedFields : Fields.Where(
                field => updateFields.Contains(field.TableField)
            );

            // Blank update set, return
            if (updateFieldsInfo.Count() < 1) {
                return;
            }

            var updateFieldStr = string.Join(", ", (updateFieldsInfo.Select(
                field => $"{AliasSql}.{field.TableFieldSql}=@{field.ModelField}"
            )));

            // Do Update
            string update = $@"
                UPDATE {AliasSql} SET {updateFieldStr}
                FROM {TableSql} {AliasSql}
                WHERE {where ?? DefaultCondition}";

            Execute(update, param, batchId);

            if (model is DataModel) {
                ((DataModel)model).ClearChangedPropertiesList();
            }
            
        }

        /// <inheritdoc />
        public override void Delete(T model, long batchId = -1) {
            Delete(DefaultCondition, model, batchId);
        }

        /// <inheritdoc />
        public override void Delete(DeleteProps updateProps, long batchId = -1) {
            // Runtime "Sanity" Check for Global
            if (!AllowGlobalDelete && string.IsNullOrWhiteSpace(updateProps?.WhereSql as string)) {
                throw new Exception("DataService.AllowDeleteAll must be enabled to delete all records.");
            }
            
            // Runtime "Sanity" Check
            if (!AllowDelete) {
                throw new Exception("DataService.AllowDelete must be enabled to delete records");
            }
            
            string query = updateProps.Top != null ?
                $"DELETE TOP({updateProps.Top}) FROM {TableSql} {AliasSql}" :
                $"DELETE FROM {TableSql} {AliasSql}";

            // Condition
            if (!string.IsNullOrWhiteSpace(updateProps?.WhereSql as string)) {
                query += " WHERE " + updateProps.WhereSql; 
            }

            // Delete by Condition
            Execute(updateProps.PostEditSQL(query), updateProps?.WhereParams, batchId);
        }

        /// <summary>
        /// Performs a standard Dapper QueryAsync but wraps SqlExceptions with SlackerSqlException
        /// </summary>
        public IEnumerable<U> Query<U>(string sql, object sqlParams = null, long batchId = -1) {
            try {
                if (batchId == -1) {
                    batchId = Thread.CurrentThread.ManagedThreadId;
                }

                return ConnectionManager.ExecuteQuery<U>(sql, sqlParams, batchId);
            }
            catch (SqlException e) {
                throw new SlackerSqlException(e, sql, sqlParams);
            }
        }

        /// <summary>
        /// Performs a standard Dapper ExecuteAsync but wraps SqlExceptions with SlackerSqlException
        /// </summary>
        public void Execute(string sql, object sqlParams = null, long batchId = -1) {
            try {
                if (batchId == -1) {
                    batchId = Thread.CurrentThread.ManagedThreadId;
                }

                ConnectionManager.ExecuteUpdate(sql, sqlParams, batchId);
            }
            catch (SqlException e) {
                throw new SlackerSqlException(e, sql, sqlParams);
            }
        }

        #endregion



        #region Helper Methods
        /// <summary>
        /// Converts dataset to DataTable
        /// </summary>
        public static DataTable ToDataTable(IEnumerable<T> data, params string[] members) {
            var dataTable = new DataTable(typeof(T).Name);
            using (var reader = ObjectReader.Create(data, members)) {
                dataTable.Load(reader);
            }

            return dataTable;
        }
        /// <summary>
        /// Retrieves a registered DataService instance for type
        /// </summary>
        public static DataService<T> GetService() {
            return (DataService<T>) SERVICE_REGISTRY.GetService(typeof(T));
        }

        #endregion

        #region Batches
        /// <inheritdoc />
        public override void StartBatch(long batchId = -1, string createTransactionAs = null) {
            if (batchId == -1) {
                batchId = Thread.CurrentThread.ManagedThreadId;
            }
            ConnectionManager.StartBatch(batchId, createTransactionAs);
        }
        /// <inheritdoc />
        public override void EndBatch(long batchId = -1) {
            if (batchId == -1) {
                batchId = Thread.CurrentThread.ManagedThreadId;
            }
            ConnectionManager.EndBatch(batchId);
        }
        /// <inheritdoc />
        public override void RollbackBatch(long batchId = -1, bool closeConnection = true) {
            if (batchId == -1) {
                batchId = Thread.CurrentThread.ManagedThreadId;
            }
            ConnectionManager.RollbackBatch(batchId, closeConnection);
        }
        #endregion

    }


}
