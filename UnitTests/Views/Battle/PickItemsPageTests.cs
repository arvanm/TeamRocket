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
using Game.ViewModels;
using Game.Models;

namespace UnitTests.Views
{
    [TestFixture]
    public class PickItemsPageTests
    {
        App app;
        PickItemsPage page;

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

            page = new PickItemsPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void PickItemsPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PickItemsPage_CloseButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.CloseButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_GetItemToDisplay_Default_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel
            {
                Name = "PrimaryHand01",
                Description = "May the force be with you!",
                ImageURI = "item_sword.png",
                Range = 5,
                Damage = 10,
                Value = 9,
                Location = ItemLocationEnum.PrimaryHand,
                Attribute = AttributeEnum.Attack
            };


            // Act
            page.GetItemToDisplay(item);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_GetItemToDisplay_Null_Should_Pass()
        {
            // Arrange          

            // Act
            page.GetItemToDisplay(null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_CreatePlayerDisplayBox_Null_Should_Pass()
        {
            // Arrange          

            // Act
            StackLayout result = page.CreatePlayerDisplayBox(null);

            // Reset

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public void PickItemsPage_CreatePlayerDisplayBox_Default_Should_Pass()
        {
            // Arrange          

            // Act
            StackLayout result = page.CreatePlayerDisplayBox(new PlayerInfoModel(CharacterIndexViewModel.Instance.Dataset.FirstOrDefault()));

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void PickItemsPage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel
            {
                Name = "PrimaryHand01",
                Description = "May the force be with you!",
                ImageURI = "item_sword.png",
                Range = 5,
                Damage = 10,
                Value = 9,
                Location = ItemLocationEnum.PrimaryHand,
                Attribute = AttributeEnum.Attack
            };

            var StackItem = page.GetItemToDisplay(item);
            var dataImage = StackItem.Children[0];

            // Act
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_GetItemToDisplay_Click_Button_Null_Should_Pass()
        {
            // Arrange
            var StackItem = page.GetItemToDisplay(null);

            // Act

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
            Assert.AreEqual(StackItem.Children.Count(), 0); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_GetItemToDisplay_Click_Button_Unknown_Should_Pass()
        {
            // Arrange
            var StackItem = page.GetItemToDisplay(new ItemModel());
            ImageButton dataImage = (ImageButton) StackItem.Children[0];

            // Act

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_SaveButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel
            {
                Name = "PrimaryHand01",
                Description = "May the force be with you!",
                ImageURI = "item_sword.png",
                Range = 5,
                Damage = 10,
                Value = 9,
                Location = ItemLocationEnum.PrimaryHand,
                Attribute = AttributeEnum.Attack
            };

            var CharacterPlayer = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = 100,
                    Level = 10,
                    CurrentHealth = 11,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Mike",
                    ListOrder = 1,
                });

            page.SelectedItem = item;
            page.SelectedCharacter = CharacterPlayer;

            // Act
            page.SaveButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_UpdateAttributeValues_Head_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel
            {
                Name = "Head01",
                Description = "May the force be with you!",
                ImageURI = "item_sword.png",
                Range = 5,
                Damage = 10,
                Value = 9,
                Location = ItemLocationEnum.Head,
                Attribute = AttributeEnum.Attack
            };

            var CharacterPlayer = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = 100,
                    Level = 10,
                    CurrentHealth = 11,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Mike",
                    ListOrder = 1,
                });

            page.SelectedItem = item;
            page.SelectedCharacter = CharacterPlayer;

            // Act
            page.UpdateAttributeValues();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_UpdateAttributeValues_Feet_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel
            {
                Name = "Feet01",
                Description = "May the force be with you!",
                ImageURI = "item_sword.png",
                Range = 5,
                Damage = 10,
                Value = 9,
                Location = ItemLocationEnum.Feet,
                Attribute = AttributeEnum.Attack
            };

            var CharacterPlayer = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = 100,
                    Level = 10,
                    CurrentHealth = 11,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Mike",
                    ListOrder = 1,
                });

            page.SelectedItem = item;
            page.SelectedCharacter = CharacterPlayer;

