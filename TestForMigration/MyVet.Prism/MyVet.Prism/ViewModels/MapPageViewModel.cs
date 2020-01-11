using Prism.Navigation;

namespace MyVet.Prism.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        public MapPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Map";
        }
    }
}
