using System;
using System.Collections.Generic;
using Assets.Scripts.Installers;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Controller
{
	public class ViewPoolController : IViewPoolController
	{
		[Inject] private UiInstaller _uiInstaller;

		private Dictionary<Type, BaseView> _views;
		public Dictionary<Type, BaseView> ViewsList
		{
			get { return _views = _views ?? new Dictionary<Type, BaseView>(); }
		}

		public T GetView<T>() where T : BaseView
		{
			var type = typeof(T);

			return (ViewsList.ContainsKey(type) ? ViewsList[type] : CacheViewAndGet<T>()) as T;
		}

		private T CacheViewAndGet<T>() where T : BaseView
		{
			var type = typeof(T);
			var typeName = type.Name;
			var loadedView = UnityEngine.Object.Instantiate(
				Resources.Load<T>(Configs.Configs.Prefabs.UIMainPath + typeName),
				_uiInstaller.MainCanvas.transform);
			ViewsList.Add(type, loadedView);
			return loadedView;
		}

	}
}
