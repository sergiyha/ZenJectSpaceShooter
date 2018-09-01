using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI.Components;
using UnityEngine;

namespace Assets.Scripts.UI
{
	public class DynamicView : BaseView
	{
		[SerializeField]
		private List<WordDynamicRectMap> _content;

		[SerializeField]
		private DynamicViewSettings _settings;

		private void InitRects()
		{
			foreach (var item in _content)
			{
				item.DynamicRect.Init();

				var item1 = item;
				Action onFinished = () => InitText(item1);
				Action unsubscriber = () => { item1.DynamicRect.OnFinished -= onFinished; };
				item1.DynamicRect.OnFinished += onFinished + unsubscriber;
			}
		}

		public void InitText(WordDynamicRectMap rect)
		{
			StartCoroutine(InitTextCoroutine(rect));
		}



		private IEnumerator InitTextCoroutine(WordDynamicRectMap rectData, string customString = null, Action onFinish = null)
		{
			string textToFill = (string.IsNullOrEmpty(customString)) ? rectData.Text : customString;
			for (int i = 0; i < textToFill.Length; i++)
			{
				yield return new WaitForSeconds(_settings.WordAppearigInterwal);
				var rect = rectData.DynamicRect;
				var textContent = rect.TextContent;

				if (i > 0)
					textContent.text = textContent.text.Substring(0, textContent.text.Length - 1);
				textContent.text += textToFill[i];
				textContent.text = (textToFill.Length - 1 == i) ? textContent.text : textContent.text + "_";
			}
			if (onFinish != null)
			{
				onFinish();
			}
		}

		public void InitWord(int index, string value)
		{
			ResetWord(_content[index], () =>
			{
				StartCoroutine(InitTextCoroutine(_content[index], value));
			});
		}

		public void ResetWord(WordDynamicRectMap data, Action onFinish = null)
		{
			StartCoroutine(ResetWordCoroutine(data, onFinish));
		}

		private IEnumerator ResetWordCoroutine(WordDynamicRectMap data, Action onFinish = null)
		{
			var textComponent = data.DynamicRect.TextContent;
			for (int i = textComponent.text.Length - 1; i >= 0; i--)
			{
				yield return new WaitForSeconds(_settings.WordDissapearingInterval);
				textComponent.text = textComponent.text.Substring(0, i);

			}

			if (onFinish != null)
			{
				onFinish();
			}
		}

		public override void Show()
		{
			base.Show();
			//InitRects();
		}

		[Serializable]
		public class WordDynamicRectMap
		{
			public string Text;
			public DynamicRect DynamicRect;
		}

		[Serializable]
		public class DynamicViewSettings
		{
			[Range(0f, 0.1f)]
			public float WordAppearigInterwal;

			[Range(0f, 0.1f)]
			public float WordDissapearingInterval;

			public float TimeBeforeWordReseting;
		}
	}
}
