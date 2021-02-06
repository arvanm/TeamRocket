﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;
using Game.GameRules;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterUpdatePage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<CharacterModel> ViewModel;

        // Empty Constructor for Tests
        public CharacterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public CharacterUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Character Update";

            LoadLevelPickerValues();

            UpdatePageBindingContext();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the Level
            var data = this.ViewModel.Data;

            // Clear the Binding and reset it
            BindingContext = null;
            this.ViewModel.Data = data;
            this.ViewModel.Title = data.Name;

            BindingContext = this.ViewModel;

            // This resets the Picker to the Character's level
            LevelPicker.SelectedIndex = ViewModel.Data.Level - 1;

            // Sets the Job Picker to the Character's Type
            CharacterJobPicker.SelectedItem = ViewModel.Data.Job.ToString();

            return true;
        }

        /// <summary>
        /// Change the attribute values after changing level in the Level Picker
        /// </summary>
        public void UpdateAttributeValues()
        {
            // Update the attributes
            MaxHealthValue.Text = ViewModel.Data.MaxHealth.ToString();
            CurrentHealthValue.Text = ViewModel.Data.CurrentHealth.ToString();
            AttackValue.Text = ViewModel.Data.Attack.ToString();
            DefenseValue.Text = ViewModel.Data.Defense.ToString();
            SpeedValue.Text = ViewModel.Data.Speed.ToString();
            TotalAttackValue.Text = ViewModel.Data.GetAttackTotal.ToString();
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

        /// <summary>
        /// Check whether the input max health is in the correct range.
        /// If the input max health is smaller or equal than 0 or larger than level times 10,
        /// display an alert, and return false.
        /// Otherwise return true
        /// </summary>
        /// <returns>Whether the input name is empty or null</returns>
        private async Task<bool> CheckCharacterMaxHealth()
        {
            if (ViewModel.Data.MaxHealth <= 0 || ViewModel.Data.MaxHealth > ViewModel.Data.Level * 10)
            {
                await DisplayAlert("Alert", "Character max health must between 1 and Level x 10!", "OK");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the input current health is in the correct range.
        /// If the input current health is smaller than 0 or larger than the max health,
        /// display an alert, and return false.
        /// Otherwise return true
        /// </summary>
        /// <returns>Whether the input name is empty or null</returns>
        private async Task<bool> CheckCharacterCurHealth()
        {
            if (ViewModel.Data.CurrentHealth < 0 || ViewModel.Data.CurrentHealth > ViewModel.Data.MaxHealth)
            {
                await DisplayAlert("Alert", "Character current health must between 0 and Max Health!", "OK");
                return false;
            }

            return true;
        }
        #endregion InputValueCheck

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // Only save when 
            // the character name is not empty,
            // the max health is not within [1, level * 10] range,
            // otherwise display an alert.
            if (await CheckCharacterName() && await CheckCharacterMaxHealth() && await CheckCharacterCurHealth())
            {
                // If the image in the data box is empty, use the default one..
                if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
                {
                    ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
                }

                MessagingCenter.Send(this, "Update", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        #region LevelPicker
        /// <summary>
        /// Load the values for the Level Picker
        /// </summary>
        /// <returns></returns>
        public bool LoadLevelPickerValues()
        {
            // Load the values for the Level into the Picker
            for (var i = 1; i <= LevelTableHelper.MaxLevel; i++)
            {
                LevelPicker.Items.Add(i.ToString());
            }

            LevelPicker.SelectedIndex = -1;

            return true;
        }

        /// <summary>
        /// The Level selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void LevelPicker_Changed(object sender, EventArgs args)
        {
            // If the Picker is not set, then set it
            if (LevelPicker.SelectedIndex == -1)
            {
                LevelPicker.SelectedIndex = ViewModel.Data.Level - 1;
                return;
            }

            var result = LevelPicker.SelectedIndex + 1;

            // When level changed, roll again for max health, and set attributes to follow the Level Table
            if (result != ViewModel.Data.Level)
            {
                // Change the Level
                ViewModel.Data.Level = result;

                // Roll for new max health
                ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

                // Set attack, defense, speed to follow the Level Table
                ViewModel.Data.Attack = LevelTableHelper.LevelDetailsList[result].Attack;
                ViewModel.Data.Defense = LevelTableHelper.LevelDetailsList[result].Defense;
                ViewModel.Data.Speed = LevelTableHelper.LevelDetailsList[result].Speed;

                // Update attribute values in the table
                UpdateAttributeValues();
            }
        }
        #endregion LevelPicker

    }
}