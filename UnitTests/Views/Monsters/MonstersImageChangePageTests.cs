using NUnit.Framework;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Views
{
    [TestFixture]
    public class MonsterImageChangePageTests : MonsterImageChangePage
    {
        App app;
        MonsterImageChangePage page;

        public MonsterImageChangePageTests() : base(true) { }
        
        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new MonsterImageChangePage(new GenericViewModel<MonsterModel>(new MonsterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void MonsterImageChangePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MonsterImageChangePage_SelectMonsterImage_Clicked_Default_Should_Pass()
        {
            // Arrange
            var data = new MonsterModel();
            MonsterIndexViewModel ViewModel = MonsterIndexViewModel.Instance;
            ViewModel.Dataset.Add(data);
            data.ImageURI = "moltres.png";
            ImageButton button = new ImageButton();
            button.CommandParameter = data;
            // Act
            page.SelectMonsterImage_Clicked(button, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterImageChangePage_SelectMonsterImage_Clicked_View_Null_Should_Pass()
        {
            // Arrange
            var data = new MonsterModel();
            MonsterIndexViewModel ViewModel = MonsterIndexViewModel.Instance;
            ViewModel.Dataset.Clear();
            data.ImageURI = "moltres.png";
            ImageButton button = new ImageButton();
            button.CommandParameter = data;
            // Act
            page.SelectMonsterImage_Clicked(button, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterImageChangePage_SelectMonsterImage_Clicked_Null_Should_Pass()
        {
            // Arrange

            // Act
            page.SelectMonsterImage_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //[Test]
        //public void MonsterImageChangePage_Delete_Clicked_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.Delete_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void MonsterImageChangePage_OnBackButtonPressed_Valid_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    OnBackButtonPressed();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

    }
}