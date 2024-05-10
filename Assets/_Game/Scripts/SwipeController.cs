using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float speed = 50f;

    private Vector3 startPos;
    [SerializeField] private Vector3 direction;
    private Vector3 endPos;
    private bool directionChosen;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Transform root;

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
        Debug.DrawLine(transform.position, temp);
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
        dash.transform.SetParent(DashParent.transform);
        Vector3 pos = PreDash.transform.localPosition;
        pos.y -= 0.2f;
        dash.transform.localPosition = pos;
        Vector3 Charcchterpos = transform.localPosition;
        Charcchterpos.y += 0.3f;
       transform.localPosition = Charcchterpos;
        PreDash = dash;
        PreDash.GetComponent<BoxCollider>().isTrigger = false;
    }

}
