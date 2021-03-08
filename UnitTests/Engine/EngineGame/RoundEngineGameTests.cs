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
    }
}