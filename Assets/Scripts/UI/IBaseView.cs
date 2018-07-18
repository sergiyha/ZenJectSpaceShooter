using Zenject;

namespace Assets.Scripts.UI
{
	public interface IBaseView<T>
		where T : class
	{
		void Init(T payload = null);
	}
}
