using NUnit.Framework;

using Game.Models;
using System.Threading.Tasks;
using Game.ViewModels;
using Game.Helpers;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        #region TestSetup
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        [SetUp]
        public void Setup()
        {
            // Choose which engine to run
            EngineViewModel.SetBattleEngineToGame();

            // Put seed data into the system for all tests
            EngineViewModel.Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            EngineViewModel.Engine.StartBattle(false);

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss = false;
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Scenario0
        [Test]
        public void HakathonScenario_Scenario_0_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Scenario0

        #region Scenario1
        [Test]
        public async Task HackathonScenario_Scenario_1_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(3);

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        }
        #endregion Scenario1

        #region Scenario2
        [Test]
        public async Task HackathonScenario_Scenario_2_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      2
            *      
            * Description: 
            *      2.	Bob always Misses
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      Check for name bob
            * 
            * Test Algrorithm:
            *      Create Character named Bob
            *      Check for name Bob When attack is set to miss
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Bob never hits and never gains experiences
            * 
            * Validation:
            *      Total Experience Gain is zero
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;
            var Character = new CharacterModel
            {
                Speed = -1, // Will go last...
                Level = 1,
                CurrentHealth = 9,
                ExperienceTotal = 4,
                ExperienceRemaining = 5,
                Name = "Bob",
            };

            var CharacterPlayerBob = new PlayerInfoModel(Character);

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerBob);

            EngineViewModel.Engine.EngineSettings.PlayerList.Add(CharacterPlayerBob);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Bob")));
            Assert.AreEqual(0, EngineViewModel.Engine.EngineSettings.BattleScore.ScoreTotal);
        }
        #endregion Scenario2

        #region Scenario9
        [Test]
        public async Task HackathonScenario_Scenario_9_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      9
            *      
            * Description: 
            *      9.	Just in Time Delivery
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      add code to RoundEngine to add new items from Database
            * 
            * Test Algrorithm:
            *      Create Character named Bob
            *      Check for name Bob When attack is set to miss
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Bob never hits and never gains experiences
            * 
            * Validation:
            *      Total Experience Gain is zero
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;
            var Character = new CharacterModel
            {
                Speed = 10, 
                Level = 20,
                CurrentHealth = 1000,
                ExperienceTotal = 4,
                ExperienceRemaining = 5,
                Name = "Test",
            };

            var CharacterPlayerBob = new PlayerInfoModel(Character);

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerBob);

            EngineViewModel.Engine.EngineSettings.PlayerList.Add(CharacterPlayerBob);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
        }
        #endregion Scenario9
    }
}