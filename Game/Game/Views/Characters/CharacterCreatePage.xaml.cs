using Game.Models;
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
    public partial class CharacterCreatePage : ContentPage
    {
        // The item to create
        public GenericViewModel<CharacterModel> ViewModel = new GenericViewModel<CharacterModel>();

        // Empty Constructor for UTs
        public CharacterCreatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public CharacterCreatePage()
        {
            InitializeComponent();

            this.ViewModel.Data = RandomPlayerHelper.GetRandomCharacter(1);

            BindingContext = this.ViewModel;

            this.ViewModel.Title = "Character Create";

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
        public async void Cancel_Clicked(object sender, EventArgs e)
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
        public async Task<bool> CheckCharacterName()
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
        public async void Save_Clicked(object sender, EventArgs e)
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
        /// Change the image of the character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ChangeImage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharacterImageChangePage(ViewModel)));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Refresh the page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            BindingContext = ViewModel;
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
            }
        }
        #endregion LevelPicker
    }
}