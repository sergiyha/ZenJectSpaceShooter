using System;
using UnityEngine;

namespace Assets.Scripts.UI.Controller
{
	public interface IViewPoolController
	{
		T GetView<T>() where T : BaseView;
	}
}
