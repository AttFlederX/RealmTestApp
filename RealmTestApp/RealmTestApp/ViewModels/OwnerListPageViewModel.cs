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
using System.Linq;

namespace RealmTestApp.ViewModels
{
	public class OwnerListPageViewModel : ViewModelBase
	{
        private readonly IEventAggregator _eventAggregator;
        private readonly IOwnerDataService _ownerDataService;

        private ObservableCollection<OwnerDTO> _ownerList;
        private OwnerDTO _selectedOwner;

        public ObservableCollection<OwnerDTO> OwnerList {
            get { return _ownerList; }
            set { SetProperty( ref _ownerList, value ); }
        }

        public OwnerDTO SelectedOwner {
            get { return _selectedOwner; }
            set { SetProperty( ref _selectedOwner, value ); }
        }

        public DelegateCommand AddOwnerCommand => new DelegateCommand( async () => {
            await NavigationService.NavigateAsync( "AddOwnerPage" );
        } );

        public DelegateCommand OwnerSelected => new DelegateCommand( async () => {
            if (SelectedOwner != null) {
                var navParams = new NavigationParameters();
                //navParams.Add( "carList", SelectedOwner.Cars );
                navParams.Add( "owner", SelectedOwner );

                await NavigationService.NavigateAsync( "CarListPage", navParams );
            }
            SelectedOwner = null;
        } );

        public OwnerListPageViewModel( INavigationService navigationService,
            IEventAggregator eventAggregator,
            IOwnerDataService ownerDataService ) : base( navigationService ) {

            Title = "Owner list";

            _eventAggregator = eventAggregator;
            _ownerDataService = ownerDataService;

            OwnerList = new ObservableCollection<OwnerDTO>( _ownerDataService.GetOwners() );

            _eventAggregator.GetEvent<OwnerAddedEvent>().Subscribe( c => {
                _ownerDataService.AddOwner( c );

                OwnerList = new ObservableCollection<OwnerDTO>( _ownerDataService.GetOwners() );
            } );
        }
    }
}
