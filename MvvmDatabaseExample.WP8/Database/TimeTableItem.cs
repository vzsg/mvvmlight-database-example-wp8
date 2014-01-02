using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using GalaSoft.MvvmLight;

namespace MvvmDatabaseExample.Database
{
    [Table]
    public class TimeTableItem : ObservableObject
    {
        private int _id;
        private string _fromStop;
        private string _toStop;
        private DateTime _start;
        private DateTime _end;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL IDENTITY", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _id; }
            set { Set(() => Id, ref _id, value); }
        }

        [Column(IsVersion = true)] private Binary _version;

        [Column]
        public string FromStop
        {
            get { return _fromStop; }
            set { Set(() => FromStop, ref _fromStop, value); }
        }

        [Column]
        public string ToStop
        {
            get { return _toStop; }
            set { Set(() => ToStop, ref _toStop, value); }
        }

        [Column]
        public DateTime StartTime
        {
            get { return _start; }
            set { Set(() => StartTime, ref _start, value); }
        }

        [Column]
        public DateTime EndTime
        {
            get { return _end; }
            set { Set(() => EndTime, ref _end, value); }
        }
    }
}