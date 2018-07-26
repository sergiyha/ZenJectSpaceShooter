using Assets.Scripts.All_In.Commands.View;
using Zenject;

namespace Assets.Scripts.Installers
{
	public class CommandInstaller : Installer<CommandInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<OpenStartViewCommand>().ToSelf().AsSingle().NonLazy();
		}
	}
}
