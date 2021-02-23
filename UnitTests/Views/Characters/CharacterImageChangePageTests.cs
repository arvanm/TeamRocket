using NUnit.Framework;
using System.Linq;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using System.Threading.Tasks;

namespace UnitTests.Views
{
    [TestFixture]
    public class CharacterImageChangePageTests : CharacterImageChangePage
    {
        App app;
        CharacterImageChangePage page;

        public CharacterImageChangePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new CharacterImageChangePage(new GenericViewModel<CharacterModel>(new CharacterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void CharacterImageChangePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CharacterImageChangePage_SelectCharacterImage_Clicked_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterModel();
            CharacterIndexViewModel ViewModel = CharacterIndexViewModel.Instance;
            ViewModel.Dataset.Add(data);
            data.ImageURI = "character_01.png";
            ImageButton button = new ImageButton();
            button.CommandParameter = data;
            // Act
            page.SelectCharacterImage_Clicked(button, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }



        [Test]
        public void CharacterImageChangePage_SelectCharacterImage_Clicked_Default_Invalid_Should_Pass()
        {
            // Arrange

            // Act
            page.SelectCharacterImage_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //    [Test]
        //    public void CharacterImageChangePage_ShowPopupPokedex_Clicked_Default_Should_Pass()
        //    {
        //        // Arrange

        //        // Act
        //        page.ShowPopupPokedex_Clicked(null, null);

        //        // Reset

        //        // Assert
        //        Assert.IsTrue(true); // Got to here, so it happened...
        //    }

        //    [Test]
        //    public void CharacterImageChangePage_ClosePopupPokedex_Clicked_Default_Should_Pass()
        //    {
        //        // Arrange

        //        // Act
        //        page.ClosePopupPokedex_Clicked(null, null);

        //        // Reset

        //        // Assert
        //        Assert.IsTrue(true); // Got to here, so it happened...
        //    }

        //    [Test]
        //    public void CharacterImageChangePage_Delete_Clicked_Default_Should_Pass()
        //    {
        //        // Arrange

        //        // Act
        //        page.Delete_Clicked(null, null);

        //        // Reset

        //        // Assert
        //        Assert.IsTrue(true); // Got to here, so it happened...
        //    }

        //    [Test]
        //    public void CharacterImageChangePage_OnBackButtonPressed_Valid_Should_Pass()
        //    {
        //        // Arrange

        //        // Act
        //        OnBackButtonPressed();

        //        // Reset

        //        // Assert
        //        Assert.IsTrue(true); // Got to here, so it happened...
        //    }

        //    [Test]
        //    public void CharacterImageChangePage_GetItemToDisplay_Valid_Should_Pass()
        //    {
        //        // Arrange

        //        // Act
        //        page.GetItemToDisplay(ItemLocationEnum.Feet);

        //        // Reset

        //        // Assert
        //        Assert.IsTrue(true); // Got to here, so it happened...
        //    }

        //    [Test]
        //    public void CharacterImageChangePage_ShowPopupItem_Clicked_Valid_Should_Pass()
        //    {
        //        // Arrange

        //        // Act
        //        page.ShowPopupItem_Clicked(new ItemModel());

        //        // Reset

        //        // Assert
        //        Assert.IsTrue(true); // Got to here, so it happened...
        //    }

        //    [Test]
        //    public void CharacterImageChangePage_ClosePopupItem_Clicked_Default_Should_Pass()
        //    {
        //        // Arrange

        //        // Act
        //        page.ClosePopupItem_Clicked(null, null);

        //        // Reset

        //        // Assert
        //        Assert.IsTrue(true); // Got to here, so it happened...
        //    }

        //    [Test]
        //    public void CharacterImageChangePage_AddItemsToDisplay_With_Data_Should_Remove_And_Pass()
        //    {
        //        // Arrange

        //        // Put some data into the box so it can be removed
        //        Grid itemBox = (Grid)page.Content.FindByName("ItemGrid");

        //        itemBox.Children.Add(new Label());
        //        itemBox.Children.Add(new Label());

        //        // Act
        //        page.AddItemsToDisplay();

        //        // Reset

        //        // Assert
        //        Assert.AreEqual(8, itemBox.Children.Count()); // Got to here, so it happened...
        //    }

        //    [Test]
        //    public async Task CharacterImageChangePage_GetItemToDisplay_With_Item_Should_Pass()
        //    {
        //        // Arrange
        //        ItemIndexViewModel.Instance.Dataset.Clear();
        //        await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Location = ItemLocationEnum.PrimaryHand });

        //        var character = new CharacterModel();
        //        character.Head = ItemIndexViewModel.Instance.GetLocationItems(ItemLocationEnum.PrimaryHand).First().Id;
        //        page.ViewModel.Data = character;

        //        // Act
        //        var result = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);

        //        // Reset
        //        ItemIndexViewModel.Instance.Dataset.Clear();
        //        await ItemIndexViewModel.Instance.LoadDefaultDataAsync();

        //        // Assert
        //        Assert.AreEqual(2, result.Children.Count()); // Got to here, so it happened...
        //    }

        //    [Test]
        //    public async Task CharacterImageChangePage_GetItemToDisplay_With_NoItem_Should_Pass()
        //    {
        //        // Arrange
        //        ItemIndexViewModel.Instance.Dataset.Clear();
        //        var item = new ItemModel { Location = ItemLocationEnum.PrimaryHand };
        //        await ItemIndexViewModel.Instance.CreateAsync(item);

        //        // Act
        //        var result = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);

        //        // Reset
        //        ItemIndexViewModel.Instance.Dataset.Clear();
        //        await ItemIndexViewModel.Instance.LoadDefaultDataAsync();

        //        // Assert
        //        Assert.AreEqual(2, result.Children.Count()); // Got to here, so it happened...
        //    }

        //    [Test]
        //    public void CharacterImageChangePage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        //    {
        //        // Arrange
        //        var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
        //        page.ViewModel.Data.PrimaryHand = item.Id;
        //        var StackItem = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);
        //        var dataImage = StackItem.Children[0];

        //        // Act
        //        ((ImageButton)dataImage).PropagateUpClicked();

        //        // Reset

        //        // Assert
        //        Assert.IsTrue(true); // Got to here, so it happened...
        //    }
        //}
    }
}