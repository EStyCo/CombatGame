﻿using AutoMapper;
using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.Utilities;
using Client.MVVM.View;
using Client.MVVM.View.Town;
using Client.Services;
using Client.Services.IServices;
using Newtonsoft.Json;
using PropertyChanged;
using System.Windows.Input;

namespace Client.MVVM.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        private readonly IAuthService authService;
        private readonly IMapper mapper;
        private readonly UserStore userStore;
        private readonly Router router;
        private readonly VitalityService vitalityService;
        public LoginRequestDTO UserLogin { get; set; }
        public bool isLoading { get; set; } = false;
        public bool canWriting { get; set; } = true;
        public string Response { get; set; } = string.Empty;
        public ICommand LoginCommand { get; set; }
        public ICommand GoToRegisterCommand { get; set; }

        public MainViewModel(IAuthService _authService, IMapper _mapper, RegistrationPage _registrationPage, UserStore _userStore, Router _router, VitalityService _vitalityService)
        {
            authService = _authService;
            mapper = _mapper;
            router = _router;
            userStore = _userStore;
            vitalityService = _vitalityService;
            UserLogin = new();

            LoginCommand = new Command(async () => await Login());
            GoToRegisterCommand = new Command(async () => await PushRegisterPage());
        }

        private async Task Login()
        {
            isLoading = true;
            canWriting = false;

            var response = await authService.LoginAsync<APIResponse>(UserLogin);
            //await Task.Delay(1000);

            if (response != null && response.IsSuccess)
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));
                userStore.Character = mapper.Map<Character>(loginResponse.Character);

                await router.GoToCurrentArea();

                await vitalityService.ConnectionToHub();
            }
            else
            {
                Response = response.ErrorMessages.FirstOrDefault().ToString();
            }

            isLoading = false;
            canWriting = true;
        }

        private async Task PushRegisterPage()
        {
            await Shell.Current.GoToAsync(nameof(RegistrationPage));
        }
    }
}
