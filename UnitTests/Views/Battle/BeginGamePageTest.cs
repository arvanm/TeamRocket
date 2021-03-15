using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game;
using Game.Views;
using Xamarin.Forms.Mocks;
using Xamarin.Forms;

namespace UnitTests.Views
{
    [TestFixture]
    class BeginGamePageTest
    {
        App app;
        BeginGame page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new BeginGame();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void BeginGamePageTest_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BeginGamePageTest_StartGame_Clicked_Should_Pass()
        {
            // Get the current valute

            // Act
            page.StartGame_Clicked(null,null);

            // Reset

            // Assert
            Assert.IsTrue(true); 
        }
    }
}
