using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
	public class Settings : ScriptableObject
	{
		public MovementController.Settings MovementSettings;

		public MovementController.Settings GetMovementSettings
		{
			get { return MovementSettings; }
		}
	}
}
