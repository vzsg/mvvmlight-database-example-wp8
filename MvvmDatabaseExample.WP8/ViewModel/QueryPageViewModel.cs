using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using MvvmDatabaseExample.Annotations;
using MvvmDatabaseExample.Database;
using MvvmDatabaseExample.Resources;

namespace MvvmDatabaseExample.ViewModel
{
    [UsedImplicitly]
    public class QueryPageViewModel : ViewModelBase
    {
        private readonly IDataSource _dataSource;

        public QueryPageViewModel(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<string> Stations
        {
            get
            {
                return Enumerable.Repeat(AppResources.AllStations, 1).Concat(
                    _dataSource.Items
                    .Select(ti => ti.FromStop)
                    .Union(_dataSource.Items.Select(ti => ti.ToStop))
                    .OrderBy(s => s));
            }
        }

        private int _selectedStationIndex;

        public int SelectedStationIndex
        {
            get
            {
                return _selectedStationIndex;
            }

            set
            {
                Set(() => SelectedStationIndex, ref _selectedStationIndex, value);
                RaisePropertyChanged(() => Items);
            }
        }

        public IEnumerable<TimeTableItem> Items
        {
            get
            {
                IEnumerable<TimeTableItem> source;

                if (SelectedStationIndex == 0)
                {
                    source = _dataSource.Items;
                }
                else
                {
                    var station = Stations.ToList()[SelectedStationIndex];
                    source = _dataSource.Items.Where(ti => ti.FromStop == station || ti.ToStop == station);                    
                }


                return source.OrderBy(ti => ti.FromStop).ThenBy(ti => ti.ToStop);
            }
        } 
    }
}