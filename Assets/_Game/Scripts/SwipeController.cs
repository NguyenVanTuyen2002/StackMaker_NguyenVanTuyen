using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Transform root;
    [SerializeField] private Vector3 direction;
    [SerializeField] Animator anim;

    private string currentAnimName;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 Charcchterpos;
    private Vector3 pos;
    private bool directionChosen;
    public int m_score;

    public LayerMask layer;
    public float raycastDistance = 1f;
    public GameObject DashParent;
    public GameObject PlayerSkin;
    public GameObject PreDash;

    UIManager m_UI;

    private Vector3 initialPosition; // Biến để lưu vị trí ban đầu

    private static SwipeController ins;
    public static SwipeController Ins => ins;

    private void Awake()
    {
        ins = this;
    }


    private void Start()
    {
        m_UI = FindObjectOfType<UIManager>();
        initialPosition = transform.position; // Lưu vị trí ban đầu
    }

    void Update()
    {
        Moving();
        MoveDirection();
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
        else
        {
            anim.ResetTrigger(currentAnimName);
            anim.SetTrigger(currentAnimName);
        }
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
        //Debug.DrawLine(transform.position, temp);
        Ray ray = new Ray(root.transform.position, moveDirection);
        Debug.DrawRay(transform.position, moveDirection, Color.red, 0.5f);
        if (Physics.Raycast(ray, out hit, raycastDistance, layer))
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
        pos = PreDash.transform.localPosition;
        pos.y -= 0.3f;
        dash.transform.localPosition = pos;
        Charcchterpos = transform.localPosition;
        Charcchterpos.y += 0.3f;
        InCrementScore();
        transform.localPosition = Charcchterpos;
        PreDash = dash;
        PreDash.GetComponent<BoxCollider>().isTrigger = false;
    }

    public void DropDash(GameObject dash)
    {
        Charcchterpos = transform.localPosition;
        Charcchterpos.y -= 0.3f;
        transform.localPosition = Charcchterpos;
    }

    public void SetScore(int value)
    {
        m_score = value;
    }

    public int GetScore()
    {
        return m_score;
    }

    public void InCrementScore()
    {
        m_score++;
        m_UI.SetScoreText("Score: " + m_score);
    }
}
