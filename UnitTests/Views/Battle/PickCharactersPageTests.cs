using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms.Mocks;
using Xamarin.Forms;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

namespace UnitTests.Views
{
    [TestFixture]
    public class PickCharactersPageTests : PickCharactersPage
    {
        App app;
        PickCharactersPage page;

        public PickCharactersPageTests() : base(true) { }

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

            page = new PickCharactersPage();

        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void PickCharactersPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        
        [Test]
        public void PickCharactersPage_Constructor_UT_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new PickCharactersPage(false);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PickCharactersPage_BattleButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.BattleButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickCharactersPage_CreateEngineCharacterList_Default_Should_Pass()
        {
            // Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList = new List<PlayerInfoModel>();
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(new CharacterModel()));

            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList = new List<PlayerInfoModel>();
            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(new MonsterModel()));

            BattleEngineViewModel.Instance.PartyCharacterList = new ObservableCollection<CharacterModel>();
            BattleEngineViewModel.Instance.PartyCharacterList.Add(new CharacterModel());

            // Act
            page.CreateEngineCharacterList();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //[Test]
        //public void PickCharactersPage_OnApperaing_Default_Should_Pass()
        //{
        //    // Arrange
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList = new List<PlayerInfoModel>();
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(new CharacterModel()));

        //    BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList = new List<PlayerInfoModel>();
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(new MonsterModel()));

        //    BattleEngineViewModel.Instance.PartyCharacterList = new ObservableCollection<CharacterModel>();
        //    BattleEngineViewModel.Instance.PartyCharacterList.Add(new CharacterModel());

        //    var temp = page.EngineViewModel.Engine;

        //    page.UpdateNextButtonState();

        //    // Act
        //    OnAppearing();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        [Test]
        public void PickCharactersPage_UpdateButtonState_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.UpdateNextButtonState();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickCharactersPage_CharacterSelected_Add_To_Empty_Should_Add()
        {
            // Arrange
            BattleEngineViewModel.Instance.PartyCharacterList.Clear();
            var Button = new ImageButton();
            Button.CommandParameter = CharacterIndexViewModel.Instance.Dataset.FirstOrDefault().Id;

            // Act
            page.CharacterSelected(Button, null);

            // Reset

            // Assert
            Assert.AreEqual(1, BattleEngineViewModel.Instance.PartyCharacterList.Count());
        }

        [Test]
        public void PickCharactersPage_CharacterSelected_Add_To_Full_Should_Not_Add()
        {
            // Arrange
            BattleEngineViewModel.Instance.PartyCharacterList.Clear();
            BattleEngineViewModel.Instance.PartyCharacterList.Add(new CharacterModel());
            BattleEngineViewModel.Instance.PartyCharacterList.Add(new CharacterModel());
            BattleEngineViewModel.Instance.PartyCharacterList.Add(new CharacterModel());
            BattleEngineViewModel.Instance.PartyCharacterList.Add(new CharacterModel());
            BattleEngineViewModel.Instance.PartyCharacterList.Add(new CharacterModel());
            BattleEngineViewModel.Instance.PartyCharacterList.Add(new CharacterModel());

            var Button = new ImageButton();
            Button.CommandParameter = CharacterIndexViewModel.Instance.Dataset.FirstOrDefault().Id;

            // Act
            page.CharacterSelected(Button, null);

            // Reset

            // Assert
            Assert.AreEqual(6, BattleEngineViewModel.Instance.PartyCharacterList.Count());
        }

        [Test]
        public void PickCharactersPage_CharacterSelected_Add_To_Existed_Should_Not_Add()
        {
            // Arrange
            BattleEngineViewModel.Instance.PartyCharacterList.Clear();
            BattleEngineViewModel.Instance.PartyCharacterList.Add(CharacterIndexViewModel.Instance.Dataset.FirstOrDefault());

            var Button = new ImageButton();
            Button.CommandParameter = CharacterIndexViewModel.Instance.Dataset.FirstOrDefault().Id;

            // Act
            page.CharacterSelected(Button, null);

            // Reset

            // Assert
            Assert.AreEqual(1, BattleEngineViewModel.Instance.PartyCharacterList.Count());
        }

        [Test]
        public void PickCharactersPage_CharacterSelected_Add_Null_Should_Not_Add()
        {
            // Arrange
            BattleEngineViewModel.Instance.PartyCharacterList.Clear();

            var Button = new ImageButton();
            Button.CommandParameter = "whatever";

            // Act
            page.CharacterSelected(Button, null);

            // Reset

            // Assert
            Assert.AreEqual(0, BattleEngineViewModel.Instance.PartyCharacterList.Count());
        }

        [Test]
        public void PickCharactersPage_CharacterDeselected_Remove_Null_Should_Not_Remove()
        {
            // Arrange
            BattleEngineViewModel.Instance.PartyCharacterList.Clear();
            BattleEngineViewModel.Instance.PartyCharacterList.Add(CharacterIndexViewModel.Instance.Dataset.FirstOrDefault());

            var Button = new ImageButton();
            Button.CommandParameter = "whatever";

            // Act
            page.CharacterDeselected(Button, null);

            // Reset

            // Assert
            Assert.AreEqual(1, BattleEngineViewModel.Instance.PartyCharacterList.Count());
        }
        [Test]
        public void PickCharactersPage_CharacterDeselected_Remove_Valid_Should_Remove()
        {
            // Arrange
            BattleEngineViewModel.Instance.PartyCharacterList.Clear();
            BattleEngineViewModel.Instance.PartyCharacterList.Add(CharacterIndexViewModel.Instance.Dataset.FirstOrDefault());

            var Button = new ImageButton();
            Button.CommandParameter = CharacterIndexViewModel.Instance.Dataset.FirstOrDefault().Id;

            // Act
            page.CharacterDeselected(Button, null);

            // Reset

            // Assert
            Assert.AreEqual(0, BattleEngineViewModel.Instance.PartyCharacterList.Count());
        }

        //[Test]
        //public void PickCharactersPage_OnPartyCharacterItemSelected_Default_Should_Pass()
        //{
        //    // Arrange

        //    var selectedCharacter = new CharacterModel();

        //    var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(selectedCharacter, 0);

        //    // Act
        //    page.OnPartyCharacterItemSelected(null, selectedCharacterChangedEventArgs);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void PickCharactersPage_OnPartyCharacterItemSelected_InValid_Should_Pass()
        //{
        //    // Arrange

        //    var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);

        //    // Act
        //    page.OnPartyCharacterItemSelected(null, selectedCharacterChangedEventArgs);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void PickCharactersPage_OnDatabaseCharacterItemSelected_Default_Should_Pass()
        //{
        //    // Arrange

        //    var selectedCharacter = new CharacterModel();

        //    var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(selectedCharacter, 0);

        //    // Act
        //    page.OnDatabaseCharacterItemSelected(null, selectedCharacterChangedEventArgs);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void PickCharactersPage_OnDatabaseCharacterItemSelected_InValid_Should_Pass()
        //{
        //    // Arrange

        //    var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);

        //    // Act
        //    page.OnDatabaseCharacterItemSelected(null, selectedCharacterChangedEventArgs);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}
    }
}