using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;


public class NodosDialog : MonoBehaviour {
    public const int INSERTAR = 2;
    public const int ELIMINAR = 1;

    public static NodosDialog Instance = null;
    public Text dialogText;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;
    public Button button8;
    public Button button9;
    public Button button10;
    public Button button11;
    public Button button12;
    public Button button13;
    public Button button14;
    public Button button15;
    public Button button16;
    public Button button17;
    public Button button18;
    public Button button19;
    public Button button20;

    private List<int> numList;

    void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
        //button1.interactable = true;
    }

    public void SetTitle(string title)
    {
        this.dialogText.text = title;
    }

    public void SetList(List<int> numList, int action)
    {
        bool interactable = action == INSERTAR ? true : false;
        button1.interactable = interactable;
        button2.interactable = interactable;
        button3.interactable = interactable;
        button4.interactable = interactable;
        button5.interactable = interactable;
        button6.interactable = interactable;
        button7.interactable = interactable;
        button8.interactable = interactable;
        button9.interactable = interactable;
        button10.interactable = interactable;
        button11.interactable = interactable;
        button12.interactable = interactable;
        button13.interactable = interactable;
        button14.interactable = interactable;
        button15.interactable = interactable;
        button16.interactable = interactable;
        button17.interactable = interactable;
        button18.interactable = interactable;
        button19.interactable = interactable;
        button20.interactable = interactable;

        this.numList = numList;
        foreach (int num in this.numList)
        {
            switch (num)
            {
                case 1:
                    button1.interactable = !interactable;
                    break;
                case 2:
                    button2.interactable = !interactable;
                    break;
                case 3:
                    button3.interactable = !interactable;
                    break;
                case 4:
                    button4.interactable = !interactable;
                    break;
                case 5:
                    button5.interactable = !interactable;
                    break;
                case 6:
                    button6.interactable = !interactable;
                    break;
                case 7:
                    button7.interactable = !interactable;
                    break;
                case 8:
                    button8.interactable = !interactable;
                    break;
                case 9:
                    button9.interactable = !interactable;
                    break;
                case 10:
                    button10.interactable = !interactable;
                    break;
                case 11:
                    button11.interactable = !interactable;
                    break;
                case 12:
                    button12.interactable = !interactable;
                    break;
                case 13:
                    button13.interactable = !interactable;
                    break;
                case 14:
                    button14.interactable = !interactable;
                    break;
                case 15:
                    button15.interactable = !interactable;
                    break;
                case 16:
                    button16.interactable = !interactable;
                    break;
                case 17:
                    button17.interactable = !interactable;
                    break;
                case 18:
                    button18.interactable = !interactable;
                    break;
                case 19:
                    button19.interactable = !interactable;
                    break;
                case 20:
                    button20.interactable = !interactable;
                    break;

            }
        }
        
    }
	
}
