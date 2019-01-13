using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using RealmTestApp.Contracts;
using RealmTestApp.Events;
using RealmTestApp.Models.Database;
using RealmTestApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RealmTestApp.ViewModels
{
	public class AddCarPageViewModel : ViewModelBase
	{
        private readonly IEventAggregator _eventAggregator;
        private readonly ICarDataService _carDataService;

        private CarDTO _newCar;
        private OwnerDTO _currentOwner;

        private string _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public CarDTO NewCar {
            get { return _newCar; }
            set { SetProperty( ref _newCar, value ); }
        }

        public OwnerDTO CurrentOwner {
            get { return _currentOwner; }
            set { SetProperty( ref _currentOwner, value ); }
        }

        public DelegateCommand AddCarCommand => new DelegateCommand( async () => {
            if (!string.IsNullOrEmpty( NewCar.Make ) && !string.IsNullOrEmpty( NewCar.Model )) {

                var r = new Random();
                NewCar.LicensePlate = $"{r.Next( 10 )}{r.Next( 10 )}{_letters[r.Next( 26 )]}{_letters[r.Next( 26 )]}{_letters[r.Next( 26 )]}{r.Next( 10 )}{r.Next( 10 )}{r.Next( 10 )}";
                Debug.WriteLine( $"AddCarCommand triggered" );
                AddCar( NewCar );
                _eventAggregator.GetEvent<CarAddedEvent>().Publish( NewCar );

                NewCar = new CarDTO();

                var navParams = new NavigationParameters();
                navParams.Add( "isReturnedTo", true );

                await NavigationService.GoBackAsync( navParams );
            }
        } );

        public AddCarPageViewModel( INavigationService navigationService,
            ICarDataService carDataService,
            IEventAggregator eventAggregator )
            : base( navigationService ) {
            Title = "Add Car";

            _carDataService = carDataService;
            _eventAggregator = eventAggregator;

            NewCar = new CarDTO();
        }

        private void AddCar( CarDTO c ) {
            c.Owner = CurrentOwner;
            Debug.WriteLine( $"CarAddedEvent triggered" );

            _carDataService.AddCar( c );

            CurrentOwner.Cars.Add( c );
            //_ownerDataService.AddOwnerCar( CurrentOwner, c );

        }

        public override void OnNavigatedTo( INavigationParameters parameters ) {
            base.OnNavigatedTo( parameters );

            CurrentOwner = (OwnerDTO)parameters["owner"];
        }
    }
}
