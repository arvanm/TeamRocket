using Game.Models;
using Game.ViewModels;

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
    public partial class ScoreCreatePage : ContentPage
    {
        // The item to create
        public GenericViewModel<ScoreModel> ViewModel { get; set; }

        // Constructor for Unit Testing
        public ScoreCreatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public ScoreCreatePage(GenericViewModel<ScoreModel> data)
        {
            InitializeComponent();

            data.Data = new ScoreModel();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create";
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // Only save when the Monster name is not empty, otherwise display an alert
            if (!await CheckScoreName())
            {
                return;
            }

            if (!await CheckScoreValue())
            {
                return;
            }

            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            MessagingCenter.Send(this, "Create", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
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
        /// Otherwise return true.
        /// </summary>
        /// <returns>Whether the input name is empty or null</returns>
        private async Task<bool> CheckScoreName()
        {
            // Check to make sure name is not null
            if (string.IsNullOrEmpty(ViewModel.Data.Name))
            {
                await DisplayAlert("Alert", "Score name cannot be empty!", "OK");
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
        private async Task<bool> CheckScoreValue()
        {
            // Check to make sure value is not negative
            if (ViewModel.Data.ScoreTotal < 0)
            {
                await DisplayAlert("Alert", "Value of Score cannot be negative!", "OK");
                return false;
            }
            // Check to make sure value is not zero
            if (ViewModel.Data.ScoreTotal == 0)
            {
                await DisplayAlert("Alert", "Value of Score cannot equal zero!", "OK");
                return false;
            }

            return true;
        }
        #endregion InputValueCheck
    }
}