using System;
using System.ComponentModel;
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
        public CharacterUpdatePage(bool UnitTest){ }

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

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            await Navigation.PopModalAsync();
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

        #region Picker
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
        #endregion Picker

    }
}