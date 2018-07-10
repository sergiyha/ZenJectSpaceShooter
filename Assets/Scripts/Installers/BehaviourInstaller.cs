using Assets.Scripts.All_In.Weapons;
using Assets.Scripts.Behaviours.Characters;
using Assets.Scripts.Behaviours.Movement;
using Assets.Scripts.Behaviours.Weapons;
using Assets.Scripts.Factories;
using UnityEngine;
using Zenject;
using PrefabsConfig = Assets.Scripts.Configs.Configs.Prefabs;

namespace Assets.Scripts.Installers
{
	public class BehaviourInstaller : Installer<BehaviourInstaller>
	{

		public override void InstallBindings()
		{
			Container.Bind<PlayerMovementBehaviour>().AsTransient();

			//bind weapons
			Container.BindFactory<EnemyWeaponBehaviour, EnemyWeaponBehaviour.EnemyWeaponBehaviourFactory>();
			Container.BindFactory<PlayerWeaponBehaviour, PlayerWeaponBehaviour.PlayerWeaporBehaviourFactory>();

			//bind movements
			Container.BindFactory<PlayerMovementBehaviour, PlayerMovementBehaviour.PlayerMovementBehaviourFactory>();
			Container.BindFactory<EnemyMovementBehaviour, EnemyMovementBehaviour.EnemyMovementBehaviourFactory>();


			Container.Bind<WeaponBehaviourFactory>().AsSingle();
			Container.Bind<MovementBehaviourFactory>().AsSingle();



			Container.BindFactory<Weapon, Weapon.WeaponFactory>().FromNewComponentOnNewGameObject();
			Container.BindFactory<Bullet, Bullet.BulletFactory>().FromComponentInNewPrefabResource(PrefabsConfig.BulletPrefabPath).UnderTransform(new GameObject("Bullets").transform);

			Container.BindFactory<EnemyBehaviour, EnemyBehaviour.EnemyBehaviourFactory>().FromNewComponentOnNewGameObject();
		}
	}
}
