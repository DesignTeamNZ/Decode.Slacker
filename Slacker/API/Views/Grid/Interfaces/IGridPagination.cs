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
        int? PageSize { get; }

        string OrderByField { get; }
        bool OrderByDesc { get; }

        void OrderBy(string field, bool desc);
        void SetPage(int pageNo);
        void SetPageSize(int? value);
        void Load();

        event EventHandler RecordSetLoaded;
        IList<dynamic> RecordSet { get; }

        bool RaisePreModelEditedEvent(object model, string field);
        void RaiseModelEditedEvent(object model, string field);
        void RaiseModelDeletedEvent(object model);

    }
    
    public interface IGridPagination<T> : IGridPagination {
        event EventHandler<PreModelEditedEventArgs<T>> PreModelEdited;
        event EventHandler<ModelEditedEventArgs<T>> ModelEdited;
        event EventHandler<ModelDeletedEventArgs<T>> ModelDeleted;
        new IList<T> RecordSet { get; }

        bool RaisePreModelEditedEvent(T model, string field);
        void RaiseModelEditedEvent(T model, string field);
        void RaiseModelDeletedEvent(T model);
    }

    public class PreModelEditedEventArgs<T> : EventArgs {
        public bool Cancelled;
        public T Model;
        public string Field;
    }

    public class ModelEditedEventArgs<T> : EventArgs {
        public T Model;
        public string Field;
    }

    public class ModelDeletedEventArgs<T> : EventArgs {
        public T Model;
    }


}
