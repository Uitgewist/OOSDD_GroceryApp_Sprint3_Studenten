using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.App.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        private readonly GlobalViewModel _global;

        [ObservableProperty]
        private string email = "user3@mail.com";

        [ObservableProperty]
        private string password = "Useruser4!";

        [ObservableProperty]
        private string name = "Bob Bladerdeeg";

        [ObservableProperty]
        private string errorMessage = "";
        public RegisterViewModel(IAuthService authService, GlobalViewModel global)
        {
            _authService = authService;
            _global = global;
        }
        [RelayCommand]
        private void Register()
        {
            ErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(Email))
                ErrorMessage += "Email moet een waarde hebben. ";
            if (string.IsNullOrWhiteSpace(Name))
                ErrorMessage += "Naam moet een waarde hebben. ";
            if (string.IsNullOrWhiteSpace(Password))
                ErrorMessage += "Wachtwoord moet een waarde hebben. ";
            if (ErrorMessage != "")
                return;
            
                Client client = _authService.Register(Email, Password, Name);
                _global.Client = client;
                Application.Current.MainPage = new AppShell();
            }
        }
    }
