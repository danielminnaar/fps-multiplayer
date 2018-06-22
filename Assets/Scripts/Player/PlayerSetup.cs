using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;
	private Camera sceneCamera;
	void Start() {
		if(!isLocalPlayer) {
			for(int i=0;i<componentsToDisable.Length;i++) {
				componentsToDisable[i].enabled = false;
			}
		}
		else {
			sceneCamera = Camera.main;
			if(sceneCamera != null) {
				sceneCamera.gameObject.SetActive(false);
			}
			
		}

		string _id = "Player " + GetComponent<NetworkIdentity>().netId.ToString();
		transform.name = _id;
	}

	private void RegisterPlayer() {

	}

	void OnDisable()
	{
		if(sceneCamera != null) {
			sceneCamera.gameObject.SetActive(true);
		}
	}
}
