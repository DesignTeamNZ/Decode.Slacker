using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastMember;

namespace Slacker.Views.Grid {

    public abstract class DataServiceGridPagination<T> : IGridPagination<T> where T : DataModel, new() {


        public abstract event EventHandler RecordSetLoaded;

        public event EventHandler<PreModelEditedEventArgs<T>> PreModelEdited;
        public event EventHandler<ModelEditedEventArgs<T>> ModelEdited;
        public event EventHandler<ModelDeletedEventArgs<T>> ModelDeleted;

        public bool RaisePreModelEditedEvent(object model, string field)
            => RaisePreModelEditedEvent((T) model, field);

        public void RaiseModelEditedEvent(object model, string field)
            => RaiseModelEditedEvent((T) model, field);

        public void RaiseModelDeletedEvent(object model)
            => RaiseModelDeletedEvent((T) model);
        
        public DataService<T> DataService { get; set; }

        public IList<T> RecordSet { get; protected set; }
        IList<object> IGridPagination.RecordSet {
            get => RecordSet.Cast<object>().ToList(); // TODO: Is this too slow?
        }

        public int CurrentPage { get; protected set; } = 1;
        public int? PageSize { get; protected set; }
        public virtual int TotalRecordCount { get; protected set; }

        protected DataServiceGridPagination() { }
        
        public string OrderByField { get; protected set; }
        public bool OrderByDesc { get; protected set; }

        public string QueryCondition { get; set; }
        public object QueryConditionParams { get; set; }
        
        public abstract void OrderBy(string field, bool desc);
        public abstract void SetPage(int pageNo);
        public abstract void SetPageSize(int? value);
        public abstract void Load();
        
        public int PageCount {
            get {
                if (PageSize == null || PageSize < 1 || TotalRecordCount < 1) {
                    return 1;
                }

                return (int)Math.Ceiling((float)TotalRecordCount / (float)PageSize);
            }
        }

        public bool RaisePreModelEditedEvent(T model, string field) {
            var eventArgs = new PreModelEditedEventArgs<T>() {
                Model = model,
                Field = field
            };

            PreModelEdited?.Invoke(this, eventArgs);
            return eventArgs.Cancelled;
        }

        public void RaiseModelEditedEvent(T model, string field) {
            ModelEdited?.Invoke(this, new ModelEditedEventArgs<T>() {
                Model = model,
                Field = field
            });
        }

        public void RaiseModelDeletedEvent(T model) {
            ModelDeleted?.Invoke(this, new ModelDeletedEventArgs<T>() {
                Model = model
            });
        }

        public static DataServiceGridPagination<T> Create(bool usePartialLoading, int pageSize = 100) {
            return (usePartialLoading ?
                (DataServiceGridPagination<T>)new PartialLoadingGridDataServiceProvider<T>() { PageSize = pageSize } :
                (DataServiceGridPagination<T>)new PreLoadingGridDataServiceProvider<T>() { PageSize = pageSize }
            );
        }
    }


    public class PreLoadingGridDataServiceProvider<T> : DataServiceGridPagination<T> where T : DataModel, new() {

        public override event EventHandler RecordSetLoaded;

        public IEnumerable<T> MasterRecordSet { get; protected set; }
        public override int TotalRecordCount { get => MasterRecordSet.Count(); }


        private TypeAccessor _typeAccessor;
        protected TypeAccessor TypeAccessor {
            get => _typeAccessor ?? (_typeAccessor = TypeAccessor.Create(typeof(T)));
        }


        public override void SetPageSize(int? value) {
            this.PageSize = value;
            ApplyFilters();
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
            if (workingSet == null) {
                return;
            }

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
            RecordSetLoaded?.Invoke(this, null);
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

        public override event EventHandler RecordSetLoaded;

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
        public override void SetPageSize(int? value) {
            this.PageSize = value;
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
            RecordSetLoaded?.Invoke(this, null);
        }



    }
}
