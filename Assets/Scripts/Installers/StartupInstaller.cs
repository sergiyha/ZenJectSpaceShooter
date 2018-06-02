using Controllers;
using Inputs;
using Zenject;

public class StartupInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.BindInterfacesTo<InputHandler>().AsSingle();
		Container.Bind<MovementController>().AsTransient();
		SignalInstaller.Install(Container);
	}
}
