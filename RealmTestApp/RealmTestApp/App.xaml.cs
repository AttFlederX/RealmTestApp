using Prism;
using Prism.Ioc;
using RealmTestApp.Contracts;
using RealmTestApp.Services;
using RealmTestApp.ViewModels;
using RealmTestApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation( XamlCompilationOptions.Compile )]
namespace RealmTestApp {
    public partial class App {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this( null ) { }

        public App( IPlatformInitializer initializer ) : base( initializer ) { }

        protected override async void OnInitialized() {
            InitializeComponent();

            await NavigationService.NavigateAsync( "NavigationPage/OwnerListPage" );
        }

        protected override void RegisterTypes( IContainerRegistry containerRegistry ) {
            containerRegistry.RegisterSingleton<ICarDataService, CarDataService>();
            containerRegistry.RegisterSingleton<IOwnerDataService, OwnerDataService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<OwnerListPage, OwnerListPageViewModel>();
            containerRegistry.RegisterForNavigation<AddOwnerPage, AddOwnerPageViewModel>();
            containerRegistry.RegisterForNavigation<CarListPage, CarListPageViewModel>();
            containerRegistry.RegisterForNavigation<AddCarPage, AddCarPageViewModel>();
        }
    }
}
