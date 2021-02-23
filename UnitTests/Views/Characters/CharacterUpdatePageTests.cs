using NUnit.Framework;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using System.Linq;

namespace UnitTests.Views
{
    [TestFixture]
    public class CharacterUpdatePageTests : CharacterUpdatePage
    {
        App app;
        CharacterUpdatePage page;

        public CharacterUpdatePageTests() : base(true) { }
        
        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new CharacterUpdatePage(new GenericViewModel<CharacterModel>(new CharacterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void CharacterUpdatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CharacterUpdatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_Save_Clicked_Null_Image_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_ChangeImage_Clicked_Null_Image_Should_Pass()
        {
            // Arrange


            // Act
            page.ChangeImage_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_CheckCharacterCurHealth_Null_Image_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.CurrentHealth = 9;

            // Act
            var boolValue = page.CheckCharacterCurHealth();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void CharacterUpdatePage_CheckCharacterMaxHealth_Null_Image_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.MaxHealth = 10;

            // Act
            var boolValue = page.CheckCharacterMaxHealth();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_ShowPopup_Clicked_Default_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel();
            item.Name = "Head";
            item.ImageURI = "";
            item.Location = ItemLocationEnum.PrimaryHand;

            // Act
            page.ShowPopup(item, ItemLocationEnum.PrimaryHand);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_ClosePopupSave_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ClosePopupSave_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_PopupItemSelected_Default_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel();
            item.Name = "Head";
            item.ImageURI = "";
            item.Location = ItemLocationEnum.PrimaryHand;

            // Act
            page.PopupItemSelected(item, item.Location);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_PopupItemSelected_OffHand_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel();
            item.Name = "Head";
            item.ImageURI = "";
            item.Location = ItemLocationEnum.OffHand;

            // Act
            page.PopupItemSelected(item, item.Location);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_PopupItemSelected_Necklass_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel();
            item.Name = "Necklass";
            item.ImageURI = "";
            item.Location = ItemLocationEnum.Necklass;

            // Act
            page.PopupItemSelected(item, item.Location);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_PopupItemSelected_Feet_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel();
            item.Name = "Feet";
            item.ImageURI = "";
            item.Location = ItemLocationEnum.Feet;

            // Act
            page.PopupItemSelected(item, item.Location);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_PopupItemSelected_LeftFinger_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel();
            item.Name = "LeftFinger";
            item.ImageURI = "";
            item.Location = ItemLocationEnum.LeftFinger;

            // Act
            page.PopupItemSelected(item, item.Location);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_PopupItemSelected_RightFinger_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel();
            item.Name = "RightFinger";
            item.ImageURI = "";
            item.Location = ItemLocationEnum.RightFinger;

            // Act
            page.PopupItemSelected(item, item.Location);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_PopupItemSelected_Pokeball_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel();
            item.Name = "Pokeball";
            item.ImageURI = "";
            item.Location = ItemLocationEnum.Pokeball;

            // Act
            page.PopupItemSelected(item, item.Location);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_PopupItemSelected_Head_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel();
            item.Name = "Head";
            item.ImageURI = "";
            item.Location = ItemLocationEnum.Head;

            // Act
            page.PopupItemSelected(item, item.Location);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void CharacterUpdatePage_PopupItemSelected_Invalid_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel();
            item.Name = "Head";
            item.ImageURI = "";
            //item.Location = ItemLocationEnum.Head;

            // Act
            page.PopupItemSelected(item, item.Location);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_UpdatePageBindingContext_Null_Image_Should_Pass()
        {
            // Arrange
            GenericViewModel<CharacterModel> ViewModel = new GenericViewModel<CharacterModel>();

            // Act
            page.UpdatePageBindingContext();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterUpdatePage_GetItemToDisplay_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.GetItemToDisplay(ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //[Test]
        //public void CharacterUpdatePage_Attack_OnStepperValueChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new CharacterModel();
        //    var ViewModel = new GenericViewModel<CharacterModel>(data);

        //    page = new CharacterUpdatePage(ViewModel);
        //    double oldValue = 0.0;
        //    double newValue = 1.0;

        //    var args = new ValueChangedEventArgs(oldValue, newValue);

        //    // Act
        //    page.Attack_OnStepperValueChanged(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_Defense_OnStepperValueChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new CharacterModel();
        //    var ViewModel = new GenericViewModel<CharacterModel>(data);

        //    page = new CharacterUpdatePage(ViewModel);
        //    double oldRange = 0.0;
        //    double newRange = 1.0;

        //    var args = new ValueChangedEventArgs(oldRange, newRange);

        //    // Act
        //    page.Defense_OnStepperValueChanged(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_Speed_OnStepperDamageChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new CharacterModel();
        //    var ViewModel = new GenericViewModel<CharacterModel>(data);

        //    page = new CharacterUpdatePage(ViewModel);
        //    double oldDamage = 0.0;
        //    double newDamage = 1.0;

        //    var args = new ValueChangedEventArgs(oldDamage, newDamage);

        //    // Act
        //    page.Speed_OnStepperValueChanged(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_Level_Changed_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new CharacterModel();
        //    var ViewModel = new GenericViewModel<CharacterModel>(data);

        //    page = new CharacterUpdatePage(ViewModel);
        //    double oldDamage = 0.0;
        //    double newDamage = 1.0;

        //    var args = new ValueChangedEventArgs(oldDamage, newDamage);

        //    // Act
        //    page.Level_Changed(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_RollDice_Clicked_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.RollDice_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_ClosePopup_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.ClosePopup();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_ClosePopup_Clicked_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.ClosePopup_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_OnPopupItemSelected_Clicked_Default_Should_Pass()
        //{
        //    // Arrange

        //    var data = new ItemModel();

        //    var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(data, 0);

        //    // Act
        //    page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_OnPopupItemSelected_Clicked_Null_Should_Fail()
        //{
        //    // Arrange

        //    var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);

        //    // Act
        //    page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_Item_ShowPopup_Default_Should_Pass()
        //{
        //    // Arrange

        //    var item = page.GetItemToDisplay(ItemLocationEnum.Head);

        //    // Act
        //    var itemButton = item.Children.FirstOrDefault(m => m.GetType().Name.Equals("Button"));

        //    page.ShowPopup(ItemLocationEnum.Head);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        //{
        //    // Arrange
        //    var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.Head);
        //    page.ViewModel.Data.Head = item.Id;
        //    var StackItem = page.GetItemToDisplay(ItemLocationEnum.Head);
        //    var dataImage = StackItem.Children[0];

        //    // Act
        //    ((ImageButton)dataImage).PropagateUpClicked();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}


        #region LevelPicker_Changed
        [Test]
        public void CharacterUpdatePage_CharacterLevelPicker_SelectedIndex_Neg1_Should_Return_Level()
        {
            // Arrange

            // Make a new Character to use for the Picker Tests
            page.ViewModel.Data = new CharacterModel()
            {
                Id = "test",
                Level = 10
            };
            
            var control = (Picker)page.FindByName("CharacterLevelPicker");
            control.SelectedIndex = -1;

            // Act
            page.CharacterLevelPicker_Changed(null, null);
            var result = control.SelectedIndex;

            // Reset

            // Assert
            Assert.AreEqual(10, result+1); 
        }

        [Test]
        public void CharacterUpdatePage_CharacterLevelPicker_SelectedIndex_No_Change_Should_Skip()
        {
            // Arrange

            // Make a new Character to use for the Picker Tests
            page.ViewModel.Data = new CharacterModel()
            {
                Id = "test",
                Level = 10
            };

            var control = (Picker)page.FindByName("CharacterLevelPicker");
            control.SelectedIndex = 10-1;

            // Act
            page.CharacterLevelPicker_Changed(null, null);
            var result = control.SelectedIndex;

            // Reset

            // Assert
            Assert.AreEqual(10, result + 1);
        }


        [Test]
        public void CharacterUpdatePage_CharacterLevelPicker_SelectedIndex_Change_Should_Update_Picker_To_Level()
        {
            // Arrange

            // Make a new Character to use for the Picker Tests
            page.ViewModel.Data = new CharacterModel()
            {
                Id = "test",
                Level = 1
            };

            var control = (Picker)page.FindByName("CharacterLevelPicker");
            control.SelectedIndex = 15;

            // Act
            page.CharacterLevelPicker_Changed(null, null);
            var result = control.SelectedIndex;

            // Reset

            // Assert
            Assert.AreEqual(16, result + 1);
        }
        #endregion LevelPicker_Changed
    }
}