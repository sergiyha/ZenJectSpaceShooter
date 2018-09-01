using Assets.Scripts.All_In.Commands.View;
using Assets.Scripts.GameSignals;
using Zenject;

namespace Assets.Scripts.All_In.Commands
{
	public class CommandBinder
	{
		//Signals
		[Inject] private StartGameSignal _startGameSignal;
		[Inject] private FinishGameSignal _finishSignals;

		//Commands
		[Inject] private OpenStartViewCommand _startViewCommand;
		[Inject] private OpenFinishingViewCommand _finishingCommand;

		[Inject]
		private void Bind()
		{
			_startGameSignal += _startViewCommand.Execute;
			_finishSignals += _finishingCommand.Execute;
		}

		~ CommandBinder()
		{
			_startGameSignal -= _startViewCommand.Execute;
		}
	}
}
