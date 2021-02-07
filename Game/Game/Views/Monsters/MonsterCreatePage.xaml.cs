﻿using Game.Models;
using Game.ViewModels;
using Game.GameRules;

using System;
using System.ComponentModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// Create Item
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterCreatePage : ContentPage
    {
        // The item to create
        public GenericViewModel<MonsterModel> ViewModel = new GenericViewModel<MonsterModel>();

        // Empty Constructor for UTs
        public MonsterCreatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public MonsterCreatePage()
        {
            InitializeComponent();

            this.ViewModel.Data = RandomPlayerHelper.GetRandomMonster(1);

            BindingContext = this.ViewModel;

            this.ViewModel.Title = "Monster Create";

            // Load Level Values for the Level Picker
            LoadCharacterLevelPickerValues();

            // Sets the Level Picker to the Character's level
            CharacterLevelPicker.SelectedIndex = ViewModel.Data.Level - 1;

            // Sets the Job Picker to the Character's Type
            CharacterJobPicker.SelectedItem = ViewModel.Data.Job.ToMessage();
        }

        /// <summary>
        /// Cancel the character creation and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        #region InputValueCheck
        /// <summary>
        /// Check whether the input name is empty or null.
        /// If the input name is empty or null, display an alert says the name cannot be empty, and return false.
        /// Otherwise return true
        /// </summary>
        /// <returns>Whether the input name is empty or null</returns>
        private async Task<bool> CheckCharacterName()
        {
            if (string.IsNullOrEmpty(ViewModel.Data.Name))
            {
                await DisplayAlert("Alert", "Character name cannot be empty!", "OK");
                return false;
            }

            return true;
        }
        #endregion InputValueCheck

        /// <summary>
        /// Save the character and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            // Only save when the character name is not empty, otherwise display an alert
            if (await CheckCharacterName())
            {
                // If the image in the data box is empty, use the default one.
                if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
                {
                    ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
                }

                MessagingCenter.Send(this, "Create", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Randomly change the image of the character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeImage_Clicked(object sender, EventArgs e)
        {

        }

        #region LevelPicker
        /// <summary>
        /// Load the values for the Level Picker
        /// </summary>
        /// <returns></returns>
        public bool LoadCharacterLevelPickerValues()
        {
            // Load the values for the Level into the Picker
            for (var i = 1; i <= LevelTableHelper.MaxLevel; i++)
            {
                CharacterLevelPicker.Items.Add(i.ToString());
            }

            CharacterLevelPicker.SelectedIndex = -1;

            return true;
        }

        /// <summary>
        /// The Level selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void CharacterLevelPicker_Changed(object sender, EventArgs args)
        {
            // If the Picker is not set, then set it
            if (CharacterLevelPicker.SelectedIndex == -1)
            {
                CharacterLevelPicker.SelectedIndex = ViewModel.Data.Level - 1;
                return;
            }

            var result = CharacterLevelPicker.SelectedIndex + 1;

            // When level changed, roll again for max health, and set attributes to follow the Level Table
            if (result != ViewModel.Data.Level)
            {
                // Change the Level
                ViewModel.Data.Level = result;

                // Roll for new max health
                ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

                // Set current health as the new max health
                ViewModel.Data.CurrentHealth = ViewModel.Data.MaxHealth;

                // Set attack, defense, speed to follow the Level Table
                ViewModel.Data.Attack = LevelTableHelper.LevelDetailsList[result].Attack;
                ViewModel.Data.Defense = LevelTableHelper.LevelDetailsList[result].Defense;
                ViewModel.Data.Speed = LevelTableHelper.LevelDetailsList[result].Speed;
            }
        }
        #endregion LevelPicker
    }
}