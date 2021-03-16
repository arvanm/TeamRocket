using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms.Mocks;
using Xamarin.Forms;

using Game;
using Game.Views;
using Game.Models;
using Game.ViewModels;
using Game.Helpers;

namespace UnitTests.Views
{
    [TestFixture]
    public class BattleSettingsPageTests
    {
        App app;
        BattleSettingsPage page;

       // public BattleSettingsPageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            // For now, set the engine to the Koenig Engine, change when ready 
            BattleEngineViewModel.Instance.SetBattleEngineToGame();

            page = new BattleSettingsPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void BattleSettingsPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BattleSettingsPage_CloseButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.CloseButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattleSettingsPage_AllowAmazonDelivery_Toggled_Default_Should_Pass()
        {
            // Arrange

            var control = (Switch)page.FindByName("AllowAmazonDeliverySwitch");
            var current = control.IsToggled;

            ToggledEventArgs args = new ToggledEventArgs(current);

            // Act
            page.AllowAmazonDelivery_Toggled(null, args);

            // Reset

            // Assert
            Assert.IsTrue(!current); // Got to here, so it happened...
        }
    }
}