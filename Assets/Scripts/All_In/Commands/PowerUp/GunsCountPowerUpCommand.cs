using Assets.Scripts.All_In.Weapons;
using Zenject;

namespace Assets.Scripts.All_In.Commands.PowerUp
{
	public class GunsCountPowerUpCommand : BasePlayerPowerUpCommand
	{
		[Inject] private Weapon.GunsSetting _gunsCollection;

		public override void Execute(Weapon.Settings payload)
		{
			int gunsCountNeed = payload.Guns.Count + 1;
			var neededGuns = _gunsCollection.GetGunsWithCount(gunsCountNeed);
			payload.Guns = neededGuns;
		}
	}
}
