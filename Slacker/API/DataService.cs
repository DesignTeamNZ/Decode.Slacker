using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Slacker.Helpers;
using System.Linq.Expressions;
using System.Data;
using System.Dynamic;
using FastMember;
using System.Threading.Tasks;
using Slacker.Exceptions;
using System.Threading;
using Slacker.Interfaces;

namespace Slacker {



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
                            field => field.FieldNameAsSql
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
                            field => field.FieldNameAsSql
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
                        field => $"@{field.BindingPropName}"
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
                            field => $"@{field.BindingPropName}"
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
                            field => $"{AliasSql}.{field.FieldNameAsSql} AS [{field.BindingPropName}]"
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
                            field => $@"{AliasSql}.{field.FieldNameAsSql} = @{field.BindingPropName}"
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
                        typeof(T).Name.PadRight(3, '0').Substring(0, 4);
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
                        field => $@"({AliasSql}.{field.FieldNameAsSql} = @{field.BindingPropName})"
                    ));
                }
                return _defaultCondition;
            }
        }

        private List<IDataFieldDefinition> _primaryKey;
        /// <summary>
        /// Return Primary Key Fields 
        /// </summary>
        public List<IDataFieldDefinition> PrimaryKey {
            get {
                if (_primaryKey == null) {
                    _primaryKey = Fields.Where(
                        field => field.KeyType == KeyType.PRIMARY_KEY
                    ).ToList();
                }
                return _primaryKey;
            }
        }

        private List<IDataFieldDefinition> _nonGeneratedFields;
        /// <summary>
        /// Return NonKey Fields
        /// </summary>
        public List<IDataFieldDefinition> NonGeneratedFields {
            get {
                if (_nonGeneratedFields == null) {
                    _nonGeneratedFields = Fields.Where(
                        field => field.KeyType == KeyType.NONE
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
        public ISqlConnectionService ConnectionManager { get; set; }

        /// <summary>
        /// Contains DataField info
        /// </summary>
        public List<IDataFieldDefinition> Fields { get; protected set; }
        
        /// <summary>
        /// Initializes a new DataService with a given connection
        /// </summary>
        /// <param name="connectionManager">Connection manager</param>
        public DataService(SqlConnectionService connectionManager = null) {
            this.ConnectionManager = connectionManager;

            // Register Fields/Properties
            // Potential TODO: Replace with FastMember
            this.Fields = GetModelDataFieldDefinition().ToList();
            
            // Add DataService as managing service for model (T)
            SERVICE_REGISTRY.Register(typeof(T), this);
        }

        public virtual IEnumerable<IDataFieldDefinition> GetModelDataFieldDefinition() {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

            // Maps MemberInfo to IDataFieldDefinition
            // returns Null if not found
            IDataFieldDefinition getDataFieldDefinition(MemberInfo member) {

                // Get underlaying prop/field type
                var memberType = (Type)null;
                if (member.MemberType == MemberTypes.Property) {
                    // Member is a Property, Return Type
                    var prop = (PropertyInfo) member;
                    memberType = prop.PropertyType;

                } else if (member.MemberType == MemberTypes.Field) {
                    // Member is a Field, Return Type
                    var field = (FieldInfo) member;
                    memberType = field.FieldType;
                } else {
                    // Not Field or Property return null
                    return null;
                }

                // If SlackerIgnoreAttribute is defined 
                // Or if properties/fields are defined on the DataModel type
                if (member.GetCustomAttribute<SlackerIgnoreAttribute>() != null ||
                    member.ReflectedType == typeof(DataModel)) {
                    return null;
                }
                // Get DataFieldDefinition from FieldAttribute
                var dataFieldDefinition = member.GetCustomAttribute<FieldAttribute>();
                if (dataFieldDefinition != null) {
                    dataFieldDefinition.BindingPropName = member.Name;
                    return dataFieldDefinition;
                }

                // Not found, use Generic Data Field Definition
                return new GenericDataFieldDefinition() {
                    BindingPropName = member.Name
                };
            }

            return typeof(T).GetMembers(bindingFlags).Select(
                getDataFieldDefinition
            ).Where(
                fieldDefinition => fieldDefinition != null    
            );
        }

        [Obsolete]
        /// <summary>
        /// Initializes a new DataService with a given connection
        /// </summary>
        /// <param name="connectionManager">The SqlConnection</param>
        public DataService(SqlConnection conn = null) 
            : this(conn == null ? null : 
                  SqlConnectionService.FromConnectionString(conn.ConnectionString)
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
                pk => pk.KeyType == KeyType.PRIMARY_KEY
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


                TypeAccessor[model, autoIncField.BindingPropName] = results.Single();
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
                _countQuery = $@"SELECT COUNT({Fields.First().FieldNameAsSql}) AS Count FROM {TableSql} {AliasSql}"
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
                field => updateFields.Contains(field.BindingPropName)
            );

            // Blank update set, return
            if (updateFieldsInfo.Count() < 1) {
                return;
            }

            var updateFieldStr = string.Join(", ", (updateFieldsInfo.Select(
                field => $"{AliasSql}.{field.FieldNameAsSql}=@{field.BindingPropName}"
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
        public override void Delete(DeleteProps deleteProps, long batchId = -1) {
            // Runtime "Sanity" Check for Global
            if (!AllowGlobalDelete && string.IsNullOrWhiteSpace(deleteProps?.WhereSql as string)) {
                throw new Exception("DataService.AllowDeleteAll must be enabled to delete all records.");
            }
            
            // Runtime "Sanity" Check
            if (!AllowDelete) {
                throw new Exception("DataService.AllowDelete must be enabled to delete records");
            }
            
            string query = deleteProps.Top != null ?
                $"DELETE TOP({deleteProps.Top}) FROM {TableSql} {AliasSql}" :
                $"DELETE FROM {TableSql} {AliasSql}";

            // Condition
            if (!string.IsNullOrWhiteSpace(deleteProps?.WhereSql as string)) {
                query += " WHERE " + deleteProps.WhereSql; 
            }

            // Delete by Condition
            Execute(deleteProps.PostEditSQL(query), deleteProps?.WhereParams, batchId);
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


    /// <summary>
    /// A generic dataservice for models that don't need an extensible dataservice
    /// </summary>
    public class GenericDataService<T> : DataService<T> where T : DataModel, new() {
        public GenericDataService(SqlConnectionService connectionManager = null)
            : base(connectionManager) {
        }
    }

}
