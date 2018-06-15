using Assets.Scripts.Inputs.Signals;
using Zenject;
using UnityEngine;

namespace Inputs
{
	public class InputHandler : ITickable
	{
		private MoveVerticalSignal _verticalMovementSignal;
		private MoveHorizontalSignal _horizontalMovementSignal;
		private BeginFiringSignal _beginFiringSignal;
		private FinishFiringSignal _finishFiringSignal;

		private const string HAxis = "Horizontal";
		private const string VAxis = "Vertical";
	
		private bool _fireStarted;

		public InputHandler
		(
			MoveVerticalSignal verticalMovementSignal,
			MoveHorizontalSignal horizontalMovementSignal,
			BeginFiringSignal beginFiringSignal,
			FinishFiringSignal finishFiringSignal
		)
		{
			_beginFiringSignal = beginFiringSignal;
			_finishFiringSignal = finishFiringSignal;

			_verticalMovementSignal = verticalMovementSignal;
			_horizontalMovementSignal = horizontalMovementSignal;
		}

		private void SetFireState(bool val)
		{
			_fireStarted = val;
		}

		public void Tick()
		{
			HandleAxis();
			HandleFires();
		}

		private void HandleAxis()
		{
			var x = Input.GetAxis(HAxis);
			var y = Input.GetAxis(VAxis);

			if (!x.Equals(0))
			{
				_horizontalMovementSignal.Fire(x);
			}

			if (!y.Equals(0))
			{
				_verticalMovementSignal.Fire(y);
			}
		}

		private void HandleFires()
		{
			if (Input.GetKeyDown(KeyCode.Q) && !_fireStarted)
			{
				_beginFiringSignal.Fire();
				SetFireState(true);
			}

			if (Input.GetKeyUp(KeyCode.Q))
			{
				_finishFiringSignal.Fire();
				SetFireState(false);
			}
		}
	}
}