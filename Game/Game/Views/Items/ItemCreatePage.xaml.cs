using Game.Models;
using Game.ViewModels;
using Game.Helpers;

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
    public partial class ItemCreatePage : ContentPage
    {
        // The item to create
        public GenericViewModel<ItemModel> ViewModel = new GenericViewModel<ItemModel>();

        // Empty Constructor for UTs
        public ItemCreatePage(bool UnitTest){}

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public ItemCreatePage()
        {
            InitializeComponent();

            this.ViewModel.Data = new ItemModel();

            BindingContext = this.ViewModel;

            this.ViewModel.Title = "Create";

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = ViewModel.Data.Location.ToString();
            AttributePicker.SelectedItem = ViewModel.Data.Attribute.ToString();
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // Only save when the item name is not empty, otherwise display an alert
            if (await CheckItemName() && await CheckItemLocation() && await CheckItemAttribute())
            {
                // If the image in the data box is empty, use the default one..
                if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
                {
                    ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
                }

                MessagingCenter.Send(this, "Create", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
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

        /// <summary>
        /// Catch the change to the Stepper for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Range_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Damage_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DamageValue.Text = String.Format("{0}", e.NewValue);
        }

        #region InputValueCheck
        /// <summary>
        /// Check whether the input name is empty or null.
        /// If the input name is empty or null, display an alert says the name cannot be empty, and return false.
        /// Otherwise return true
        /// </summary>
        /// <returns>Whether the input name is empty or null</returns>
        private async Task<bool> CheckItemName()
        {
            if (string.IsNullOrEmpty(ViewModel.Data.Name))
            {
                await DisplayAlert("Alert", "Item name cannot be empty!", "OK");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the item location is selected.
        /// If the item location is not selected., display an alert says the location cannot be empty, and return false.
        /// Otherwise return true
        /// </summary>
        /// <returns>Whether the input name is empty or null</returns>
        private async Task<bool> CheckItemLocation()
        {
            if (ViewModel.Data.Location == ItemLocationEnum.Unknown)
            {
                await DisplayAlert("Alert", "Item location cannot be empty!", "OK");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the item attribute is selected.
        /// If the item attribute is not selected., display an alert says the attribute cannot be empty, and return false.
        /// Otherwise return true
        /// </summary>
        /// <returns>Whether the input name is empty or null</returns>
        private async Task<bool> CheckItemAttribute()
        {
            if (ViewModel.Data.Attribute == AttributeEnum.Unknown)
            {
                await DisplayAlert("Alert", "Item attribute cannot be empty!", "OK");
                return false;
            }

            return true;
        }
        #endregion InputValueCheck

        #region LocationPicker
        /// <summary>
        /// The Level selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void LocationPicker_Changed(object sender, EventArgs args)
        {
            var selectedLocationString = (string) LocationPicker.SelectedItem;
            var selectedLocation = ItemLocationEnumHelper.ConvertMessageToEnum(selectedLocationString);

            // Set visibility of damage stepper
            if (selectedLocation == ItemLocationEnum.PrimaryHand || selectedLocation == ItemLocationEnum.Pokeball)
            {
                DamageStack.IsVisible = true;
            }
            else
            {
                DamageStack.IsVisible = false;
                ViewModel.Data.Damage = 0;
            }
            
            // Set visibility of range stepper
            if (selectedLocation == ItemLocationEnum.PrimaryHand)
            {
                RangeStack.IsVisible = true;
                ViewModel.Data.Range = 1;
            }
            else
            {
                RangeStack.IsVisible = false;
                ViewModel.Data.Range = 0;
            }
        }
        #endregion LocationPicker
    }
}