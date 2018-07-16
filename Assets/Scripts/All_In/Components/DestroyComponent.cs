using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts.All_In.Components
{
	public class DestroyComponent : MonoBehaviour
	{
		private GameObject _objectToDestroy;
		private float _timeToDestroy;
		private Coroutine _destroyCoro;

		public void InitData(GameObject goToDestroy, float timeToDestroy = 0)
		{
			_objectToDestroy = goToDestroy;
			_timeToDestroy = timeToDestroy;

		}

		private void Start()
		{
			_destroyCoro = StartCoroutine(DestroyGoCoro());
		}

		private IEnumerator DestroyGoCoro()
		{
			yield return new WaitForSeconds(_timeToDestroy);
			Destroy(_objectToDestroy);
		}

		private void OnDestroy()
		{
			if (_destroyCoro != null)
			{
				StopCoroutine(_destroyCoro);
			}
		}

	}
}
