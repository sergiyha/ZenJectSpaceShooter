using System;
using System.Collections.Generic;
using Assets.Scripts.All_In.Components.Effects;
using Assets.Scripts.Behaviours.Movement;
using Assets.Scripts.Behaviours.Weapons;
using Assets.Scripts.Controllers.EnemiesHandler;
using Assets.Scripts.Controllers.Signals;
using Assets.Scripts.Enums;
using Assets.Scripts.Factories;
using Assets.Scripts.UI.Enemy;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Behaviours.Characters
{
	public class EnemyBehaviour : MonoBehaviour
	{
		public IMovementBehaviour MovementBehaviour;
		public IWeaponBehaviour WeaponBehaviour;
		private EnemiesSettings _settings;
		private EnemyData _enemyData;
		private int _damageDealed;
		private IEnemiesHandler _enemiesHandler;

		private EnemyWasDestroyedSignal _enemyWasDestroyedSignal;
		private EnemyKilledWithGunSignal _enemyKilledWithGunSignal;

		private HittingEffectComponent _hitEffectComp;
		private EnemyUi _enemyUi;


		[Inject]
		public void Init(
			MovementBehaviourFactory movementBehaviourFactory,
			WeaponBehaviourFactory weaponBehaviourFactory,
			EnemiesSettings settings,
			IEnemiesHandler enemiesHandler,
			EnemyWasDestroyedSignal enemyDestroyedSignal,
			EnemyKilledWithGunSignal enemyDestroyedWithGunSignal,
			EnemyUi enemyUI)
		{
			_enemyKilledWithGunSignal = enemyDestroyedWithGunSignal;
			_enemyWasDestroyedSignal = enemyDestroyedSignal;

			_enemiesHandler = enemiesHandler;
			_settings = settings;

			MovementBehaviour = movementBehaviourFactory.Create(Oponents.Enemy);
			MovementBehaviour.Init(transform);

			WeaponBehaviour = weaponBehaviourFactory.Create(Oponents.Enemy);
			WeaponBehaviour.Init(transform);

			_enemyUi = enemyUI;
			_enemyUi.transform.SetParent(this.transform);
		}

		public void DealDamage(int damageValue)
		{
			_hitEffectComp.FireEffect();
			_damageDealed += damageValue;
			var relativisticDamageValue = 1 - (float)_damageDealed / _enemyData.LifePoints;
			_enemyUi.UpdateHealthBar(relativisticDamageValue);
			if (_damageDealed >= _enemyData.LifePoints)
			{
				//Debug.LogError("Damege dealed");
				_enemyKilledWithGunSignal.Fire();
				_enemiesHandler.DestroyEnemy(gameObject);
			}
		}

		public EnemyBehaviour SetUp(EnemyType enemyType)
		{
			_enemyData = _settings.GetEnemyData(enemyType);

			_hitEffectComp = this.gameObject.AddComponent<HittingEffectComponent>();
			_hitEffectComp.SetUp(_enemyData.HittingEffects);

			Instantiate(_enemyData.GetView, this.transform).transform.localPosition = Vector3.zero;
			return this;
		}

		private void OnDestroy()
		{
			_enemyWasDestroyedSignal.Fire();
		}

		public class EnemyBehaviourFactory : Factory<EnemyBehaviour>
		{

		}

		[Serializable]
		public class EnemiesSettings
		{
			public List<EnemyData> Enemies;

			public EnemyData GetEnemyData(EnemyType type)
			{
				var enemyData = Enemies.Find(i => i.Type == type);
				if (enemyData == null)
				{
					Debug.LogError("EnemyData wasn't found. Type: type");
				}

				return enemyData;
			}
		}

		[Serializable]

		public class EnemyData
		{
			public string ViewPath;
			public int LifePoints;
			public float MovementSpeed = 1.0f;
			public EnemyType Type;
			public HittingEffectComponent.HittingEffects HittingEffects;

			public GameObject GetView
			{
				get { return Resources.Load<GameObject>(ViewPath); }
			}
		}



	}

	public enum EnemyType
	{
		Nan,
		Ufo,
	}
}
