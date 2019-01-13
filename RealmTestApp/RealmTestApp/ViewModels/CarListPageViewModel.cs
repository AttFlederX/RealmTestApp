using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using RealmTestApp.Contracts;
using RealmTestApp.Events;
using RealmTestApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace RealmTestApp.ViewModels
{
	public class CarListPageViewModel : ViewModelBase
	{
        private readonly IEventAggregator _eventAggregator;
        private readonly ICarDataService _carDataService;
        private readonly IOwnerDataService _ownerDataService;

        private ObservableCollection<CarDTO> _carList;
        private OwnerDTO _currentOwner;

        private bool _isNavigatedToAddCar;

        public ObservableCollection<CarDTO> CarList {
            get { return _carList; }
            set { SetProperty( ref _carList, value ); }
        }

        public OwnerDTO CurrentOwner {
            get { return _currentOwner; }
            set { SetProperty( ref _currentOwner, value ); }
        }

        public DelegateCommand AddCarCommand => new DelegateCommand( async () => {
            _isNavigatedToAddCar = true;
            var navParams = new NavigationParameters();
            //navParams.Add( "carList", SelectedOwner.Cars );
            navParams.Add( "owner", CurrentOwner );

            await NavigationService.NavigateAsync( "AddCarPage", navParams );
        } );

        public CarListPageViewModel( INavigationService navigationService,
            IEventAggregator eventAggregator,
            ICarDataService carDataService,
            IOwnerDataService ownerDataService ) : base( navigationService ) {

            Title = "Main Page";

            _eventAggregator = eventAggregator;
            _carDataService = carDataService;
            _ownerDataService = ownerDataService;

            _isNavigatedToAddCar = false;

            _eventAggregator.GetEvent<CarAddedEvent>().Subscribe( c => CarList = new ObservableCollection<CarDTO>( CurrentOwner.Cars ) );
        }

        
        public override void OnNavigatingTo( INavigationParameters parameters ) {
            base.OnNavigatingTo( parameters );

            if (parameters.Keys.Contains( "isReturnedTo" ) && (bool)parameters["isReturnedTo"]) {
                return;
            }
            //CarList = new ObservableCollection<CarDTO>( (IEnumerable<CarDTO>)parameters["carList"] );
            CurrentOwner = (OwnerDTO)parameters["owner"];
            CarList = new ObservableCollection<CarDTO>(CurrentOwner.Cars);
        }

        public override void OnNavigatedFrom( INavigationParameters parameters ) {
            base.OnNavigatedFrom( parameters );

            if (!_isNavigatedToAddCar) {
                //_eventAggregator.GetEvent<CarAddedEvent>().Unsubscribe( AddCar );
            }
        }
    }
}
