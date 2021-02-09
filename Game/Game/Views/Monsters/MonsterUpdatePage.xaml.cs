using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

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
    public partial class MonsterUpdatePage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<MonsterModel> ViewModel;

        // Empty Constructor for Tests
        public MonsterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Monster Update";

            LoadMonsterLevelPickerValues();

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

            // This resets the Picker to the monster's level
            MonsterLevelPicker.SelectedIndex = ViewModel.Data.Level - 1;

            // Sets the Job Picker to the monster's Type
            MonsterTypePicker.SelectedItem = ViewModel.Data.MonsterType.ToMessage();

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
        private async Task<bool> CheckMonsterName()
        {
            if (string.IsNullOrEmpty(ViewModel.Data.Name))
            {
                await DisplayAlert("Alert", "Monster name cannot be empty!", "OK");
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
            // the Monster name is not empty,
            // the max health is not within [1, level * 10] range,
            // otherwise display an alert.
            if (await CheckMonsterName())
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
        public bool LoadMonsterLevelPickerValues()
        {
            // Load the values for the Level into the Picker
            for (var i = 1; i <= LevelTableHelper.MaxLevel; i++)
            {
                MonsterLevelPicker.Items.Add(i.ToString());
            }

            MonsterLevelPicker.SelectedIndex = -1;

            return true;
        }

        /// <summary>
        /// The Level selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void MonsterLevelPicker_Changed(object sender, EventArgs args)
        {
            // If the Picker is not set, then set it
            if (MonsterLevelPicker.SelectedIndex == -1)
            {
                MonsterLevelPicker.SelectedIndex = ViewModel.Data.Level - 1;
                return;
            }

            var result = MonsterLevelPicker.SelectedIndex + 1;

            // When level changed, roll again for max health, and set attributes to follow the Level Table
            if (result != ViewModel.Data.Level)
            {
                // Change the Level
                ViewModel.Data.Level = result;

                // Roll for new max health
                var oldMaxHealth = ViewModel.Data.MaxHealth;
                ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

                // Add the different between old and new max health to the current health
                // Set a lower limit 1 to avoid the current health drop to too low
                ViewModel.Data.CurrentHealth = Math.Max(1, ViewModel.Data.CurrentHealth + ViewModel.Data.MaxHealth - oldMaxHealth);

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