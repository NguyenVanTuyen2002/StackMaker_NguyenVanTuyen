using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DashPickup")
        {
            other.gameObject.tag = "normal";
            SwipeController.Ins.PickDash(other.gameObject);
            //other.gameObject.AddComponent<Rigidbody>();
            //other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //other.gameObject.AddComponent<Stack>();
            //Destroy(this);
            Debug.Log("hit");
            SwipeController.Ins.ChangeAnim("Hit");
        }
        else if (other.tag == "Finish")
        {
            Debug.Log("aaa");
            UIManager.Ins.TurnOnPrize();
            Debug.Log("win");
            SwipeController.Ins.ChangeAnim("Win");
        }
        else if (other.tag == "DropBrickDown")
        {
            other.gameObject.tag = "normal";
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            SwipeController.Ins.DropDash(other.gameObject);
            //other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //other.gameObject.AddComponent<Stack>();
            //Destroy(this);
        }
    }
}
