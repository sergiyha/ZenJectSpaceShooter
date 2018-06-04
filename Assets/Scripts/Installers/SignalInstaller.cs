using Inputs.Signals;
using Zenject;

namespace Installers
{
	public class SignalInstaller : Installer<SignalInstaller>
	{
		public override void InstallBindings()
		{
			Container.DeclareSignal<MoveHorizontalSignal>();
			Container.DeclareSignal<MoveVerticalSignal>();
		}
	}
}
