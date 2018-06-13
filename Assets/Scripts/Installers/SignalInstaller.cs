using Assets.Scripts.Inputs.Signals;
using Zenject;

namespace Assets.Scripts.Installers
{
	public class SignalInstaller : Installer<SignalInstaller>
	{
		public override void InstallBindings()
		{

			Container.DeclareSignal<MoveHorizontalSignal>();
			Container.DeclareSignal<MoveVerticalSignal>();
			Container.DeclareSignal<BeginFiringSignal>();
			Container.DeclareSignal<FinishFiringSignal>();

		}
	}
}
