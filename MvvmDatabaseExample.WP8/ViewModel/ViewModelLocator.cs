using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MvvmDatabaseExample.Database;

namespace MvvmDatabaseExample.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataSource, DesignDataSource>();
            }
            else
            {
                SimpleIoc.Default.Register(() => new TimeTableDataContext("isostore:/example.sdf"));
                SimpleIoc.Default.Register<IDataSource, DbDataSource>();
            }

            SimpleIoc.Default.Register<QueryPageViewModel>();
        }

        public QueryPageViewModel QueryPage
        {
            get { return ServiceLocator.Current.GetInstance<QueryPageViewModel>(); }
        }

        public static void Cleanup()
        {
            if (ViewModelBase.IsInDesignModeStatic) return;
            ServiceLocator.Current.GetInstance<TimeTableDataContext>().Dispose();
        }
    }
}