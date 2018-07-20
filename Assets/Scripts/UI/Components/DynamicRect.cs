using System;
using UnityEngine;

namespace Assets.Scripts.UI.Components
{
	[RequireComponent(typeof(RectTransform))]
	public class DynamicRect : MonoBehaviour, IBaseView<DynamicRect.DynamicRectSettings>
	{
		public event Action OnFinished;

		private RectTransform _bodyRect;



		public void Init(DynamicRectSettings payload = null)
		{
			_bodyRect = _bodyRect ?? GetComponent<RectTransform>();
		}


		public class DynamicRectSettings
		{
		}
	}
}
