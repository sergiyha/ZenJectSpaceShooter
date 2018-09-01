using Assets.Scripts.GameSignals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI
{
	public class MainView : MonoBehaviour
	{
		[SerializeField] private Text _wavesCount;
		[SerializeField] private Text _subwavesCount;

		[Inject] private UpdateMainViewSignal _updateViewSignal;


		private void Start()
		{
			_updateViewSignal += UpdateView;
		}

		private void UpdateView(int w, int s)
		{
			_wavesCount.text = "w:" + w;
			_subwavesCount.text = "s:" + s;
		}

		private void OnDestroy()
		{
			_updateViewSignal -= UpdateView;
		}
	}
}
