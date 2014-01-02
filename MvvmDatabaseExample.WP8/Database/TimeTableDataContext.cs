using System;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using MvvmDatabaseExample.Annotations;

namespace MvvmDatabaseExample.Database
{
    public class TimeTableDataContext : DataContext
    {
        [UsedImplicitly] public Table<TimeTableItem> TimeTable;

        public TimeTableDataContext(string fileOrConnection) : base(fileOrConnection)
        {
            if (Debugger.IsAttached)
            {
                Log = new DebugStreamWriter();                
            }

            if (DatabaseExists()) return;
            CreateDatabase();
            BatchInsertItems(12, "Kecskemét", "Budapest", 8, 80);
            BatchInsertItems(6, "Debrecen", "Miskolc", 14, 60);
            BatchInsertItems(3, "Abony", "Szolnok", 16, 15);
            BatchInsertItems(4, "Pécs", "Sopron", 12, 180);
            BatchInsertItems(2, "Baja", "Mohács", 7, 30);
            SubmitChanges();
        }

        private void BatchInsertItems(int count, string from, string to, int startHours, int durationMinutes)
        {
            TimeTable.InsertAllOnSubmit(Enumerable.Range(startHours, count)
                .Select(
                    i =>
                        new TimeTableItem
                        {
                            FromStop = from,
                            ToStop = to,
                            StartTime = DateTime.Today.AddHours(i),
                            EndTime = DateTime.Today.AddHours(i).AddMinutes(durationMinutes)
                        }));
        }
    }
}