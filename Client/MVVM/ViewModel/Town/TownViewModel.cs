﻿using Client.MVVM.Model;
using Client.MVVM.Model.Utilities;
using Client.MVVM.View;
using Client.Services;
using Microsoft.Maui.Controls;
using PropertyChanged;
using System.Windows.Input;

namespace Client.MVVM.ViewModel.Town
{
    [AddINotifyPropertyChangedInterface]
    public class TownViewModel
    {
        private readonly Router router;
        private readonly TravelService travelService;
        private readonly VitalityService vitalityService;
        public UserStore UserStore {  get; set; }
        public HP HP { get; set; }

        public ICommand BreakCharacterCommand { get; set; }

        public ICommand GoToGladeCommand { get; set; }
        public ICommand ShowCharacterCommand { get; set; }
        public ICommand ShowSpellBookCommand { get; set; }
        public ICommand ShowInventoryCommand { get; set; }

        public TownViewModel(UserStore _userStore, 
                             VitalityService _vitalityService, 
                             TravelService _travelService,
                             Router _router, 
                             HP _HP) 
        {
            UserStore = _userStore;
            vitalityService = _vitalityService;
            travelService = _travelService;
            router = _router;
            HP = _HP;

            BreakCharacterCommand = new Command(async () => await BreakCharacter());

            GoToGladeCommand = new Command(async () => await GoToGlade());
            ShowCharacterCommand = new Command(async () => await ShowCharacter());
            ShowSpellBookCommand = new Command(async () => await ShowSpellBook());
            ShowInventoryCommand = new Command(async () => await ShowInventory());


            Shell.Current.SetValue(AppShell.ShowTabsProperty, true);
        }

        private async Task BreakCharacter()
        {
            await travelService.BreakChar<APIResponse>(UserStore.Character.CharacterName);
        }

        private async Task GoToGlade()
        {
            await router.GoToNewArea(Area.Glade);
        }

        private async Task ShowCharacter()
        {
            await router.GoToModalArea(ModalArea.Character);
        }

        private async Task ShowSpellBook()
        {
            await router.GoToModalArea(ModalArea.SpellBook);
        }
        private async Task ShowInventory()
        {
            await router.GoToModalArea(ModalArea.Inventory);
        }
    }
}
