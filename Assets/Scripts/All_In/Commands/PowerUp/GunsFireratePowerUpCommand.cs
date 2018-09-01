using Assets.Scripts.All_In.Weapons;

namespace Assets.Scripts.All_In.Commands.PowerUp
{
	public class GunsFireratePowerUpCommand : BasePlayerPowerUpCommand
	{
		public override void Execute(Weapon.Settings payload)
		{
			payload.BulletsCount += (int)(payload.BulletsCount * 0.1f);
		}
	}
}
