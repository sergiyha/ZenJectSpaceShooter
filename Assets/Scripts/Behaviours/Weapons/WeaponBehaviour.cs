using UnityEngine;
using Zenject;

namespace Assets.Scripts.Behaviours.Weapons
{
	public abstract class WeaponBehaviour : IWeaponBehaviour
	{
		protected abstract void OnFireBegin();
		protected abstract void OnFireFinish();



		public abstract void Init(Transform transform);

	}

	public interface IWeaponBehaviour
	{
		void Init(Transform transform);
	}
}
