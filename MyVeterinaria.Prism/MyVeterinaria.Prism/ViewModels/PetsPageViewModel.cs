using System.Collections.ObjectModel;
using System.Linq;
using MyVeterinaria.Prism.Models;
using Prism.Navigation;

namespace MyVeterinaria.Prism.ViewModels
{
    public class PetsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private OwnerResponse _owner;
        private ObservableCollection<PetItemViewModel> _pets;

        public PetsPageViewModel(INavigationService navigationService)
        : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Pets";
        }

        public ObservableCollection<PetItemViewModel> Pets
        {
            get => _pets;
            set => SetProperty(ref _pets, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("owner"))
            {
                _owner = parameters.GetValue<OwnerResponse>("owner");
                Title = $"Pets of: {_owner.FullName}";

                Pets = new ObservableCollection<PetItemViewModel>(_owner.Pets
                    .Select(x => new PetItemViewModel(_navigationService)
                    {
                        PetType = x.PetType,
                        Histories = x.Histories,
                        Id = x.Id,
                        Remarks = x.Remarks,
                        Name = x.Name,
                        Race = x.Race,
                        Born = x.Born,
                        ImagenUrl = x.ImagenUrl
                    }));
            }
        }
    }
}
