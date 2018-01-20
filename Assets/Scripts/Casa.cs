using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Casa : MonoBehaviour, MovimientoPuntero.onPunteroMoved, Codigos.ICodigo {

    //Constantes
    private const int MAX_NODOS = 20;

	private const int ELIMINAR = 1;
	private const int INSERTAR = 2;
	private const int NUEVO = 3;

	private const int TIPO_LISTA = 11;
	private const int TIPO_COLA = 12;
	private const int TIPO_PILA = 13;

    //Tiempo de espera antes de eliminar un nodo
	private const float deleteDelay = 1.5f;
    //Tiempo de espero antes de destruir GameObject de nodo
	private const float destroyDelay = 0.4f;

    //Action a realizar
	private int action;
    //Tipo de estructura
    private int mTipo;
    //Leccion seleccionada
    private int topic;
    //Ultimo dato ingresado 
    private int dato;

    //Lista de nodos como GameObject
	private List<GameObject> goList;
    //Lista de ID's de nodos
	public List<int> numList;
	
    //GameObject del puntero
	private GameObject mPuntero;
    //Registro de numeros agregados en la lista para evitar repetidos
	private bool[] numAgregados;
    //Coroutine ejecutandose
	private bool isCoroutineExecuting = false;
    //Esta esperando que termine una accion
	private bool isWaiting = false;

    public GameObject Puntero;
    public GameObject nodo;
    public GameObject parent;
    public GameObject arrow;
    public Material primeroMaterial;
    public Material segundoMaterial;
    public Transform arrowContent;
    public Transform nodoContent;
    public Transform punteroContent;
	public Text cantidad;
	public Text codigo;
    public Text codigo2;

	#region MONOBEHAVIOUR_METHODS
	// Use this for initialization
	void Start () {
        //Forzar orientacion Landscape
        Screen.orientation = ScreenOrientation.LandscapeRight;
        ShowStartDialog();
		goList = new List<GameObject>();
		numList = new List<int> ();
		numAgregados = new bool[MAX_NODOS];
        topic = ProjectVars.Instance.selectedTopic;
	}

	// Update is called once per frame
	void Update () {
		
	}
	#endregion

	#region INTERFACE
	//Callback que indica cuando el puntero llego al objetivo
	bool MovimientoPuntero.onPunteroMoved.isPunteroMoved(bool moved){
		isWaiting = true;
		StartCoroutine (WaitSelectedSeconds (deleteDelay,action));
		return moved;
	}
    //Callback para escribir codigo en la pantalla
    void Codigos.ICodigo.onWriteCode(int listIndex, string codigo)
    {
        if (listIndex != -1)
        {
            if (listIndex < numList.Count)
            {
                int numAtIndex = numList.ElementAt(listIndex);
                string text = dato + codigo + numAtIndex;
                codigo2.text = dato <= numAtIndex ? Codigos.getSuccessString(text) : Codigos.getFailString(text);
            }
            else
            {
                codigo2.text = Codigos.getSuccessString(dato + " es el mayor.") + "\nSe inserta como ultimo de la lista";
            }
            
        }
        else
        {
            codigo2.text = Codigos.getFailString(codigo);
        }
        
    }

	#endregion
		
	#region CREACION_NODO
	//Creacion de una instancia de GameObject para un nodo
	public GameObject crearNodoGO(){
		int nodosCount = goList.Count;
		if (nodosCount < MAX_NODOS) {
			GameObject temp = Instantiate (nodo, nodoContent.position, nodoContent.rotation) as GameObject;
			temp.transform.parent = parent.transform;

			GameObject nArrow = Instantiate (arrow, arrowContent.position, arrowContent.rotation) as GameObject;
			nArrow.transform.parent = temp.transform.GetChild(0);
			return temp;
		}
		return null;
	}

	//Establecer el dato de un nodo
	private int setDatoNodo(){
		int data;
		bool maximo = false;
		//Validar que no hayan datos repetidos
		int c = 0;
		do {
			data = Random.Range (1, MAX_NODOS+1);
			if (c == MAX_NODOS+5){ //Umbral de hasta 5 veces en caso de un random repetido
				maximo = true;
				break;
			}
            c++;
		} while (numAgregados [data-1] == true);
		if (!maximo) {
			numAgregados [data-1] = true;
		} else {
			data = -1;
		}
		return data;
	}
    //Establecer el dato de un nodo a partir del cuadro de Dialog
    private int setDatoNodo(int input)
    {
        int data = input;
        numAgregados[data - 1] = true;
        return data;
    }
    //Escribir el numero del nodo en el TextMesh encima del GameObject
	private void setTextMeshNumber(GameObject temp, int data){
		TextMesh numero = temp.GetComponentInChildren (typeof(TextMesh)) as TextMesh;
		numero.text = data.ToString ();
	}
    //Imprimir la cantidad de nodos en la lista
    private void setTextoCantidad()
    {
        cantidad.text = "Cantidad: " + goList.Count.ToString();
    }
	#endregion

    #region INSERTAR
    //Insertar nodos en la correspodiente estructura dependiendo de su tipo
	public void insertarNodo(int tipo){
        mTipo = tipo;
		if (tipo == TIPO_COLA) {
            codigo.text = goList.Any() ? Codigos.ColasLeccion.insertar : Codigos.insertarInicio;
			insertarSimple ();
		} else if (tipo == TIPO_LISTA) {
            if (!goList.Any())
                codigo.text = Codigos.insertarInicio;
            //nuevoBtnSelected (); //Usar este para valores aleatorios
            action = NUEVO;
            createNodosDialog(INSERTAR);
		} else if (tipo == TIPO_PILA) {
            codigo.text = goList.Any() ? Codigos.PilasLeccion.insertar : Codigos.insertarInicio;
            insertarSimple ();
		}
		Debug.Log (getNumList());
		setMaterials ();
	}

	//Insertar cuando no hay elementos en la estructura (en caso de Lista) o para Colas y Pilas
	private void insertarSimple(){
		dato = setDatoNodo ();
        AgregarNodoEnLista();
	}
    //Insertar desde cuadro de Dialog cuando no hay elementos en la estructura
    private void insertarSimple(int input)
    {
        dato = setDatoNodo(input);
        AgregarNodoEnLista();
    }
    //Agregar nodo en la lista
    private void AgregarNodoEnLista()
    {
        //Lista no ha alcanzado el maximo
        if (dato != -1)
        {
            GameObject temp = crearNodoGO();
            setTextMeshNumber(temp, dato);
            goList.Insert(0, temp);
            numList.Insert(0, dato);
            setTextoCantidad();
            setMaterials();
        }
        else //Lista alcanzo el maximo
        {
            createDialog("Has alcanzado el maximo de nodos para agregar");
        }
    }

    //Insertar nodos de forma ordenada en la estructura de Lista
    public void insertarOrdenado()
    {
        GameObject temp = crearNodoGO();
        setTextMeshNumber(temp, dato);
        if (goList.Any())
        {
            int index = buscarOrdenado(dato);
            Debug.Log("index = " + index);
            if (index != -1)
            {
                adelantarNodos(index);
                goList.Insert(index, temp);
                numList.Insert(index, dato);
                MovimientoNodo movNod = (MovimientoNodo)temp.GetComponent(typeof(MovimientoNodo));
                movNod.MoveFromAside(index);
                codigo.text = Codigos.ListaLeccion.insertarOrdenadoFinal;
                codigo2.text = Codigos.ListaLeccion.insertarOrdenadoFinal2(numList.ElementAt(index).ToString(), index + 1 == numList.Count ? "<color=#E64A4AFF>null</color>" : numList.ElementAt(index + 1).ToString());
                setTextoCantidad();
                setMaterials();
                Destroy(mPuntero);
            }
        }
        else
        {
            goList.Add(temp);
            numList.Add(dato);
        }
    }
    #endregion

    #region BUSQUEDA
    //Buscar indice de un nodo a partir de su dato
    private int buscarNodo(int dato)
    {
        int index = -1;
        for (int i = 0; i < numList.Count; i++)
        {
            if (numList.ElementAt(i) == dato)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    private int buscarOrdenado(int num)
    {
        int index = -1;
        int listCount = numList.Count;
        if (dato > numList.ElementAt(listCount - 1))
        {
            index = listCount;
        }
        else
        {
            for (int i = 0; i < listCount; i++)
            {
                index = i;
                if (num < numList.ElementAt(i))
                {
                    break;
                }
            }
        }
        return index;
    }
    #endregion

    #region BOTONES
	//Accion del boton NUEVO
	public void nuevoBtnSelected(){
        action = NUEVO;
		if (goList.Any()){
			if (!isWaiting) {
                dato = setDatoNodo();
                if (dato != -1)
                {
                    codigo.text = "Dato: " + dato;
                    codigo2.text = "";
                    if (goList.Any())
                    {
                        int index = buscarOrdenado(dato);
                        moverPuntero(index == goList.Count ? index : index + 1);
                    }
                    else
                    {
                        insertarOrdenado();
                    }
                }
                else
                {
                    createDialog("Has alcanzado el maximo de nodos para agregar");
                }
			
			}
		}else{
			insertarSimple();
		}
	}

    public void nuevoBtnSelected(int input)
    {
        if (action == NUEVO)
        {
            if (goList.Any())
            {
                if (!isWaiting)
                {
                    dato = setDatoNodo(input);
                    if (dato != -1)
                    {
                        codigo.text = "Dato: " + dato;
                        codigo2.text = "";
                        if (goList.Any())
                        {
                            int index = buscarOrdenado(dato);
                            moverPuntero(/*index == goList.Count ? index : */index + 1);
                        }
                        else
                        {
                            insertarOrdenado();
                        }
                    }
                    else
                    {
                        createDialog("Has alcanzado el maximo de nodos para agregar");
                    }

                }
            }
            else
            {
                insertarSimple(input);
            }
        }
        
    }

    //Accion del boton ELIMINAR
    public void eliminarBtnSelected()
    {
        action = ELIMINAR;
        if (topic == ProjectVars.LISTA)
        {
            if (!isWaiting && goList.Any())
            {
                createNodosDialog(ELIMINAR);
            }
            else
            {
                if (!goList.Any())
                    createDialog("La lista no contiene elementos");
            }
        }
        else
        {
            if (topic == ProjectVars.COLA)
            {
                if (goList.Count > 0)
                {
                    eliminarUltimo();
                    return;
                }
                else
                {
                    createDialog("La lista no contiene elementos");
                }

            }
            if (topic == ProjectVars.PILA)
            {
                if (goList.Any())
                {
                    eliminarPrimero();
                    return;
                }
                else
                {
                    createDialog("La lista no contiene elementos");
                }
            }

        }
    }

	public void eliminarBtnSelected(int input){
        dato = input;
        if (action == ELIMINAR)
        {
            codigo.text = "Eliminar";
            if (topic == ProjectVars.LISTA)
            {
                int index = buscarNodo(input);
                if (index != -1)
                {
                    moverPuntero(index + 1);      //Se agrega +1 porque o sino quedará un nodo detras
                }
            }
        }
	}

    #endregion

    #region ELIMINAR
    //Eliminar ultimo nodo
    private void eliminarUltimo()
    {
        int index = goList.Count - 1;
        eliminarAt(index);
    }

    //Eliminar primer nodo
    private void eliminarPrimero()
    {
        int index = 0;
        eliminarAt(index);
    }

	//Eliminar el nodo encontrado
	private void eliminarNodo(){
		int nodosCount = goList.Count;
		int index = -1;
		for (int i = 0; i < nodosCount; i++) {
			if (numList.ElementAt (i) == dato) {
				index = i;
				dato = index;
				break;
			}
		}
		if (index != -1) {
            eliminarAt(index);
		} else {
			Debug.Log ("Index = -1");
		}
	}

    //Eliminar en la ubicacion señalada
    private void eliminarAt(int index)
    {
        GameObject go = goList.ElementAt(index);
        MovimientoNodo movNod = (MovimientoNodo)go.GetComponent(typeof(MovimientoNodo));
        movNod.MoveDelAside(index);
        Destroy(go, destroyDelay);
        goList.RemoveAt(index);
        numList.RemoveAt(index);
        numAgregados[index] = false;
        Debug.Log(getNumList());
        /*if (data == nodosCount - 1 && goList.Any())
        {
            GameObject antGo = goList.ElementAt(index);
            Destroy(antGo.transform.GetChild(0).FindChild("Arrow(Clone)").gameObject);
        }*/
        setMaterials();
        setTextoCantidad();
        codigo.text = Codigos.ListaLeccion.EliminarFinal;
        retrocederNodos(index);
        Destroy(mPuntero);
        if (NodosDialog.Instance!=null)
            NodosDialog.Instance.SetList(numList,action);
    }
    #endregion

    #region MOVIMIENTO
    //Mover el puntero hacia el nodo indicado antes de realizar la accion
	public void moverPuntero(int index){
		mPuntero = Instantiate (Puntero, punteroContent.position, punteroContent.rotation) as GameObject;
		mPuntero.transform.parent = parent.transform;
		MovimientoPuntero movPun = (MovimientoPuntero)mPuntero.GetComponent (typeof(MovimientoPuntero));
		movPun.Move (this,index,goList.Count,action);
	}

    //Si esta activo, mover hacia adelante todos los nodos en la estructura
    public void moverNodo(bool check)
    {
        if (numList.Count < MAX_NODOS)
        {
            if (check)
            {
                foreach (GameObject go in goList)
                {
                    MovimientoNodo movNod = (MovimientoNodo)go.GetComponent(typeof(MovimientoNodo));
                    movNod.Move();
                }
            }
        }
        
    }

	//Mover los nodos de la lista hacia atras desde del punto indicado
	private void retrocederNodos(int index){
		for (int i = 0; i < goList.Count; i++){
			if (i >= index){
				GameObject go = goList.ElementAt(i);
				MovimientoNodo movNod = (MovimientoNodo)go.GetComponent (typeof(MovimientoNodo));
				movNod.MoveBackward ();
			}
		}
	}

	//Mover los nodos de la lista hacia adelante a partir del punto indicado
	private void adelantarNodos(int index){
		for (int i = 0; i < goList.Count; i++){
			if (i >= index){
				GameObject go = goList.ElementAt(i);
				MovimientoNodo movNod = (MovimientoNodo)go.GetComponent (typeof(MovimientoNodo));
				movNod.Move ();
			}
		}
	}
    #endregion

    #region VALIDACIONES
    /*obtener el valor ingresado en el input
    //De estar vacio o no ser valido, devuelve -1
    private int getInputValue()
    {
        int n = -1;
        string input = inputField.text.ToString();
        if (input.Any())
        {
            int.TryParse(input, out n);
            return n;
        }
        else
        {
            return -1;
        }
    }

    //Valida si el valor del input es valido
    private bool inputIsValid()
    {
        string input = inputField.text.ToString();
        if (input.Any())
        {
            return getInputValue() > 0;
        }
        else
        {
            return false;
        }
    }*/
    #endregion

    #region MISC
    //Establecer colores de los nodos al modificar la estructura
	private void setMaterials(){
		int nodosCount = goList.Count;
		for (int i = 0; i < nodosCount; i++){
			GameObject go = goList.ElementAt(i);
			Renderer renderer = go.GetComponentInChildren<Renderer> ();
			if (renderer != null) {
				if (i == 0)
					renderer.material = primeroMaterial;
				else
					renderer.material = segundoMaterial;
			}
		}
	}

    //Obtener los elementos de estructura como string (Debug)
    private string getNumList()
    {
        string list = "";
        foreach (int num in numList)
        {
            list += " - " + num;
        }
        return list;
    }

	//Esperar la cantidad de segundos indicar antes de realizar la accion
	IEnumerator WaitSelectedSeconds(float seconds, int action){
		if (isCoroutineExecuting)
			yield break;
		isCoroutineExecuting = true;
		yield return new WaitForSeconds (seconds);
        if (action == ELIMINAR)
        {
            if (mTipo == TIPO_COLA)
            {
                eliminarUltimo();
            }
            else if (mTipo == TIPO_PILA)
            {
                eliminarPrimero();
            }
            else if (mTipo == TIPO_LISTA)
            {
                eliminarNodo();
            }
        }
        //else if (action == INSERTAR)
            //insertarNodoAt();
        else if (action == NUEVO)
        {
            if (mTipo == TIPO_COLA)
            {
                insertarSimple();
            }
            else if (mTipo == TIPO_LISTA)
            {
                insertarOrdenado();
            }
            else if (mTipo == TIPO_PILA)
            {
                insertarSimple();
            }
        }
		isCoroutineExecuting = false;
		isWaiting = false;
	}

    public void ShowStartDialog()
    {
        createDialog("Apunta con la camara al tracker para iniciar","Entendido");
    }

	//Salir de la escena
	public void exit(){
        ProjectVars.Instance.selectedTopic = -1;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu");
    }

    private void createDialog(string message)
    {
        createDialog(message, "Aceptar");
    }

    private void createDialog(string message, string confirmText)
    {
        DialogObject dialog = new DialogObject(
                        message,
                        confirmText, null);
        ModalDialog.Instance.gameObject.SetActive(true);
        ModalDialog.Instance.SetDialog(dialog);
    }

    private void createNodosDialog(int action)
    {
        string title = "Indique el nodo que desea ";
        title += action == ELIMINAR ? "eliminar" : "agregar";
        NodosDialog.Instance.SetTitle(title);
        NodosDialog.Instance.SetList(numList,action);
        NodosDialog.Instance.gameObject.SetActive(true);
        //NodosDialog.Instance.SetDialog(dialog);
    }

    #endregion
}
