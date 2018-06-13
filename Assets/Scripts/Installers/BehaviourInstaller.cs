using Assets.Scripts.All_In.Weapons;
using Assets.Scripts.Behaviours;
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
			Container.Bind<MovementBehaviour>().AsTransient();

			Container.BindFactory<EnemyWeaponBehaviour, EnemyWeaponBehaviour.EnemyWeaponBehaviourFactory>();
			Container.BindFactory<PlayerWeaponBehaviour, PlayerWeaponBehaviour.PlayerWeaporBehaviourFactory>();
			Container.Bind<WeaponBehaviourFactory>().AsSingle();

			Container.BindFactory<Weapon, Weapon.WeaponFactory>().FromNewComponentOnNewGameObject();
			Container.BindFactory<Bullet, Bullet.BulletFactory>().FromComponentInNewPrefabResource(PrefabsConfig.BulletPrefabPath).UnderTransform(new GameObject("Bullets").transform);
			//Container.Bind<Weapon>().FromNewComponentSibling();


			//Container.BindFactory<WeaponBehaviour, WeaponBehaviourFactory>().To<PlayerWeaponBehaviour>();
		}
	}
}
