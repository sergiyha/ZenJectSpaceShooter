using Assets.Scripts.Behaviours;
using Inputs;
using Zenject;

namespace Assets.Scripts.Installers
{
	public class StartupInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<InputHandler>().AsSingle();
			BehaviourInstaller.Install(Container);
			SettingsInstaller.Install(Container);
			SignalInstaller.Install(Container);
		}
	}
}
