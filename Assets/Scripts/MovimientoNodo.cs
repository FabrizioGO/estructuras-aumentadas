using UnityEngine;
using System.Collections;

public class MovimientoNodo : MonoBehaviour {

	public float speed;
	public float avance;
	private int cantidad;
	private Vector3 endPos,TargetPosition,lastPosition;
	private bool isMovingForward,isMovingBackward,isInitialize,isAddButtonClicked,IsMovingAside;

	// Use this for initialization
	void Start () {
		inicializarNodo();
	}
		
	// Update is called once per frame
	void Update(){
		SetupNodeMovement ();
	}

	private void inicializarNodo (){
		endPos.Set (avance, 0, 0);
		TargetPosition = transform.position;
		isMovingForward = false;
		isInitialize = true;
		isAddButtonClicked = false;
	}

	private void SetupNodeMovement(){
		if (isInitialize) {
			TargetPosition += endPos;
			isMovingForward = true;
		}
		if (IsMovingAside) {
			for (int i = 0; i < cantidad; i++){
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
			transform.position = Vector3.Lerp (transform.position, TargetPosition, speed * Time.deltaTime);
			if (lastPosition.x < transform.position.x) {
				isMovingForward = false;
				isAddButtonClicked = false;
			}
		}
		if (isMovingBackward) {
			transform.position = Vector3.Lerp (transform.position, TargetPosition, speed * Time.deltaTime);
			if (lastPosition.x > transform.position.x) {
				isMovingBackward = false;
				isAddButtonClicked = false;
			}
		}
		if (isInitialize && transform.position.x > -3)
			isInitialize = false;
	}

	public void MoveDelAside(int index){
		Vector3 sidePos = new Vector3 (0, 0, 0.4f);
		TargetPosition += sidePos;
		isMovingForward = true;
		isInitialize = false;
	}

	public void MoveFromAside(int index){
		cantidad = index;
		IsMovingAside = true;
	}

	public void Move(){
		TargetPosition += endPos;
		isMovingForward = true;   
	}

	public void MoveBackward(){
		TargetPosition -= endPos;  
		isMovingBackward = true;
	}
		
}
