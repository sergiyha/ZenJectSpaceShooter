using Assets.Scripts.Behaviours.Weapons;
using Assets.Scripts.Enums;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Factories
{
	public class WeaponBehaviourFactory : IFactory<Oponents, IWeaponBehaviour>
	{
		[Inject] private EnemyWeaponBehaviour.EnemyWeaponBehaviourFactory _enemyWeaponBehFactory;
		[Inject] private PlayerWeaponBehaviour.PlayerWeaporBehaviourFactory _playerWeaporBehFactory;


		public IWeaponBehaviour Create(Oponents param)
		{
			IWeaponBehaviour weapBeh = null;
			switch (param)
			{
				case Oponents.NaN:
					break;
				case Oponents.Player:
					weapBeh = _playerWeaporBehFactory.Create();
					break;
				case Oponents.Enemy:
					weapBeh = _enemyWeaponBehFactory.Create();
					break;
			}
			return weapBeh;
		}
	}
}