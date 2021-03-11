using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Game.Engine.EngineBase;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.GameRules;
using Game.Models;
using Game.Services;
using Game.ViewModels;

namespace Game.Engine.EngineGame
{
    /// <summary>
    /// Manages the Rounds
    /// </summary>
    public class RoundEngine : RoundEngineBase, IRoundEngineInterface
    {
        // Hold the BaseEngine
        public new EngineSettingsModel EngineSettings = EngineSettingsModel.Instance;

        // The Turn Engine
        public new ITurnEngineInterface Turn
        {
            get
            {
                if (base.Turn == null)
                {
                    base.Turn = new TurnEngine();
                }
                return base.Turn;
            }
            set { base.Turn = Turn; }
        }

        /// <summary>
        /// Clear the List between Rounds
        /// </summary>
        public override bool ClearLists()
        {
            EngineSettings.ItemPool.Clear();
            EngineSettings.MonsterList.Clear();
            return true;
        }

        /// <summary>
        /// Call to make a new set of monsters..
        /// </summary>
        public override bool NewRound()
        {
            // End the existing round
            EndRound();

            // Remove Character Buffs
            RemoveCharacterBuffs();

            // Populate New Monsters..
            AddMonstersToRound();

            // Make the BaseEngine.PlayerList
            MakePlayerList();

            // Set Order for the Round
            OrderPlayerListByTurnOrder();

            // Populate BaseEngine.MapModel with Characters and Monsters
            EngineSettings.MapModel.PopulateMapModel(EngineSettings.PlayerList);

            // Update Score for the RoundCount
            EngineSettings.BattleScore.RoundCount++;

            return true;
        }

        /// <summary>
        /// Add Monsters to the Round
        /// 
        /// Because Monsters can be duplicated, will add 1, 2, 3 to their name
        ///   
        /*
            * Hint: 
            * I don't have crudi monsters yet so will add 6 new ones..
            * If you have crudi monsters, then pick from the list

            * Consdier how you will scale the monsters up to be appropriate for the characters to fight
            * 
            */
        /// </summary>
        /// <returns></returns>
        public override int AddMonstersToRound()
        {

            int TargetLevel = 1;

            if (EngineSettings.CharacterList.Count() > 0)
            {
                // Get the Avg Character Level (linq is soo cool....)
                TargetLevel = Convert.ToInt32(EngineSettings.CharacterList.Average(m => m.Level));
            }

            // get the same amount of monster as characters in game
            for (var i = 0; i < EngineSettings.CharacterList.Count(); i++)
            {
                var data = RandomPlayerHelper.GetRandomMonster(TargetLevel, EngineSettings.BattleSettingsModel.AllowMonsterItems);

                // Help identify which Monster it is
                data.Name += " " + EngineSettings.MonsterList.Count() + 1;

                EngineSettings.MonsterList.Add(new PlayerInfoModel(data));
            }

            return EngineSettings.MonsterList.Count();
        }


