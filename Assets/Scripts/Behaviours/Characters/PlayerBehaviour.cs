using Assets.Scripts.Behaviours;
using Assets.Scripts.Behaviours.Characters;
using Assets.Scripts.Behaviours.Movement;
using Assets.Scripts.Behaviours.Weapons;
using Assets.Scripts.Enums;
using Assets.Scripts.Factories;
using Assets.Scripts.GameSignals;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Tets
{
	public class PlayerBehaviour : MonoBehaviour
	{
		private IMovementBehaviour _movementBehaviour;
		private IWeaponBehaviour _weaponBehaviour;

		public LayerMask PlayerDestroyes;

		[Inject] private FinishGameSignal _finishingSignal;

		[Inject]
		public void Init
		(
			MovementBehaviourFactory movementBehaviourFactory,
			WeaponBehaviourFactory weaponBehaviourFactory
		)
		{
			_weaponBehaviour = weaponBehaviourFactory.Create(Oponents.Player);
			_weaponBehaviour.Init(this.transform);

			_movementBehaviour = movementBehaviourFactory.Create(Oponents.Player);
			_movementBehaviour.Init(this.transform);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Enemy"))
			{
				_finishingSignal.Fire(default(object));
				Destroy(gameObject);
			}
		}



	}
}
