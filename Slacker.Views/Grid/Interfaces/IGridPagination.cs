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

        event EventHandler OnRecordSetLoaded;


        void OrderBy(string field, bool desc);
        void SetPage(int pageNo);
        void SetPageSize(int? value);

        void Load();


        IEnumerable<object> RecordSet { get; }

    }
    
    public interface IGridPagination<T> : IGridPagination {
        new IEnumerable<T> RecordSet { get; }
    }
}
