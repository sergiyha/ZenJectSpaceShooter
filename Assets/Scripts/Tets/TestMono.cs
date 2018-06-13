using Assets.Scripts.Behaviours;
using Assets.Scripts.Behaviours.Weapons;
using Assets.Scripts.Enums;
using Assets.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Tets
{
	public class TestMono : MonoBehaviour
	{
		private MovementBehaviour _movementBehaviour;
		private IWeaponBehaviour _weaponBehaviour;

		[Inject]
		private void Init
		(
			MovementBehaviour movementBehaviour,
			WeaponBehaviourFactory wbFactory
		)
		{
			_weaponBehaviour = wbFactory.Create(Oponents.Player);
			_weaponBehaviour.Init(this.transform);

			_movementBehaviour = movementBehaviour;
			_movementBehaviour.Init(this.transform);
		}

		private void OnDestroy()
		{

		}

	}
}
