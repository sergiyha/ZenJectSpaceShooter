using UnityEngine;
using Zenject;

namespace Assets.Scripts.Behaviours.Weapons
{
	public class EnemyWeaponBehaviour : WeaponBehaviour
	{

		protected override void OnFireBegin()
		{

		}

		protected override void OnFireFinish()
		{

		}

		public override void Init(Transform transform)
		{
			//throw new System.NotImplementedException();
		}

		public class EnemyWeaponBehaviourFactory : Factory<EnemyWeaponBehaviour>
		{
		}
	}
}
