using UnityEngine;
using System.Collections;

public class ProjectVars : Singleton<ProjectVars> {

    public const int LISTA = 1;
    public const int COLA = 2;
    public const int PILA = 3;
    public int selectedTopic = -1;

    public static ProjectVars Instance
    {
        get
        {
            return ((ProjectVars)mInstance);
        }
        set
        {
            mInstance = value;
        }
    }

    protected ProjectVars() { }
}
