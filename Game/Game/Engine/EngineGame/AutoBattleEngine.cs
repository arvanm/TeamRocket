using System;
using System.Linq;
using System.Threading.Tasks;
using Game.Engine.EngineBase;
using Game.Engine.EngineInterfaces;
using Game.GameRules;
using Game.Models;
using Game.Services;
using Game.ViewModels;
using Xamarin.Forms;

namespace Game.Engine.EngineGame
{
    /// <summary>
    /// AutoBattle Engine
    /// 
    /// Runs the engine simulation with no user interaction
    /// 
    /// </summary>
    public class AutoBattleEngine : AutoBattleEngineBase, IAutoBattleInterface
    {


        #region Algrorithm
        // Prepare for Battle
        // Pick 6 Characters
        // Initialize the Battle
        // Start a Round

        // Fight Loop
        // Loop in the round each turn
        // If Round is over, Start New Round
        // Check end state of round/game

        // Wrap up
        // Get Score
        // Save Score
        // Output Score
        #endregion Algrorithm

        /// <summary>
        /// Create the Score data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateScoreAsync(ScoreModel data)
        {
            if (data == null)
            {
                return false;
            }

            var result = await ScoreIndexViewModel.Instance.CreateAsync(data);

            return result;
        }

        /// <summary>
        /// Define the Battle variable 
        /// </summary>
        public new IBattleEngineInterface Battle
        {
            get
            {
                if (base.Battle == null)
                {
                    base.Battle = new BattleEngine();
                }
                return base.Battle;
            }
            set { base.Battle = Battle; }
        }

        /// <summary>
        /// Create character list and monster list
        /// </summary>
        /// <returns></returns>
        public override bool CreateCharacterParty()
        {
            // Picks 6 or less Characters, no repeat ones

            // Will pull from existing characters
            foreach (var data in CharacterIndexViewModel.Instance.Dataset)
            {
                if (Battle.EngineSettings.CharacterList.Count() >= Battle.EngineSettings.MaxNumberPartyCharacters)
                {
                    break;
                }

                // Start off with max health if adding a character in
                data.CurrentHealth = data.GetMaxHealthTotal;
                Battle.PopulateCharacterList(data);
            }

            return true;
        }

        /// <summary>
        /// detect if there's infinite rounds (no game end)
        /// </summary>
        /// <returns></returns>
        public override bool DetectInfinateLoop()
        {
            return base.DetectInfinateLoop();
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Start the automatic battle
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> RunAutoBattle()
        {
            var BattleResult = await base.RunAutoBattle();

            // Save score
            var Score = Battle.EngineSettings.BattleScore;
            Score.Name = "AutoBattle " + DateTime.Now.ToString("G");
            var ScoreResult = await CreateScoreAsync(Score);

            return (BattleResult && ScoreResult);
        }
    }
}