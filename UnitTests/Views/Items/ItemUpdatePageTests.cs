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
    public class ItemUpdatePageTests : ItemUpdatePage
    {
        App app;
        ItemUpdatePage page;

        public ItemUpdatePageTests() : base(true) { }
        
        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ItemUpdatePage(new GenericViewModel<ItemModel>(new ItemModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ItemUpdatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemUpdatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_CheckItemName_Default_Should_Pass()
        {
            // Arrange

            page.ViewModel.Data.Name = null;
            // Act
            _ = page.CheckItemName();
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void ItemUpdatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Value_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();
            var ViewModel = new GenericViewModel<ItemModel>(data);

            page = new ItemUpdatePage(ViewModel);
            double oldValue = 0.0;
            double newValue = 1.0;

            var args = new ValueChangedEventArgs(oldValue, newValue);

            // Act
            page.Value_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        
        [Test]
        public void ItemUpdatePage_Range_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();
            var ViewModel = new GenericViewModel<ItemModel>(data);

            page = new ItemUpdatePage(ViewModel);
            double oldRange = 0.0;
            double newRange = 1.0;

            var args = new ValueChangedEventArgs(oldRange, newRange);

            // Act
            page.Range_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Damage_OnStepperDamageChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();
            var ViewModel = new GenericViewModel<ItemModel>(data);

            page = new ItemUpdatePage(ViewModel);
            double oldDamage = 0.0;
            double newDamage = 1.0;

            var args = new ValueChangedEventArgs(oldDamage, newDamage);

            // Act
            page.Damage_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_LocationPicker_Changed_Null_Image_Should_Pass()
        {
            // Arrange


            // Act
            page.LocationPicker_Changed(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_LocationPicker_Changed_PrimaryHand_Should_Pass()
        {
            // Arrange
            var myPicker = (Picker)page.FindByName("LocationPicker");
            myPicker.SelectedItem = "Primary Hand";

            // Act
            page.LocationPicker_Changed(null, null);
            var myDamageStack = (StackLayout)page.FindByName("DamageStack");
            var myRangeStack = (StackLayout)page.FindByName("RangeStack");

            // Reset

            // Assert
            Assert.IsTrue(myDamageStack.IsVisible);
            Assert.IsTrue(myRangeStack.IsVisible);
            Assert.AreEqual(page.ViewModel.Data.Range, 1);
        }

        [Test]
        public void ItemUpdatePage_LocationPicker_Changed_Pokeball_Should_Pass()
        {
            // Arrange
            var myPicker = (Picker)page.FindByName("LocationPicker");
            myPicker.SelectedItem = "Pokeball";

            // Act
            page.LocationPicker_Changed(null, null);
            var myDamageStack = (StackLayout)page.FindByName("DamageStack");
            var myRangeStack = (StackLayout)page.FindByName("RangeStack");

            // Reset

            // Assert
            Assert.IsTrue(myDamageStack.IsVisible);
            Assert.IsFalse(myRangeStack.IsVisible);
            Assert.AreEqual(page.ViewModel.Data.Range, 0);
        }

        [Test]
        public void ItemUpdatePage_LocationPicker_Changed_Other_Location_Should_Pass()
        {
            // Arrange
            var myPicker = (Picker)page.FindByName("LocationPicker");
            myPicker.SelectedItem = "Head";

            // Act
            page.LocationPicker_Changed(null, null);
            var myDamageStack = (StackLayout)page.FindByName("DamageStack");
            var myRangeStack = (StackLayout)page.FindByName("RangeStack");

            // Reset

            // Assert
            Assert.IsFalse(myDamageStack.IsVisible);
            Assert.IsFalse(myRangeStack.IsVisible);
            Assert.AreEqual(page.ViewModel.Data.Range, 0);
        }
    }
}