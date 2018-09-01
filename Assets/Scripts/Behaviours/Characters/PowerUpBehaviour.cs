using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Behaviours.Characters
{
	public class PowerUpBehaviour : MonoBehaviour
	{

		public event Action BehaviourTrigered;

		private void OnDestroy()
		{
			BehaviourTrigered = null;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				if (BehaviourTrigered != null) BehaviourTrigered();
				Destroy(this.gameObject);
			}
		}

		public class PowerUpBehaviourFactory : Factory<PowerUpBehaviour>
		{
		}

	}
}

