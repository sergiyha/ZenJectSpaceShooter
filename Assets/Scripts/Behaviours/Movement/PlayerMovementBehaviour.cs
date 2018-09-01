using System;
using Assets.Scripts.Behaviours.Weapons;
using Assets.Scripts.GameSignals;
using Assets.Scripts.Inputs.Signals;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Behaviours.Movement
{
	public class PlayerMovementBehaviour : IMovementBehaviour
	{
		private Settings _settings;
		private Transform transform;
		private bool _isInited;

		public PlayerMovementBehaviour
		(
			MoveVerticalSignal ySignal,
			MoveHorizontalSignal xSignal,
			Settings movementSettings,
			FinishGameSignal finishSignal
		)
		{
			_settings = movementSettings;
			ySignal += MoveY;
			xSignal += MoveX;

			finishSignal += (o) =>
			{
				ySignal -= MoveY;
				xSignal -= MoveX;
			};
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

		public void PerformToMove(object payload)
		{
			//throw new NotImplementedException();
		}


		[Serializable]
		public class Settings
		{
			public Vector2 MinMaxX;
			public Vector2 MinMaxY;
			public float Speed;
		}

		public class PlayerMovementBehaviourFactory : Factory<PlayerMovementBehaviour>
		{
		}
	}
}
