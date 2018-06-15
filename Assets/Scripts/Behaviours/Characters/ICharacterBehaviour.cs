using Assets.Scripts.Factories;
using JetBrains.Annotations;

namespace Assets.Scripts.Behaviours.Characters
{
	public interface ICharacterBehaviour
	{
		void Init(MovementBehaviourFactory movementBehaviourFactory, WeaponBehaviourFactory weaponBehaviourFactory);

	}
}
