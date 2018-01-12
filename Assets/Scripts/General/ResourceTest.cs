using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTest : MonoBehaviour {

	[SerializeField] private Resource life;

	public int delta;


	// Use this for initialization
	void OnEnable() {
		Debug.Log(life.Change(delta));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
