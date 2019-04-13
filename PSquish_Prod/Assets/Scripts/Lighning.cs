using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighning : MonoBehaviour
{
    // Start is called before the first frame update
	public ParticleSystem part;
	public List<ParticleCollisionEvent> collisionEvents;
	public float chargeMod = 1.5f;

	void Start()
	{
		part = GetComponent<ParticleSystem>();
		collisionEvents = new List<ParticleCollisionEvent>();
	}

	void OnParticleCollision(GameObject other)
	{
		other.SendMessage("Charge", chargeMod);
		//print(chargeMod);
	}


}
