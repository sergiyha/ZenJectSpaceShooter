using Assets.Scripts.Controllers.Signals;
using Assets.Scripts.Controllers.Signals.PowerUp;
using Assets.Scripts.GameSignals;
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
			Container.DeclareSignal<StartGameSignal>();
			Container.DeclareSignal<FinishGameSignal>();
			Container.DeclareSignal<EnemyKilledWithGunSignal>();
			Container.DeclareSignal<UpdateMainViewSignal>();
			Container.DeclareSignal<EnemyWasDestroyedSignal>();
			Container.DeclareSignal<PowerUpSignal>();
		}
	}
}
