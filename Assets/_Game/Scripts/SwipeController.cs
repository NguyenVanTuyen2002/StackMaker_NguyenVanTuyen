using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField]private Rigidbody rb;

    private Vector3 startPos;
    private Vector3 direction;
    private Vector3 endPos;
    private bool directionChosen;

    public LayerMask layer;
    public float raycastDistance = 1f;

    public GameObject DashParent;
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
            MoveDirection(Vector3.forward);
        }
        else if (direction.x > 0 && angle1 < 45)
        {
            Debug.Log("Swipe Right");
            MoveDirection(Vector3.right);
        }
        else if (direction.x < 0 && angle1 > 135)
        {
            Debug.Log("Swipe Left ");
            MoveDirection(Vector3.left);
        }
        else if (direction.y < 0 && (angle > 135))
        {
            Debug.Log("Swipe Down");
            MoveDirection(Vector3.back);
        }

        directionChosen = true;
    }
    

    private void MoveDirection(Vector3 direction)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out hit, raycastDistance, layer))
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = direction.normalized * speed;
            transform.Translate(direction.normalized * Time.deltaTime * speed);
            transform.Translate(Vector3.up * 0.3f);
        }
    }

    public void PickDash(GameObject dash)
    {
        dash.transform.SetParent(DashParent.transform);
        Vector3 pos = PreDash.transform.localPosition;
        pos.y -= 0.2f;
        dash.transform.localPosition = pos;
        Vector3 Charcchterpos = transform.localPosition;
        Charcchterpos.y += 0.2f;
        transform.localPosition = Charcchterpos;
        PreDash = dash;
        PreDash.GetComponent<BoxCollider>().isTrigger = false;
    }
}
