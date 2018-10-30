using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Slacker.Helpers {

    public static class AsyncHelpers {
        /// <summary>
        /// Execute's an async Task<T> method which has a void return value synchronously
        /// </summary>
        /// <param name="task">Task<T> method to execute</param>
        public static void RunSync(Task task) {
            try {
                task.Wait();
            }
            catch (AggregateException e) {
                // GetResult should throw initial exception, 
                // otherwise just throw aggregate exception.
                task.GetAwaiter().GetResult();
                throw e;
            }
        }

        /// <summary>
        /// Execute's an async Task<T> method which has a T return type synchronously
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="task">Task<T> method to execute</param>
        /// <returns></returns>
        public static T RunSync<T>(Task<T> task) {
            try {
                task.Wait();
            }
            catch (AggregateException) {}

            return task.GetAwaiter().GetResult();
        }
        
    }
}
