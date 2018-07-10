using System.Collections;
using Assets.Scripts.Controllers.EnemiesHandler;
using Assets.Scripts.Enums;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using Zenject;

namespace Assets.Scripts.All_In.Weapons
{
	public class Bullet : MonoBehaviour
	{
		private Vector2 _direction;
		private float _speed;
		private int _damage;

		private bool _isInited = false;
		private Oponents _ownerType = Oponents.NaN;
		private FastCollisionDetector _fastCollisionDetector;
		private IEnemiesHandler _enemiesHandler;



		private void HandleTheCollision(GameObject go)
		{
			Debug.LogError(go.name);
			_enemiesHandler.MakeDamage(go, _damage);
			Destroy(this.gameObject);
		}

		[Inject]
		private void Initilization(IEnemiesHandler enemiesHandler)
		{
			_enemiesHandler = enemiesHandler;
		}

		public void Init(Vector2 direction, float speed, int damage, Oponents ownerType)
		{
			_fastCollisionDetector = _fastCollisionDetector ?? this.gameObject.GetComponent<FastCollisionDetector>() ?? this.gameObject.AddComponent<FastCollisionDetector>();
			_fastCollisionDetector.OnCollisionDetected += HandleTheCollision;
			_ownerType = ownerType;
			_isInited = true;
			_direction = direction.normalized;
			_speed = speed;
			_damage = damage;
		}

		private void OnDestroy()
		{
			_fastCollisionDetector.OnCollisionDetected -= HandleTheCollision;
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
