using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using Zenject;

namespace Assets.Scripts.Utilities.Editor.Scripts
{
	[ExecuteInEditMode]
	public class EnemyPathCreator : MonoBehaviour
	{
		private Settings _settings;

		[SerializeField] private int TrajectoryIndex = 0;
		private int currentIndex = 0;
		private List<GameObject> _controlpoints;
		private Vector2[] _currTrajectory = null;

		private void Start()
		{
			_settings = Resources.Load<Settings>(Configs.Configs.Scriptables.SettingsPath);
			if (_settings == null) Debug.LogError("settings is null");

			_controlpoints = new List<GameObject>();



		}

		private void Update()
		{

			var trajectorySet = _settings.GetWavesSettings.Trajectories[TrajectoryIndex].Vectors;
			_currTrajectory = trajectorySet;

			if (_controlpoints.Count != trajectorySet.Length || _controlpoints.Any(i => i == null) || currentIndex != TrajectoryIndex)
			{
				_controlpoints.ForEach(DestroyImmediate);
				_controlpoints = new List<GameObject>();
				int index = 0;
				foreach (var vector in trajectorySet)
				{
					index++;
					var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
					go.name = index.ToString();
					_controlpoints.Add(go);
					go.transform.position = vector;
				}
			}
			else
			{
				for (int i = 0; i < _controlpoints.Count; i++)
				{
					_currTrajectory[i] = _controlpoints[i].transform.position;
				}
			}

			currentIndex = TrajectoryIndex;

		}

		private void OnDrawGizmos()
		{
			if (_currTrajectory == null) return;

			for (int i = 0; i < _currTrajectory.Length; i++)
			{
				if (i + 1 >= _currTrajectory.Length) continue;
				Gizmos.color = Color.cyan;
				Gizmos.DrawRay(_currTrajectory[i], _currTrajectory[i + 1] - _currTrajectory[i]);
				if (i == 0)
				{
					Gizmos.DrawSphere(_currTrajectory[0], 0.9f);
				}
			}
		}
	}
}
