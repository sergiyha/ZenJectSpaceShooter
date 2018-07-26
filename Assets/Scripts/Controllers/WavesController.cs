using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.All_In.Async;
using Assets.Scripts.Behaviours.Characters;
using Assets.Scripts.Controllers.EnemiesHandler;
using Assets.Scripts.Controllers.Signals;
using Assets.Scripts.GameSignals;
using Assets.Scripts.Utilities.StructUtils;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
	public class WavesController : IWavesController
	{
		private int _enemiesWereUsed = 0;
		private int _enemiesWereUsedInSubwave = 0;

		private int _currentWaveNumber = 0;
		private int _currentSubwaveNumber = 0;

		private Settings _currentWaveSettings;
		private EnemyBehaviour.EnemyBehaviourFactory _enemyFactory;

		private EnemyWasDestroyedSignal _enemyDestroyedSignal;


		[Inject] private SettingsWrapper _wavesData;

		[Inject] private IEnemiesHandler _enemiesHandler;
		[Inject] private StartGameSignal _gameStartedSignal;

		[Inject]
		private void Init()
		{
			_currentWaveSettings = _wavesData.GetNeededSetting(_currentWaveNumber);
			_enemyDestroyedSignal += OnEnemyWasUsed;
			_asyncProcessor.StartCoroutine(StartWave(true));
		}
		private AsyncProcessor _asyncProcessor;

		public WavesController
		(
			SettingsWrapper settings,
			EnemyBehaviour.EnemyBehaviourFactory enemyBehFactory,
			EnemyWasDestroyedSignal enemyDestroyedSignal,
			AsyncProcessor asyncProcessor
		)
		{

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

		private void OnEnemyWasUsed()
		{
			_enemiesWereUsed++;
			_enemiesWereUsedInSubwave++;
			CheckUsedEnemies();
		}

		private void CheckUsedEnemies()
		{
			if (_enemiesWereUsedInSubwave != _currentWaveSettings.EnemiesInSubwave) return;
			if (_enemiesWereUsed == _currentWaveSettings.EnemiesCountToBeUsed)
			{
				Debug.LogError("finish wave");

				StartNextWave();
				return;
			}
			StartNextSubwave();
		}

		private void StartNextSubwave()
		{
			_currentSubwaveNumber++;
			_enemiesWereUsedInSubwave = 0;
			_asyncProcessor.StartCoroutine(StartSubwave());
		}


		private void StartNextWave()
		{
			_enemiesWereUsedInSubwave = 0;
			_currentWaveNumber++;
			_currentWaveSettings = _wavesData.GetNeededSetting(_currentWaveNumber);
			_asyncProcessor.StartCoroutine(StartWave(false));
		}

		private IEnumerator StartWave(bool isfirst)
		{
			Debug.LogError("Wave started");
			if (isfirst)
			{

				_gameStartedSignal.Fire(default(object));
				yield return new WaitForSeconds(_currentWaveSettings.TimeToReady + 1);
			}
			else
			{
				yield return new WaitForSeconds(_currentWaveSettings.TimeBetweenWaves);
			}
			yield return StartSubwave(true);
		}

		private IEnumerator StartSubwave(bool isFirstInWave = false)
		{
			Debug.LogError("SubWaveStarted");
			if (!isFirstInWave)
				yield return new WaitForSeconds(_currentWaveSettings.TimeBetweenSubwave);
			var trajectory = _wavesData.GetRangomTrajectory().Vectors;
			for (var i = 0; i < _currentWaveSettings.EnemiesInSubwave; i++)
			{
				yield return new WaitForSeconds(_currentWaveSettings.TimeBetweenEnemy);

				CreateEnemy(trajectory);
			}
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

			public float TimeToReady;
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
