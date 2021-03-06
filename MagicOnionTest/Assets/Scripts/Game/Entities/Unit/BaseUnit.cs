﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game;
using TMPro;
using UnityEngine;

//This should become BasePlayer LOL.
public class BaseUnit : MonoBehaviour
{
    Animator m_Animator;
    Rigidbody m_Rigidbody;

    //Movements/Animations
    private bool m_Jump;
    private bool m_Walking;
    private bool m_Running = true;
    private bool m_CanStand = true;
    private CharAnimState m_OldCharAnimState = CharAnimState.Stand;
    public float speedWalk = 1.0f;
    public float speedRun = 2.0f;

    internal bool IsAlive = true;
    internal Vector3 Position;
    private bool settedTarget = false;

    private bool m_ChatIsOpen = false;

    private static BaseUnit _instance;
    public static BaseUnit Instance { get { return _instance; } }

    public VirtualJoystick moveJoystick;

    void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_Walking = m_Animator.GetBool("IsWalking");
        Position = transform.position;
        moveJoystick = VirtualJoystick.Instance;
        Debug.Log("Start, walking = " + m_Walking);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.gameObject.name == InitClient.GetCurrentPlayerName())
        {
            if (!settedTarget)
            {
                var camera = Camera.main;
                camera.GetComponent<WarcraftCamera>().Target = this;
                settedTarget = true;
            }

            
            if (Input.GetKeyUp(KeyCode.Slash))
            {
                m_Running = !m_Running;
                Debug.Log($"Running: " + m_Running);
            }
            //Move forward + turn left
            else if (((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) || moveJoystick.IsMovingWA()) && !m_ChatIsOpen)
            {
                transform.position += transform.TransformDirection(Vector3.forward * (m_Running ? speedRun : speedWalk) * Time.deltaTime);
                transform.Rotate(Vector3.up, -1, Space.Self);
                SetAnimState(m_Running ? CharAnimState.Run : CharAnimState.Walk);

                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            //Move forward + turn right
            else if (((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) || moveJoystick.IsMovingWD()) && !m_ChatIsOpen)
            {
                transform.position += transform.TransformDirection(Vector3.forward * (m_Running ? speedRun : speedWalk) * Time.deltaTime);
                transform.Rotate(Vector3.up, 1, Space.Self);
                SetAnimState(m_Running ? CharAnimState.Run : CharAnimState.Walk);

                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            //Move backward + turn left
            else if (((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) || moveJoystick.IsMovingSA()) && !m_ChatIsOpen)
            {
                transform.position += transform.TransformDirection(Vector3.back * speedWalk * Time.deltaTime);
                transform.Rotate(Vector3.up, -1, Space.Self);
                SetAnimState(CharAnimState.WalkBackwards);

                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            //Move backward + turn right
            else if (((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) || moveJoystick.IsMovingSD()) && !m_ChatIsOpen)
            {
                transform.position += transform.TransformDirection(Vector3.back * speedWalk * Time.deltaTime);
                transform.Rotate(Vector3.up, 1, Space.Self);
                SetAnimState(CharAnimState.WalkBackwards);

                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            //Turn right
            else if ((Input.GetKey(KeyCode.D)) && !m_ChatIsOpen)//Should rotate unit
            {
                transform.Rotate(Vector3.up, 1, Space.World);
                SetAnimState(CharAnimState.ShuffleRight);
                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            //Turn left
            else if ((Input.GetKey(KeyCode.A)) && !m_ChatIsOpen)//Should rotate unit
            {
                transform.Rotate(Vector3.up, -1, Space.World); //-1 sono i gradi di rotazione
                SetAnimState(CharAnimState.ShuffleLeft);
                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            //Move forward
            else if ((Input.GetKey(KeyCode.W) || moveJoystick.IsMovingW()) && !m_ChatIsOpen)
            {
                transform.position += transform.TransformDirection(Vector3.forward * ( m_Running ? speedRun : speedWalk ) * Time.deltaTime);
                SetAnimState(m_Running ? CharAnimState.Run : CharAnimState.Walk);

                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            //Move backward
            else if ((Input.GetKey(KeyCode.S) || moveJoystick.IsMovingS()) && !m_ChatIsOpen)
            {
                transform.position += transform.TransformDirection(Vector3.back * speedWalk * Time.deltaTime);
                SetAnimState(CharAnimState.WalkBackwards);

                InitClient.Instance.MoveAsync(transform.position, transform.rotation);
            }
            //Sit down / stand up
            else if (Input.GetKeyUp(KeyCode.X) && !m_ChatIsOpen)
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
            //Jump
            else if (Input.GetKeyUp(KeyCode.Space) && !m_ChatIsOpen)
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
        }
        Position = transform.position;

        if (moveJoystick.InputDirection != Vector3.zero)
        {
            //dir = moveJoystick.InputDirection
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

    public void SetChatIsOpen(bool isOpen)
    {
        m_ChatIsOpen = isOpen;
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
    }

    public void SimulateWalk(Vector3 newPosition)
    {
        SetAnimState(m_Running ? CharAnimState.Run : CharAnimState.Walk);
        transform.position = newPosition;
    }

    public void ChangeRotation(Quaternion newRot)
    {
        transform.rotation = newRot;
    }

    public void SetIdle()
    {
        SetAnimState(CharAnimState.Stand);
    }
}
