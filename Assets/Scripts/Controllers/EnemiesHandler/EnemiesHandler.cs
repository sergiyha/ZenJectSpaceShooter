using System.Collections.Generic;
using Assets.Scripts.All_In.Components;
using Assets.Scripts.Behaviours.Characters;
using UnityEngine;

namespace Assets.Scripts.Controllers.EnemiesHandler
{
	public class EnemiesHandler : IEnemiesHandler
	{

		private Dictionary<GameObject, EnemyBehaviour> _enemiesPool;


		EnemiesHandler()
		{
			_enemiesPool = new Dictionary<GameObject, EnemyBehaviour>();
		}

		public void AddEnemy(GameObject go, EnemyBehaviour eb)
		{
			if (_enemiesPool.ContainsKey(go)) return;
			_enemiesPool.Add(go, eb);
		}

		public void RemoveEnemy(GameObject go)
		{
			if (!_enemiesPool.ContainsKey(go))
			{
				Debug.LogError("There is no enemy in pool: " + go);
				return;
			}
			_enemiesPool.Remove(go);
		}

		public void DestroyEnemy(GameObject go)
		{
			RemoveEnemy(go);
			Object.Destroy(go);
		}

		public void MakeDamage(GameObject go, int damageValue)
		{
			if (!_enemiesPool.ContainsKey(go))
			{
				Debug.LogError("Can't deal damage to unregister enemy !!!");
				return;
			}
			var enemyBeh = _enemiesPool[go];
			enemyBeh.DealDamage(damageValue);
		}
	}
}
