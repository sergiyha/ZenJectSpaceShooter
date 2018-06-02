using Inputs.Signals;
using UnityEngine;

namespace Controllers
{
	public class MovementController
	{
		private Transform transform;
		private bool _isInited;
		private float speed = 100;

		public MovementController
		(
			MoveVerticalSignal ySignal,
			MoveHorizontalSignal xSignal
		)
		{
			ySignal += MoveY;
			xSignal += MoveX;
		}

		private void MoveX(float value)
		{
			transform.position = new Vector3(transform.position.x + value * Time.deltaTime * speed, transform.position.y);
		}

		private void MoveY(float value)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + value * Time.deltaTime * speed);
		}

		public void Init(Transform transform)
		{
			_isInited = true;
			this.transform = transform;
		}
	}
}
