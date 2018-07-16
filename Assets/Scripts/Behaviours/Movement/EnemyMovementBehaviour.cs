﻿using Assets.Scripts.All_In.Async;
using Assets.Scripts.Controllers;
using Assets.Scripts.Controllers.EnemiesHandler;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Behaviours.Movement
{
	public class EnemyMovementBehaviour : IMovementBehaviour
	{
		[Inject] private IEnemiesHandler _enemyHandler;
		[Inject] private IWavesController WavesController;
		[Inject] private AsyncProcessor _asyncProcessor;
		private Transform _mainTransform;

		public void Init(Transform transform)
		{
			_mainTransform = transform;
		}

		private void DestroyEnemyOnFinishMovement()
		{
			_enemyHandler.DestroyEnemy(_mainTransform.gameObject);
		}

		public void PerformToMove(object payload)
		{
			var isTrajectoryData = payload is Vector2[];
			if (isTrajectoryData)
			{
				var vectorsTrajectory = (Vector2[])payload;
				var totalPathLength = 0;
				Vector2 direction = Vector2.zero;

				_mainTransform.transform.position = vectorsTrajectory[0];

				var sequence = DOTween.Sequence();
				sequence.OnComplete(DestroyEnemyOnFinishMovement);

				for (int i = 0; i < vectorsTrajectory.Length; i++)
				{
					if (i + 1 == vectorsTrajectory.Length)
						break;


					var distance = Vector2.Distance(vectorsTrajectory[i], vectorsTrajectory[i + 1]);
					var timetoMove = distance / WavesController.GetCurrentWaveSetting().EnemySpeed;

					var tween = _mainTransform.DOMove(vectorsTrajectory[i + 1], timetoMove).SetEase(Ease.Linear);



					sequence.Append(tween);

				}

				sequence.Play();

			}
		}

		//private IEnumerator Move((Vector2[] trajectory)
		//{

		//}


		public class EnemyMovementBehaviourFactory : Factory<EnemyMovementBehaviour>
		{
		}

	}
}

