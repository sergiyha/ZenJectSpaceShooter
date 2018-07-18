using UnityEngine;

namespace Assets.Scripts.UI
{
	public abstract class BaseView : MonoBehaviour, IBaseView<object>
	{
		protected virtual void Show()
		{
			this.gameObject.SetActive(true);
		}

		protected virtual void Hide()
		{
			this.gameObject.SetActive(false );
		}

		public virtual void Init(object payload)
		{
		}
	}
}
