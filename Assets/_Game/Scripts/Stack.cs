using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DashPickup")
        {
            other.gameObject.tag = "normal";
            SwipeController.instance.PickDash(other.gameObject);
            //other.gameObject.AddComponent<Rigidbody>();
            //other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.AddComponent<Stack>();
            //Destroy(this);
        }
        else if (other.tag == "DropBrickDown")
        {
            other.gameObject.tag = "normal";
            SwipeController.instance.DropDash(other.gameObject);
            //other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.AddComponent<Stack>();
            //Destroy(this);

        }
    }
}
