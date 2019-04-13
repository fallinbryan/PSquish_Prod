using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargetest : MonoBehaviour
{
	public float charge = 0f;
	public float time = 0f;
	public ParticleSystem dischrg;

	// Start is called before the first frame update
    void Start()
    {
		dischrg.Stop();  
    }

    // Update is called once per frame
    void Update()
    {
		if(time > 0f)
		{
			time -= Time.deltaTime;
		} else {
			dischrg.Stop();
		}
		if(charge > 0f)
		{
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, charge);
			if (hitColliders.Length != 1)
			{
				GameObject target = this.gameObject;
				float close = charge * 10 * charge * 10;

				for(int i = 1; i < hitColliders.Length; i++)
				{
					float distance = (transform.position - hitColliders[i].transform.position).sqrMagnitude;

					if(distance < close) {
						close = distance;
						switch(hitColliders[i].transform.tag)
						{
						case "Enemy":
							target = hitColliders[i].gameObject;
							break;
						default:
							break;
						}
					}
				}

				if(target != this.gameObject){
					Discharge(target);
				}
			}

			charge -= Time.deltaTime;
			//print("CHARGE:" + charge);
		} else{
			//print("No Charge");
			charge = 0f;

		}
    }

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawSphere (transform.position, charge);
	}

	void Charge(float chargeMod)
	{
		charge = charge + chargeMod;
		//print(charge);
	}

	void Discharge(GameObject target)
	{
		print("DISCHARGED");
		dischrg.transform.LookAt(target.transform);
		dischrg.Play(true);
		time = 0.5f;
		charge = 0f;
	}
}
