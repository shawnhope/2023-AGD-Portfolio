using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public CharacterController controller;
    public float speed, jumpF;
    public Vector3 locomotion;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        speed = 5f;
        jumpF = 3f;
    }
    void Update()
    {
        Movement();
        if (controller.isGrounded)
        {
            Jump();
            if (locomotion.y < -1) { locomotion.y = -0.6f; return; }
        }
        else {
            locomotion.y += Physics.gravity.y * Time.deltaTime;
        }
    }
    public void Movement() {
        float _yTemp = locomotion.y;
        locomotion = (Input.GetAxisRaw("Horizontal") * transform.right) + (Input.GetAxisRaw("Vertical") * transform.forward);
        locomotion = locomotion.normalized;

        locomotion.y = _yTemp;

        controller.Move(locomotion * speed * Time.deltaTime);
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            locomotion.y = jumpF;
        }
    }
}