        public ItemModel GetNullOrBetterItem(PlayerInfoModel Character, ItemLocationEnum Location) 
        {
            // Get the current item on Character
            ItemModel CurrentItem = null;

            switch (Location)
            {
                case ItemLocationEnum.Feet:
                    CurrentItem = ItemIndexViewModel.Instance.GetItem(Character.Feet);
                    break;
                case ItemLocationEnum.Head:
                    CurrentItem = ItemIndexViewModel.Instance.GetItem(Character.Head);
                    break;
                case ItemLocationEnum.LeftFinger:
                    CurrentItem = ItemIndexViewModel.Instance.GetItem(Character.LeftFinger);
                    break;
                case ItemLocationEnum.RightFinger:
                    CurrentItem = ItemIndexViewModel.Instance.GetItem(Character.RightFinger);
                    break;
                case ItemLocationEnum.PrimaryHand:
                    CurrentItem = ItemIndexViewModel.Instance.GetItem(Character.PrimaryHand);
                    break;
                case ItemLocationEnum.OffHand:
                    CurrentItem = ItemIndexViewModel.Instance.GetItem(Character.OffHand);
                    break;
                case ItemLocationEnum.Necklass:
                    CurrentItem = ItemIndexViewModel.Instance.GetItem(Character.Necklass);
                    break;
                default:
                    break;
            }

            // Location adjust for Finger
            if (Location == ItemLocationEnum.LeftFinger || Location == ItemLocationEnum.RightFinger)
            { 
                Location = ItemLocationEnum.Finger;
            }

            // If null, Character not having it
            if (CurrentItem == null)
            {
                var task = ItemService.GetItemsFromServerPostAsync(1, 20, AttributeEnum.Unknown, Location, 0, true, true);
                task.Wait();
                // Does not return items from server
                if (task.Result == null || task.Result.Count() == 0)
                {
                    return null;
                }
                // Return the item
                return task.Result[0];
            }

            // Chararcter has it, need a better item
            else
            {
                // Best item, no need
                if (CurrentItem.Value == 20)
                {
                    return null;
                }

                // Get a better one by value + 1
                var task = ItemService.GetItemsFromServerPostAsync(1, CurrentItem.Value + 1, CurrentItem.Attribute, Location, 0, false, true);
                task.Wait();
                // Does not return items from server
                if (task.Result == null || task.Result.Count() == 0)
                {
                    return null;
                }
                // Return the item
                return task.Result[0];
            }
        }
        /// <summary>
        /// At the end of the round
        /// Clear the ItemModel List
        /// Clear the MonsterModel List
        /// </summary>
        public override bool EndRound()
        {
            // Add needed item for each character
            var LocationList = Enum.GetValues(typeof(ItemLocationEnum))
                                   .Cast<ItemLocationEnum>()
                                   .Where(a =>
                                        a.ToString() != ItemLocationEnum.Unknown.ToString() &&
                                        a.ToString() != ItemLocationEnum.Finger.ToString() &&
                                        a.ToString() != ItemLocationEnum.Pokeball.ToString()
                                     )
                                   .ToList();

            foreach (PlayerInfoModel Character in EngineSettings.CharacterList)
            {
                foreach (ItemLocationEnum Location in LocationList)
                {
                    ItemModel item = GetNullOrBetterItem(Character, Location);
                    if (item != null)
                    {                        EngineSettings.ItemPool.Add(item);
                        Debug.WriteLine("Amazon delivers {0} for {1}, {2}, {3} +{4}", item.Name, Character.Name, item.Location.ToString(), item.Value);
                        break;
                    }
                }
            }

            // In Auto Battle this happens and the characters get their items, In manual mode need to do it manualy
            if (EngineSettings.BattleScore.AutoBattle)
            {
                PickupItemsForAllCharacters();
            }

            // Reset Monster and Item Lists
            ClearLists();

            return true;
        }

        /// <summary>
        /// For each character pickup the items
        /// </summary>
        public override bool PickupItemsForAllCharacters()
        {
            // In Auto Battle this happens and the characters get their items
            // When called manualy, make sure to do the character pickup before calling EndRound

            // Have each character pickup items...
            foreach (var character in EngineSettings.CharacterList)
            {
                PickupItemsFromPool(character);
            }

            return true;
        }

