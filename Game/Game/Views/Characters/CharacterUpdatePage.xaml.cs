﻿using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;
using Game.GameRules;
using System.Collections.ObjectModel;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterUpdatePage : ContentPage
    {
        // View Model for Character
        public readonly GenericViewModel<CharacterModel> ViewModel;

        // ItemIndexViewModel for get items
        public readonly ItemIndexViewModel ItemListModel;

        // Hold a copy of the original data for Cancel to use
        public CharacterModel DataCopy;

        // Empty Constructor for Tests
        public CharacterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public CharacterUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            // Get instance of ItemIndexViewModel
            ItemListModel = ItemIndexViewModel.Instance;

            // Make a copy of the character for cancel to restore
            DataCopy = new CharacterModel(data.Data);

            this.ViewModel.Title = "Character Update";

            LoadCharacterLevelPickerValues();

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
            CharacterLevelPicker.SelectedIndex = ViewModel.Data.Level - 1;

            // Sets the Job Picker to the Character's Type
            CharacterJobPicker.SelectedItem = ViewModel.Data.Job.ToMessage();

            // Show the Character's Items
            AddItemsToDisplay();

            // Update the attribute table
            UpdateAttributeValues();

            return true;
        }

        /// <summary>
        /// Change the image of the Character
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

            UpdatePageBindingContext();
        }

        /// <summary>
        /// Change the attribute values after changing level in the Level Picker
        /// </summary>
        public void UpdateAttributeValues()
        {
            // Current Health
            CurrentHealthValue.Text = ViewModel.Data.CurrentHealth.ToString();
            CurrentHealthValueItemBonus.Text = ViewModel.Data.GetCurrentHealthItemBonus.ToString();
            CurrentHealthValueTotal.Text = ViewModel.Data.GetCurrentHealthTotal.ToString();

            // Max Health
            MaxHealthValue.Text = ViewModel.Data.MaxHealth.ToString();
            MaxHealthValueItemBonus.Text = ViewModel.Data.GetMaxHealthItemBonus.ToString();
            MaxHealthValueTotal.Text = ViewModel.Data.GetMaxHealthTotal.ToString();

            // Attack
            AttackValue.Text = ViewModel.Data.GetAttackLevelBonus.ToString();
            AttackValueItemBonus.Text = ViewModel.Data.GetAttackItemBonus.ToString();
            AttackValuePokedexBonus.Text = ViewModel.Data.GetAttackPokedexBonus.ToString();
            AttackValueTotal.Text = ViewModel.Data.GetAttackTotal.ToString();

            // Damage
            DamageValue.Text = ViewModel.Data.GetDamageLevelBonus.ToString();
            DamageValueItemBonus.Text = ViewModel.Data.GetDamageItemBonusString;
            DamageValuePokedexBonus.Text = ViewModel.Data.GetDamagePokedexBonusString;
            DamageValueTotal.Text = ViewModel.Data.GetDamageTotalString;

            // Defense
            DefenseValue.Text = ViewModel.Data.GetDefenseLevelBonus.ToString();
            DefenseValueItemBonus.Text = ViewModel.Data.GetDefenseItemBonus.ToString();
            DefenseValueTotal.Text = ViewModel.Data.GetDefenseTotal.ToString();

            // Speed
            SpeedValue.Text = ViewModel.Data.GetSpeedLevelBonus.ToString();
            SpeedValueItemBonus.Text = ViewModel.Data.GetSpeedItemBonus.ToString();
            SpeedValueTotal.Text = ViewModel.Data.GetSpeedTotal.ToString();
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
                await DisplayAlert("Alert", "Character's Name cannot be empty!", "OK");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the input max health is in the correct range.
        /// If the input max health is smaller or equal than 0 or larger than level times 10,
        /// display an alert, and return false.
        /// Otherwise return true
        /// </summary>
        /// <returns>Whether the input name is empty or null</returns>
        public async Task<bool> CheckCharacterMaxHealth()
        {
            if (ViewModel.Data.MaxHealth <= 0 || ViewModel.Data.MaxHealth > ViewModel.Data.Level * 10)
            {
                await DisplayAlert("Alert", "Character's Base Max Health must be between 1 and Level x 10!", "OK");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the input current health is in the correct range.
        /// If the input current health is smaller than 0 or larger than the max health,
        /// display an alert, and return false.
        /// Otherwise return true
        /// </summary>
        /// <returns>Whether the input name is empty or null</returns>
        public async Task<bool> CheckCharacterCurHealth()
        {
            if (ViewModel.Data.CurrentHealth < 0 || ViewModel.Data.CurrentHealth > ViewModel.Data.MaxHealth)
            {
                await DisplayAlert("Alert", "Character's Base Current Health must be between 0 and Base Max Health!", "OK");
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
            // the character name is not empty,
            // the max health is not within [1, level * 10] range,
            // otherwise display an alert.

            if (!await CheckCharacterName())
            {
                return;
            }

            if (!await CheckCharacterMaxHealth())
            {
                return;
            }

            if (!await CheckCharacterCurHealth())
            { 
                return; 
            }

            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            OnAppearing();

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
            // Put the copy back
            ViewModel.Data.Update(DataCopy);

            await Navigation.PopModalAsync();
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
                var oldMaxHealth = ViewModel.Data.MaxHealth;
                ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

                // Add the different between old and new max health to the current health
                // Set a lower limit 1 to avoid the current health drop to too low
                ViewModel.Data.CurrentHealth = Math.Max(1, ViewModel.Data.CurrentHealth + ViewModel.Data.MaxHealth - oldMaxHealth);

                // Update attribute values in the table
                UpdateAttributeValues();
            }
        }
        #endregion LevelPicker


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
                ItemButton.Clicked += (sender, args) => ShowPopup(data, location);
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
        /// <param name="location"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemModel data, ItemLocationEnum location)
        {
            // Show the Popup View
            PopupLoadingView.IsVisible = true;

            // Update the displayed selected item as the current item
            UpdatePopupSelectedItemValues(data);

            // Get List of ItemModels for list, and bind click function
            PopupListView.ItemsSource = new ObservableCollection<ItemModel>(ItemListModel.GetLocationItems(location));
            PopupListView.ItemSelected += (sender, args) => PopupItemSelected((ItemModel)args.SelectedItem, location);

            return true;
        }

        /// <summary>
        /// Update the displayed information for selected item in Popup
        /// </summary>
        /// <param name="data"></param>
        public void UpdatePopupSelectedItemValues(ItemModel data)
        {
            // Image, name, attribute, value
            PopupSelectedItemImage.Source = data.ImageURI;
            PopupSelectedItemName.Text = data.Name;
            PopupSelectedItemAttribute.Text = data.Attribute.ToMessage();
            PopupSelectedItemValue.Text = data.Value.ToString();

            // Damage for Primary Hand and Pokeball
            if (data.Location == ItemLocationEnum.PrimaryHand || data.Location == ItemLocationEnum.Pokeball)
            {
                PopupSelectedItemDamageStack.IsVisible = true;
                PopupSelectedItemDamageValue.Text = data.Damage.ToString();
            }
            else
            {
                PopupSelectedItemDamageStack.IsVisible = false;
            }

            // Range for Primary Hand
            if (data.Location == ItemLocationEnum.PrimaryHand)
            {
                PopupSelectedItemRangeStack.IsVisible = true;
                PopupSelectedItemRangeValue.Text = data.Range.ToString();
            }
            else
            {
                PopupSelectedItemRangeStack.IsVisible = false;
            }

        }

        /// <summary>
        /// The item selected from the list. Set location to the item and update selected view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void PopupItemSelected(ItemModel data, ItemLocationEnum location)
        {
            // If data is null, return
            if (data == null)
            {
                return;
            }

            // Set item to corresponding location
            switch (location)
            {
                case ItemLocationEnum.PrimaryHand:
                    ViewModel.Data.PrimaryHand = data.Id;
                    break;
                case ItemLocationEnum.OffHand:
                    ViewModel.Data.OffHand = data.Id;
                    break;
                case ItemLocationEnum.Necklass:
                    ViewModel.Data.Necklass = data.Id;
                    break;
                case ItemLocationEnum.Feet:
                    ViewModel.Data.Feet = data.Id;
                    break;
                case ItemLocationEnum.LeftFinger:
                    ViewModel.Data.LeftFinger = data.Id;
                    break;
                case ItemLocationEnum.RightFinger:
                    ViewModel.Data.RightFinger = data.Id;
                    break;
                case ItemLocationEnum.Head:
                    ViewModel.Data.Head = data.Id;
                    break;
                case ItemLocationEnum.Pokeball:
                    ViewModel.Data.Pokeball = data.Id;
                    break;
                default:
                    return;
                    break;
            }

            // Update selected view
            UpdatePopupSelectedItemValues(data);
        }

        /// <summary>
        /// When the user clicks the Save botton in the Popup
        /// hide the view
        /// show the scroll view
        /// refresh the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopupSave_Clicked(object sender, EventArgs e)
        {
            // Set the popup view to invisible
            PopupLoadingView.IsVisible = false;

            // Refresh the character page to reveal changes
            UpdatePageBindingContext();
        }

        #endregion PopupManagement

        #endregion ManageItems
    }
}