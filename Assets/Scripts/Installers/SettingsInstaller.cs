using Assets.Scripts.All_In.Weapons;
using Assets.Scripts.Behaviours;
using Assets.Scripts.ScriptableObjects;
using Zenject;

namespace Assets.Scripts.Installers
{
	public class SettingsInstaller : Installer<SettingsInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<Settings>().FromScriptableObjectResource(Configs.Configs.Scriptables.SettingsPath).AsSingle();
			Container.Bind<MovementBehaviour.Settings>().FromResolveGetter<Settings>(s => s.GetMovementSettings).AsSingle();
			Container.Bind<Weapon.Settings>().FromResolveGetter<Settings>(s => s.GetWeaponSettings).AsSingle();
		}

	}
}
