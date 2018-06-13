using Assets.Scripts.All_In.Weapons;
using Assets.Scripts.Behaviours;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
	public class Settings : ScriptableObject
	{
		[SerializeField]
		private MovementBehaviour.Settings MovementSettings;

		[SerializeField]
		private Weapon.Settings WeaponSettings;

		public MovementBehaviour.Settings GetMovementSettings
		{
			get { return MovementSettings; }
		}

		public Weapon.Settings GetWeaponSettings
		{
			get { return WeaponSettings; }
		}
	}
}
