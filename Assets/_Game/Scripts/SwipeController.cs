using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Transform root;
    [SerializeField] private Vector3 direction;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 pos;
    private Vector3 Charcchterpos;
    private bool directionChosen;

    public LayerMask layer;
    public float raycastDistance = 1f;

    public GameObject DashParent;
    public GameObject PlayerSkin;
    public GameObject PreDash;
    public static SwipeController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        Moving();
        MoveDirection();
    }

    private void Moving()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            directionChosen = false;
        }
        else if (Input.GetMouseButtonUp(0) && !directionChosen)
        {
            endPos = Input.mousePosition;
            direction = endPos - startPos;
            direction.Normalize();
            ConvertMoving();
        }
    }
    
    private void ConvertMoving()
    {
        float angle = Vector2.Angle(direction, Vector2.up);
        float angle1 = Vector2.Angle(direction, Vector2.right);

        // Detect swipe direction
        if (direction.y > 0 && angle < 45)
        {
            Debug.Log("Swipe Up");
            moveDirection = Vector3.forward;
        }
        else if (direction.x > 0 && angle1 < 45)
        {
            Debug.Log("Swipe Right");
            moveDirection = Vector3.right;
        }
        else if (direction.x < 0 && angle1 > 135)
        {
            Debug.Log("Swipe Left ");
            moveDirection = Vector3.left;
        }
        else if (direction.y < 0 && (angle > 135))
        {
            Debug.Log("Swipe Down");
            moveDirection = Vector3.back;
        }

        directionChosen = true;
    }


    private void MoveDirection()
    {
        RaycastHit hit;
        Vector3 temp = new(direction.x, transform.position.y + 0.5f, direction.z);
        Ray ray = new Ray(root.transform.position, moveDirection);
        Debug.DrawRay(transform.position, moveDirection, Color.red, 0.5f);
        if (Physics.Raycast(ray, out hit, 0.5f, layer))
        {
            moveDirection = Vector3.zero;
        }
        else
        {
            transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        }
    }

    public void PickDash(GameObject dash)
    {
        // vi tri cua dash se theo vi tri cua DashParent vi la con cua DashParent
        dash.transform.SetParent(DashParent.transform);
        // vi tri PreDash gan cho pos sau moi lan PickDash thi giam 0.3
        pos = PreDash.transform.localPosition;
        pos.y -= 0.3f;
        // vi tri cua dash moi la vi tri cua pos
        dash.transform.localPosition = pos;

        Charcchterpos = transform.localPosition;
        Charcchterpos.y += 0.3f;
        transform.localPosition = Charcchterpos;
        PreDash = dash;
        PreDash.GetComponent<BoxCollider>().isTrigger = false;
    }

    public void DropDash(GameObject dash)
    {
        /*// ??t dash tr? l?i cha g?c
        dash.transform.SetParent(null);

        // Gi?m v? trí c?a Charcchterpos.y ?i 0.3
        Charcchterpos = transform.localPosition;
        Charcchterpos.y -= 0.3f;
        transform.localPosition = Charcchterpos;

        // ??t l?i dash v? v? trí ban ??u c?a PreDash
        if (PreDash != null)
        {
            dash.transform.localPosition = PreDash.transform.localPosition;

            // N?u có BoxCollider, ??t l?i là isTrigger = true
            BoxCollider boxCollider = PreDash.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                boxCollider.isTrigger = true;
            }
        }*/



        /*
        dash.transform.SetParent(DashParent.transform);
        pos = PreDash.transform.localPosition;
        pos.y += 0.3f;
        dash.transform.localPosition = pos;
        Charcchterpos = transform.localPosition;
        Charcchterpos.y -= 0.3f;
        transform.localPosition = Charcchterpos;
        PreDash = dash;
        PreDash.GetComponent<BoxCollider>().isTrigger = false;*/
        Debug.Log("aaa");
        Charcchterpos = transform.localPosition;
        Charcchterpos.y -= 0.3f;
        transform.localPosition = Charcchterpos;

    }
}
