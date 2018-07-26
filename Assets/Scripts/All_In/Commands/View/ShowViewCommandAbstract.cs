using System;
using Assets.Scripts.UI.Controller;
using Zenject;

namespace Assets.Scripts.All_In.Commands.View
{
	public abstract class ShowViewCommandAbstract<T> : ICommand<Type>
	{
		
		public abstract void Execute(Type payload);

	}
}
