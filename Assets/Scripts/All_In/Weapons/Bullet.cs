using System.Collections;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using Zenject;

namespace Assets.Scripts.All_In.Weapons
{
	public class Bullet : MonoBehaviour
	{
		private Vector2 _direction;
		private float _speed;

		private bool _isInited = false;

		public void Init(Vector2 direction, float speed)
		{
			_isInited = true;
			_direction = direction.normalized;
			_speed = speed;
		}

		private void Update()
		{
			if (!_isInited) return;
			var position = this.transform.position;
			transform.position = position + (Vector3)_direction * Time.deltaTime * _speed;
		}

		public class BulletFactory : Factory<Bullet>
		{

		}
	}
}
