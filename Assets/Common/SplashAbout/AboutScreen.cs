/*===============================================================================
Copyright (c) 2015-2016 PTC Inc. All Rights Reserved.
 
Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class AboutScreen : MonoBehaviour
{
    public Text title;
    public Text aboutText;
    private int topic;

    #region PUBLIC_METHODS
    public void OnStartAR()
    {
        Debug.Log("Starttt");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
    #endregion // PUBLIC_METHODS


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        topic = ProjectVars.Instance.selectedTopic;
        switch (topic)
        {
            case ProjectVars.LISTA:
                title.text = "Listas";
                aboutText.text = getListaLeccionText();
                break;

            case ProjectVars.COLA:
                title.text = "Colas";
                aboutText.text = getColaLeccionText();
                break;

            case ProjectVars.PILA:
                title.text = "Pilas";
                aboutText.text = getPilaLeccionText();
                break;

        }
       
    }

    string getTextFromFile(string path)
    {
        TextAsset topic = Resources.Load<TextAsset>(path);
        if (topic != null)
        {
            using (StreamReader reader = new StreamReader(new MemoryStream(topic.bytes)))
            {
                string text = reader.ReadToEnd();
                reader.Close();
                return text;
            }
        }
        return null;
        
    }

    string getListaLeccionText()
    {
        //string path = "AboutScreen/lista.txt";
        string string1 = Codigos.ListaLeccion.introduccion;
        string1 += "\n\n<b><size=28>Estrucura</size></b>\n";
        string1 += Codigos.estructura;
        string1 += "\n\n<b><size=28>Declaracion</size></b>\n";
        string1 += Codigos.declaracion;
        string1 += "\n\n<b><size=28>Inicializacion</size></b>\n";
        string1 += Codigos.insertarInicio;
        string1 += "\n\n";
        return string1;
    }

    string getPilaLeccionText()
    {
        string path = "Assets/Resources/AboutScreen/pila.txt";
        return getTextFromFile(path);
    }

    string getColaLeccionText()
    {
        string path = "Assets/Resources/AboutScreen/cola.txt";
        return getTextFromFile(path);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            // Treat 'Return' key as pressing the Close button and dismiss the About Screen
            OnStartAR();
        }
        else if (Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            // Similar to above except detecting the first Joystick button
            // Allows external controllers to dismiss the About Screen
            // On an ODG R7 this is the select button
            OnStartAR();
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
            // On Android, the Back button is mapped to the Esc key
            Application.Quit();
#endif
        }
    }
    #endregion // MONOBEHAVIOUR_METHODS
}