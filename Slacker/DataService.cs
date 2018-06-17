using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Slacker.Helpers;
using Slacker.Helpers.Attributes;
using Slacker.Helpers.DataTables;
using System.Linq.Expressions;

namespace Slacker {

    public interface IDataService {
        // todo
    }

    public abstract class DataServiceProvider<T> : IDataService where T: DataModel {

        #region Insert
        /// <summary>
        /// Perform insert query using data model
        /// </summary>
        /// <param name="model">The Model</param>
        public void Insert(T model) {
            Insert(new[] { model });
        }

        /// <summary>
        /// Perform insert query using data model(s)
        /// </summary>
        /// <param name="models">The Models</param>
        public abstract void Insert(params T[] models);
        #endregion

        #region Select
        /// <summary>
        /// Select all records
        /// </summary>
        /// <returns>IEnumerable<typeparamref name="T"/> results</returns>
        public IEnumerable<T> SelectAll() {
            return Select("", false);
        }
        
        /// <summary>
        /// Select using Expression and Parameter Object
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="whereParam"></param>
        /// <returns>IEnumerable<typeparamref name="T"/> results</returns>
        public IEnumerable<T> Select(Expression<Func<T, bool>> predicate, object whereParam) {
            return Select(Condition.Where<T>(predicate, whereParam));
        }

        /// <summary>
        /// Perform a select query with Condition
        /// </summary>
        /// <param name="where">The where condition</param>
        /// <returns>IEnumerable<typeparamref name="T"/> results</returns>
        public IEnumerable<T> Select(Condition where) {
            return Select(where.QueryString, where.Parameters);
        }
        
        /// <summary>
        /// Selects using a default condition with param object
        /// </summary>
        /// <param name="whereParam"></param>
        /// <returns>IEnumerable<typeparamref name="T"/> results</returns>
        public abstract IEnumerable<T> SelectByKey(object whereParam);

        /// <summary>
        /// Perform a select query with Condition
        /// </summary>
        /// <param name="where">Condition query</param>
        /// <param name="whereParam">Condition parameter</param>
        /// <returns>IEnumerable<typeparamref name="T"/>results</returns>
        public abstract IEnumerable<T> Select(string where, object whereParam);
        #endregion

        #region Update

        #endregion

