using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.All_In.Components.Effects;
using Assets.Scripts.Behaviours.Characters;
using UnityEngine;

[ExecuteInEditMode]
public class EffetSizeSpawnTester : MonoBehaviour
{
	[NonSerialized]
	private HittingEffectComponent.HittingEffects eff;
	// Use this for initialization
	void Start()
	{
		
	}

	void OnDrawGizmos()
	{
		eff = eff ?? Resources.Load<Assets.Scripts.ScriptableObjects.Settings>("Scriptables/Settings").GetEnemiesSettings
			      .GetEnemyData(EnemyType.Ufo).HittingEffects;
		

		float radius = eff.Radius;
		Gizmos.DrawWireSphere(this.gameObject.transform.localPosition, radius);

	}

	void Update()
	{

	}
}
