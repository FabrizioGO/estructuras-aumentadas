using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Application.platform == RuntimePlatform.Android) {
                AndroidJavaObject activity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")
                    .GetStatic<AndroidJavaObject> ("currentActivity");
                activity.Call<bool> ("moveTaskToBack", true);
		    } else {
			    Application.Quit();
		    }
        }
        
    }
	public void OnStartAR (int id) {
		Debug.Log ("Item: " + id);
		switch (id) {
            case ProjectVars.LISTA:
                ProjectVars.Instance.selectedTopic = ProjectVars.LISTA;
			    break;
            case ProjectVars.COLA:
                ProjectVars.Instance.selectedTopic = ProjectVars.COLA;
                break;
            case ProjectVars.PILA:
                ProjectVars.Instance.selectedTopic = ProjectVars.PILA;
                break;
        }
        SceneManager.LoadScene("About");
    }

    public void OnStartMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
