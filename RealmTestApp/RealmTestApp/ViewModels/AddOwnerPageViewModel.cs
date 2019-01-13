using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using RealmTestApp.Events;
using RealmTestApp.Models.Database;
using RealmTestApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealmTestApp.ViewModels
{
	public class AddOwnerPageViewModel : ViewModelBase
	{
        private readonly IEventAggregator _eventAggregator;

        private OwnerDTO _newOwner;

        public OwnerDTO NewOwner {
            get { return _newOwner; }
            set { SetProperty( ref _newOwner, value ); }
        }

        public DelegateCommand AddOwnerCommand => new DelegateCommand( async () => {
            if (!string.IsNullOrEmpty( NewOwner.FirstName ) && !string.IsNullOrEmpty( NewOwner.LastName )) {

                _eventAggregator.GetEvent<OwnerAddedEvent>().Publish( NewOwner );
                NewOwner = new OwnerDTO();

                await NavigationService.GoBackAsync();
            }
        } );

        public AddOwnerPageViewModel( INavigationService navigationService,
            IEventAggregator eventAggregator ) : base( navigationService ) {
            Title = "Add Owner";

            _eventAggregator = eventAggregator;

            NewOwner = new OwnerDTO();
        }
    }
}
