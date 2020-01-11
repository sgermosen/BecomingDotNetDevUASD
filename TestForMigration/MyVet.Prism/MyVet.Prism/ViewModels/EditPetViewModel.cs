using MyVet.Common.Helpers;
using MyVet.Common.Models;
using MyVet.Common.Services;
using MyVet.Prism.Helpers;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyVet.Prism.ViewModels
{
    public class EditPetViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private PetResponse _pet;
        private ImageSource _imageSource;
        private bool _isRunning;
        private bool _isEnabled;
        private bool _isEdit;
        private ObservableCollection<PetTypeResponse> _petTypes;
        private PetTypeResponse _petType;
        private MediaFile _file;
        private DelegateCommand _changeImageCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _deleteCommand;

        public EditPetViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            IsEnabled = true;
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        public DelegateCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(DeleteAsync));

        public ObservableCollection<PetTypeResponse> PetTypes
        {
            get => _petTypes;
            set => SetProperty(ref _petTypes, value);
        }

        public PetTypeResponse PetType
        {
            get => _petType;
            set => SetProperty(ref _petType, value);
        }


        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public PetResponse Pet
        {
            get => _pet;
            set => SetProperty(ref _pet, value);
        }

        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("pet"))
            {
                Pet = parameters.GetValue<PetResponse>("pet");
                ImageSource = Pet.ImageUrl;
                IsEdit = true;
                Title = "Edit Pet";
            }
            else
            {
                Pet = new PetResponse { Born = DateTime.Today };
                ImageSource = "noimage";
                IsEdit = false;
                Title = "New Pet";
            }

            LoadPetTypesAsync();
        }

        private async void LoadPetTypesAsync()
        {
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();

            var connection = await _apiService.CheckConnection(url);
            if (!connection)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(
                    "Error", 
                    "Check the internet connection.", 
                    "Accept");
                await _navigationService.GoBackAsync();
                return;
            }

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await _apiService.GetListAsync<PetTypeResponse>(
                url, 
                "/api", 
                "/PetTypes", 
                "bearer", 
                token.Token);

            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error", 
                    "Getting the pet types list, please try later,", 
                    "Accept");
                await _navigationService.GoBackAsync();
                return;
            }

            var petTypes = (List<PetTypeResponse>)response.Result;
            PetTypes = new ObservableCollection<PetTypeResponse>(petTypes);

            if (!string.IsNullOrEmpty(Pet.PetType))
            {
                PetType = PetTypes.FirstOrDefault(pt => pt.Name == Pet.PetType);
            }
        }

        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                "Where do you want to get the picture from?",
                "Cancel",
                null,
                "From Gallery",
                "From Camera");

            if (source == "Cancel")
            {
                _file = null;
                return;
            }

            if (source == "From Camera")
            {
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = _file.GetStream();
                    return stream;
                });
            }
        }

        private async void SaveAsync()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var owner = JsonConvert.DeserializeObject<OwnerResponse>(Settings.Owner);

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = FilesHelper.ReadFully(_file.GetStream());
            }

            var petRequest = new PetRequest
            {
                Born = Pet.Born,
                Id = Pet.Id,
                ImageArray = imageArray,
                Name = Pet.Name,
                OwnerId = owner.Id,
                PetTypeId = PetType.Id,
                Race = Pet.Race,
                Remarks = Pet.Remarks
            };

            Response<object> response;
            if (IsEdit)
            {
                response = await _apiService.PutAsync(
                    url, "/api", "/Pets", petRequest.Id, petRequest, "bearer", token.Token);
            }
            else
            {
                response = await _apiService.PostAsync(
                    url, "/api", "/Pets", petRequest, "bearer", token.Token);
            }

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(
                Languages.Accept,
                string.Format(Languages.CreateEditPetConfirm, IsEdit ? Languages.Edited : Languages.Created),
                Languages.Accept);

            await PetsPageViewModel.GetInstance().UpdateOwnerAsync();

            await _navigationService.GoBackToRootAsync();
        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(Pet.Name))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Pet.Race))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.RaceError, Languages.Accept);
                return false;
            }

            if (PetType == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PetTypeError, Languages.Accept);
                return false;
            }

            return true;
        }

        private async void DeleteAsync()
        {
            var answer = await App.Current.MainPage.DisplayAlert(
                Languages.Confirm,
                Languages.QuestionToDeletePet,
                Languages.Yes,
                Languages.No);

            if (!answer)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await _apiService.DeleteAsync(
                url, "/api", "/Pets", Pet.Id, "bearer", token.Token);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await PetsPageViewModel.GetInstance().UpdateOwnerAsync();

            IsRunning = false;
            IsEnabled = true;
            await _navigationService.GoBackToRootAsync();
        }
    }
}
