using Assets.Scripts.All_In.Weapons;
using Assets.Scripts.Behaviours;
using Assets.Scripts.Behaviours.Movement;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
	public class Settings : ScriptableObject
	{
		[SerializeField]
		private PlayerMovementBehaviour.Settings MovementSettings;

		[SerializeField]
		private Weapon.Settings WeaponSettings;

		public PlayerMovementBehaviour.Settings GetMovementSettings
		{
			get { return MovementSettings; }
		}

		public Weapon.Settings GetWeaponSettings
		{
			get { return WeaponSettings; }
		}
	}
}
