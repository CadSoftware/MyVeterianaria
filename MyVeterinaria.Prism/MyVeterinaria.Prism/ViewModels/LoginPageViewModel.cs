using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using MyVeterinaria.Prism.Models;
using MyVeterinaria.Prism.Service;
using Prism.Navigation;

namespace MyVeterinaria.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;

        public LoginPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Login";
            IsEnabled = true;

            Email = "mariana777@hotmail.com";
            Password = "123456";
        }

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an Email", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an Password", "Aceptar");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            var url = App.Current.Resources["UrlApi"].ToString();
            var responseToken = await _apiService.GetTokenAsync(url, "Account", "/CreateToken", request);
            
            if (!responseToken.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await App.Current.MainPage.DisplayAlert("Error", "Email or Password Incorrect.", "Aceptar");
                Password = string.Empty;
                return;
            }

            var token = (TokenResponse)responseToken.Result;

            var responseOwner = await _apiService.GetOwnerByEmailAsync(url, "api", "/GetOwnerByEmail", "bearer", token.Token, Email);

            if (!responseOwner.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await App.Current.MainPage.DisplayAlert("Error", "This User have a big Problems, call support", "Aceptar");
                Password = string.Empty;
                return;
            }

            var owner = (OwnerResponse) responseOwner.Result;

            var parameters = new NavigationParameters {{"owner", owner}};

            IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync("PetsPage", parameters);
            Password = string.Empty;
        }
    }
}
