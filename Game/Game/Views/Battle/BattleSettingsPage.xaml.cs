using Game.Helpers;
using Game.Models;
using Game.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattleSettingsPage : ContentPage
    {
        // Empty Constructor for UTs
        // public BattleSettingsPage(bool UnitTest) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleSettingsPage()
        {
            InitializeComponent();

            // Set Values to current State
            AllowAmazonDeliverySwitch.IsToggled = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.AllowAmazonDelivery;
        }

        /// <summary>
        /// Close The Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Toggle Amazon Delivery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AllowAmazonDelivery_Toggled(object sender, EventArgs e)
        {
            // Flip the settings
            if (AllowAmazonDeliverySwitch.IsToggled == true)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.AllowAmazonDelivery = true;
                return;
            }

            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.AllowAmazonDelivery = false;
        }
    }
}