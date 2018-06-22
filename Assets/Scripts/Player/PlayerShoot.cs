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
		cam = GetComponent<Camera>();
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

		}
	}
}
