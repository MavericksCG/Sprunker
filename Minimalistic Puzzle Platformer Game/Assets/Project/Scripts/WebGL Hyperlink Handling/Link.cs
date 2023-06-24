using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour {

	public void OpenRedditWebGL() {
		#if !UNITY_EDITOR
		openWindow("https://www.reddit.com/user/DankMavericks/");
		#endif
	}
	
	public void OpenYouTubeWebGL() {
		#if !UNITY_EDITOR
		openWindow("https://www.youtube.com/channel/UC-GC41tCMv0TkDx0zddTK7w");
		#endif
	}

	public void OpenGithubWebGL() {
		#if !UNITY_EDITOR
		openWindow("https://github.com/MavericksCG");
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}