using System;
using System.Collections.Generic;
using Assets.Scripts.All_In.Weapons;
using Assets.Scripts.Behaviours.Movement;
using Assets.Scripts.Behaviours.Weapons;
using Assets.Scripts.Controllers.EnemiesHandler;
using Assets.Scripts.Enums;
using Assets.Scripts.Factories;
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


		[Inject]
		public void Init(
			MovementBehaviourFactory movementBehaviourFactory,
			WeaponBehaviourFactory weaponBehaviourFactory,
			EnemiesSettings settings,
			IEnemiesHandler enemiesHandler)
		{
			_enemiesHandler = enemiesHandler;
			_settings = settings;

			MovementBehaviour = movementBehaviourFactory.Create(Oponents.Enemy);
			MovementBehaviour.Init(transform);

			WeaponBehaviour = weaponBehaviourFactory.Create(Oponents.Enemy);
			WeaponBehaviour.Init(transform);
		}

		public void DealDamage(int damageValue)
		{
			_damageDealed += damageValue;
			if (_damageDealed >= _enemyData.LifePoints)
			{
				Debug.LogError("Damege dealed");
				_enemiesHandler.RemoveEnemy(this.gameObject);
				Destroy(gameObject);
			}

		}

		public EnemyBehaviour SetUp(EnemyType enemyType)
		{
			_enemyData = _settings.GetEnemyData(enemyType);
			Instantiate(_enemyData.GetView, this.transform).transform.localPosition = Vector3.zero;
			return this;
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