        #region Delete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        public void Delete(Condition where) {
            Delete(where.QueryString, where.Parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public abstract void Delete(T model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="whereParam"></param>
        public abstract void Delete(string where, object whereParam);
        #endregion

    }

    public class DataService<T> : DataServiceProvider<T> where T : DataModel {

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

        private string _queryFieldCols;
        /// <summary>
        /// Returns a pre-generated field string for table fields
        /// </summary>
        public string QueryFieldCols {
            get {
                if (_queryFieldCols == null) {
                    _queryFieldCols = string.Join(",", Fields.Keys.Select(
                        col => $@"[{col}]"
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
                        NonKeyFields.Keys.Select(
                            col => $@"[{col}]"
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
                    _queryModelRefs = string.Join(",", Fields.Values.Select(
                        col => $@"@{col}"
                    ));
                }
                return _queryModelRefs;
            }
        }

        private string _queryNonKeyModelRefs;
        /// <summary>
        /// Returns a pre-generated field string for table non-key fields
        /// </summary>
        public string QueryNonKeyModelRefs {
            get {
                if (_queryNonKeyModelRefs == null) {
                    _queryNonKeyModelRefs = string.Join(",",
                        NonKeyFields.Values.Select(
                            col => $@"@{col}"
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
                        NonKeyFields.Select(
                            kv => $@"[{Alias}].[{kv.Key}] AS [{kv.Value}]"
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
                        NonKeyFields.Select(kv => $@"[{Alias}].[{kv.Key}] = @{kv.Value}")
                    );
                }
                return _queryDefaultUpdateRefs;
            }
        }


        private bool _tableAttributeSearched;
        public Table _tableAttribute;
        /// <summary>
        /// Returns the Table Attribute if defined for this table
        /// </summary>
        public Table TableAttribute {
            get {
                if (_tableAttributeSearched) {
                    _tableAttribute = typeof(T).GetCustomAttribute<Table>();
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
                    _table = TableAttribute?.Name ?? typeof(T).Name;
                }
                return _table;
            }
            set {
                _table = value;
            }
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

        
        private string _defaultCondition;
        /// <summary>
        /// Default condition based on Primary Key
        /// </summary>
        public string DefaultCondition {
            get {
                if (_defaultCondition == null) {
                    _defaultCondition = string.Join(" AND ", PrimaryKey.Select(
                        field => $@"([{Alias}].[{field}] = @{Fields[field]})"
                    ));
                }
                return _defaultCondition;
            }
        }

        private DataTableConverter<T> _datatableConverter;
        /// <summary>
        /// Slacker Object to DataTable Converter
        /// </summary>
        public DataTableConverter<T> DataTableConverter {
            get {
                if (_datatableConverter == null) {
                    _datatableConverter = new DataTableConverter<T>();
                }
                return _datatableConverter;
            }
        }

        /// <summary>
        /// Enable this setting to allow this model to use delete
        /// </summary>
        public bool AllowDelete { get; set; }

        /// <summary>
        /// Enable this setting to allow this model to use delete all
        /// </summary>
        public bool AllowDeleteAll { get; set; }

        /// <summary>
        /// Enable this setting to allow global updates on this service
        /// </summary>
        public bool AllowGlobalUpdates { get; set; }
        
        /// <summary>
        /// The SQLConnection for this DataService
        /// </summary>
        public SqlConnection Connection { get; set; }

        /// <summary>
        /// Table key fields
        /// </summary>
        public List<string> PrimaryKey { get; set; }

        /// <summary>
        /// Table Fields
        /// TableColumn -> ModelProperty
        /// </summary>
        public Dictionary<string, string> Fields { get; private set; }

        /// <summary>
        /// Table Non-Key Fields
        /// TableColumn -> ModelProperty
        /// </summary>
        public Dictionary<string, string> NonKeyFields { get; private set; }


        /// <summary>
        /// Initializes a new DataService with a given connection
        /// </summary>
        /// <param name="sqlConnection">The SqlConnection</param>
        public DataService(SqlConnection sqlConnection = null) {
            this.Connection = sqlConnection;

            // Get Model Properties
            var props = typeof(T).GetProperties(
                BindingFlags.Instance | BindingFlags.Public
            ).ToList();
            
            // Register Fields
            this.PrimaryKey = new List<string>();
            this.Fields = new Dictionary<string, string>();
            this.NonKeyFields = new Dictionary<string, string>();
            //  
            props.ForEach(propInfo => {
                var field = propInfo.GetCustomAttribute<Field>();
                if (field == null) {
                    Fields.Add(propInfo.Name, propInfo.Name);
                    return;
                }

                // Ignored Fields
                if (field.Ignored) {
                    return;
                }
                
                Fields.Add(field.Name ?? propInfo.Name, propInfo.Name);
            });
            
            // Add DataService as managing service for model (T)
            SERVICE_REGISTRY.Register(typeof(T), this);
        }
        

        #region CRUD Functions
        private string _insertQuery;
        public override void Insert(params T[] models) {

            // Build Insert Query
            if(_insertQuery == null) { 
                _insertQuery = $@"
                    INSERT INTO [{Table}] [{Alias}] ({QueryNonKeyFieldCols}) 
                    VALUES  {QueryNonKeyModelRefs};";
            }

            // Do Insert
            // todo Execute Multiple
            foreach (var model in models) {
                Connection.Execute(_insertQuery, model);
            }
        }
        
        public override IEnumerable<T> SelectByKey(object whereParam) {
            return Select(DefaultCondition, whereParam);
        }
        
        private string _selectQuery;
        public override IEnumerable<T> Select(string where, object whereParam) {
            // Build Query
            if (_selectQuery == null) {
                _selectQuery = $@"SELECT {QuerySelects} FROM [{Table}]";
            }

            // Select all if no condition
            if (string.IsNullOrEmpty(where)) {
                return Connection.Query<T>(_selectQuery, null);
            }

            // Select with condition
            return Connection.Query<T>(
                _selectQuery + " WHERE " + where, 
                whereParam
            );
        }

        /// <summary>
        /// Updates a model using default condition chang
        /// </summary>
        /// <param name="model"></param>
        public void Update(T model, bool onlyChanged = true) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="fields">The fields to be updated or null for all</param>
        /// <param name="where">The condition or null for all</param>
        public void Update(object param, string[] fields = null, string where = null) {
            throw new NotImplementedException();
        }
        
        public override void Delete(T model) {
            Delete(DefaultCondition, model);
        }
        
        public override void Delete(string where, object whereParam) {
            string query = $@"DELETE FROM {Table}";

            if (string.IsNullOrEmpty(where)) {
                // Runtime "Sanity" Check
                if (!AllowDeleteAll) {
                    throw new Exception("DataService.AllowDeleteAll must be enabled to delete all records.");
                }
                // Delete All
                Connection.Execute(query);
            }

            // Runtime "Sanity" Check
            if (!AllowDelete) {
                throw new Exception("DataService.AllowDelete must be enabled to delete records");
            }
            // Delete by Condition
            Connection.Execute(query + " WHERE " + where, whereParam);
        }
        #endregion
        
        

        #region Helper Methods
        /// <summary>
        /// Retrieves a registered DataService for Model
        /// </summary>
        /// <typeparam name="M">The DataModel Type</typeparam>
        /// <returns>The responsible DataService for given Model M</returns>
        public static DataService<ST> GetModelService<ST>() where ST: DataModel  {
            return (DataService<ST>) SERVICE_REGISTRY.GetService(typeof(ST));
        }
        #endregion

    }
}
