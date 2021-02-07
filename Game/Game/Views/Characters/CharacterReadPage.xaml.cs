using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterReadPage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<CharacterModel> ViewModel;

        // Empty Constructor for UTs
        public CharacterReadPage(bool UnitTest) { }

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public CharacterReadPage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
            
            this.ViewModel.Title = "Character Read";

            // Show the Character's Items
            AddItemsToDisplay();
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharacterUpdatePage(ViewModel)));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharacterDeletePage(ViewModel)));
            await Navigation.PopAsync();
        }


        #region ManageItems

        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        public void AddItemsToDisplay()
        {
            // Remove current data
            var FlexList = ItemGrid.Children.ToList();
            foreach (var data in FlexList)
            {
                ItemGrid.Children.Remove(data);
            }

            // Generate Layouts for Items
            var ItemLayoutList = new List<StackLayout>();
            ItemLayoutList.Add(GetItemToDisplay(ItemLocationEnum.Head));
            ItemLayoutList.Add(GetItemToDisplay(ItemLocationEnum.Necklass));
            ItemLayoutList.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand));
            ItemLayoutList.Add(GetItemToDisplay(ItemLocationEnum.OffHand));
            ItemLayoutList.Add(GetItemToDisplay(ItemLocationEnum.RightFinger));
            ItemLayoutList.Add(GetItemToDisplay(ItemLocationEnum.LeftFinger));
            ItemLayoutList.Add(GetItemToDisplay(ItemLocationEnum.Feet));
            ItemLayoutList.Add(GetItemToDisplay(ItemLocationEnum.Pokeball));

            // Add Item Layouts to the Grid
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    ItemGrid.Children.Add(ItemLayoutList[i * 4 + j]);
                    Grid.SetRow(ItemLayoutList[i * 4 + j], i);
                    Grid.SetColumn(ItemLayoutList[i * 4 + j], j);
                }
            }
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemLocationEnum location)
        {
            // Defualt Image is the Plus
            var ImageSource = "icon_cancel.png";
            var ClickableButton = true;

            // Get ItemModel
            var data = ViewModel.Data.GetItemByLocation(location);

            // ItemModel is null, no item in the location
            if (data == null)
            {
                // Show the Default Icon for the Location
                data = new ItemModel { Location = location, ImageURI = ImageSource };

                // Turn off click action
                ClickableButton = false;
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI
            };

            if (ClickableButton)
            {
                // Add a event to the user can click the item and see more
                ItemButton.Clicked += (sender, args) => ShowPopup(data);
            }

            // Add the Display Text for the item
            var ItemLabel = new Label
            {
                Text = location.ToMessage(),
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = new Thickness(15, 12),
                Style = (Style)Application.Current.Resources["ItemImageBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                    ItemLabel
                },
            };

            return ItemStack;
        }

        #region PopupManagement

        /// <summary>
        /// Show the Popup for the Item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemModel data)
        {
            PopupLoadingView.IsVisible = true;
            PopupItemImage.Source = data.ImageURI;

            PopupItemName.Text = data.Name;
            PopupItemDescription.Text = data.Description;
            PopupItemLocation.Text = data.Location.ToMessage();
            PopupItemAttribute.Text = data.Attribute.ToMessage();
            PopupItemValue.Text = " + " + data.Value.ToString();

            return true;
        }

        /// <summary>
        /// When the user clicks the close in the Popup
        /// hide the view
        /// show the scroll view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            PopupLoadingView.IsVisible = false;
        }

        #endregion PopupManagement

        #endregion ManageItems
    }
}