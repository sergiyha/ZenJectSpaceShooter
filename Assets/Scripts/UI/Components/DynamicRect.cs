using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Components
{
	[RequireComponent(typeof(RectTransform))]
	public class DynamicRect : MonoBehaviour, IBaseView<DynamicRect.DynamicRectSettings>
	{


		public event Action OnFinished;

		public Text TextContent;

		private RectTransform _bodyRect;


		[SerializeField]
		private DynamicRectSettings _settings;

		public int GetId()
		{
			return _settings.Id;
		}

		public void Init(DynamicRectSettings payload = null)
		{
			//_settings = payload;
			_bodyRect = _bodyRect ?? GetComponent<RectTransform>();
			_originOffsetMin = _bodyRect.offsetMin;
			_originOffsetMax = _bodyRect.offsetMax;
			_originWidth = _bodyRect.rect.width;
			_originHeight = _bodyRect.rect.height;

			StartCoroutine(RightMovement());
		}



		private float _originHeight;
		private float _originWidth;
		private Vector2 _originOffsetMin;
		private Vector2 _originOffsetMax;

		private IEnumerator RightMovement()
		{

			//[left - bottom]
			// [ right - top ]
			//_bodyRect.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(10f, 10f);
			//	_bodyRect.gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(-10f, -10f);

			var topOffset = (1 - _settings.TopInitOffset) * _bodyRect.rect.height;
			var leftOffset = _settings.LeftInitOffset * _bodyRect.rect.width;

			_bodyRect.offsetMin = new Vector2(_bodyRect.offsetMin.x, _bodyRect.offsetMin.y + topOffset);

			Debug.LogError(_bodyRect.offsetMin.x);
			var startPosition = (_bodyRect.offsetMin.x + _originWidth - leftOffset);//Right
			var finishPos = _bodyRect.offsetMax.x;

			float tempTime = 0;
			while (tempTime <= _settings.TweenTime)
			{
				tempTime += Time.deltaTime;

				var smooth = Mathf.Pow(tempTime / _settings.TweenTime, 2) * Mathf.Pow(tempTime / _settings.TweenTime, 2);

				Debug.LogError(smooth);
				_bodyRect.offsetMax = new Vector2(Mathf.Lerp(-startPosition, finishPos, smooth), _bodyRect.offsetMax.y);

				yield return new WaitForEndOfFrame();
			}

			yield return BotMovement();
		}

		private IEnumerator BotMovement()
		{
			var topOffset = (1 - _settings.TopInitOffset) * _originHeight;

			Debug.LogError(_bodyRect.offsetMin.x);
			var startPosition = _originOffsetMin.y + topOffset;//Top
			var finishPos = _originOffsetMin.y;

			float tempTime = 0;
			while (tempTime <= _settings.TweenTime)
			{
				tempTime += Time.deltaTime;

				var smooth = Mathf.Pow(tempTime / _settings.TweenTime, 2) * Mathf.Pow(tempTime / _settings.TweenTime, 2);

				Debug.LogError(smooth);
				_bodyRect.offsetMin = new Vector2(_bodyRect.offsetMin.x, Mathf.Lerp(startPosition, finishPos, smooth));

				yield return new WaitForEndOfFrame();
			}

			if (OnFinished != null) OnFinished();
		}


		[Serializable]
		public class DynamicRectSettings
		{
			public int Id;

			[Range(0, 1)]
			public float TopInitOffset = 0.2f;

			[Range(0, 1)]
			public float LeftInitOffset = 0.2f;

			[Range(0, 1)]
			public float TweenTime = 0.2f;


		}
	}
}
