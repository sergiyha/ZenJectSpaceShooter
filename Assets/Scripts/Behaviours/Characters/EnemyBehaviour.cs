using Assets.Scripts.Behaviours.Movement;
using Assets.Scripts.Behaviours.Weapons;
using Assets.Scripts.Enums;
using Assets.Scripts.Factories;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Characters
{
	public class EnemyBehaviour : MonoBehaviour, ICharacterBehaviour
	{

		private IMovementBehaviour _movementBehaviour;
		private IWeaponBehaviour _weaponBehaviour;

		public void Init(MovementBehaviourFactory movementBehaviourFactory, WeaponBehaviourFactory weaponBehaviourFactory)
		{
			_movementBehaviour = movementBehaviourFactory.Create(Oponents.Enemy);
			_movementBehaviour.Init(transform);

			_weaponBehaviour = weaponBehaviourFactory.Create(Oponents.Player);
			_weaponBehaviour.Init(transform);
		}
	}
}
