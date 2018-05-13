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
using System.Windows.Forms;

namespace Slacker {

    public interface IDataService { }

    public interface IDataService<T> {

        // Create
        bool Insert(T model);
        bool Insert(IEnumerable<T> models);

        // Retrieve
        IEnumerable<T> Select();
        IEnumerable<T> Select(Condition where);

        // Update
        bool Update(T model);
        bool Update(dynamic model, Condition where);
        bool Update(dynamic model, string[] changedFields, Condition where);
        
        // Delete
        bool Delete(T model);
        bool Delete(Condition where);
        
        // Database(SQL) Helper Methods
        IEnumerable<dynamic> DoSafeQuery(string query);
        IEnumerable<M> DoSafeQuery<M>(string query);
        bool DoSafeUpdate(string update, object model);
    }

    public class DataService<T> : IDataService<T> {

        protected static ServiceRegistry SERVICE_REGISTRY = new ServiceRegistry();

        protected SqlConnection Connection { get; set; }
        protected DataTableConverter<T> DataTableConverter { get; set; }

        // <tblField>
        public List<string> PrimaryKey { get; private set; }

        // <tblField, classField>
        public Dictionary<string, string> ModelFields { get; private set; }
        public Dictionary<string, string> ModelFieldsNonKey { get; private set; }


        // Stores Default Condition based on PrimaryKey
        protected string keyCondition;

        public DataService() {}

        public DataService(SqlConnection sqlConnection) {
            this.Connection = sqlConnection;
            this.DataTableConverter = new DataTableConverter<T>();

            this.PrimaryKey = new List<string>();
            this.ModelFields = new Dictionary<string, string>();

            RegisterModelInfo();

            // Add DataService as managing service for model (T)
            SERVICE_REGISTRY.Register(typeof(T), this);
        }

        /// <summary>
        /// Performs reflection on given DataModel (T) and caches result
        /// </summary>
        private void RegisterModelInfo() {
            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo info in fields) {
                Field field = info.GetCustomAttribute<Field>();
                if (field == null) {
                    ModelFields.Add(info.Name, info.Name);
                    continue;
                }

                // Ignored Fields
                if (field.Ignored)
                    continue;

                // Add Primary Key
                if (field.IsPrimary)
                    PrimaryKey.Add(field.Name ?? info.Name);
                else
                    ModelFieldsNonKey.Add(field.Name ?? info.Name, info.Name);

                // Add Field
                ModelFields.Add(field.Name ?? info.Name, info.Name);
            }
        }


        #region CRUD Functions

        public bool Insert(T model) {
            throw new NotImplementedException();
        }

