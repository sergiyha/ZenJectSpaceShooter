using UnityEngine;
using Zenject;

namespace Assets.Scripts.Behaviours.Movement
{
	public class EnemyMovementBehaviour : IMovementBehaviour
	{
		public void Init(Transform transform)
		{
			
		}

	public class EnemyMovementBehaviourFactory : Factory<EnemyMovementBehaviour>
	{
	}

	}
}
