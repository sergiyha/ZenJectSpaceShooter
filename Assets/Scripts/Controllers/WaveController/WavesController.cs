using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.All_In.Async;
using Assets.Scripts.Behaviours.Characters;
using Assets.Scripts.Controllers.EnemiesHandler;
using Assets.Scripts.Controllers.PowerUpCreator;
using Assets.Scripts.Controllers.Signals;
using Assets.Scripts.GameSignals;
using Assets.Scripts.Utilities.StructUtils;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
	public class WavesController : IWavesController
	{
		private int _enemiesWereUsedInSubwave = 0;
		private int _enemiesKilledWithGun = 0;

		private int _currentWaveNumber = 0;
		private int _currentSubwaveNumber = 0;


		private Settings _currentWaveSettings;
		private EnemyBehaviour.EnemyBehaviourFactory _enemyFactory;

		private EnemyWasDestroyedSignal _enemyDestroyedSignal;
		private EnemyKilledWithGunSignal _enemyKilledWithGunSignal;
		private AsyncProcessor _asyncProcessor;


		[Inject] private SettingsWrapper _wavesData;

		[Inject] private IEnemiesHandler _enemiesHandler;
		[Inject] private StartGameSignal _gameStartedSignal;
		[Inject] private FinishGameSignal _finishGameSignal;
		[Inject] private UpdateMainViewSignal _updateViewSignal;
		[Inject] private IPowerUpCreator _powerUpCreator;

		[Inject]
		private void Init()
		{
			_finishGameSignal += (o) =>
			{
				_enemyDestroyedSignal -= OnEnemyWasUsed;
			};
			_enemyDestroyedSignal += OnEnemyWasUsed;
			_asyncProcessor.StartCoroutine(StartWave(true));
		}

		public WavesController
		(
			SettingsWrapper settings,
			EnemyBehaviour.EnemyBehaviourFactory enemyBehFactory,
			EnemyWasDestroyedSignal enemyDestroyedSignal,
			AsyncProcessor asyncProcessor,
			EnemyKilledWithGunSignal enemykilledWithGun
		)
		{
			_enemyKilledWithGunSignal = enemykilledWithGun;
			_enemyKilledWithGunSignal += OnEnemiesWasKilledWithGun;
			_enemyDestroyedSignal = enemyDestroyedSignal;
			_wavesData = settings;
			_enemyFactory = enemyBehFactory;
			_asyncProcessor = asyncProcessor;
		}


		~WavesController()
		{
			_enemyDestroyedSignal -= OnEnemyWasUsed;
		}



		public Settings GetCurrentWaveSetting()
		{
			return _currentWaveSettings;
		}

		private void OnEnemiesWasKilledWithGun()
		{
			_enemiesKilledWithGun++;
		}


		private void OnEnemyWasUsed()
		{
			_enemiesWereUsedInSubwave++;
			CheckUsedEnemies();
		}

		private void CheckUsedEnemies()
		{
			if (_enemiesWereUsedInSubwave != _currentWaveSettings.EnemiesInSubwave) return;
			if (_currentSubwaveNumber == _currentWaveSettings.SubwavesCount)
			{
				Debug.LogError("finish wave");

				_asyncProcessor.StartCoroutine(StartWave(false));
				return;
			}
			_asyncProcessor.StartCoroutine(StartSubwave());
		}

		private IEnumerator StartWave(bool isFirstTime)
		{
			_currentWaveNumber++;
			_currentWaveSettings = _wavesData.GetNeededSetting(_currentWaveNumber);

			if (isFirstTime)
			{

				_gameStartedSignal.Fire(default(object));

				yield return new WaitForSeconds(_wavesData.TimeToReady + 1);
			}
			else
			{
				_powerUpCreator.CreatePowerUp();
				yield return new WaitForSeconds(_currentWaveSettings.TimeBetweenWaves);
				_currentSubwaveNumber = 0;
				_enemiesWereUsedInSubwave = 0;
				_currentWaveSettings = _wavesData.GetNeededSetting(_currentWaveNumber);
			}

			//Debug.LogError("WaveStarted // n: " + _currentWaveNumber);

			yield return StartSubwave(true);
		}

		private IEnumerator StartSubwave(bool isFirstInWave = false)
		{

			if (!isFirstInWave)
			{
				_currentSubwaveNumber += (_enemiesWereUsedInSubwave == _enemiesKilledWithGun) ? 1 : 0; // if user 
				_enemiesWereUsedInSubwave = 0;
				_enemiesKilledWithGun = 0;
				_updateViewSignal.Fire(_currentWaveNumber, _currentSubwaveNumber);
				yield return new WaitForSeconds(_currentWaveSettings.TimeBetweenSubwave);
			}
			else
			{
				_currentSubwaveNumber++;

			}
			_updateViewSignal.Fire(_currentWaveNumber, _currentSubwaveNumber);

			var trajectory = _wavesData.GetRangomTrajectory().Vectors;
			for (var i = 0; i < _currentWaveSettings.EnemiesInSubwave; i++)
			{
				yield return new WaitForSeconds(_currentWaveSettings.TimeBetweenEnemy);
				CreateEnemy(trajectory);
			}

			//Debug.LogError("SubwaveStarted // n: " + _currentSubwaveNumber);
		}

		private void CreateEnemy(Vector2[] trajectory)
		{
			var enemy = _enemyFactory.Create();
			enemy.transform.position = new Vector2(1000, 1000);
			enemy.SetUp(EnemyType.Ufo).MovementBehaviour.PerformToMove(trajectory);
			_enemiesHandler.AddEnemy(enemy.gameObject, enemy);
		}

		[Serializable]
		public class SettingsWrapper
		{
			public float TimeToReady = 3f;
			public List<Vector2ListWrapper> Trajectories;

			public List<Settings> WavesData;

			public Settings GetNeededSetting(int waveNumber)
			{
				return WavesData.First(i => i.Number == waveNumber);
			}

			public Vector2ListWrapper GetRangomTrajectory()
			{
				return Trajectories[UnityEngine.Random.Range(0, Trajectories.Count)];
			}
		}

		[Serializable]
		public class Settings
		{
			public int Number;

			public float TimeBetweenEnemy = 0.3f;
			public float TimeBetweenWaves = 2;
			public float TimeBetweenSubwave = 0.5f;
			public int SubwavesCount = 3;
			public int EnemySpeed = 20;
			public int EnemiesInSubwave;

			public int EnemiesCountToBeUsed
			{
				get
				{
					return SubwavesCount * EnemiesInSubwave;
				}
			}
		}
	}
}
