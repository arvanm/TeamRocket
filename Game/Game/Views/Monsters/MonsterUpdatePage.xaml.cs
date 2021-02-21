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

        // Hold a copy of the original data for Cancel to use
        public MonsterModel DataCopy;

        // Empty Constructor for Tests
        public MonsterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            // Make a copy of the monster for cancel to restore
            DataCopy = new MonsterModel(data.Data);

            this.ViewModel.Title = "Monster Update";

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

            // Sets the Job Picker to the monster's Type
            MonsterTypePicker.SelectedItem = ViewModel.Data.MonsterType.ToMessage();

            return true;
        }

        /// <summary>
        /// Change the image of the Monster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ChangeImage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterImageChangePage(ViewModel)));
            await Navigation.PopAsync();
        }

        #region InputValueCheck
        /// <summary>
        /// Check whether the input name is empty or null.
        /// If the input name is empty or null, display an alert says the name cannot be empty, and return false.
        /// Otherwise return true
        /// </summary>
        /// <returns>Whether the input name is empty or null</returns>
        public async Task<bool> CheckMonsterName()
        {
            if (string.IsNullOrEmpty(ViewModel.Data.Name))
            {
                await DisplayAlert("Alert", "Monster name cannot be empty!", "OK");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the attribute values are negative.
        /// If any attribute value is empty or null, display an alert says the attribute value cannot be negative, and return false.
        /// Otherwise return true.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckAttributeValue()
        {
            if (ViewModel.Data.Attack < 0)
            {
                await DisplayAlert("Alert", "Monster attack cannot be negative!", "OK");
                return false;
            }

            if (ViewModel.Data.SpecialAttack < 0)
            {
                await DisplayAlert("Alert", "Monster special attack cannot be negative!", "OK");
                return false;
            }

            if (ViewModel.Data.Defense < 0)
            {
                await DisplayAlert("Alert", "Monster defense cannot be negative!", "OK");
                return false;
            }

            if (ViewModel.Data.Speed < 0)
            {
                await DisplayAlert("Alert", "Monster speed cannot be negative!", "OK");
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
            if (await CheckMonsterName() && await CheckAttributeValue())
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
        /// Refresh the page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            BindingContext = ViewModel;
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            // Put the copy back
            ViewModel.Data.Update(DataCopy);

            await Navigation.PopModalAsync();
        }

    }
}