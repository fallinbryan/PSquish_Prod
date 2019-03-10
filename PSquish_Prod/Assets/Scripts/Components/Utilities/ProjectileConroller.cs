using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProfessorSquish.Components.Utilities.Projectile
{
    public class ProjectileConroller : MonoBehaviour
    {
        public float lifespan = 20f;


        // Start is called before the first frame update
        void Start()
        {

            Destroy(gameObject, lifespan);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
