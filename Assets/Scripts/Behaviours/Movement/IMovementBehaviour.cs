using UnityEngine;

namespace Assets.Scripts.Behaviours.Movement
{
	public interface IMovementBehaviour 
	{
		void Init(Transform transform);
		void PerformToMove(object payload);
	}
};
