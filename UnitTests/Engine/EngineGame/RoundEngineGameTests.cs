using System.Threading.Tasks;
using System.Linq;

using NUnit.Framework;

using Game.Models;
using Game.ViewModels;
using Game.Engine.EngineGame;

namespace UnitTests.Engine.EngineGame
{
    [TestFixture]
    public class RoundEngineGameTests
    {
        #region TestSetup
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();

            Engine.Round = new RoundEngine();
            Engine.Round.Turn = new TurnEngine();
            Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            //Engine.StartBattle(true);   
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Constructor
        [Test]
        public void RoundEngine_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Constructor

        #region RoundNextTurn
        [Test]
        public void RoundEngine_RoundNextTurn_Valid_No_Characters_Should_Return_GameOver()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
            };

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.RoundNextTurn();

            // Reset

            // Assert
            Assert.AreEqual(RoundEnum.GameOver, result);
        }

        [Test]
        public void RoundEngine_RoundNextTurn_Valid_No_Monsters_Should_Return_NewRound()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
            };

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            //Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.RoundNextTurn();

            // Reset

            // Assert
            Assert.AreEqual(RoundEnum.NewRound, result);
        }

        #endregion RoundNextTurn

        #region ItemDelivery
        [Test]
        public void RoundEngine_GetLocationItem_Head_Should_Return_Head()
        {
            // Arrange
            Game.Helpers.DataSetsHelper.WarmUp();
            var HeadItem = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.Head);
            var Character = new PlayerInfoModel(new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
                Head = HeadItem.Id
            });

            // Act
            var result = Engine.Round.GetLocationItem(Character, ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(HeadItem, result);
        }

        [Test]
        public void RoundEngine_GetLocationItem_Unknown_Should_Return_Null()
        {
            // Arrange
            Game.Helpers.DataSetsHelper.WarmUp();
            var Character = new PlayerInfoModel(new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
            });

            // Act
            var result = Engine.Round.GetLocationItem(Character, ItemLocationEnum.Unknown);

            // Reset

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void RoundEngine_GetDeliveryForNullItem_Has_Item_Should_Return_Null()
        {
            // Arrange
            Game.Helpers.DataSetsHelper.WarmUp();
            var Character = new PlayerInfoModel(new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
                Head = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.Head).Id
            });

            // Act
            var result = Engine.Round.GetDeliveryForNullItem(Character, ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task RoundEngine_GetDeliveryForNullItem_No_Head_Should_Return_Head()
        {
            // Arrange
            Game.Helpers.DataSetsHelper.WarmUp();
            var Character = new PlayerInfoModel(new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
            });

            // Act
            var result = Engine.Round.GetDeliveryForNullItem(Character, ItemLocationEnum.Head);

            // Reset
            await ItemIndexViewModel.Instance.DeleteAsync(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Location, ItemLocationEnum.Head);
        }

        [Test]
        public void RoundEngine_GetDeliveryForNullItem_Invalid_Location_Should_Null()
        {
            // Arrange
            Game.Helpers.DataSetsHelper.WarmUp();
            var Character = new PlayerInfoModel(new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
            });

            // Act
            var result = Engine.Round.GetDeliveryForNullItem(Character, ItemLocationEnum.Pokeball);

            // Reset

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task RoundEngine_GetDeliveryForBetterItem_Has_No_Item_Should_Return_Null()
        {
            // Arrange
            Game.Helpers.DataSetsHelper.WarmUp();
            var Character = new PlayerInfoModel(new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
            });

            // Act
            var result = Engine.Round.GetDeliveryForBetterItem(Character, ItemLocationEnum.Head);

            // Reset
            await ItemIndexViewModel.Instance.DeleteAsync(result);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task RoundEngine_GetDeliveryForBetterItem_Not_Perfect_Head_Should_Return_Better_Head()
        {
            // Arrange
            Game.Helpers.DataSetsHelper.WarmUp();
            var HeadItem = new ItemModel { Name = "TestHead", Value = 19, Attribute = AttributeEnum.Defense, Location = ItemLocationEnum.Head };
            await ItemIndexViewModel.Instance.CreateAsync(HeadItem);

            var Character = new PlayerInfoModel(new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
                Head = HeadItem.Id
            });

            // Act
            var result = Engine.Round.GetDeliveryForBetterItem(Character, ItemLocationEnum.Head);

            // Reset
            await ItemIndexViewModel.Instance.DeleteAsync(HeadItem);
            await ItemIndexViewModel.Instance.DeleteAsync(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Location, ItemLocationEnum.Head);
            Assert.AreEqual(result.Value, 20);
        }

        [Test]
        public async Task RoundEngine_GetDeliveryForBetterItem_Perfect_Head_Should_Return_Null()
        {
            // Arrange
            Game.Helpers.DataSetsHelper.WarmUp();
            var HeadItem = new ItemModel { Name = "TestHead", Value = 20, Attribute = AttributeEnum.Defense, Location = ItemLocationEnum.Head };
            await ItemIndexViewModel.Instance.CreateAsync(HeadItem);
            var Character = new PlayerInfoModel(new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
                Head = HeadItem.Id
            });

            // Act
            var result = Engine.Round.GetDeliveryForBetterItem(Character, ItemLocationEnum.Head);

            // Reset
            await ItemIndexViewModel.Instance.DeleteAsync(HeadItem);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void RoundEngine_GetDeliveryForBetterItem_Invalid_Location_Should_Null()
        {
            // Arrange
            Game.Helpers.DataSetsHelper.WarmUp();
            var Character = new PlayerInfoModel(new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
                Pokeball = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.Pokeball).Id
            });

            // Act
            var result = Engine.Round.GetDeliveryForBetterItem(Character, ItemLocationEnum.Pokeball);

            // Reset

            // Assert
            Assert.IsNull(result);
        }
        #endregion ItemDelivery

    }
}