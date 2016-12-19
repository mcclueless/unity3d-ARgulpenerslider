using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerSelector : MonoBehaviour {

	public GameObject ball;

	public int BeerSelected;
	// Use this for initialization
	void Start () {
		ball.SetActive (false);
		BeerSelected = 1;
	}

	public void LoadBeer(){
		ball.SetActive (true);
		BeerSelected = 1;
	// Update is called once per frame
		
	}
}
