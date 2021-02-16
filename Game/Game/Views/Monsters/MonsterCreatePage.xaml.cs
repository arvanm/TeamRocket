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

            // Sets the Job Picker to the Monster's Type
            MonsterTypePicker.SelectedItem = ViewModel.Data.MonsterType.ToMessage();
        }

        /// <summary>
        /// Cancel the Monster creation and close this page
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
        /// Save the Monster and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            // Only save when the Monster name is not empty, otherwise display an alert
            if (await CheckMonsterName())
            {
                // If the image in the data box is empty, use the default one.
                if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
                {
                    ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
                }
                // Check to see if MonsterTypePicker has been set
                if(MonsterTypePicker.SelectedIndex == -1)
                {
                    await DisplayAlert("Alert", "Monster type cannot be empty!", "OK");
                    return;
                }

                MessagingCenter.Send(this, "Create", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Randomly change the image of the Monster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ChangeImage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterImageChangePage(ViewModel)));
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
    }
}