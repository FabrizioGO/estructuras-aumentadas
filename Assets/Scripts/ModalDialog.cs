using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;


public class ModalDialog : MonoBehaviour {
    public static ModalDialog Instance = null;
    public Text dialogText;
    public Button confirmBtn;
    //public Button cancelBtn;
    public Text confirmText;

    void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void SetDialog(DialogObject d)
    {
        dialogText.text = d.dText;
        confirmText.text = d.confirmText;
        //confirmBtn.onClick.RemoveAllListeners();
        //confirmBtn.onClick.AddListener(d.confirmEvent.Invoke);
    }
	
}

[System.Serializable]
public class DialogObject
{
    public string dText;
    public string confirmText;
    public UnityEvent confirmEvent;

   public DialogObject(string dText, string confirmText, UnityEvent confirmEvent){
        this.dText = dText;
        this.confirmText = confirmText;
        this.confirmEvent = confirmEvent;
    }
}
