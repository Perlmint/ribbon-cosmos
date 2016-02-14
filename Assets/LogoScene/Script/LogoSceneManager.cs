using UnityEngine;
using System.Collections;

public class LogoSceneManager : MonoBehaviour
{
	public GameObject NextScene;

	public void AnimationEnded()
	{
		Application.LoadLevel(Constants.StageSelectSceneName);
	}
}