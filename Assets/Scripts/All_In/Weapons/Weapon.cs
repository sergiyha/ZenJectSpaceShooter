using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.All_In.Weapons
{
	[Serializable]
	public class Weapon : MonoBehaviour
	{
		//FiringSettings


		private Coroutine _firingCoroutine = null;



		[Inject] private Bullet.BulletFactory _bulletFactory;

		[SerializeField]
		private Settings _settings;

		private Oponents _ownerType = Oponents.NaN;

		private void OnDrawGizmos()
		{
			if (_settings == null) return;
			var objPosition = this.transform.position;

			Gizmos.color = Color.red;
			Gizmos.DrawLine((Vector3)_settings.Guns[1].starPos + objPosition, (Vector3)_settings.Guns[1].endPos + objPosition);
			Gizmos.color = Color.blue;
			Gizmos.DrawLine((Vector3)_settings.Guns[0].starPos + objPosition, (Vector3)_settings.Guns[0].endPos + objPosition);

		}

		public void Init(Settings settings)
		{
			_settings = settings;
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
				bullet.Init(_settings.Guns[gunIndex].endPos - _settings.Guns[gunIndex].starPos, _settings.BulletSpeed, _settings.BulletDamage, _ownerType);

				//Debug.LogError("Piu");
			}
		}

		public void FinishFire()
		{
			StopCoroutine(_firingCoroutine);
		}

		[Serializable]
		public class GunsSetting
		{
			public List<GunsSettingsContainer> GunsVariations;

			public List<Settings.GunPos> GetGunsWithCount(int gunsCount)
			{
				return GunsVariations.Find(g => g.GunsContainer.Count == gunsCount).GunsContainer;
			}

			[Serializable]
			public class GunsSettingsContainer
			{
				public List<Settings.GunPos> GunsContainer;
			}
		}

		[Serializable]
		public class Settings : ICloneable<Settings>
		{
			public int BulletDamage = 10;
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

			public Settings Clone()
			{
				return new Settings()
				{
					BulletDamage = this.BulletDamage,
					BulletSpeed = this.BulletSpeed,
					BulletsCount = this.BulletsCount,
					PerSecondsCount = this.PerSecondsCount,
					Guns = new List<GunPos>(this.Guns),
				};
			}

			object ICloneable.Clone()
			{
				return Clone();
			}
		}

		public interface ICloneable<T> : ICloneable
		{
			new T Clone();
		}

		public class WeaponFactory : Factory<Weapon>
		{

		}
	}
}
