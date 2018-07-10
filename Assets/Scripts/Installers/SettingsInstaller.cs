using Assets.Scripts.All_In.Weapons;
using Assets.Scripts.Behaviours;
using Assets.Scripts.Behaviours.Characters;
using Assets.Scripts.Behaviours.Movement;
using Assets.Scripts.Controllers;
using Assets.Scripts.ScriptableObjects;
using Zenject;

namespace Assets.Scripts.Installers
{
	public class SettingsInstaller : Installer<SettingsInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<Settings>().FromScriptableObjectResource(Configs.Configs.Scriptables.SettingsPath).AsSingle();
			Container.Bind<PlayerMovementBehaviour.Settings>().FromResolveGetter<Settings>(s => s.GetMovementSettings).AsSingle();
			Container.Bind<Weapon.Settings>().FromResolveGetter<Settings>(s => s.GetWeaponSettings).AsSingle();
			Container.Bind<WavesController.SettingsWrapper>().FromResolveGetter<Settings>(s => s.GetWavesSettings).AsSingle();
			Container.Bind<EnemyBehaviour.EnemiesSettings>().FromResolveGetter<Settings>(s => s.GetEnemiesSettings).AsSingle();
		}

	}
}
