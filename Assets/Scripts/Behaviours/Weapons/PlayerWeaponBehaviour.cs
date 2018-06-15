using Assets.Scripts.All_In.Weapons;
using Assets.Scripts.Inputs.Signals;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Behaviours.Weapons
{
	public class PlayerWeaponBehaviour : WeaponBehaviour
	{
		private Weapon _weapon;

		public PlayerWeaponBehaviour
		(
			FinishFiringSignal finishFiringSiglal,
			BeginFiringSignal beginFiringSignal,
			Weapon.WeaponFactory weapon
		)
		{
			_weapon = weapon.Create();
			finishFiringSiglal += OnFireFinish;
			beginFiringSignal += OnFireBegin;
		}

		protected override void OnFireBegin()
		{
			_weapon.BeginFire();
			Debug.LogError("Firing Starts");
		}

		protected override void OnFireFinish()
		{
			_weapon.FinishFire();
			Debug.LogError("Firing Finish");
		}

		public override void Init(Transform transform)
		{
			_weapon.transform.SetParent(transform, false);
		}

		public class PlayerWeaporBehaviourFactory : Factory<PlayerWeaponBehaviour>
		{
		}
	}
}
