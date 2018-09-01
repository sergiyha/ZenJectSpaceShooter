using Assets.Scripts.All_In.Weapons;

namespace Assets.Scripts.All_In.Commands.PowerUp
{
	public abstract class BasePlayerPowerUpCommand : ICommand<Weapon.Settings>
	{
	

		public abstract void Execute(Weapon.Settings payload);
	}
}
