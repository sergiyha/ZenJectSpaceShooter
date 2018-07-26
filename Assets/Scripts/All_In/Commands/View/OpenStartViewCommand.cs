using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.All_In.Commands.View
{
	public class OpenStartViewCommand : ShowViewCommandAbstract
	{
		public override void Execute(object payload)
		{
			var view = _viewPoolController.GetView<StartView>();
			//view.Show();
		}
	}
}
