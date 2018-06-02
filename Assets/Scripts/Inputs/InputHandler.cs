using Zenject;
using UnityEngine;
using Inputs.Signals;

namespace Inputs
{
	public class InputHandler : ITickable
	{
		private MoveVerticalSignal verticalMovementSignal;
		private MoveHorizontalSignal horizontalMovementSignal;

		private const string HAxis = "Horizontal";
		private const string VAxis = "Vertical";


		public InputHandler
		(
			MoveVerticalSignal verticalMovementSignal,
			MoveHorizontalSignal horizontalMovementSignal
		)
		{
			this.verticalMovementSignal = verticalMovementSignal;
			this.horizontalMovementSignal = horizontalMovementSignal;

		}

		public void Tick()
		{
			var x = Input.GetAxis(HAxis);
			var y = Input.GetAxis(VAxis);

			if (!x.Equals(0))
			{
				horizontalMovementSignal.Fire(x);
			}

			if (!y.Equals(0))
			{
				verticalMovementSignal.Fire(y);
			}
		}
	}
}