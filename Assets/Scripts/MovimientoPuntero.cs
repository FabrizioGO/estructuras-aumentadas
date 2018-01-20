using UnityEngine;
using System.Collections;

public class MovimientoPuntero : MonoBehaviour {

    private const int ELIMINAR = 1;
    private const int INSERTAR = 3;

	private Casa casa;
	public float avance;
    private float timer;
    public float delay;
	private int cantidad, total,iterador, action;
	private Vector3 endPos,TargetPosition,lastPosition;
	private bool isMovingForward,isMovingBackward,isInitialize,isAddButtonClicked,IsMovingAside;
    private Vector3[] positions;
    private onPunteroMoved mListener;
    private Codigos.ICodigo codeListener;

	// Use this for initialization
	void Start () {
		inicializar();
	}

	// Update is called once per frame
	void Update(){
		SetupNodeMovement ();
	}

	private void inicializar (){
		endPos.Set (avance, 0, 0);
		TargetPosition = transform.position;
		isMovingForward = false;
		isInitialize = true;
		isAddButtonClicked = false;
        timer = 0;
        iterador = 0;
	}

	private void SetupNodeMovement(){
		int position;
        timer += Time.deltaTime;
		if (isInitialize) {
			position = total - cantidad - 1;
			for (int i = 0; i < cantidad; i++){
                positions[i] = TargetPosition;
				TargetPosition += endPos;
			}
            isMovingForward = true;
		}
		if (IsMovingAside) {
			position = total - cantidad - 1;
			for (int i = 0; i < position; i++){
				TargetPosition += endPos;
			}
			IsMovingAside = false;
		}
		if (isAddButtonClicked) {
			TargetPosition += endPos;
			isMovingForward = true;
		}
		lastPosition = transform.position;
		if (isMovingForward) {

            //transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.75f * Time.deltaTime);
            //onPunteroMoved mListener = (onPunteroMoved)casa;
            //mListener.isPunteroMoved(true);
            
			//transform.Translate (TargetPosition);
            if (cantidad == 0)
            {
                casa.codigo.text = Codigos.ListaLeccion.insertarOrdenadoIfInicio;
            }
            if (timer > delay)
            {
                //
                
                //transform.position = Vector3.Lerp(transform.position, TargetPosition, 2 * Time.deltaTime);
                //
                if (action == INSERTAR)
                {
                    if (iterador >= cantidad)
                    {
                        Debug.Log("Iterador es mayor a la cantidad");
                        int listCount = casa.numList.Count;
                        Debug.Log("ListCount: " + listCount);
                        Debug.Log("cantidad: " + cantidad);
                        casa.codigo.text = Codigos.ListaLeccion.getInsertarOrdenadoPaso(cantidad != listCount+1);
                        if (cantidad == listCount+1)
                        {
                            codeListener.onWriteCode(-1, "actual==NULL");
                        }
                        mListener.isPunteroMoved(true);
                        isMovingForward = false;
                    }
                    else
                    {
                        int pos = positions.Length;

                        if (iterador < pos)
                        {
                            codeListener.onWriteCode(iterador, " < ");
                            casa.codigo.text = Codigos.ListaLeccion.getInsertarOrdenadoPaso(false);
                            transform.position = positions[iterador];
                        }
                        iterador++;
                        
                    }
                }
                else
                {
                    if (iterador == cantidad)
                    {
                        int listCount = casa.numList.Count;
                        casa.codigo.text = Codigos.ListaLeccion.getEliminarPaso(cantidad != listCount);
                        if (iterador == cantidad)
                        {
                            codeListener.onWriteCode(iterador - 1, " == ");
                        }
                        if (cantidad == listCount)
                        {
                            codeListener.onWriteCode(-1, "actual==NULL");
                        }
                        mListener.isPunteroMoved(true);
                    }
                    else
                    {
                        int pos = positions.Length;

                        if (iterador < pos)
                        {
                            codeListener.onWriteCode(iterador, action == ELIMINAR ? " == " : " < ");
                            casa.codigo.text = Codigos.ListaLeccion.getEliminarPaso(false);
                            transform.position = positions[iterador];
                        }
                        iterador++;
                    }
                }
                
                timer = 0;
            }
			
		}
		if (isMovingBackward) {
			transform.Translate (TargetPosition);
			if (lastPosition.x > transform.position.x) {
				isMovingBackward = false;
				isAddButtonClicked = false;
			}
		}
		if (isInitialize && transform.position.x > -3)
			isInitialize = false;
	}

	public void Move(Casa casa, int index, int total, int action){
		this.casa = casa;
		this.cantidad = index;
		this.total = total;
        this.action = action;
        codeListener = (Codigos.ICodigo)casa;
        mListener = (onPunteroMoved)casa;
		isMovingForward = true;
        positions = new Vector3[total+1];
       
	}

	public void MoveBackward(){
		TargetPosition -= endPos;
		isMovingBackward = true;
	}

	public interface onPunteroMoved{
		bool isPunteroMoved(bool moved);
	}

}
