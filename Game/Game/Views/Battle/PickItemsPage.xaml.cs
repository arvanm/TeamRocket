using Game.Models;
using Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickItemsPage : ContentPage
    {

        public List<ItemModel> AvailableItem;
        public List<PlayerInfoModel> AvailableCharacter;

        public ItemModel SelectedItem;
        public PlayerInfoModel SelectedCharacter;

        /// <summary>
        /// Constructor
        /// </summary>
        public PickItemsPage()
        {
            AvailableCharacter = new List<PlayerInfoModel>();
            AvailableItem = new List<ItemModel>();
            InitializeComponent();
            DrawCharacterList();

            DrawItemLists();
        }

        /// <summary>
        /// Clear and Add the Characters that survived
        /// </summary>
        public void DrawCharacterList()
        {
            // Clear and Populate the Characters Remaining
            var FlexList = CharacterListFrame.Children.ToList();
            foreach (var data in FlexList)
            {
                CharacterListFrame.Children.Remove(data);
            }

            // Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList)
            {
                AvailableCharacter.Add(data);
                CharacterListFrame.Children.Add(CreatePlayerDisplayBox(data));
            }
        }


        /// <summary>
        /// Draw the List of Items
        /// 
        /// The Ones Dropped
        /// 
        /// The Ones Selected
        /// 
        /// </summary>
        public void DrawItemLists()
        {
            DrawDroppedItems();
        }

        /// <summary>
        /// Add the Dropped Items to the Display
        /// </summary>
        public void DrawDroppedItems()
        {
            // Clear and Populate the Dropped Items
            var FlexList = ItemListFoundFrame.Children.ToList();
            foreach (var data in FlexList)
            {
                ItemListFoundFrame.Children.Remove(data);
            }

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Distinct())
            {
                AvailableItem.Add(data);
                ItemListFoundFrame.Children.Add(GetItemToDisplay(data));
            }
        }

        public void UpdateDrawDroppedItems()
        {
            // Clear and Populate the Dropped Items
            var FlexList = ItemListFoundFrame.Children.ToList();
            foreach (var data in FlexList)
            {
                ItemListFoundFrame.Children.Remove(data);
            }

            foreach (var data in AvailableItem.ToList())
            {
                ItemListFoundFrame.Children.Add(GetItemToDisplay(data));
            }
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemModel item)
        {
            if (item == null)
            {
                return new StackLayout();
            }

            // Defualt Image is the Plus
            var ClickableButton = true;

            var data = ItemIndexViewModel.Instance.GetItem(item.Id);
            if (data == null)
            {
                // Show the Default Icon for the Location
                data = new ItemModel { Name = "Unknown", ImageURI = "icon_cancel.png" };

                // Turn off click action
                ClickableButton = false;
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI,
            };

            if (ClickableButton)
            {            
                // Add a event to the user can click the item and see more
                ItemButton.Clicked += (sender, args) =>
                {

                    if (SelectedItem == data)
                    {
                        _ = ItemButton.IsFocused;
                        ItemButton.BorderColor = Color.Transparent;
                        ItemButton.BorderWidth = 0;
                        SelectedItem = null;
                        return;
                    }

                    // Get focused on Item and create a border
                    _ = ItemButton.IsFocused;
                    ItemButton.BorderColor = Color.Red;
                    ItemButton.BorderWidth = 2;

                    // Close manualy Item assign when items is zero
                    if (AvailableItem.Count() == 0)
                    {
                        object s = new object();
                        EventArgs e = new EventArgs();
                        CloseButton_Clicked(s, e);
                    }
                    //set Item value
                    SelectedItem = data;

                    //Check to see if we can enable the save button
                    if (SelectedCharacter != null && SelectedItem != null)
                    {
                        SaveButton.IsEnabled = true;
                    }
                };
            }

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                },
            };

            return ItemStack;
        }

        /// <summary>
        /// Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreatePlayerDisplayBox(PlayerInfoModel data)
        {
            // Defualt Image is the Plus
            var ClickableButton = true;

            if (data == null)
            {
                data = new PlayerInfoModel();
                // Turn off click action
                ClickableButton = false;

            }

            // Hookup the image
            var PlayerImage = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageBattleLargeStyle"],
                Source = data.ImageURI
            };

            if (ClickableButton)
            {
                // Add a event to the user can click the charcter image and see more
                PlayerImage.Clicked += (sender, args) =>
                {
                    if (SelectedCharacter == data)
                    {
                        _ = PlayerImage.IsFocused;
                        PlayerImage.BorderColor = Color.Transparent;
                        PlayerImage.BorderWidth = 0;
                        SelectedCharacter = null;
                        return;
                    }

                    // Get focused on Item and create a border
                    _ = PlayerImage.IsFocused;
                    PlayerImage.BorderColor = Color.Red;
                    PlayerImage.BorderWidth = 2;


                    SelectedCharacter = data;

                    if (SelectedCharacter != null && SelectedItem != null)
                    {
                        SaveButton.IsEnabled = true;
                    }
                };
            }

            // Add the Level
            var PlayerLevelLabel = new Label
            {
                Text = "Level : " + data.Level,
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Add the HP
            var PlayerHPLabel = new Label
            {
                Text = "HP : " + data.GetCurrentHealthTotal,
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            var PlayerNameLabel = new Label()
            {
                Text = data.Name,
                Style = (Style)Application.Current.Resources["ValueStyle"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                    PlayerNameLabel,
                    PlayerLevelLabel,
                    PlayerHPLabel,
                },
            };

            return PlayerStack;
        }

        public void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (SelectedCharacter != null && SelectedItem != null)
            {
                // Update the character with Item attributes
                UpdateAttributeValues();

                // Clear and Populate the Dropped Items
                foreach (var item in AvailableItem.ToList())
                {
                    if (SelectedItem.Id == item.Id.ToString())
                    {
                        AvailableItem.Remove(SelectedItem);
                    }
                }

                if (AvailableItem.Count() == 0)
                {
                    object s = new object();
                    CloseButton_Clicked(s, e);
                }
                LabelActionTaken.IsVisible = true;
                ActionDone.IsVisible = true;
                ActionDone.Text = SelectedItem.Name + " was added to " + SelectedCharacter.Name;
                SelectedCharacter = null;
                SelectedItem = null;
                SaveButton.IsEnabled = false;

                OnAppearing();

            }
        }


        /// <summary>
        /// Refresh the page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateDrawDroppedItems();
        }

        /// <summary>
        /// Quit the Battle
        /// 
        /// Quitting out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Change the attribute values after changing level in the Level Picker
        /// </summary>
        public void UpdateAttributeValues()
        {
            // Current Health
            if (ItemLocationEnum.Head == SelectedItem.Location)
            {
                SelectedCharacter.Head = SelectedItem.Id;
            }

            if (ItemLocationEnum.PrimaryHand == SelectedItem.Location)
            {
                SelectedCharacter.PrimaryHand = SelectedItem.Id;
            }

            if (ItemLocationEnum.Feet == SelectedItem.Location)
            {
                SelectedCharacter.Feet = SelectedItem.Id;
            }

            if (ItemLocationEnum.Finger == SelectedItem.Location)
            {
                SelectedCharacter.LeftFinger = SelectedItem.Id;
            }

            if (ItemLocationEnum.LeftFinger == SelectedItem.Location)
            {
                SelectedCharacter.LeftFinger = SelectedItem.Id;
            }

            if (ItemLocationEnum.RightFinger== SelectedItem.Location)
            {
                SelectedCharacter.RightFinger = SelectedItem.Id;
            }

            if (ItemLocationEnum.Necklass == SelectedItem.Location)
            {
                SelectedCharacter.Necklass = SelectedItem.Id;
            }

            if (ItemLocationEnum.OffHand == SelectedItem.Location)
            {
                SelectedCharacter.OffHand = SelectedItem.Id;
            }
        }
    }
}