        /// <summary>
        /// Manage Next Turn
        /// 
        /// Decides Who's Turn
        /// Remembers Current Player
        /// 
        /// Starts the Turn
        /// 
        /// </summary>
        public override RoundEnum RoundNextTurn()
        {
            // No characters, game is over...
            if (EngineSettings.CharacterList.Count < 1)
            {
                // Game Over
                EngineSettings.RoundStateEnum = RoundEnum.GameOver;
                return EngineSettings.RoundStateEnum;
            }

            // Check if round is over
            if (EngineSettings.MonsterList.Count < 1)
            {
                // If over, New Round
                EngineSettings.RoundStateEnum = RoundEnum.NewRound;
                return RoundEnum.NewRound;
            }

            if (EngineSettings.BattleScore.AutoBattle)
            {
                // Decide Who gets next turn
                // Remember who just went...
                EngineSettings.CurrentAttacker = GetNextPlayerTurn();

                // Only Attack for now
                EngineSettings.CurrentAction = ActionEnum.Attack;
            }

            // Do the turn....
            Turn.TakeTurn(EngineSettings.CurrentAttacker);

            EngineSettings.RoundStateEnum = RoundEnum.NextTurn;

            return EngineSettings.RoundStateEnum;
        }

        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        public override PlayerInfoModel GetNextPlayerTurn()
        {
            // Remove the Dead
            RemoveDeadPlayersFromList();

            // Get Next Player
            var PlayerCurrent = GetNextPlayerInList();

            return PlayerCurrent;
        }

        /// <summary>
        /// Remove Dead Players from the List
        /// </summary>
        public override List<PlayerInfoModel> RemoveDeadPlayersFromList()
        {
            return base.RemoveDeadPlayersFromList();
        }

        /// <summary>
        /// Order the Players in Turn Sequence
        /// </summary>
        public override List<PlayerInfoModel> OrderPlayerListByTurnOrder()
        {
            // Players are order by speed and name.
            EngineSettings.PlayerList = EngineSettings.PlayerList.OrderByDescending(a => a.Level)
                .ThenByDescending(a => a.GetSpeed())
                .ThenBy(a => a.Name)
                .ToList();

            return EngineSettings.PlayerList;
        }

        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public override List<PlayerInfoModel> MakePlayerList()
        {
            // Start from a clean list of players
            EngineSettings.PlayerList.Clear();

            // Remember the Insert order, used for Sorting
            var ListOrder = 0;

            foreach (var data in EngineSettings.CharacterList)
            {
                if (data.Alive)
                {
                    EngineSettings.PlayerList.Add(
                        new PlayerInfoModel(data)
                        {
                            // Remember the order
                            ListOrder = ListOrder
                        });

                    ListOrder++;
                }
            }

            foreach (var data in EngineSettings.MonsterList)
            {
                if (data.Alive)
                {
                    EngineSettings.PlayerList.Add(
                        new PlayerInfoModel(data)
                        {
                            // Remember the order
                            ListOrder = ListOrder
                        });

                    ListOrder++;
                }
            }

            return EngineSettings.PlayerList;
        }

        /// <summary>
        /// Get the Information about the Player
        /// </summary>
        public override PlayerInfoModel GetNextPlayerInList()
        {
            return base.GetNextPlayerInList();
        }

        /// <summary>
        /// Pickup Items Dropped
        /// </summary>
        public override bool PickupItemsFromPool(PlayerInfoModel character)
        {

            // TODO: Teams, You need to implement your own Logic if not using auto apply

            // I use the same logic for Auto Battle as I do for Manual Battle

            GetItemFromPoolIfBetter(character, ItemLocationEnum.Head);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Necklass);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.PrimaryHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.OffHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.RightFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.LeftFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Feet);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Pokeball);

            return true;
        }

        /// <summary>
        /// Swap out the item if it is better
        /// 
        /// Uses Value to determine
        /// </summary>
        public override bool GetItemFromPoolIfBetter(PlayerInfoModel character, ItemLocationEnum setLocation)
        {
            return base.GetItemFromPoolIfBetter(character, setLocation);
        }

        /// <summary>
        /// Swap the Item the character has for one from the pool
        /// 
        /// Drop the current item back into the Pool
        /// </summary>
        public override ItemModel SwapCharacterItem(PlayerInfoModel character, ItemLocationEnum setLocation, ItemModel PoolItem)
        {
            return base.SwapCharacterItem(character,setLocation,PoolItem);
        }

        /// <summary>
        /// For all characters in player list, remove their buffs
        /// </summary>
        public override bool RemoveCharacterBuffs()
        {
            return base.RemoveCharacterBuffs();
        }
    }
}