        public bool Insert(IEnumerable<T> models) {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Select() {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Select(Condition where) {
            throw new NotImplementedException();
        }

        public bool Update(T model) {
            throw new NotImplementedException();
        }

        public bool Update(dynamic model, Condition where) {
            throw new NotImplementedException();
        }

        public bool Update(dynamic model, string[] changedFields, Condition where) {
            throw new NotImplementedException();
        }

        public bool Delete(T model) {
            throw new NotImplementedException();
        }

        public bool Delete(Condition where) {
            throw new NotImplementedException();
        }

        #endregion


        #region Query Building 

        // Create
        /// <summary>
        /// SQL Insert Query
        /// </summary>
        protected string InsertQuery {
            get {
                return InsertQuery ?? (InsertQuery = BuildInsert());
            }
            set {
                InsertQuery = value;
            }
        }

        /// <summary>
        /// Builds SQL Insert Query
        /// </summary>
        /// <returns>SQL Insert Query</returns>
        protected string BuildInsert() {
            IEnumerable<string> tableFields = ModelFields.Keys.Select(field => "[" + field + "]");
            IEnumerable<string> modelFields = ModelFields.Values.Select(field => "@" + field);

            return $@"INSERT INTO {GetTableName()} ( {string.Join(",", tableFields)} ) 
                        VALUES ( {string.Join(",", modelFields) } );";
        }

        // Retrieve
        /// <summary>
        /// SQL Select Query
        /// </summary>
        protected string SelectQuery {
            get {
                return SelectQuery ?? (SelectQuery = BuildSelect());
            }
            set {
                SelectQuery = value;
            }
        }

        /// <summary>
        /// Builds SQL Select Query
        /// </summary>
        /// <returns></returns>
        protected string BuildSelect() {
            return $@"SELECT * FROM {GetTableName()}";
        }

        // Update
        /// <summary>
        /// Builds SQL Update Query
        /// </summary>
        /// <param name="changedFields">Which fields need to be updated in the query</param>
        /// <returns>SQL Update Query</returns>
        protected string BuildUpdate(string[] changedFields) {
            string[] setFields = ModelFields.Where(
                kv => changedFields.Contains(kv.Key)
            ).Select(
                kv => $@"[{kv.Key}]=@{kv.Value}"
            ).ToArray();

            return $@"UPDATE {GetTableName()} SET {string.Join(" ", setFields)}";
        }


        // Delete
        /// <summary>
        /// SQL Delete Query
        /// </summary>
        protected string DeleteQuery {
            get {
                return DeleteQuery ?? (DeleteQuery = BuildDelete());
            }
            set {
                DeleteQuery = value;
            }
        }

        /// <summary>
        /// Builds SQL Delete Query
        /// </summary>
        /// <returns>Delete Query string without condition</returns>
        protected string BuildDelete() {
            return $@"DELETE FROM {GetTableName()}";
        }

        #endregion

        

        #region Helper Methods

        /// <summary>
        /// Table Name for Managing Model
        /// </summary>
        public string Table {
            get {
                return Table ?? (Table = GetTableName());
            }
            private set {
                this.Table = value;
            }
        }

        /// <summary>
        /// Gets Table Name for Given Model
        /// </summary>
        /// <returns>Table Name</returns>
        private string GetTableName() {
            var tableAttr = typeof(T).GetCustomAttribute<Table>(false);
            return tableAttr != null ? tableAttr.Name : typeof(T).Name;
        }

        /// <summary>
        /// Called By all "Safe" functions when exception is thrown
        /// </summary>
        public static Action<Exception> OnExceptionThrown = delegate (Exception e) {
            Console.WriteLine(e.ToString());
            if (SlackerApp.Flags.HasFlag(SlackerFlags.ON_EXCEPTION_DISPLAY_MESSAGEBOX)) {
                //TODO: Figure out best implementation here
            }

            if (SlackerApp.Flags.HasFlag(SlackerFlags.ON_EXCEPTION_THROW)) {
                throw e;
            }
        };

        /// <summary>
        /// Performs a Dapper SQL Query and Maps back to dynamic type.
        /// Calls DataService.OnExceptionThrow on exception
        /// </summary>
        /// <typeparam name="M">Model to Map back to</typeparam>
        /// <param name="query">SQL Query to be executed</param>
        /// <returns>IEnumerable containing results from query</returns>
        public IEnumerable<dynamic> DoSafeQuery(string query) {
            try {
                Connection.Open();
                return Connection.Query(query);
            }
            catch (Exception e) {
                OnExceptionThrown(e);
                return null;
            }
            finally {
                Connection.Close();
            }
        }


        /// <summary>
        /// Performs a Dapper SQL Query and Maps back to Model.
        /// Calls DataService.OnExceptionThrow on exception
        /// </summary>
        /// <typeparam name="M">Model to Map back to</typeparam>
        /// <param name="query">SQL Query to be executed</param>
        /// <returns>IEnumerable containing results from query</returns>
        public IEnumerable<M> DoSafeQuery<M>(string query) {
            try {
                Connection.Open();
                return Connection.Query<M>(query);
            }
            catch (Exception e) {
                OnExceptionThrown(e);
                return null;
            }
            finally {
                Connection.Close();
            }
        }

        /// <summary>
        /// Performs an Dapper SQL Update with given parameter model.
        /// Calls DataService.OnExceptionThrow on exception
        /// </summary>
        /// <param name="update">The SQL Update String to be executed.</param>
        /// <param name="model">Object model to be handed to dapper for parameter binding.</param>
        /// <returns>Whether the given update was successful or not</returns>
        public bool DoSafeUpdate(string update, object model) {
            try {
                Connection.Open();
                Connection.Execute(update, model);
            }
            catch (Exception e) {
                OnExceptionThrown(e);
                return false;
            }
            finally {
                Connection.Close();
            }

            return true;
        }

        /// <summary>
        /// Retrieves a registered DataService 
        /// </summary>
        /// <typeparam name="M">M : DataModel</typeparam>
        /// <returns>The responsible DataService for given Model M</returns>
        public static DataService<M> GetModelService<M>()  {
            return (DataService<M>) SERVICE_REGISTRY.GetService(typeof(M));
        }
        
        #endregion

    }
}
