using System;
using Assets.Scripts.Behaviours.Movement;
using Assets.Scripts.Enums;
using Zenject;

namespace Assets.Scripts.Factories
{
	public class MovementBehaviourFactory : IFactory<Oponents, IMovementBehaviour>
	{
		[Inject] private EnemyMovementBehaviour.EnemyMovementBehaviourFactory _enemyFactory;
		[Inject] private PlayerMovementBehaviour.PlayerMovementBehaviourFactory _playerFactory;

		public IMovementBehaviour Create(Oponents param)
		{
			IMovementBehaviour movementBehaviour = null;
			switch (param)
			{
				case Oponents.NaN:
					break;
				case Oponents.Player:
					movementBehaviour = _playerFactory.Create();
					break;
				case Oponents.Enemy:
					movementBehaviour = _enemyFactory.Create();
					break;
			}

			return movementBehaviour;
		}
	}
}
