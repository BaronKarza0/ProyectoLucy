using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController Control;
    public Vector3 Mov;
    public Vector3 NewMov;
    public float SpeedOld;
    public float Gravity;
    public float Jump;
    public int Njump;
    public bool DoubleJump;
    public int Lives = 10;
    //public Text Vidas;
    public RaycastHit HitRay;
    public GameObject particle;
    public int RadioAttack;
    public List<Collider> AreaObject;
    public bool isAttackArea;
    public float Count;

    [Header("NewMove")]
    public Rigidbody Rigid;
    public float Speed = 10.0f; 
    public float Jumpspeed;
    private float xInput;
    private float zInput;

    public Transform cam;
    Vector2 input;
    //public Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        Control = GetComponent<CharacterController>();
        //DoubleJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        //JumpAttack();
        //AreaAttack();  

    }
    
    public void PlayerMove()
    {/*
        //Vidas.text = Lives.ToString();
        if(Lives <= 0)
        {
            Destroy(gameObject);
        }

        if(Control.isGrounded)
        {
            Mov = new Vector3(Input.GetAxis("Horizontal")*Speed,0,Input.GetAxis("Vertical")*Speed);

            //if(Mov.x !=0 || Mov.z != 0)Anim.SetBool("iswalk", true);
            //if(Mov.x ==0 && Mov.z == 0)Anim.SetBool("iswalk", false);

            //Anim.SetFloat("isidle",Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")));
        
            
            if(Input.GetButtonDown("Jump"))
            {
                
                Mov.y = Jump;
            }

            Njump = 0;
        }
        else
        {

            Mov = new Vector3(Input.GetAxis("Horizontal")*Speed,Mov.y,Input.GetAxis("Vertical")*Speed);
            if(Input.GetButtonDown("Jump") && DoubleJump == true && Njump < 1)
            {
                Mov.y = Jump;
                Njump++;
            }
            
        }

        Mov.y -= Gravity * Time.deltaTime;
        Control.Move(Mov*Time.deltaTime);

        NewMov = Mov;
        NewMov.y = 0;

        if(Mov.x !=0 || Mov.z != 0)
        {
            transform.rotation = Quaternion.LookRotation(NewMov);
        }*/
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input,1);

        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        Mov = new Vector3(input.y*Speed,0f,input.x*Speed);
        //Rigid.AddForce(new Vector3(xInput, 0f, zInput) * Speed);
        //Rigid.AddForce((camF*input.y + camR*input.x) * Speed);
    }

    public void JumpAttack()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out HitRay, 1.5f))
        {
            if(HitRay.collider.tag == "Enemy")
            {
                GameObject newparticle = (GameObject)Instantiate(particle, HitRay.transform.position, Quaternion.Euler(90,0,0));
                Mov.y = Jump*2;
                Destroy(HitRay.transform.parent.gameObject);
            }
        }            
    }

    public void AreaAttack()
    {
        if(isAttackArea == true)
        {
            //Anim.SetBool("isattack", true);
            AreaObject = new List<Collider>(Physics.OverlapSphere(transform.position, RadioAttack));

            for(int i=0; i<AreaObject.Count; i++)
            {
            if(AreaObject[i].tag == "Enemy")
            {
                Destroy(AreaObject[i].transform.parent.gameObject);
            }
            }
        }
        else
        {   

            Count += Time.deltaTime;
            if(Count >= 3 && Input.GetButtonDown("Fire1"))
            {
                Count = 0;
                isAttackArea = true;
                Invoke("DesactiveAttack",1);
            }
        }
        
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadioAttack);
    }

    public void DesactiveAttack()
    {
        isAttackArea = false;
        //Anim.SetBool("isattack", false);
        AreaObject.Clear();
    }
}
