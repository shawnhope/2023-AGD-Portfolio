using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] SpriteRenderer spRen;
    public CharacterController controller;
    public Animator anim;
    public GameObject punch1;
    public float speed, jumpF, punch1Wait=.25f;
    public bool inCombat;
    public Vector3 locomotion;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        spRen = GetComponent<SpriteRenderer>();
        speed = 5f;
        jumpF = 3f;
    }
    void Update()
    {
        Attack();
       // if (!inCombat) { 
            Movement(); 
        //}
        if (controller.isGrounded/*&& !inCombat*/)
        {
            Jump();
            if (locomotion.y < -1) { locomotion.y = -0.6f; return; }
        }
        else {
           // if (!inCombat) {
                locomotion.y += Physics.gravity.y * Time.deltaTime; 
            //}
        }
        SetAnimations();
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
    public void Attack() {
        if (Input.GetKeyUp(KeyCode.Mouse0)/*&&!inCombat*/) {              //problem: nothing is stopping the animation from being constantly active bc player can spam click the attack button.
            //inCombat = true;
            anim.SetTrigger("Punch");                                       //bug for only ground combos: can be triggered in air and doesn't resolve until isGrounded; could be fixed with aerial attacks? or a ground check
            //punch1.SetActive(true);
            //StartCoroutine(Punch1Wait(punch1Wait));
        }
    }
    //could be changed out for animation states?:
    IEnumerator Punch1Wait(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        //punch1.SetActive(false);
        //inCombat = false;
        StopCoroutine(Punch1Wait(punch1Wait));
    }
    void SetAnimations() {
        //isMoving
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) { anim.SetBool("isMoving", true); } 
        else { anim.SetBool("isMoving", false); }
        //isGrounded
        if (controller.isGrounded) { anim.SetBool("onGround", true); } 
        else { anim.SetBool("onGround", false); }
        //flip sprite
        if (Input.GetAxisRaw("Horizontal") > 0.1 ) { spRen.flipX = false; punch1.transform.localPosition = new Vector3(2.5f, 2, 0); } 
        else if((Input.GetAxisRaw("Horizontal") < -0.1)) { spRen.flipX = true; punch1.transform.localPosition = new Vector3(-2.5f, 2, 0); }
    }
}
