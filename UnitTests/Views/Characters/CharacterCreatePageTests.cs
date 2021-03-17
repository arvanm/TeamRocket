using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class CharacterCreatePageTests : CharacterCreatePage
    {
        App app;
        CharacterCreatePage page;

        public CharacterCreatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            //page = new CharacterCreatePage(new GenericViewModel<CharacterModel>(new CharacterModel()));
            page = new CharacterCreatePage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void CharacterCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CharacterCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public async Task CharacterCreatePage_CheckCharacterName_Name_Should_Return_True()
        {
            // Arrange
            page.ViewModel.Data.Name = "test";

            // Act
            var Result = await page.CheckCharacterName();

            // Reset

            // Assert
            Assert.IsTrue(Result);
        }

        [Test]
        public void CharacterCreatePage_ChangeImage_Clicked_Null_Image_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.ChangeImage_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_OnAppear_Clicked_Null_Image_Should_Pass()
        {
            // Arrange
            GenericViewModel<CharacterModel> ViewModel = new GenericViewModel<CharacterModel>();

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_CheckCharacterName_Null_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.Name = null;

            // Act
            var boolValue = page.CheckCharacterName();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        #region LevelPicker_Changed
        [Test]
        public void CharacterCreatePage_LevelPicker_SelectedIndex_Neg1_Should_Return_Level()
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
            Assert.AreEqual(10, result + 1);
        }

        [Test]
        public void CharacterCreatePage_LevelPicker_SelectedIndex_No_Change_Should_Skip()
        {
            // Arrange

            // Make a new Character to use for the Picker Tests
            page.ViewModel.Data = new CharacterModel()
            {
                Id = "test",
                Level = 10
            };

            var control = (Picker)page.FindByName("CharacterLevelPicker");
            control.SelectedIndex = 10 - 1;

            // Act
            page.CharacterLevelPicker_Changed(null, null);
            var result = control.SelectedIndex;

            // Reset

            // Assert
            Assert.AreEqual(10, result + 1);
        }


        [Test]
        public void CharacterCreatePage_LevelPicker_SelectedIndex_Change_Should_Update_Picker_To_Level()
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