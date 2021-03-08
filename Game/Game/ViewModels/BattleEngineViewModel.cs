using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Game.Models;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.Engine.EngineBase;
using Xamarin.Forms;
using Game.Views;
using Game.Services;
using System.Threading.Tasks;

namespace Game.ViewModels
{
    /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class BattleEngineViewModel
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile BattleEngineViewModel instance;
        private static readonly object syncRoot = new Object();

        public static BattleEngineViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new BattleEngineViewModel();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton

        #region BattleEngineSelection
        // The Battle Engine
        public IBattleEngineInterface EngineKoenig = new Engine.EngineKoenig.BattleEngine();

        // Auto Battle Engine (used for scneario testing)
        public IAutoBattleInterface AutoBattleEngineKoenig = new Engine.EngineKoenig.AutoBattleEngine();

        // The Battle Engine
        public IBattleEngineInterface EngineGame = new Engine.EngineGame.BattleEngine();

        // Auto Battle Engine (used for scneario testing)
        public IAutoBattleInterface AutoBattleEngineGame = new Engine.EngineGame.AutoBattleEngine();

        // Set the Battle Engine
        public IBattleEngineInterface Engine;

        // Auto Battle Engine (used for scneario testing)
        public IAutoBattleInterface AutoBattleEngine;

        #endregion BattleEngineSelection

        // Hold the Proposed List of Characters for the Battle to Use
        public ObservableCollection<CharacterModel> PartyCharacterList { get; set; } = new ObservableCollection<CharacterModel>();

        //// Hold the View Model to the CharacterIndexViewModel
        //public CharacterIndexViewModel DatabaseCharacterViewModel = CharacterIndexViewModel.Instance;

        //// Have the Database Character List point to the Character View Model List
        public ObservableCollection<CharacterModel> DatabaseCharacterList { get; set; } = CharacterIndexViewModel.Instance.Dataset;

        // Datastore for Scores
        public IDataStore<ScoreModel> ScoreDataStore;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleEngineViewModel()
        {
            //SetBattleEngineToKoenig();
            SetBattleEngineToGame();

            // Set Datastore
            CharacterDataStore = MockDataStore<CharacterModel>.Instance;
            ScoreDataStore = MockDataStore<ScoreModel>.Instance;

            // Register the Update Score Message
            MessagingCenter.Subscribe<BattlePage, ScoreModel>(this, "CreateScore", async (obj, data) =>
            {
                await CreateScoreAsync(data as ScoreModel);
            });
        }

        /// <summary>
        /// Set the Battle Engine to the Game Version for actual use
        /// </summary>
        /// <returns></returns>
        public bool SetBattleEngineToGame()
        {
            Engine = EngineGame;
            AutoBattleEngine = AutoBattleEngineGame;
            return true;
        }

        /// <summary>
        /// Set the Battle Engine to the Koenig Version for Testing
        /// </summary>
        /// <returns></returns>
        public bool SetBattleEngineToKoenig()
        {
            Engine = EngineKoenig;
            AutoBattleEngine = AutoBattleEngineKoenig;
            return true;
        }

        #endregion Constructor

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

            // Check that the record exists, if it does not, then exit with false
            var BaseDataId = ((BaseModel<ScoreModel>)(object)data).Id;
            var record = await ScoreDataStore.ReadAsync(BaseDataId);
            if (record != null)
            {
                return false;
            }

            var result = await ScoreDataStore.CreateAsync(data);

            return result;
        }
    }
}