using Assets.Scripts.All_In.Commands.PowerUp;
using Assets.Scripts.All_In.Weapons;
using Assets.Scripts.Controllers.Signals.PowerUp;
using UnityEngine;

namespace Assets.Scripts.Controllers.PowerUpAbsorber
{
	public class PowerUpObsorber : IPowerUpObsorber
	{
		private PowerUpSignal _powerUpSignal;

		private Weapon.Settings _settings;

		public PowerUpObsorber
		(
			PowerUpSignal powerUpSignal
		)
		{
			_powerUpSignal = powerUpSignal;
			_powerUpSignal += OnPowerUp;
		}

		~PowerUpObsorber()
		{
			_powerUpSignal -= OnPowerUp;
		}

		public void Init(Weapon.Settings settings)
		{
			_settings = settings;
		}

		private void OnPowerUp(BasePlayerPowerUpCommand powerUpCommand)
		{
			powerUpCommand.Execute(_settings);

			Debug.LogError("Bullet Damage: " + _settings.BulletDamage + " bullets speed: " + _settings.BulletSpeed + " bullets speed: " + _settings.BulletsCount);

		}
	}
}
