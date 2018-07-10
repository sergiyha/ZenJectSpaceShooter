using Assets.Scripts.All_In.Async;
using Assets.Scripts.Behaviours;
using Assets.Scripts.Controllers;
using Assets.Scripts.Controllers.EnemiesHandler;
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

			Container.Bind<IWavesController>().To<WavesController>().AsSingle().NonLazy();
			Container.Bind<IEnemiesHandler>().To<EnemiesHandler>().AsSingle().NonLazy();
			Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
		}
	}
}
