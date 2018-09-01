using System;
using System.Collections.Generic;
using Assets.Scripts.All_In.Commands.PowerUp;
using Assets.Scripts.Behaviours.Characters;
using Assets.Scripts.Controllers.Signals.PowerUp;
using UniRx;
using UnityEngine;


namespace Assets.Scripts.Controllers.PowerUpCreator
{
	public class PowerUpCreator : IPowerUpCreator
	{
		private Tuple<float, BasePlayerPowerUpCommand>[] _commandsColection;
		private List<int> _probabilitiesIds;
		private int ProbabilitiesCount = 100;
		private PowerUpSignal _powerUpSignal;
		private PowerUpBehaviour.PowerUpBehaviourFactory _behaviourFactory;



		public PowerUpCreator
		(
			PowerUpSignal powerUpSignal,
			PowerUpBehaviour.PowerUpBehaviourFactory behaviourFactory
		)
		{
			_behaviourFactory = behaviourFactory;
			_powerUpSignal = powerUpSignal;
			_commandsColection = new[]
			{
				 CreatePair<BulletDamagePowerUpCommand>(0.45f),
				 CreatePair<BulletSpeedPowerUpCommand>(0.27f),
				 CreatePair<GunsFireratePowerUpCommand>(0.18f),
				 CreatePair<GunsCountPowerUpCommand>(0.1f),
			};
			CreateProbabilities();

		}



		private BasePlayerPowerUpCommand GetRandomPowerUp()
		{
			var i = _commandsColection[_probabilitiesIds[UnityEngine.Random.Range(0, _probabilitiesIds.Count)]].Item2;
			Debug.LogError(i);
			return i;
		}

		private void OnGetPowerUp()
		{
			_powerUpSignal.Fire(GetRandomPowerUp());
		}

		private void CreateProbabilities()
		{
			_probabilitiesIds = new List<int>();
			for (var i = 0; i < _commandsColection.Length; i++)
			{
				var tupleItem = _commandsColection[i];
				var count = ProbabilitiesCount * tupleItem.Item1;
				for (int j = 0; j < count; j++)
				{
					_probabilitiesIds.Add(i);
				}
			}
			_probabilitiesIds.Shuffle();
		}

		private Tuple<float, BasePlayerPowerUpCommand> CreatePair<T>(float probability) where T : BasePlayerPowerUpCommand, new()//probability and BasePlayerPowerUpCommand
		{
			return new Tuple<float, BasePlayerPowerUpCommand>(probability, new T());
		}

		public void CreatePowerUp()
		{
			var behaviour = _behaviourFactory.Create();
			behaviour.BehaviourTrigered += OnGetPowerUp;
		}
	}


}

