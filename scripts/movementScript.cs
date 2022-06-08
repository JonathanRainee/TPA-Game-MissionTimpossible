using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class movement : MonoBehaviour
{
    [SerializeField] private LayerMask terrainlayer;
    private Animator animate; 
    private Rigidbody rigid;
    [SerializeField] private CharacterController control;
    public float health = 100f;
    public bool jumpp;
    //public float jumpSpeed;
    //private float ySpeed;
    //public float rotationSpeed;
    //public float speed;
    [SerializeField] private Camera cam; //ngambil var cam dr inspector
    [SerializeField] float turnSpeed;

    //public float aimduration = 0.3f;

    //public Rig aimlayer;

    //jump
    [SerializeField] private float height, directionalSpeed, groundY;
    private float gravity = -9.8f;
    private Vector3 velocity;
    private float timer = 0f;
    private bool isJump, isHorizontal, isVertical;


    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<Animator>(); //agar comp aniamtor bs d acc pk getcomp
        rigid = GetComponent<Rigidbody>();
        velocity.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float InputVer = Input.GetAxis("Vertical"); //mskin k dlm vari dr input acis vertical
        float InputHori = Input.GetAxis("Horizontal");
        animate.SetFloat("velocityz", InputVer);
        animate.SetFloat("velocityx", InputHori);
        animate.SetBool("ISJUMP", false);

        //if (velocity.y < 0 && isGrounded())
        //{
        //    velocity.y = 0;
        //}

        velocity.y = 0f;

        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        Debug.Log("velo y " +velocity.y);

        if (isGrounded() == true)
        {
            Debug.Log("grounded bener");
        }

        if (isGrounded() == true && velocity.y == 0)
        {
            animate.SetBool("ISJUMP", false); 
            Debug.Log("grounded");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("mau jump");
                jumpp = true;
                animate.SetBool("ISJUMP", true);
            }

        }
        else if (!jumpp && isGrounded() == false)
        {
            animate.SetBool("ISJUMP", true);
        }

        if (jumpp)
        {
            velocity.y = Mathf.Sqrt(height * -2 * gravity);
            jumpp = false;
        }

        velocity.y += (gravity * Time.deltaTime);
        control.Move(velocity * Time.deltaTime);

        //if (Input.GetMouseButton(1))
        //{
        //Debug.Log("pressed");
        //aimlayer.weight += Time.deltaTime / aimduration;
        //}
        //else
        //{
        //    aimlayer.weight -= Time.deltaTime / aimduration;
        //}

        //this
        //Vector3 movedir = new Vector3(InputHori, 0, InputVer);
        //float magnitude = Mathf.Clamp01(movedir.magnitude) * speed;
        //movedir.Normalize();

        //ySpeed += Physics.gravity.y * Time.deltaTime;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //Debug.Log("jump");
        //    ySpeed = jumpSpeed;
        //}

        //Vector3 velocity = movedir * magnitude;
        //velocity.y = ySpeed;
        //control.Move(velocity * Time.deltaTime);


        //if(movedir != Vector3.zero)
        //{
        //    Quaternion rotate = Quaternion.LookRotation(movedir, Vector3.up);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, rotationSpeed * Time.deltaTime);
        //}
    }

    private bool isGrounded()
    {
        if (Physics.CheckSphere(transform.position, groundY, terrainlayer))
        {
            return true;
        }

        return false;
    }

}