using Assets.Scripts.All_In.Weapons;
using Assets.Scripts.Controllers.PowerUpAbsorber;
using Assets.Scripts.GameSignals;
using Assets.Scripts.Inputs.Signals;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Behaviours.Weapons
{
	public class PlayerWeaponBehaviour : WeaponBehaviour
	{
		[SerializeField]
		private Weapon _weapon;

		private Weapon.Settings _settings;

		[Inject] private IPowerUpObsorber _powerUpObsorber;

		public PlayerWeaponBehaviour
		(
			FinishFiringSignal finishFiringSiglal,
			BeginFiringSignal beginFiringSignal,
			Weapon.WeaponFactory weapon,
			FinishGameSignal finishSignal,
			Weapon.Settings settings,
			IPowerUpObsorber powerUpObsorber
		)
		{
			_powerUpObsorber = powerUpObsorber;
			_settings = settings;
			_weapon = weapon.Create();

			finishFiringSiglal += OnFireFinish;
			beginFiringSignal += OnFireBegin;

			_weapon.Init(_settings);
			_powerUpObsorber.Init(_settings);
			finishSignal += (o) =>
			{
				finishFiringSiglal -= OnFireFinish;
				beginFiringSignal -= OnFireBegin;
			};
		}


		protected override void OnFireBegin()
		{
			_weapon.BeginFire();
			//Debug.LogError("Firing Starts");
		}

		protected override void OnFireFinish()
		{
			_weapon.FinishFire();
			//Debug.LogError("Firing Finish");
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
