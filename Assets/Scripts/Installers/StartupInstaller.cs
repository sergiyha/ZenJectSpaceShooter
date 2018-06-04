using Controllers;
using Inputs;
using Zenject;

namespace Installers
{
	public class StartupInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<InputHandler>().AsSingle();
			Container.Bind<MovementController>().AsTransient();
			SettingsInstaller.Install(Container);
			SignalInstaller.Install(Container);
		}
	}
}
