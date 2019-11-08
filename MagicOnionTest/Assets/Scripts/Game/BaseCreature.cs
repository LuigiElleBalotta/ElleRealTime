using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreature : MonoBehaviour
{
    Animator m_Animator;

    private bool m_Jump;

    private bool m_Walking;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Walking = m_Animator.GetBool("IsWalking");
        Debug.Log("Start, walking = " + m_Walking);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.U))
        {
            Debug.Log("Pressed U!!");
            m_Walking = !m_Walking;
            m_Animator.SetBool("IsWalking", m_Walking);
            Debug.Log("Walking: " + m_Walking);
        }
    }
}
