using Assets.Scripts.All_In.Weapons;

namespace Assets.Scripts.All_In.Commands.PowerUp
{
	public class BulletSpeedPowerUpCommand : BasePlayerPowerUpCommand
	{
		public override void Execute(Weapon.Settings payload)
		{
			payload.BulletSpeed += (int)(payload.BulletSpeed * 0.1f);
		}
	}
}
