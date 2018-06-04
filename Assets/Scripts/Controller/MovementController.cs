using Inputs.Signals;
using System;
using UnityEngine;

namespace Controllers
{
	public class MovementController
	{
		private Settings _settings;
		private Transform transform;
		private bool _isInited;
		private float speed = 100;

		public MovementController
		(
			MoveVerticalSignal ySignal,
			MoveHorizontalSignal xSignal,
			Settings movementSettings
		)
		{
			_settings = movementSettings;
			ySignal += MoveY;
			xSignal += MoveX;
		}

		private void MoveX(float value)
		{
			if (_isInited)
			{
				transform.position = new Vector3(transform.position.x + value * Time.deltaTime * _settings.Speed, transform.position.y, transform.position.z);
				transform.position = new Vector3
				(
					Mathf.Clamp(transform.position.x, _settings.MinMaxX.x, _settings.MinMaxX.y),
					transform.position.y,
					transform.position.z
				);
			}
		}

		private void MoveY(float value)
		{
			if (_isInited)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + value * Time.deltaTime * _settings.Speed, transform.position.z);
				transform.position = new Vector3
				(
					transform.position.x,
					Mathf.Clamp(transform.position.y, _settings.MinMaxY.x, _settings.MinMaxX.y),
					transform.position.z
				);
			}
		}

		public void Init(Transform transform)
		{
			_isInited = true;
			this.transform = transform;
		}

		[Serializable]
		public class Settings
		{
			public Vector2 MinMaxX;
			public Vector2 MinMaxY;
			public float Speed;
		}

	}
}
