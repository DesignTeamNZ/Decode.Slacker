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

namespace Slacker {

    public interface IDataService {
        // todo
    }

    public abstract class DataServiceProvider<T> : IDataService where T: DataModel, new() {

        #region Insert
        /// <summary>
        /// Perform insert query using data model
        /// </summary>
        /// <param name="model">The Model</param>
        public void Insert(T model, bool loadGeneratedKeys = true) {
            Insert(new[] { model }, loadGeneratedKeys);
        }

        /// <summary>
        /// Perform insert query using data model(s)
        /// </summary>
        /// <param name="models">The Models</param>
        public abstract void Insert(T[] models, bool loadGeneratedKeys = true);
        #endregion

        #region Select
        /// <summary>
        /// Select all records
        /// </summary>
        /// <returns>IEnumerable<typeparamref name="T"/> results</returns>
        public IEnumerable<T> SelectAll() {
            return SelectWhere("", false);
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
        public abstract IEnumerable<T> SelectWhere(string where, object whereParam);
        #endregion

        #region Update

        #endregion

        #region Delete

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
                            field => $@"[{field.TableField}]"
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
                            field => $@"[{field.TableField}]"
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
                            field => $"[{Alias}].[{field.TableField}] AS [{field.ModelField}]"
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
                            field => $@"[{Alias}].[{field.TableField}] = @{field.ModelField}"
                        )
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
                if (!_tableAttributeSearched) {
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
                        field => $@"([{Alias}].[{field.TableField}] = @{field.ModelField})"
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
        public SqlConnection Connection { get; set; }

        /// <summary>
        /// Contains DataField info
        /// </summary>
        public List<DataModelField> Fields { get; protected set; }
        
        /// <summary>
        /// Initializes a new DataService with a given connection
        /// </summary>
        /// <param name="sqlConnection">The SqlConnection</param>
        public DataService(SqlConnection sqlConnection = null) {
            this.Connection = sqlConnection;

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
        

        #region CRUD Functions
        private string _insertQuery;
        public override void Insert(T[] models, bool loadGeneratedKeys = true) {

            // Build Insert Query
            if(_insertQuery == null) { 
                _insertQuery = $@"
                    INSERT INTO [{Table}] ({QueryNonKeyFieldCols}) 
                    VALUES ({QueryNonKeyGeneratedModelRefs});";
            }

            var autoIncField = PrimaryKey.FirstOrDefault(
                pk => pk.FieldAttribute.IsGenerated
            );

            // Do Insert
            foreach (var model in models) {
                if (autoIncField == null || !loadGeneratedKeys) {
                    Connection.Execute(_insertQuery, model);
                    continue;
                }

                // Update and save generated id to model
                var id = Connection.Query<int>(
                    _insertQuery + @"SELECT CAST(SCOPE_IDENTITY() as int)",
                    model
                ).Single();

                TypeAccessor[model, autoIncField.ModelField] = id;
                model.ChangedProperties.Clear();
            }
        }
        
        public override IEnumerable<T> SelectByKey(object whereParam) {
            return SelectWhere(DefaultCondition, whereParam);
        }
        
        private string _selectQuery;
        public override IEnumerable<T> SelectWhere(string where, object whereParam) {
            // Build Query
            if (_selectQuery == null) {
                _selectQuery = $@"SELECT {QuerySelects} FROM [{Table}] [{Alias}]";
            }
            
            // Select with condition
            var results = Connection.Query<T>(
                _selectQuery + (!string.IsNullOrEmpty(where) ?  " WHERE " + where : ""), 
                whereParam
            );

            // Clear model changes
            results.ToList().ForEach(
                res => res.ChangedProperties.Clear()
            );

            return results;
        }

        /// <summary>
        /// Updates from model using default primary key condition 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="onlyChanged">Only update changed fields on the model</param>
        public void Update(T model, bool updateOnlyChangedProperties = true) {
            if (updateOnlyChangedProperties) {
                if (model.ChangedProperties.Count < 1) {
                    return;
                }

                Update(model, model.ChangedProperties);
                return;
            }

            Update(model);
        }

        /// <summary>
        /// Updates from model
        /// </summary>
        /// <param name="model">T or anonymous Object (Uses reflection unless updateFields is set)</param>
        /// <param name="updateFields">The fields to be updated or null for all</param>
        /// <param name="where">The condition or null for all</param>
        /// <param name="whereObj">Additional where object</param>
        public void Update(object model, IEnumerable<string> updateFields = null, 
            string where = null, object whereObj = null) {
            
            // Build combined parameter object
            var param = new DynamicParameters(model);
            if(whereObj != null) { 
                param.AddDynamicParams(whereObj);
            }

            // If fields is null and model is anonymous object, lookup properties.
            if (updateFields == null && !(model is T)) {
                updateFields = model.GetType().GetMembers().Select(
                    m => m.Name
                );
            }

            // If fields is null, use all NonKeyFields else map Fields by tableFieldsToUpdate
            var updateFieldsInfo = updateFields == null ? NonGeneratedFields : Fields.Where(
                field => updateFields.Contains(field.TableField)
            );

            var updateFieldStr = string.Join(", ", (updateFieldsInfo.Select(
                field => $"[{Alias}].[{field.TableField}]=@{field.ModelField}"
            )));

            // Do Update
            string update = $@"
                UPDATE [{Alias}] SET {updateFieldStr}
                FROM [{Table}] [{Alias}]
                WHERE {where ?? DefaultCondition}";

            Connection.Execute(update, model);

            if (model is DataModel) {
                ((DataModel) model).ChangedProperties.Clear();
            }
            
        }
        
        public override void Delete(T model) {
            Delete(DefaultCondition, model);
        }
        
        public override void Delete(string where, object whereParam) {
            string query = $@"DELETE FROM {Table}";

            if (string.IsNullOrEmpty(where)) {
                // Runtime "Sanity" Check
                if (!AllowGlobalDelete) {
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
        public DataTable ConvertDataModelsToDataTable(IEnumerable<dynamic> models) {
            var dataTable = new DataTable(Table);

            // Build DataTable Structure
            var columns = Fields.Select(dataField => new DataColumn(
                    dataField.TableField,
                    dataField.ModelFieldType
            ));
            dataTable.Columns.AddRange(columns.ToArray());

            // DataModel -> DataRow Conversion
            models.Cast<IDictionary<string, object>>().ToList().ForEach(
                dataModel => dataTable.Rows.Add(
                    // Get Values from Model
                    Fields.Select(field => 
                        dataModel.TryGetValue(
                            field.ModelField, out object value
                        ) ? value : null
                    )
                )
            );

            return dataTable;
        }

        /// <summary>
        /// Retrieves a registered DataService for Model
        /// </summary>
        /// <typeparam name="M">The DataModel Type</typeparam>
        /// <returns>The responsible DataService for given Model M</returns>
        public static DataService<T> GetModelService() {
            return (DataService<T>) SERVICE_REGISTRY.GetService(typeof(T));
        }
        #endregion

    }
}
