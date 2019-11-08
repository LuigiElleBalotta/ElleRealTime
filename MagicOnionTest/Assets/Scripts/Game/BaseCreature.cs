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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.U))
            m_Animator.SetBool("IsWalking", !m_Walking);
    }
}