            // Act
            page.UpdateAttributeValues();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_UpdateAttributeValues_Finger_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel
            {
                Name = "Finger01",
                Description = "May the force be with you!",
                ImageURI = "item_sword.png",
                Range = 5,
                Damage = 10,
                Value = 9,
                Location = ItemLocationEnum.Finger,
                Attribute = AttributeEnum.Attack
            };

            var CharacterPlayer = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = 100,
                    Level = 10,
                    CurrentHealth = 11,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Mike",
                    ListOrder = 1,
                });

            page.SelectedItem = item;
            page.SelectedCharacter = CharacterPlayer;

            // Act
            page.UpdateAttributeValues();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_UpdateAttributeValues_LeftFinger_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel
            {
                Name = "LeftFinger01",
                Description = "May the force be with you!",
                ImageURI = "item_sword.png",
                Range = 5,
                Damage = 10,
                Value = 9,
                Location = ItemLocationEnum.LeftFinger,
                Attribute = AttributeEnum.Attack
            };

            var CharacterPlayer = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = 100,
                    Level = 10,
                    CurrentHealth = 11,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Mike",
                    ListOrder = 1,
                });

            page.SelectedItem = item;
            page.SelectedCharacter = CharacterPlayer;

            // Act
            page.UpdateAttributeValues();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_UpdateAttributeValues_RightFinger_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel
            {
                Name = "RightFinger01",
                Description = "May the force be with you!",
                ImageURI = "item_sword.png",
                Range = 5,
                Damage = 10,
                Value = 9,
                Location = ItemLocationEnum.RightFinger,
                Attribute = AttributeEnum.Attack
            };

            var CharacterPlayer = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = 100,
                    Level = 10,
                    CurrentHealth = 11,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Mike",
                    ListOrder = 1,
                });

            page.SelectedItem = item;
            page.SelectedCharacter = CharacterPlayer;

            // Act
            page.UpdateAttributeValues();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_UpdateAttributeValues_Necklass_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel
            {
                Name = "Necklass01",
                Description = "May the force be with you!",
                ImageURI = "item_sword.png",
                Range = 5,
                Damage = 10,
                Value = 9,
                Location = ItemLocationEnum.Necklass,
                Attribute = AttributeEnum.Attack
            };

            var CharacterPlayer = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = 100,
                    Level = 10,
                    CurrentHealth = 11,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Mike",
                    ListOrder = 1,
                });

            page.SelectedItem = item;
            page.SelectedCharacter = CharacterPlayer;

            // Act
            page.UpdateAttributeValues();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_UpdateAttributeValues_OffHand_Should_Pass()
        {
            // Arrange
            ItemModel item = new ItemModel
            {
                Name = "OffHand01",
                Description = "May the force be with you!",
                ImageURI = "item_sword.png",
                Range = 5,
                Damage = 10,
                Value = 9,
                Location = ItemLocationEnum.OffHand,
                Attribute = AttributeEnum.Attack
            };

            var CharacterPlayer = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = 100,
                    Level = 10,
                    CurrentHealth = 11,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Mike",
                    ListOrder = 1,
                });

            page.SelectedItem = item;
            page.SelectedCharacter = CharacterPlayer;

            // Act
            page.UpdateAttributeValues();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void PickItemsPage_DrawCharacterList_Default_Should_Pass()
        {
            // Arrange
            FlexLayout CharacterListFrame = (FlexLayout)page.FindByName("CharacterListFrame");
            CharacterListFrame.Children.Add(new StackLayout());

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(new CharacterModel()));

            // Act
            page.DrawCharacterList();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_DrawDroppedItems_Default_Should_Pass()
        {
            // Arrange
            FlexLayout ItemListFoundFrame = (FlexLayout)page.FindByName("ItemListFoundFrame");
            ItemListFoundFrame.Children.Add(new StackLayout());

            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(new ItemModel());

            // Act
            page.DrawDroppedItems();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_UpdateDrawDroppedItems_Default_Should_Pass()
        {
            // Arrange
            FlexLayout ItemListFoundFrame = (FlexLayout)page.FindByName("ItemListFoundFrame");
            ItemListFoundFrame.Children.Add(new StackLayout());

            page.AvailableItem.Add(new ItemModel());

            // Act
            page.UpdateDrawDroppedItems();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}