﻿using UnityEngine;
using System.Collections;

public class ExampleUseof_MeshCut : MonoBehaviour {

	public Material capMaterial;

	// Use this for initialization
	void Start () {

		
	}
	
	void Update(){

		if(Input.GetKeyDown(KeyCode.C)){
			RaycastHit hit;

			if(Physics.Raycast(transform.position, transform.forward, out hit)){

				GameObject victim = hit.collider.gameObject;

				GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

				if(!pieces[1].GetComponent<Rigidbody>())
					pieces[1].AddComponent<Rigidbody>();

				Debug.Log("Fuck you");
				Destroy(pieces[1], 1);
			}

		}
	}

	void OnDrawGizmosSelected() {

		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
		Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * 5.0f);
		Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.forward * 5.0f);

		Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
		Gizmos.DrawLine(transform.position,  transform.position + -transform.up * 0.5f);

	}

}
