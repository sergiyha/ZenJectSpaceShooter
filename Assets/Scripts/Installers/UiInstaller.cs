using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
	public class UiInstaller : MonoInstaller<UiInstaller>
	{
		public Canvas MainCanvas;

		public override void InstallBindings()
		{
			Container.Bind<UiInstaller>().FromInstance(this).AsSingle();
		}
	}
}
