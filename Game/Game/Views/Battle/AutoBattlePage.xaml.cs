﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using Game.Engine.EngineInterfaces;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AutoBattlePage : ContentPage
	{
		// Hold the Engine, so it can be swapped out for unit testing
		public IAutoBattleInterface AutoBattle = BattleEngineViewModel.Instance.AutoBattleEngine;

		/// <summary>
		/// Constructor
		/// </summary>
		public AutoBattlePage ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Defines what happens when the AutoBattleButton is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void AutobattleButton_Clicked(object sender, EventArgs e)
		{
			// Call into Auto Battle from here to do the Battle...

			BattleEngineViewModel.Instance.SetBattleEngineToGame();

			await BattleEngineViewModel.Instance.AutoBattleEngine.RunAutoBattle();
			
			var BattleMessage = string.Format("Done {0} Rounds", AutoBattle.Battle.EngineSettings.BattleScore.RoundCount);

			BattleMessageValue.Text = BattleMessage;

			AutobattleImage.Source = "troll6_d.gif";
		}
	}
}