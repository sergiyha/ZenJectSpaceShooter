using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Controllers.Signals;
using Assets.Scripts.UI.Components;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
	public class StartView : DynamicView
	{
		[SerializeField]
		private CounterViewSettings _counterViewSettings;

		public void Start()
		{
			Show();
		}

		[Serializable]
		public class CounterViewSettings
		{
			public List<CounterMapingData> counterData;
		}

		[Serializable]
		public class CounterMapingData
		{
			public int Index;
			public string CountingText;
			public string AdditionalText;
		}




	}

}
