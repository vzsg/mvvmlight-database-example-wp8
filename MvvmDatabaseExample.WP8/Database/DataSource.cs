using System;
using System.Linq;
using MvvmDatabaseExample.Annotations;

namespace MvvmDatabaseExample.Database
{
    public interface IDataSource
    {
        IQueryable<TimeTableItem> Items { get; }
    }

    [UsedImplicitly]
    public class DesignDataSource : IDataSource
    {
        public IQueryable<TimeTableItem> Items
        {
            get
            {
                return Enumerable.Range(14, 5)
                        .Select(
                            i =>
                                new TimeTableItem
                                {
                                    FromStop = "Design From",
                                    ToStop = "Design To",
                                    StartTime = DateTime.Today.AddHours(i),
                                    EndTime = DateTime.Today.AddHours(i + 1).AddMinutes(30)
                                }).AsQueryable();
            }           
        }
    }

    [UsedImplicitly]
    public class DbDataSource : IDataSource
    {
        private readonly TimeTableDataContext _context;
        public DbDataSource(TimeTableDataContext context)
        {
            _context = context;
        }

        public IQueryable<TimeTableItem> Items
        {
            get
            {
                return _context.TimeTable;
            }
        }
    }
}
