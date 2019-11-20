using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyVeterinaria.Prism.Models;
using Prism.Navigation;

namespace MyVeterinaria.Prism.ViewModels
{
    public class PetsPageViewModel : ViewModelBase
    {
        private OwnerResponse _owner;
        private ObservableCollection<PetResponse> _pets;

        public PetsPageViewModel(INavigationService navigationService)
        : base(navigationService)
        {
            Title = "Pets";
        }

        public ObservableCollection<PetResponse> Pets
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

                Pets = new ObservableCollection<PetResponse>(_owner.Pets);
            }
        }
    }
}
