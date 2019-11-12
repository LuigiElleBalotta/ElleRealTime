using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game;
using TMPro;
using UnityEngine;

//This should become BasePlayer LOL.
public class BaseUnit : MonoBehaviour
{
    Animator m_Animator;
    Rigidbody m_Rigidbody;

    private bool m_Jump;

    private bool m_Walking;
    private bool m_Running = true;
    private bool m_CanStand = true;
    private CharAnimState m_OldCharAnimState = CharAnimState.Stand;

    public float speedWalk = 1.0f;
    public float speedRun = 2.0f;

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
            if (Input.GetKeyUp(KeyCode.Slash))
            {
                m_Running = !m_Running;
                Debug.Log($"Running: " + m_Running);
            }
            else if (Input.GetKey(KeyCode.D))//Should rotate unit
            {
                /*transform.position += Vector3.forward * speed * Time.deltaTime;
                SetAnimState(CharAnimState.Walk);*/

                transform.rotation = Quaternion.Euler( new Vector3(0, 90 * Time.deltaTime, 0));

                //Send my actual position to all connected clients.
                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            else if (Input.GetKey(KeyCode.A))//Should rotate unit
            {
                /*transform.position += Vector3.back * speed * Time.deltaTime;
                SetAnimState(CharAnimState.Walk);*/

                transform.Rotate(-Vector3.up * 4.0f * Time.deltaTime);

                //Send my actual position to all connected clients.
                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.left * ( m_Running ? speedRun : speedWalk ) * Time.deltaTime;
                SetAnimState(m_Running ? CharAnimState.Run : CharAnimState.Walk);
                //Send my actual position to all connected clients.
                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.right * speedWalk * Time.deltaTime;
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
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                SetAnimState(CharAnimState.Jump);
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
        if (m_OldCharAnimState != state)
        {
            InitClient.Instance.SendAnimationAsync(state);
            m_OldCharAnimState = state;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
    }
}
