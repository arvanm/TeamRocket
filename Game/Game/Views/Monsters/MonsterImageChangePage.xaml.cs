using System;
using System.Linq;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// Index Page
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [DesignTimeVisible(false)] 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterImageChangePage : ContentPage
    {
        // Hold the ViewModel that was passed in
        public GenericViewModel<MonsterModel> viewModel;

        // Empty Constructor for UTs
        public MonsterImageChangePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the ItemIndexView Model
        /// </summary>
        public MonsterImageChangePage()
        {
            InitializeComponent();

            //BindingContext = ViewModel;
        }

        public MonsterImageChangePage(GenericViewModel<MonsterModel> viewModel)
        {
            this.viewModel = viewModel;

            InitializeComponent();

           // BindingContext = ViewModel;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void SelectMonsterImage_Clicked(object sender, EventArgs args)
        {
            // Handle null input
            if (sender == null)
            {
                return;
            }

            // Get MonsterModel from the button clicked
            var button = sender as ImageButton;
            var imageSelected = button.CommandParameter as String;
            viewModel.Data.ImageURI = imageSelected;

            // Handle null data
            if (viewModel == null)
            {
                return;
            }

            MessagingCenter.Send(this, "Update", viewModel);
            await Navigation.PopModalAsync();
        }



    }
}