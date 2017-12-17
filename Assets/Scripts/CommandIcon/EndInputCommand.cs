using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndInputCommand : MonoBehaviour {

	bool isend = false;

	// Use this for initialization
	void Start () {
		isend = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnEndOfInputCommand()
	{
		isend = true;
	}

	public bool IsEndOfInputCommand
	{
		get { return isend; }
	}
}
