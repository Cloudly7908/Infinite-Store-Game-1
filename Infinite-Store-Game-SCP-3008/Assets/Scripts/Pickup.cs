using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float pickUpRange = 5;
    public float moveForce = 250;
    public Transform holdParent;

    private GameObject heldOBj;

    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.F))
     {
            if (heldOBj == null)
            {

                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    PickUpObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
     }

       

        if (heldOBj != null)
        {
            MoveObject();
        }
        
    }

    void MoveObject()
    {
        if (Input.GetMouseButton(0))
        {
            holdParent.rotation *= Quaternion.Euler(0, 1, 0);
        }

        if (Input.GetMouseButton(1))
        {
            holdParent.rotation *= Quaternion.Euler(1, 0, 0);
        }

        if (Vector3.Distance(heldOBj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDistance = (holdParent.position - heldOBj.transform.position);
            heldOBj.GetComponent<Rigidbody>().AddForce(moveDistance * moveForce);
        }
    }

    void PickUpObject(GameObject pickObj)
    {
        if(pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.drag = 5;
            objRig.freezeRotation = true;
            objRig.mass = 2;

            objRig.transform.parent = holdParent;
            heldOBj = pickObj;
        }
    }

    void DropObject()
    {
        Rigidbody heldrig = heldOBj.GetComponent<Rigidbody>();
        heldrig.useGravity = true;
        heldrig.drag = 1;
        heldrig.freezeRotation = false;
        heldrig.mass = 50;

        heldOBj.transform.parent = null;
        heldOBj = null;
    }

  
}
