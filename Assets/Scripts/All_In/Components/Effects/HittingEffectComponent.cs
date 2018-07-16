using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.All_In.Components.Effects
{
	public class HittingEffectComponent : MonoBehaviour
	{

		private HittingEffects _data;
		private List<ParticleSystem> _particles;

		[Serializable]
		public class HittingEffects
		{
			public float Radius;
			public List<ParticleSystem> HittingParticles;
		}

		public void SetUp(HittingEffects data)
		{
			_data = data;

			_particles = new List<ParticleSystem>();
			foreach (var particle in data.HittingParticles)
			{
				_particles.Add(Instantiate(particle, this.transform));
			}
		}

		public void FireEffect()
		{
			var positionToSpawn = new Vector3(Random.Range(0 - _data.Radius, _data.Radius), Random.Range(0 - _data.Radius, _data.Radius),-2);

			var objectToShow = _particles[Random.Range(0, _particles.Count)];
			objectToShow.transform.localPosition = positionToSpawn;
			objectToShow.Play();
		}
	}
}
