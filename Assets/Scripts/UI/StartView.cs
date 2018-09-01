using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Controllers.Signals;
using Assets.Scripts.UI.Components;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI
{
	public class StartView : DynamicView
	{
		[SerializeField]
		private CounterViewSettings _counterViewSettings;
		[SerializeField]
		private Text _top;

		[SerializeField]
		private Text _bot;


		private Dictionary<int, string> countingData = new Dictionary<int, string>()
		{
			{ 1,"Go"},
			{ 2, "Set"},
			{ 3, "Ready"}
		};

		public void Start()
		{
			Show();
			Init();
		}

		public void Init()
		{
			StartCoroutine(StartCoro());
		}

		private IEnumerator StartCoro()
		{
			float t = 1;
			int coounter = 3;
			while (coounter > 0)
			{
				t -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
				if (t <= 0)
				{
					t = 1;

					if (coounter == 1)
					{
						_bot.text = countingData[coounter];
						_top.text = countingData[coounter];
					}
					else
					{
						//Debug.LogError(coounter);
						_bot.text = countingData[coounter];
						_top.text = coounter.ToString();
					}
					coounter--;
				}
			}
			yield return new WaitForSeconds(1);
			Hide();
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
