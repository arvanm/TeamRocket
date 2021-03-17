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
	public partial class BeginGame: ContentPage
	{

        /// <summary>
        /// Constructor
        /// </summary>
        public BeginGame()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Start Button move page to next battle page view, which is picking characters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PickCharactersPage());
        }
    }
}