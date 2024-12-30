using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 6f;  //玩家移动速度

    private Rigidbody rb;
    private Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        Move(h, v);//移动

        Turning();//旋转

        //动画切换
        Anmimating(h, v);


    }

    void Move(float h,float v)
    {
        Vector3 movementV3 = new Vector3(h, 0, v);
        movementV3 = movementV3.normalized * Speed * Time.deltaTime;

        rb.MovePosition(transform.position + movementV3);
    }

    void Turning()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);//创建相机

        int floorLayer = LayerMask.GetMask("Floor");

        RaycastHit floorHit;

        bool isTouchFloor = Physics.Raycast(cameraRay, out floorHit, 100, floorLayer);
        if(isTouchFloor)
        {
            Vector3 v3=floorHit.point - transform.position;
            v3.y = 0;
            
            Quaternion quaternion = Quaternion.LookRotation(v3);
            rb.MoveRotation(quaternion);
        }

    }

    void Anmimating(float h,float v)
    {
        bool isW = false;
        if (h != 0 || v != 0)
            isW = true;

        anim.SetBool("IsWallking", isW);
    }


    
}
