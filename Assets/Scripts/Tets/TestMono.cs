using Controllers;
using Inputs.Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Test
{
	public class TestMono : MonoBehaviour
	{

		private MovementController movementController;

		[Inject]
		private void Init
		(
			MovementController movementController
		)
		{
			this.movementController = movementController;
			movementController.Init(this.transform);
		}


		private void OnDestroy()
		{

		}



	}
}
