﻿using Dapper;
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

    public abstract class DataService<T> : DataServiceProvider<T> where T : DataModel {

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
                        NonKeyFields.Select(
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
        public string QueryNonKeyModelRefs {
            get {
                if (_queryNonKeyModelRefs == null) {
                    _queryNonKeyModelRefs = string.Join(",",
                        NonKeyFields.Select(
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
                        NonKeyFields.Select(
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
                        field => field.IsPrimaryKeyField
                    ).ToList();
                }
                return _primaryKey;
            }
        }

        private List<DataModelField> _nonKeyFields;
        /// <summary>
        /// Return NonKey Fields
        /// </summary>
        public List<DataModelField> NonKeyFields {
            get {
                if (_nonKeyFields == null) {
                    _nonKeyFields = Fields.Where(
                        field => !field.IsPrimaryKeyField
                    ).ToList();
                }
                return _nonKeyFields;
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
        /// Contains DataField info
        /// </summary>
        public List<DataModelField> Fields { get; protected set; }

        /// <summary>
        /// Initializes a new DataService with a given connection string
        /// </summary>
        /// <param name="connStr">The Connection String</param>
        public DataService(string connStr) : this(new SqlConnection(connStr)) {}
        

        /// <summary>
        /// Initializes a new DataService with a given connection
        /// </summary>
        /// <param name="sqlConnection">The SqlConnection</param>
        public DataService(SqlConnection sqlConnection = null) {
            this.Connection = sqlConnection;

            // Register Fields/Properties
            // Potential TODO: Replace with FastMember
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            this.Fields = typeof(T).GetFields(bindingFlags).Cast<MemberInfo>().Concat(
                typeof(T).GetProperties(bindingFlags)
            ).Select(
                memberInfo => new DataModelField(memberInfo)
            ).Where(
                dataField => !dataField.IsIgnored
            ).ToList();
            
            // Add DataService as managing service for model (T)
            SERVICE_REGISTRY.Register(typeof(T), this);
        }
        

        #region CRUD Functions
        private string _insertQuery;
        public override void Insert(params T[] models) {

            // Build Insert Query
            if(_insertQuery == null) { 
                _insertQuery = $@"
                    INSERT INTO [{Table}] ({QueryNonKeyFieldCols}) 
                    VALUES ({QueryNonKeyModelRefs});";
            }

            // Do Insert
            // todo Execute Multiple
            foreach (var model in models) {
                Connection.Execute(_insertQuery, model);
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
                res => res.ClearChangeTracking()
            );

            return results;
        }

        /// <summary>
        /// Updates a model using default condition change
        /// </summary>
        /// <param name="model"></param>
        /// <param name="onlyChanged">Only update changed fields on the model</param>
        public void Update(T model, bool updateOnlyChangedProperties = true) {



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableFieldsToUpdate">The fields to be updated or null for all</param>
        /// <param name="where">The condition or null for all</param>
        public void Update(object model, string[] tableFieldsToUpdate = null, string where = null, dynamic whereObj = null) {
            
            // Build combined parameter object
            var param = new DynamicParameters(model);
            if(whereObj != null) { 
                param.AddDynamicParams(whereObj);
            }
            
            // If fields is null and model is dynamic (IDictionary), use model keys
            if (tableFieldsToUpdate == null && model is IDictionary<string, object>) {
                tableFieldsToUpdate = (model as IDictionary<string, object>).Keys.ToArray();
            }

            // If fields is null, use all NonKeyFields else map Fields by tableFieldsToUpdate
            var updateFields = tableFieldsToUpdate == null ? NonKeyFields : Fields.Where(
                field => tableFieldsToUpdate.Contains(field.TableField)
            );

            var updateFieldStr = string.Join(", ", (updateFields.Select(
                field => $"[{field.TableField}]=@{field.ModelField}"
            )));

            // Do Update
            string update = $@"UPDATE {Table} SET {updateFieldStr} WHERE {where ?? DefaultCondition}";
            Connection.Execute(update, param);
            
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
