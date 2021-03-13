using NUnit.Framework;

using Game.Models;
using System.Threading.Tasks;
using Game.ViewModels;
using Game.Helpers;
using System.Linq;

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
            EngineViewModel.Engine.EngineSettings.CharacterList.Clear();
            EngineViewModel.Engine.EngineSettings.PlayerList.Clear();
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
            EngineViewModel.Engine.EngineSettings.CharacterList.Clear();
            EngineViewModel.Engine.EngineSettings.PlayerList.Clear();
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Bob")));
            Assert.AreEqual(0, EngineViewModel.Engine.EngineSettings.BattleScore.ScoreTotal);
        }
        #endregion Scenario2

        #region Scenario9
        [Test]
        public async Task HackathonScenario_Scenario_9_Valid_No_Head_Should_Added()
        {
            /* 
            * Scenario Number:  
            *      9
            *      
            * Description: 
            *      9.	Just in Time Delivery
            * 
            * Changes Required (Classes, Methods etc.) :
            *      Modifies EndRound() in RoundEngine.cs
            * 
            * Test Algrorithm:
            *      Add one character with 7 items, no head
            *      Run 1 round to make sure everything delivers
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      head item the test character have
            * 
            * Validation:
            *      should not be null
            */

            //Arrange

            // Max Character Size is 1
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;
            
            // Make sure all items (except Pokeball) are delivered
            EngineViewModel.Engine.EngineSettings.MaxRoundCount = 1; 

            // Monster always miss
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Miss;

            // Amazon delivers
            EngineViewModel.Engine.EngineSettings.AmazonDeliver = true;

            // Add Character
            var CharacterPlayer = new PlayerInfoModel(new CharacterModel
            {
                Speed = 10,
                Level = 20,
                CurrentHealth = 1000,
                ExperienceTotal = 4,
                ExperienceRemaining = 5,
                Name = "Test",
                Feet = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.Feet).Id,
                Necklass = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.Necklass).Id,
                PrimaryHand = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand).Id,
                OffHand = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.OffHand).Id,
                LeftFinger = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.Finger).Id,
                RightFinger = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.Finger).Id,
                Pokeball = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.Pokeball).Id
            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);
            EngineViewModel.Engine.EngineSettings.PlayerList.Add(CharacterPlayer);


            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();
            var NewHeadItem = EngineViewModel.Engine.EngineSettings.PlayerList.Where(m => m.Name == "Test").FirstOrDefault().DropAllItems().Where(m => m.Location == ItemLocationEnum.Head).FirstOrDefault();

            //Reset
            EngineViewModel.Engine.EngineSettings.CharacterList.Clear();
            EngineViewModel.Engine.EngineSettings.PlayerList.Clear();
            EngineViewModel.Engine.EngineSettings.MaxRoundCount = 100;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.AmazonDeliver = false;
            await ItemIndexViewModel.Instance.DeleteAsync(NewHeadItem);

            //Assert
            Assert.NotNull(NewHeadItem);
        }

        [Test]
        public async Task HackathonScenario_Scenario_9_Valid_Bad_Primary_Hand_Should_Improved()
        {
            /* 
            * Scenario Number:  
            *      9
            *      
            * Description: 
            *      9.	Just in Time Delivery
            * 
            * Changes Required (Classes, Methods etc.) :
            *      Modifies EndRound() in RoundEngine.cs
            * 
            * Test Algrorithm:
            *      Add one character with 8 items, all with value 20 except for Primary hand, which is 19
            *      Run 1 round to make sure better primary hand delivers
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Value of Primary Hand Item the test character 
            * 
            * Validation:
            *      Test character should have primary hand with value 20
            */

            //Arrange

            // Max Character Size is 1
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            // Make sure all items (except Pokeball) are delivered
            EngineViewModel.Engine.EngineSettings.MaxRoundCount = 1;

            // Monster always miss
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Miss;

            // Amazon delivers
            EngineViewModel.Engine.EngineSettings.AmazonDeliver = true;

            // Add Items
            var FeetItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.Feet, Attribute = AttributeEnum.Attack, Value = 20 };
            var NeckItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.Necklass, Attribute = AttributeEnum.Attack, Value = 20 };
            var PHandItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.PrimaryHand, Attribute = AttributeEnum.Attack, Value = 19 };
            var OHandItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.OffHand, Attribute = AttributeEnum.Attack, Value = 20 };
            var LFingerItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.Finger, Attribute = AttributeEnum.Attack, Value = 20 };
            var RFingerItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.Finger, Attribute = AttributeEnum.Attack, Value = 20 };
            var PokeballItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.Pokeball, Attribute = AttributeEnum.Attack, Value = 20 };
            var HeadItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.Head, Attribute = AttributeEnum.Attack, Value = 20 };
            await ItemIndexViewModel.Instance.CreateAsync(FeetItem);
            await ItemIndexViewModel.Instance.CreateAsync(NeckItem);
            await ItemIndexViewModel.Instance.CreateAsync(PHandItem);
            await ItemIndexViewModel.Instance.CreateAsync(OHandItem);
            await ItemIndexViewModel.Instance.CreateAsync(LFingerItem);
            await ItemIndexViewModel.Instance.CreateAsync(RFingerItem);
            await ItemIndexViewModel.Instance.CreateAsync(PokeballItem);
            await ItemIndexViewModel.Instance.CreateAsync(HeadItem);
            ItemIndexViewModel.Instance.SetNeedsRefresh(true);

            // Add Character
            var CharacterPlayer = new PlayerInfoModel(new CharacterModel
            {
                Speed = 10,
                Level = 20,
                CurrentHealth = 1000,
                ExperienceTotal = 4,
                ExperienceRemaining = 5,
                Name = "Test",
                Feet = FeetItem.Id,
                Necklass = NeckItem.Id,
                PrimaryHand = PHandItem.Id,
                OffHand = OHandItem.Id,
                LeftFinger = LFingerItem.Id,
                RightFinger = RFingerItem.Id,
                Pokeball = PokeballItem.Id,
                Head = HeadItem.Id
            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);
            EngineViewModel.Engine.EngineSettings.PlayerList.Add(CharacterPlayer);

            //Act
            await EngineViewModel.AutoBattleEngine.RunAutoBattle();
            var NewPrimaryHandItem = EngineViewModel.Engine.EngineSettings.PlayerList.Where(m => m.Name == "Test").FirstOrDefault().DropAllItems().Where(m => m.Location == ItemLocationEnum.PrimaryHand).FirstOrDefault();

            //Reset
            EngineViewModel.Engine.EngineSettings.CharacterList.Clear();
            EngineViewModel.Engine.EngineSettings.PlayerList.Clear();
            EngineViewModel.Engine.EngineSettings.MaxRoundCount = 100;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.AmazonDeliver = false;
            await ItemIndexViewModel.Instance.DeleteAsync(FeetItem);
            await ItemIndexViewModel.Instance.DeleteAsync(NeckItem);
            await ItemIndexViewModel.Instance.DeleteAsync(PHandItem);
            await ItemIndexViewModel.Instance.DeleteAsync(OHandItem);
            await ItemIndexViewModel.Instance.DeleteAsync(LFingerItem);
            await ItemIndexViewModel.Instance.DeleteAsync(RFingerItem);
            await ItemIndexViewModel.Instance.DeleteAsync(PokeballItem);
            await ItemIndexViewModel.Instance.DeleteAsync(HeadItem);
            await ItemIndexViewModel.Instance.DeleteAsync(NewPrimaryHandItem);

            //Assert
            Assert.AreEqual(NewPrimaryHandItem.Value, 20);
        }

        [Test]
        public async Task HackathonScenario_Scenario_9_Valid_Perfect_Items_Should_Not_Deliver()
        {
            /* 
            * Scenario Number:  
            *      9
            *      
            * Description: 
            *      9.	Just in Time Delivery
            * 
            * Changes Required (Classes, Methods etc.) :
            *      Modifies EndRound() in RoundEngine.cs
            * 
            * Test Algrorithm:
            *      Add one character with 8 items, all with value 20
            *      Run 1 round to try to have deliver
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Number of items in ItemsViewModel
            * 
            * Validation:
            *      Number of items in ItemsViewModel should not change
            */

            //Arrange

            // Max Character Size is 1
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            // Make sure all items (except Pokeball) are delivered
            EngineViewModel.Engine.EngineSettings.MaxRoundCount = 1;

            // Monster always miss
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Miss;

            // Amazon delivers
            EngineViewModel.Engine.EngineSettings.AmazonDeliver = true;

            // Add Items
            var FeetItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.Feet, Attribute = AttributeEnum.Attack, Value = 20 };
            var NeckItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.Necklass, Attribute = AttributeEnum.Attack, Value = 20 };
            var PHandItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.PrimaryHand, Attribute = AttributeEnum.Attack, Value = 20 };
            var OHandItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.OffHand, Attribute = AttributeEnum.Attack, Value = 20 };
            var LFingerItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.Finger, Attribute = AttributeEnum.Attack, Value = 20 };
            var RFingerItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.Finger, Attribute = AttributeEnum.Attack, Value = 20 };
            var PokeballItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.Pokeball, Attribute = AttributeEnum.Attack, Value = 20 };
            var HeadItem = new ItemModel { Name = "Test", Location = ItemLocationEnum.Head, Attribute = AttributeEnum.Attack, Value = 20 };
            await ItemIndexViewModel.Instance.CreateAsync(FeetItem);
            await ItemIndexViewModel.Instance.CreateAsync(NeckItem);
            await ItemIndexViewModel.Instance.CreateAsync(PHandItem);
            await ItemIndexViewModel.Instance.CreateAsync(OHandItem);
            await ItemIndexViewModel.Instance.CreateAsync(LFingerItem);
            await ItemIndexViewModel.Instance.CreateAsync(RFingerItem);
            await ItemIndexViewModel.Instance.CreateAsync(PokeballItem);
            await ItemIndexViewModel.Instance.CreateAsync(HeadItem);
            ItemIndexViewModel.Instance.SetNeedsRefresh(true);

            // Add Character
            var CharacterPlayer = new PlayerInfoModel(new CharacterModel
            {
                Speed = 10,
                Level = 20,
                CurrentHealth = 1000,
                ExperienceTotal = 4,
                ExperienceRemaining = 5,
                Name = "Test",
                Feet = FeetItem.Id,
                Necklass = NeckItem.Id,
                PrimaryHand = PHandItem.Id,
                OffHand = OHandItem.Id,
                LeftFinger = LFingerItem.Id,
                RightFinger = RFingerItem.Id,
                Pokeball = PokeballItem.Id,
                Head = HeadItem.Id
            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);
            EngineViewModel.Engine.EngineSettings.PlayerList.Add(CharacterPlayer);

            // Dice for monster drop, set to 0 so monster does not drop items
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(1);

            //Act
            var OldItemsCount = ItemIndexViewModel.Instance.Dataset.Count();
            await EngineViewModel.AutoBattleEngine.RunAutoBattle();
            var NewItemsCount = ItemIndexViewModel.Instance.Dataset.Count();

            //Reset
            EngineViewModel.Engine.EngineSettings.CharacterList.Clear();
            EngineViewModel.Engine.EngineSettings.PlayerList.Clear();
            EngineViewModel.Engine.EngineSettings.MaxRoundCount = 100;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.AmazonDeliver = false;
            DiceHelper.DisableForcedRolls();
            await ItemIndexViewModel.Instance.DeleteAsync(FeetItem);
            await ItemIndexViewModel.Instance.DeleteAsync(NeckItem);
            await ItemIndexViewModel.Instance.DeleteAsync(PHandItem);
            await ItemIndexViewModel.Instance.DeleteAsync(OHandItem);
            await ItemIndexViewModel.Instance.DeleteAsync(LFingerItem);
            await ItemIndexViewModel.Instance.DeleteAsync(RFingerItem);
            await ItemIndexViewModel.Instance.DeleteAsync(PokeballItem);
            await ItemIndexViewModel.Instance.DeleteAsync(HeadItem);

            //Assert
            Assert.AreEqual(NewItemsCount, OldItemsCount);
        }
        #endregion Scenario9
    }
}