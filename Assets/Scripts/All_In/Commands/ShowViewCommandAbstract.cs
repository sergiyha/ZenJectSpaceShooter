using System;
using Assets.Scripts.UI.Controller;
using Zenject;

namespace Assets.Scripts.All_In.Commands
{
	public abstract class ShowViewCommandAbstract : ICommand<object>
	{
		[Inject]
		protected IViewPoolController _viewPoolController;

		public abstract void Execute(object payload);

	}
}
