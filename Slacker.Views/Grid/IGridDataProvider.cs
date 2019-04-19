using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastMember;

namespace Slacker.Views.Grid {

    public interface IGridPagination {
        int CurrentPage { get; }
        int PageCount { get; }
        int TotalRecordCount { get; }

        string OrderByField { get; }
        bool OrderByDesc { get; }

        event EventHandler OnRecordSetLoaded;

        int? PageSize { get; set; }

        void OrderBy(string field, bool desc);
        void SetPage(int pageNo);

        void Load();

    }
    
    public interface IGridPagination<T> : IGridPagination {
        IEnumerable<T> RecordSet { get; }
    }

    public abstract class DataServiceGridPagination<T> : IGridPagination<T> where T : DataModel, new() {

        public DataService<T> DataService { get; protected set; }
        public IEnumerable<T> RecordSet { get; protected set; }

        public int CurrentPage { get; protected set; } = 1;
        public virtual int TotalRecordCount { get; protected set; }
        public int PageCount {
            get {
                if (PageSize == null || PageSize < 1 || TotalRecordCount < 1) {
                    return 1;
                }

                return (int) Math.Ceiling((float) TotalRecordCount / (float) PageSize);
            }
        }

        public string OrderByField { get; protected set; }
        public bool OrderByDesc { get; protected set; }

        public abstract event EventHandler OnRecordSetLoaded;

        public int? PageSize { get; set; }
        public string QueryCondition { get; set; }
        public object QueryConditionParams { get; set; }
        
        public abstract void OrderBy(string field, bool desc);
        public abstract void SetPage(int pageNo);
        public abstract void Load();
    }


    public class PreLoadingGridDataServiceProvider<T> : DataServiceGridPagination<T> where T : DataModel, new() {

        public override event EventHandler OnRecordSetLoaded;

        public IEnumerable<T> MasterRecordSet { get; protected set; }
        public override int TotalRecordCount { get => MasterRecordSet.Count(); }


        private TypeAccessor _typeAccessor;
        protected TypeAccessor TypeAccessor {
            get => _typeAccessor ?? (_typeAccessor = TypeAccessor.Create(typeof(T)));
        }


        public override void OrderBy(string field, bool desc) {
            this.OrderByField = field;
            this.OrderByDesc = desc;
            ApplyFilters();
        }

        public override void SetPage(int pageNo) {
            if (pageNo < 1) {
                throw new Exception("Param 'pageNo' must be postive.");
            }

            this.CurrentPage = pageNo;
            ApplyFilters();
        }

        protected void ApplyFilters() {
            var workingSet = MasterRecordSet;

            // Order
            if (OrderByField != null) {
                workingSet = OrderByDesc ?
                    workingSet.OrderByDescending(t => TypeAccessor[t, OrderByField]) :
                    workingSet.OrderBy(t => TypeAccessor[t, OrderByField]);
            }

            // Pagination
            if (PageSize != null && PageSize > 0) {
                workingSet = workingSet.Skip((CurrentPage - 1) * (int)PageSize).Take((int)PageSize);
            }

            // Set RecordSet
            RecordSet = workingSet.ToList().AsReadOnly();
            OnRecordSetLoaded?.Invoke(this, null);
        }

        public override void Load() {
            if (DataService == null) {
                throw new Exception("Could not load recordset. DataService cannot be null.");
            }

            var queryProps = new QueryProps {
                WhereSql = QueryCondition,
                WhereParams = QueryConditionParams
            };

            // Build Master Record Set
            this.MasterRecordSet = DataService.Select(
                queryProps
            ).ToList().AsReadOnly();

            // Apply Filters and Call Events
            ApplyFilters();
        }

    }

    public class PartialLoadingGridDataServiceProvider<T> : DataServiceGridPagination<T> where T : DataModel, new() {

        public override event EventHandler OnRecordSetLoaded;
        
        public override void OrderBy(string field, bool desc) {
            this.OrderByField = field;
            this.OrderByDesc = desc;
            Load();
        }

        public override void SetPage(int pageNo) {
            if (pageNo < 1) {
                throw new Exception("Param 'pageNo' must be postive.");
            }

            this.CurrentPage = pageNo;
            Load();
        }
        
        public override void Load() {
            if (DataService == null) {
                throw new Exception("Could not load recordset. DataService cannot be null.");
            }

            // Build Query Props
            var queryProps = new QueryProps {
                WhereSql = QueryCondition,
                WhereParams = QueryConditionParams,
            };

            // Order By
            if (OrderByField != null) {
                queryProps.OrderBy = $"{OrderByField} {(OrderByDesc ? "DESC" : "ASC")}";
            }

            // Do a partal load
            long batchId = DateTime.Now.Ticks;
            DataService.StartBatch(batchId);

            // Get Total Record Count from DB
            this.TotalRecordCount = DataService.Count(queryProps, batchId);

            // Apply partial query
            if (PageSize != null && PageSize > 0) {
                queryProps.Limit = PageSize;
                queryProps.Offset = (CurrentPage - 1) * PageSize;
            }

            // Get active Record Set
            this.RecordSet = DataService.Select(queryProps).ToList().AsReadOnly();
            OnRecordSetLoaded?.Invoke(this, null);
        }



    }
}
