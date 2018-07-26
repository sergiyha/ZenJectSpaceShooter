using Assets.Scripts.All_In.Commands.View;
using Assets.Scripts.GameSignals;
using Zenject;

namespace Assets.Scripts.All_In.Commands
{
	public class CommandBinder
	{
		//Signals
		[Inject] private StartGameSignal _startGameSignal;

		//Commands
		[Inject] private OpenStartViewCommand _startViewCommand;

		[Inject]
		private void Bind()
		{
			_startGameSignal += _startViewCommand.Execute;
		}

		~ CommandBinder()
		{
			_startGameSignal -= _startViewCommand.Execute;
		}
	}
}
