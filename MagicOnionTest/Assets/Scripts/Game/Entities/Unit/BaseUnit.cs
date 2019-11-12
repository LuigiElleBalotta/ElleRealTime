using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game;
using UnityEngine;

//This should become BasePlayer LOL.
public class BaseUnit : MonoBehaviour
{
    Animator m_Animator;
    Rigidbody m_Rigidbody;

    private bool m_Jump;

    private bool m_Walking;
    private bool m_CanStand = true;

    public float speed = 4.0f;

    private Transform target;

    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_Walking = m_Animator.GetBool("IsWalking");
        Debug.Log("Start, walking = " + m_Walking);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.gameObject.name == InitClient.GetCurrentPlayerName())
        {
            if (Input.GetKey(KeyCode.D))//Should rotate unit
            {
                transform.position += Vector3.forward * speed * Time.deltaTime;
                SetAnimState(CharAnimState.Walk);
                //Send my actual position to all connected clients.
                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            else if (Input.GetKey(KeyCode.A))//Should rotate unit
            {
                transform.position += Vector3.back * speed * Time.deltaTime;
                SetAnimState(CharAnimState.Walk);
                //Send my actual position to all connected clients.
                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                SetAnimState(CharAnimState.Walk);
                //Send my actual position to all connected clients.
                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                SetAnimState(CharAnimState.WalkBackwards);
                //Send my actual position to all connected clients.
                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                if (m_Animator.GetInteger("CharAnimState") == (int) CharAnimState.Stand)
                {
                    m_CanStand = false;
                    SetAnimState(CharAnimState.SitGroundDown);
                }
                else if (m_Animator.GetInteger("CharAnimState") == (int) CharAnimState.SitGroundDown)
                {
                    SetAnimState(CharAnimState.SitGroundUp);
                    m_CanStand = true;
                }
            }
            else
            {
                if (m_CanStand)
                {
                    if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !m_Animator.IsInTransition(0))
                    {
                        SetAnimState(CharAnimState.Stand);
                    }
                }
                    
            }

            /*if (m_Walking)
            {
                float step = speed * Time.deltaTime; //calculate distance to move
                var newPos = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPos, step);

                if (Vector3.Distance(transform.position, newPos) < 0.001f)
                {
                    //Swap the position
                    //newPos *= -1.0f;
                    Debug.Log("Arrived.");
                }
                
            }*/

        }
        
    }

    void SetAnimState(CharAnimState state)
    {
        m_Animator.StopPlayback();
        m_Animator.SetInteger("CharAnimState", (int)state);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
    }
}
