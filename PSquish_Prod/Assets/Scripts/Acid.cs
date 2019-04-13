using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
	// Start is called before the first frame update
	public ParticleSystem part;
	public List<ParticleCollisionEvent> collisionEvents;

	void Start()
	{
		part = GetComponent<ParticleSystem>();
		collisionEvents = new List<ParticleCollisionEvent>();
	}

	void OnParticleCollision(GameObject other)
	{
		other.SendMessage("Acid");
	}


}
