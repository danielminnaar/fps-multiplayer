using UnityEngine.Networking;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerShoot : NetworkBehaviour {

	[SerializeField]
	private LayerMask mask;
	[SerializeField]	
	private Camera cam;
	public PlayerWeapon weapon;
	void Start() {
		
	}

	void Update()
	{
		if(Input.GetButtonDown("Fire1")) {
			Shoot();
		}
	}
	[Client]
	private void Shoot() {
		RaycastHit hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask)) {
			print("shooting at " +  hit.collider.name);
			if(hit.collider.tag == "Player")
				CmdPlayerShot(hit.collider.name, weapon.damage);
		}
	}

	[Command]
	void CmdPlayerShot(string _id, int damage) {
		print(_id + " was hit");
		var player = GameManager.GetPlayer(_id);
		player.TakeDamage(damage);
	}
}
