using System.Collections.Generic;

using Game.Models;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.Engine.EngineBase;
using System.Diagnostics;
using System.Linq;
using Game.Helpers;
using Game.ViewModels;
using Game.GameRules;
using System;
using Game.Services;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Game.Engine.EngineGame
{
    /* 
     * Need to decide who takes the next turn
     * Target to Attack
     * Should Move, or Stay put (can hit with weapon range?)
     * Death
     * Manage Round...
     * 
     */

    /// <summary>
    /// Engine controls the turns
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// 
    /// </summary>
    public class TurnEngine : TurnEngineBase, ITurnEngineInterface
    {
        #region Algrorithm
        // Capture or Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over
        #endregion Algrorithm

        // Hold the BaseEngine
        public new EngineSettingsModel EngineSettings = EngineSettingsModel.Instance;

        /// <summary>
        /// Update the Character Pokedex data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCharacterPokedexAsync(PlayerInfoModel data)
        {
            if (data == null)
            {
                return false;
            }

            if (data.PlayerType == PlayerTypeEnum.Character)
            {
                // Get the character from list
                var Character = CharacterIndexViewModel.Instance.Dataset.Where(m => m.Name.Equals(data.Name)).FirstOrDefault();

                // Set pokedex of the character
                Character.Pokedex = data.Pokedex;

                // Save the change to the Data Store
                var result = await CharacterIndexViewModel.Instance.UpdateAsync(Character);

                return result;
            }

            // Not Character
            return false;
            
        }

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool TakeTurn(PlayerInfoModel Attacker)
        {
            // Choose Action.  Such as Move, Attack etc.

            // INFO: Teams, if you have other actions they would go here.

            bool result = false;

            // Set the action if one is not set
            EngineSettings.CurrentAction = DetermineActionChoice(Attacker);

            // When in doubt, attack...
            // We do not use ability here, so if ability, also attack
            if (EngineSettings.CurrentAction == ActionEnum.Unknown || EngineSettings.CurrentAction == ActionEnum.Ability)
            {
                EngineSettings.CurrentAction = ActionEnum.Attack;
            }

            switch (EngineSettings.CurrentAction)
            {
                case ActionEnum.Attack:
                    result = Attack(Attacker);
                    break;

                case ActionEnum.Move:
                    result = MoveAsTurn(Attacker);
                    break;

                case ActionEnum.Capture:
                    result = Capture(Attacker);
                    break;
            }

            EngineSettings.BattleScore.TurnCount++;

            // Save the Previous Action off
            EngineSettings.PreviousAction = EngineSettings.CurrentAction;

            // Reset the Action to unknown for next time
            EngineSettings.CurrentAction = ActionEnum.Unknown;



            return result;
        }

        /// <summary>
        /// Determine what Actions to do
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override ActionEnum DetermineActionChoice(PlayerInfoModel Attacker)
        {
            // If it is the characters turn, and NOT auto battle, use what was sent into the engine
            if (Attacker.PlayerType == PlayerTypeEnum.Character)
            {
                if (EngineSettings.BattleScore.AutoBattle == false)
                {
                    return EngineSettings.CurrentAction;
                }
            }

            /*
             * The following is Used for Monsters, and Auto Battle Characters
             * 
             * Order of Priority
             * If within Range
             * If can capture Then capture
             * Next Attack
             * If not within Range Then Move
             */

            // Assume Move if nothing else happens
            EngineSettings.CurrentAction = ActionEnum.Move;

            // See if Desired Target is within Range, and if so capture or attack 
            if (EngineSettings.MapModel.IsTargetInRange(Attacker, AttackChoice(Attacker)))
            {
                // Check to see if capture is available
                if (ChooseToUseCapture(Attacker, AttackChoice(Attacker)))
                {
                    EngineSettings.CurrentAction = ActionEnum.Capture;
                    return EngineSettings.CurrentAction;
                }

                // Otherwise Attack
                EngineSettings.CurrentAction = ActionEnum.Attack;
            }

            return EngineSettings.CurrentAction;
        }

        /// <summary>
        /// Find a Desired Target
        /// Move close to them
        /// Get to move the number of Speed
        /// </summary>
        public override bool MoveAsTurn(PlayerInfoModel Attacker)
        {

            /*
             * Both monster and characters can move 
             */

            // For Attack, Choose Who
            EngineSettings.CurrentDefender = AttackChoice(Attacker);

            if (EngineSettings.CurrentDefender == null)
            {
                return false;
            }

            // Get X, Y for Defender
            var locationDefender = EngineSettings.MapModel.GetLocationForPlayer(EngineSettings.CurrentDefender);
            if (locationDefender == null)
            {
                return false;
            }

            var locationAttacker = EngineSettings.MapModel.GetLocationForPlayer(Attacker);
            if (locationAttacker == null)
            {
                return false;
            }

            // Find Location Nearest to Defender that is Open.

            // Get the Open Locations
            var openSquare = EngineSettings.MapModel.ReturnClosestEmptyLocation(locationDefender);

            Debug.WriteLine(string.Format("{0} moves from {1},{2} to {3},{4}", locationAttacker.Player.Name, locationAttacker.Column, locationAttacker.Row, openSquare.Column, openSquare.Row));

            EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + " moves closer to " + EngineSettings.CurrentDefender.Name;

            return EngineSettings.MapModel.MovePlayerOnMap(locationAttacker, openSquare);

        }

        /// <summary>
        /// Decide to use an Ability or not
        /// No ability in our design, Don't try
        /// 
        /// </summary>
        public override bool ChooseToUseAbility(PlayerInfoModel Attacker)
        {
            // Don't try
            return false;
        }

        /// <summary>
        /// Get Pokemon's name from Monster
        /// </summary>
        /// <param name="Pokemon"></param>
        /// <returns></returns>
        public string GetPokemonName(PlayerInfoModel Pokemon)
        {
            if (Pokemon.PlayerType != PlayerTypeEnum.Monster)
            {
                return "";
            }

            // Get Pokemon's actual name (remove the number at the end)
            var PokemonNameArr = Pokemon.Name.Trim().Split(' ');
            PokemonNameArr = PokemonNameArr.Where((o, i) => i != PokemonNameArr.Length - 1).ToArray();
            var PokemonName = string.Join(" ", PokemonNameArr);
            return PokemonName;
        }

        /// <summary>
        /// Decide to use Capture or not
        /// 
        /// </summary>
        public bool ChooseToUseCapture(PlayerInfoModel Attacker, PlayerInfoModel Defender)
        {
            // Only character can capture
            if (Attacker.PlayerType != PlayerTypeEnum.Character)
            {
                return false;
            }

            // Only monster can be captured
            if (Defender.PlayerType != PlayerTypeEnum.Monster)
            {
                return false;
            }

            // If a Pokemon with the same name already in Pokedex, and the attack is higher than the current target, skip
            var MonsterInPokedex = Attacker.Pokedex.Where(m => m.Name == GetPokemonName(Defender)).FirstOrDefault();
            if (MonsterInPokedex != null && MonsterInPokedex.Attack >= Defender.Attack)
            {
                return false;
            }

            // Roll dice to see whether to capture, 20% chance
            if (DiceHelper.RollDice(1, 10) <= 2)
            {
                return true;
            }

            // Don't try
            return false;
        }

        /// <summary>
        /// Attack as a Turn
        /// 
        /// Pick who to go after
        /// 
        /// Determine Attack Score
        /// Determine DefenseScore
        /// 
        /// Do the Attack
        /// 
        /// </summary>
        public override bool Attack(PlayerInfoModel Attacker)
        {
            return base.Attack(Attacker);
        }

        /// </summary>
        /// Do the Capture
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool Capture(PlayerInfoModel Attacker)
        {
            // AttackChoice will auto pick the target, good for auto battle
            if (EngineSettings.BattleScore.AutoBattle)
            {
                // For Capture, Choose Who
                EngineSettings.CurrentDefender = AttackChoice(Attacker);

                if (EngineSettings.CurrentDefender == null)
                {
                    return false;
                }
            }

            // Do Capture
            return TurnAsCapture(Attacker, EngineSettings.CurrentDefender); ;
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        public override PlayerInfoModel AttackChoice(PlayerInfoModel data)
        {
            return base.AttackChoice(data);
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        public override PlayerInfoModel SelectCharacterToAttack()
        {
            // Select first one to hit in the list for now...
            // Attack the least leveled character

            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Count < 1)
            {
                return null;
            }

            var Defender = EngineSettings.PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Character)
                .OrderBy(m => m.Level).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        public override PlayerInfoModel SelectMonsterToAttack()
        {
            // Select first one to hit in the list for now...
            // Attack the Monster with the most current health first 

            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Count < 1)
            {
                return null;
            }

            var Defender = EngineSettings.PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Monster)
                .OrderByDescending(m => m.CurrentHealth).FirstOrDefault();

            return Defender;

        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        public override bool TurnAsAttack(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            // Check Null inputs
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            // Set Messages to empty
            EngineSettings.BattleMessagesModel.ClearMessages();

            // Do the Attack
            CalculateAttackStatus(Attacker, Target);

            // See if the Battle Settings Overrides the Roll
            EngineSettings.BattleMessagesModel.HitStatus = BattleSettingsOverride(Attacker);

            switch (EngineSettings.BattleMessagesModel.HitStatus)
            {
                case HitStatusEnum.Miss:
                    // It's a Miss
                    break;

                case HitStatusEnum.CriticalMiss:
                    // It's a Critical Miss, so Bad things may happen
                    DetermineCriticalMissProblem(Attacker);
                    break;

                case HitStatusEnum.CriticalHit:
                case HitStatusEnum.Hit:
                    // It's a Hit
                    // Calculate Damage
                    EngineSettings.BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue();

                    // If Quick Attacker, 50% chance to deal critical Hit, double the damage
                    if (Attacker.PlayerType == PlayerTypeEnum.Character && Attacker.Job == CharacterJobEnum.QuickAttacker)
                    {
                        // Roll Dice
                        var d = DiceHelper.RollDice(1, 10);

                        // 50% to double the damage
                        if (d <= 5)
                        {
                            EngineSettings.BattleMessagesModel.DamageAmount *= 2;
                        }
                    }
                        
                    // Apply the Damage
                    ApplyDamage(Target);

                    EngineSettings.BattleMessagesModel.TurnMessageSpecial = EngineSettings.BattleMessagesModel.GetCurrentHealthMessage();

                    // Check if Dead and Remove
                    RemoveIfDead(Target);

                    // If it is a character apply the experience earned
                    CalculateExperience(Attacker, Target);

                    break;
            }

            // Turn Message
            EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + EngineSettings.BattleMessagesModel.AttackStatus + Target.Name + EngineSettings.BattleMessagesModel.TurnMessageSpecial + EngineSettings.BattleMessagesModel.ExperienceEarned;
            Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// CharacterModel captures MonsterModel
        /// </summary>
        public override bool TurnAsCapture(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            // Check for null
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            // Check for PlayerType
            if (Attacker.PlayerType != PlayerTypeEnum.Character)
            {
                return false;
            }

            if (Target.PlayerType != PlayerTypeEnum.Monster)
            {
                return false;
            }

            // Set Messages to empty
            EngineSettings.BattleMessagesModel.ClearMessages();

            // Remove Pokemon with the same name in Pokedex
            Attacker.Pokedex.RemoveAll(m => m.Name == GetPokemonName(Target));

            // Do the capture
            var PokemonToCapture = new MonsterModel(Target);
            PokemonToCapture.Name = GetPokemonName(Target);
            Attacker.Pokedex.Add(PokemonToCapture);

            // Save Pokedex to Character
            UpdateCharacterPokedexAsync(Attacker).Wait();

            // Set Monster to not alive, remove
            Target.Alive = false;
            RemoveIfDead(Target);

            // Apply the experience earned
            CalculateExperience(Attacker, Target);

            // Message
            EngineSettings.BattleMessagesModel.CaptureStatus = " captures ";
            EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + EngineSettings.BattleMessagesModel.CaptureStatus + Target.Name + EngineSettings.BattleMessagesModel.ExperienceEarned;
            Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// See if the Battle Settings will Override the Hit
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverride(PlayerInfoModel Attacker)
        {
            return base.BattleSettingsOverride(Attacker);
        }

        /// <summary>
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverrideHitStatusEnum(HitStatusEnum myEnum)
        {
            // Based on the Hit Status, establish a message

            return base.BattleSettingsOverrideHitStatusEnum(myEnum);
        }

        /// <summary>
        /// Apply the Damage to the Target
        /// </summary>
        public override int ApplyDamage(PlayerInfoModel Target)
        {
             return base.ApplyDamage(Target);
        }

        /// <summary>
        /// Calculate the Attack, return if it hit or missed.
        /// </summary>
        public override HitStatusEnum CalculateAttackStatus(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            return base.CalculateAttackStatus(Attacker, Target);
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        public override bool CalculateExperience(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            return base.CalculateExperience(Attacker, Target);
        }

        /// <summary>
        /// If Dead process Target Died
        /// </summary>
        public override bool RemoveIfDead(PlayerInfoModel Target)
        {
            return base.RemoveIfDead(Target);
        }

        /// <summary>
        /// Target Died
        /// 
        /// Process for death...
        /// 
        /// Returns the count of items dropped at death
        /// </summary>
        public override bool TargetDied(PlayerInfoModel Target)
        {
            return base.TargetDied(Target); 
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        public override int DropItems(PlayerInfoModel Target)
        {
            // Drop Items to ItemModel Pool

            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....

            // Add to ScoreModel

            return base.DropItems(Target);
        }

        /// <summary>
        /// Roll To Hit
        /// </summary>
        /// <param name="AttackScore"></param>
        /// <param name="DefenseScore"></param>
        public override HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            // Roll a 20 sided dice
            var d20 = DiceHelper.RollDice(1, 20);

            // if dice roll is 1, automatic miss
            if (d20 == 1)
            {
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls 1 to miss ";

                if (EngineSettings.BattleSettingsModel.AllowCriticalMiss)
                {
                    EngineSettings.BattleMessagesModel.AttackStatus = " rolls 1 to completly miss ";
                    EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalMiss;
                }

                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            // if dice is 20, automatic hit
            if (d20 == 20)
            {
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls 20 for hit ";
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;

                if (EngineSettings.BattleSettingsModel.AllowCriticalHit)
                {
                    EngineSettings.BattleMessagesModel.AttackStatus = " rolls 20 for lucky hit ";
                    EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalHit;
                }
                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            // if hit score is less than defense, it's a miss
            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls " + d20 + " and misses ";

                // Miss
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                EngineSettings.BattleMessagesModel.DamageAmount = 0;
                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            EngineSettings.BattleMessagesModel.AttackStatus = " rolls " + d20 + " and hits ";

            // Hit
            EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
            return EngineSettings.BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Will drop between 1 and number of (round // 10 + 1) items from the ItemModel set...
        /// </summary>
        public override List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            // Roll dice to drop number of items between 1 and number of (rounds divide by 10 + 1)
            var NumberToDrop = DiceHelper.RollDice(1, round / 10 + 1);

            var result = new List<ItemModel>();

            for (var i = 0; i < NumberToDrop; i++)
            {
                // Get a random Unique Item
                var data = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
                result.Add(data);
            }

            return result;
        }

        /// <summary>
        /// Critical Miss Problem
        /// </summary>
        public override bool DetermineCriticalMissProblem(PlayerInfoModel attacker)
        {
            return base.DetermineCriticalMissProblem(attacker);
        }
    }
}