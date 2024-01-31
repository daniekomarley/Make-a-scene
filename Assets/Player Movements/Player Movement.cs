using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speedCoefficient = 5f;
    [SerializeField] float jumpSpeedCoefficient = 15f;
    [SerializeField] float coyoteTimeDuration = 0.125f;
    [SerializeField] float maxExtraJumps = 0f;
    bool onGround = false;
    float coyoteTime = 0f;
    float extraJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        extraJump = maxExtraJumps;
    }

    // Update is called once per frame
    void Update()
    {
        if (coyoteTime > 0f)
        {
            coyoteTime -= Time.deltaTime;
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 v = (value.Get<Vector2>()).normalized;

        rb.velocity = new Vector3(v.x * speedCoefficient, rb.velocity.y, v.y * speedCoefficient);
    }

    private void OnJump()
    {
        Debug.Log("OnJump() called");

        if (onGround || coyoteTime > 0f)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeedCoefficient, rb.velocity.z);
        }
        else if (extraJump >= 1)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeedCoefficient, rb.velocity.z);
            extraJump--;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollisionEnter() called");
        if (other.gameObject.CompareTag("ground"))
        {
            Debug.Log("OnGroundEnter");
            extraJump = maxExtraJumps;
            Debug.Log("jump is " + extraJump.ToString());
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("OnCollisionExit() called");

        if (other.gameObject.CompareTag("ground"))
        {
            Debug.Log("OnGroundExit");
            coyoteTime = coyoteTimeDuration;
            onGround = false;
        }
    }
}
