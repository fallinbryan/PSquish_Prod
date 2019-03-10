using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProfessorSquish.Components
{

    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public float rotationSpeed;
        public Transform pivot;
        public bool isGamePaused = false;
        private Vector3 offset;

        [Range(0f, 100f)]
        public float minZoom = 10.0f;

        [Range(0f, 100f)]
        public float maxZoom = 100.0f;

        [Range(0f, 100f)]
        public float zoomSensitivity = 20.0f;

        private float currentZoom;

        // Start is called before the first frame update
        void Start()
        {

            offset = target.position - transform.position;

            pivot.transform.position = target.transform.position; // move the pivot to the player
			//pivot.transform.parent = target.transform; //This is what's messing up your rotation stuff. Reverting this back because 
															//the new camera rig is harder to work with than the old one, and it's going to take a lot more work to figure that out.

            transform.LookAt(target);

            currentZoom = Vector3.Distance(transform.position, target.transform.position);


        }

        // Update is called once per frame
        void Update()
        {
            //get x of mouse
            if (!isGamePaused)
            {

				Vector3 horizontal = new Vector3(0f, Input.GetAxis("Mouse X") * rotationSpeed, 0f);

				float playerForward = Input.GetAxis("Vertical");

				//target.Rotate(horizontal); // rotate target					
				pivot.Rotate(horizontal); // rotate pivot

                float vertical = Input.GetAxis("Mouse Y") * rotationSpeed;
                pivot.Rotate(-vertical, 0, 0);

			float desiredYAngle;
			float desiredXAngle = pivot.eulerAngles.x;
			/*if(playerForward != 0)
			{
					desiredYAngle = Mathf.Lerp(pivot.eulerAngles.x, 0f, Time.deltaTime);
					print(desiredYAngle);
			}else*/
			{
					desiredYAngle = pivot.eulerAngles.y;
			}
                Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);

                transform.position = target.position - (rotation * offset);
             

                if (Input.GetAxis("Mouse ScrollWheel") > 0 && currentZoom > minZoom)
                {

                    offset += Vector3.forward * -0.1f * zoomSensitivity;
                }

                if (Input.GetAxis("Mouse ScrollWheel") < 0 && currentZoom < maxZoom)
                {

                   offset += Vector3.forward * +0.1f * zoomSensitivity;

                }

                if (transform.position.y < Terrain.activeTerrain.SampleHeight(transform.position) + 1)
                {
                    transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position) + 1, transform.position.z);
                }

                if (transform.position.y > target.position.y + offset.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                }

                currentZoom = Vector3.Distance(transform.position, target.transform.position);
                transform.LookAt(target);


            }


             pivot.transform.position = target.transform.position; // move the pivot to the player

        }
    }
}