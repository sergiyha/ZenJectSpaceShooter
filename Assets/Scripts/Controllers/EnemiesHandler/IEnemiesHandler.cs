using System.Collections.Generic;
using Assets.Scripts.Behaviours.Characters;
using UnityEngine;

namespace Assets.Scripts.Controllers.EnemiesHandler
{
	public interface IEnemiesHandler
	{
		void AddEnemy(GameObject go, EnemyBehaviour eb);
		void RemoveEnemy(GameObject go);
		void MakeDamage(GameObject go, int damageValue);
	}
}
