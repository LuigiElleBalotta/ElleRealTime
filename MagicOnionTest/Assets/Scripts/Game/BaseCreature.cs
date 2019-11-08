using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreature : MonoBehaviour
{
    Animator m_Animator;
    Rigidbody m_Rigidbody;

    private bool m_Jump;

    private bool m_Walking;

    public float speed = 1.0f;

    private Transform target;
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
        if (Input.GetKeyUp(KeyCode.U))
        {
            Debug.Log("Pressed U!!");
            m_Walking = !m_Walking;
            m_Animator.SetBool("IsWalking", m_Walking);
            Debug.Log("Walking: " + m_Walking);
        }

        if (m_Walking)
        {
            float step = speed * Time.deltaTime; //calculate distance to move
            var newPos = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPos, step);

            if (Vector3.Distance(transform.position, newPos) < 0.001f)
            {
                //Swap the position
                //newPos *= -1.0f;
            }
            //m_Rigidbody.velocity = transform.forward * speed;
        }
    }
}
