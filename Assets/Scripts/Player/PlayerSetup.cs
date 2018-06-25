using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
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

	}

	public override void OnStartClient() {
		base.OnStartClient();
		RegisterPlayer();
	}

	private void RegisterPlayer() {
		string id = GetComponent<NetworkIdentity>().netId.ToString();
		var player = GetComponent<Player>();
		GameManager.RegisterPlayer(id, player);
	}

	void OnDisable()
	{
		if(sceneCamera != null) {
			sceneCamera.gameObject.SetActive(true);
		}

		GameManager.DeregisterPlayer(GetComponent<NetworkIdentity>().netId.ToString());
	}


}
