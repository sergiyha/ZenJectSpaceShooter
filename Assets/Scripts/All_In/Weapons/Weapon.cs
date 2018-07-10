using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.All_In.Weapons
{
	public class Weapon : MonoBehaviour
	{
		//FiringSettings


		private Coroutine _firingCoroutine = null;

		[Inject] private Settings _settings;

		[Inject] private Bullet.BulletFactory _bulletFactory;

		private Oponents _ownerType = Oponents.NaN;

		private void OnDrawGizmos()
		{
			var objPosition = this.transform.position;

			Gizmos.color = Color.red;
			Gizmos.DrawLine((Vector3)_settings.Guns[1].starPos + objPosition, (Vector3)_settings.Guns[1].endPos + objPosition);
			Gizmos.color = Color.blue;
			Gizmos.DrawLine((Vector3)_settings.Guns[0].starPos + objPosition, (Vector3)_settings.Guns[0].endPos + objPosition);

		}

		private void Init(Oponents userType)
		{
			_ownerType = userType;
		}


		public void BeginFire()
		{
			_firingCoroutine = StartCoroutine(FiringCoro());
		}

		private IEnumerator FiringCoro()
		{
			var releaseInterval = _settings.PerSecondsCount / _settings.BulletsCount;
			while (true)
			{
				yield return new WaitForSeconds(releaseInterval);

				var bullet = _bulletFactory.Create();
				var gunIndex = Random.Range(0, _settings.Guns.Count);

				bullet.transform.localPosition = (Vector3)_settings.Guns[gunIndex].starPos + transform.position;
				bullet.Init(_settings.Guns[gunIndex].endPos - _settings.Guns[gunIndex].starPos, _settings.BulletSpeed, _settings.bulletDamage, _ownerType);

				Debug.LogError("Piu");
			}
		}



		public void FinishFire()
		{
			StopCoroutine(_firingCoroutine);
		}


		[Serializable]
		public class Settings
		{
			public int bulletDamage = 10;
			public float BulletSpeed = 100;
			public int BulletsCount = 20;
			public float PerSecondsCount = 4;
			public List<GunPos> Guns;

			[Serializable]
			public struct GunPos
			{
				public Vector2 starPos;
				public Vector2 endPos;

			}
		}

		public class WeaponFactory : Factory<Weapon>
		{

		}
	}
}
