using Prism.Navigation;

namespace MyVet.Prism.ViewModels
{
    public class AgendaPageViewModel : ViewModelBase
    {
        public AgendaPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Agenda";
        }
    }
}
