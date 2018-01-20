/*===============================================================================
Copyright (c) 2015-2016 PTC Inc. All Rights Reserved.
 
Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour 
{
    #region PRIVATE_MEMBER_VARIABLES
    private bool mChangeLevel = true;
    private RawImage mUISpinner;
    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS
    void Start () 
    {
        mUISpinner = FindSpinnerImage();
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        mChangeLevel = true;
    }
    
    void Update () 
    {
        mUISpinner.rectTransform.Rotate(Vector3.forward, 90.0f * Time.deltaTime);

        if (mChangeLevel)
        {
            LoadNextSceneAsync();
            mChangeLevel = false;
        }
    }
    #endregion // MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS

    private void LoadNextSceneAsync()
    {
        int topic = ProjectVars.Instance.selectedTopic;
        if (topic != -1)
        {
            switch (topic)
            {
                case ProjectVars.LISTA:
                    SceneManager.LoadSceneAsync("Lista");
                    break;
                case ProjectVars.COLA:
                    SceneManager.LoadSceneAsync("Cola");
                    break;
                case ProjectVars.PILA:
                    SceneManager.LoadSceneAsync("Pila");
                    break;
            }
        }
           
    }

    private RawImage FindSpinnerImage()
    {
        RawImage[] images = FindObjectsOfType<RawImage>();
        foreach (var img in images)
        {
            if (img.name.Contains("Spinner"))
            {
                return img;
            }
        }
        return null;
    }
    #endregion // PRIVATE_METHODS
}
