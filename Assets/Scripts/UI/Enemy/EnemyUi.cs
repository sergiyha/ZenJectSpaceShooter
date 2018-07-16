using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Enemy
{
	
	public class EnemyUi : MonoBehaviour
	{
		[SerializeField] private Image _healthBar;

		[SerializeField] private HealthBarLimits _healthLimis;

		private float _healthReal = 1f;

		private void Start()
		{
			_healthBar.color = _healthLimis.StartColor;
		}

		public void UpdateHealthBar(float value)
		{
			_healthBar.color = _healthLimis.GetLimitColor(value);
			_healthReal = value;
		}

		private void CheckHealthBar()
		{
			if (!(Math.Abs(_healthReal - _healthBar.fillAmount) > 0.01)) return;
			if (_healthReal > _healthBar.fillAmount)
				_healthBar.fillAmount += Time.fixedDeltaTime * Math.Abs(_healthReal - _healthBar.fillAmount) * 10;
			else
			{
				_healthBar.fillAmount -= Time.fixedDeltaTime * Math.Abs(_healthReal - _healthBar.fillAmount) * 10;

			}

		}

		void Update()
		{
			CheckHealthBar();
		}

		[Serializable]
		public class HealthBarLimits
		{
			public Color StartColor;
			public HealtBarLimit[] Limits;
			public Color GetLimitColor(float value)
			{
				for (int i = 0; i < Limits.Length; i++)
				{
					if (value > Limits[i].StartsFrom)
					{
						return Limits[i].Color;
					}
				}
				return Color.white;
			}
		}

		[Serializable]
		public class HealtBarLimit
		{
			public float StartsFrom;
			public Color Color;
		}

	}
}
