using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastMember;

namespace Slacker.Views.Controls.WinForms {
    public partial class ServiceGrid<T> : UserControl where T : DataModel, new() {

        /// <summary>
        /// The active DataService for ServiceGrid
        /// </summary>
        public DataService<T> DataService { get; set; }

        private TypeAccessor _typeAccessor;
        protected TypeAccessor TypeAccessor {
            get => _typeAccessor ?? (_typeAccessor = TypeAccessor.Create(typeof(T))
        }

        public IReadOnlyList<T> _completeRecordSet;
        /// <summary>
        /// Stores the complete record set, will be null if partial loading is used
        /// </summary>
        public IReadOnlyList<T> CompleteRecordSet {
            get => _completeRecordSet;
            protected set => _completeRecordSet = value;
        }

        private IReadOnlyList<T> _activeRecordSet;
        /// <summary>
        /// Active record set for current page
        /// </summary>
        public IReadOnlyList<T> ActiveRecordSet {
            get => _activeRecordSet;
            protected set => _activeRecordSet = value;
        }

        /// <summary>
        /// Set to false if this ServiceGrid should preload all results under the query
        /// instead of applying offsets and loading the next set on page navigation
        /// </summary>
        public bool PartialLoading { get; set; } = true;

        /// <summary>
        /// The query condition for query
        /// </summary>
        public string QueryCondition { get; set; }
        
        /// <summary>
        /// The query condition parameter object
        /// </summary>
        public object QueryConditionParams { get; set; }

        /// <summary>
        /// The page size limit for results. -1 for no page size.
        /// </summary>
        public int PageSize { get; set; } = 100;

        /// <summary>
        /// The current page number
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// The current number of results since last load
        /// </summary>
        public int TotalRecordCount { get; set; }

        /// <summary>
        /// The number of pages with results
        /// </summary>
        public int PageCount {
            get => TotalRecordCount / (PageSize < 1 ? 1 : PageSize);
        }

        protected BindingSource GridBinding { get; set; } = new BindingSource();

        public ServiceGrid() {
            InitializeComponent();
            RecordSetLoaded += (s,e) => Render();
            Render();
        }

        /// <summary>
        /// Renders the current active record set to Grid
        /// </summary>
        public void Render() {
            GridBinding.Clear();
            ActiveRecordSet?.ToList().ForEach(r => GridBinding.Add(r));
            dataGrid.DataSource = GridBinding;
        }


        private void DataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {

        }
        

        public string OrderBy(string field, bool desc = true) {
            if (!PartialLoading) {
                var workingSet = desc ?
                    this.CompleteRecordSet.OrderByDescending(t => TypeAccessor[t, field]) :
                    this.CompleteRecordSet.OrderBy(t => TypeAccessor[t, field]);

                this.ActiveRecordSet = (
                    PageSize > 1 ?
                        workingSet.Skip((CurrentPage - 1) * PageSize).Take(PageSize) :
                        workingSet
                ).ToList().AsReadOnly();
            } else {
                LoadRecordSet()
            }
        }

        public virtual async Task LoadRecordSetAsync(string orderBy = null) {
            await Task.Run(new Action(() => { LoadRecordSet(); }));
        }

        public virtual void LoadRecordSet() {
            if (DataService == null) {
                throw new Exception("Could not load recordset. DataService cannot be null.");
            }

            var queryProps = new QueryProps {
                WhereSql = QueryCondition,
                WhereParams = QueryConditionParams
            };

            if (!PartialLoading) {
                // Do a complete load
                this.CompleteRecordSet = DataService.Select(queryProps).ToList().AsReadOnly();
                this.TotalRecordCount = CompleteRecordSet.Count;

                if (PageSize < 1) {
                    this.ActiveRecordSet = CompleteRecordSet;
                }
                else {
                    this.ActiveRecordSet = CompleteRecordSet
                        .Skip((CurrentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList().AsReadOnly();
                }

                RecordSetLoaded?.Invoke(this, null);
                return;
            }
            
            // Do a partal load
            long batchId = DateTime.Now.Ticks;
            DataService.StartBatch(batchId);

            // Get Total Record Count from DB
            this.TotalRecordCount = DataService.Count(queryProps, batchId);

            // Apply partial query
            queryProps.Limit = PageSize;
            queryProps.Offset = (CurrentPage - 1) * PageSize; 
            
            // Get active Record Set
            this.ActiveRecordSet = DataService.Select(queryProps).ToList().AsReadOnly();

            DataService.EndBatch(batchId);
            RecordSetLoaded?.Invoke(this, null);
        }

        protected event EventHandler RecordSetLoaded;
        public void RaiseOnRecordSetLoaded() {
            RecordSetLoaded?.Invoke(this, null);
        }
    }
}
