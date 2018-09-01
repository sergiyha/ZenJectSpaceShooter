using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	public class FinishView : DynamicView
	{
		[SerializeField] private Button _quitButton;
		[SerializeField] private Button _restartButton;

		private void Start()
		{
			_quitButton.onClick.AddListener(OnQuit);
			_restartButton.onClick.AddListener(LoadStartScene);
			Show();
		}

		private void OnQuit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
		}

		private void LoadStartScene()
		{
			SceneManager.LoadScene(0);
		}
	}
}
