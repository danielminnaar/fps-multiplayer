using UnityEngine.Networking;
using UnityEngine;

public class Player :  NetworkBehaviour {
	[SerializeField]
	private int maxHealth = 100;
	[SyncVar]
	private int currentHealth;
	void Start()
	{
		currentHealth = maxHealth;		
	}
	public void TakeDamage(int damage) {
		if((currentHealth - damage) < 0)
			currentHealth = 0;
		else
			currentHealth -= damage;
		
		print(transform.name + " took damage; now has " + currentHealth.ToString() + " hp");
	}

}
