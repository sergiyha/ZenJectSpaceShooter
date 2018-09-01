using System.Security.Permissions;
using Assets.Scripts.All_In.Weapons;
using Assets.Scripts.Behaviours;
using Assets.Scripts.Behaviours.Characters;
using Assets.Scripts.Behaviours.Movement;
using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
	public class Settings : ScriptableObject
	{
		[SerializeField] private PlayerMovementBehaviour.Settings MovementSettings;

		[SerializeField] private Weapon.Settings WeaponSettings;

		[SerializeField] private Weapon.GunsSetting GunsSettings;

		[SerializeField] private WavesController.SettingsWrapper WavesSettings;

		[SerializeField] private EnemyBehaviour.EnemiesSettings Enemies;


		public Weapon.GunsSetting GetGuns
		{
			get { return GunsSettings; }
		}

		public EnemyBehaviour.EnemiesSettings GetEnemiesSettings
		{
			get { return Enemies; }
		}

		public PlayerMovementBehaviour.Settings GetMovementSettings
		{
			get { return MovementSettings; }
		}

		public Weapon.Settings GetWeaponSettings
		{
			get { return WeaponSettings; }
		}

		public WavesController.SettingsWrapper GetWavesSettings
		{
			get { return WavesSettings; }
		}
	}
}
