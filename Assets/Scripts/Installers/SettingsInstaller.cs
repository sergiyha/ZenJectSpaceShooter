using Zenject;
using ScriptableObjects;
using Controllers;

namespace Installers
{
	public class SettingsInstaller : Installer<SettingsInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<Settings>().FromScriptableObjectResource(Configs.Configs.Scriptables.SettingsPath).AsSingle();
			Container.Bind<MovementController.Settings>().FromResolveGetter<Settings>(s => s.GetMovementSettings).AsSingle();
		}

	}
}
