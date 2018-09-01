using Assets.Scripts.All_In.Weapons;

namespace Assets.Scripts.All_In.Commands.PowerUp
{
	public class BulletDamagePowerUpCommand : BasePlayerPowerUpCommand
	{
		public override void Execute(Weapon.Settings payload)
		{
			payload.BulletDamage += (int)(payload.BulletDamage * 0.1f);
		}
	}
}
