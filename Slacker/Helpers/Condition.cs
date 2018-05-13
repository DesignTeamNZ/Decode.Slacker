using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Helpers
{

    /// <summary>
    /// Utility Class for building Dapper Conditions to be used in Query Strings
    /// </summary>
    public class Condition {
        /// <summary>
        /// Default Mapping Profile for Condition Building using Predicate
        /// </summary>
        private static IConditionMappingProfile DEFAULT_MAPPING_PROFILE = new SimpleConditionMappingProfile();
        
        /// <summary>
        /// The Query String
        /// </summary>
        public string QueryString { get; set; }
        
        /// <summary>
        /// Parameter Object
        /// </summary>
        public dynamic Parameters { get; set; }

        /// <summary>
        /// Builds a Static Condition from Query String and Parameter Object
        /// </summary>
        /// <param name="queryString">The Query String</param>
        /// <param name="parameters">Parameters List</param>
        protected Condition(string queryString, dynamic parameters) {
            this.QueryString = queryString;
            this.Parameters = parameters;
        }
        
        /// <summary>
        /// Builds a Static Condition from Query String and Parameter Object
        /// </summary>
        /// <param name="queryString">The Query String</param>
        /// <param name="parameters">Parameters List</param>
        /// <returns>Condition</returns>
        protected static Condition Where(String queryString, dynamic parameters) {
            return new Condition(queryString, parameters);
        }
        
        /// <summary>
        /// Compiles a .NET lambda expression into a SQL readable condition
        /// Use with caution as this implementation can be slow.Perform caching where possible
        /// and consider replacing parameters on existing conditions where possible
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="predicate">Condition</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Condition build from predicate</returns>
        public static Condition Where<T>(Expression<Func<T, bool>> predicate, dynamic parameters) {
            string queryString = DEFAULT_MAPPING_PROFILE.DoMapCondition<T>(predicate);
            return new Condition(queryString, parameters);
        }

        /// <summary>
        /// Sets the default lambda expression mapping profile
        /// </summary>
        /// <param name="profile">Profile to be set</param>
        public static void SetMappingProfile(IConditionMappingProfile profile) {
            DEFAULT_MAPPING_PROFILE = profile;
        }

    }

    public interface IConditionMappingProfile {
        string DoMapCondition<T>(Expression<Func<T, bool>> predicate);
    }

    public class SimpleConditionMappingProfile : IConditionMappingProfile
    {

        /// <summary>
        /// A list of replacements to be performed while mapping Expression strings to SQL conditions
        /// </summary>
        protected Dictionary<string, string> replacements = new Dictionary<string, string>() {
            { "== \"like:", "LIKE " },
            { "== \"in:(", "IN (" },
            { "AndAlso", "AND" },
            { "OrElse",  "OR" },
            { "==", "=" },
            { "\"", "" }
        }; 

        /// <summary>
        /// Maps Expression to Query String
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="predicate">Expression to be mapped to Query String</param>
        /// <returns>Query String</returns>
        public string DoMapCondition<T>(Expression<Func<T, bool>> predicate) {
            StringBuilder exprBody = new StringBuilder(predicate.Body.ToString());

            // Strip parameter name from any property references
            var param = predicate.Parameters.First();
            exprBody.Replace(param.Name + ".", "");

            // Do Mapping Replacements
            foreach(KeyValuePair<string, string> kv in replacements) {
                exprBody.Replace(kv.Key, kv.Value);
            }

            return exprBody.ToString();
        }
        
    }


}
