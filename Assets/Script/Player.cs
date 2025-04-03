using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool wDown;
    bool jDown;

    bool isJump;
    bool isDodge;

    Vector3 moveVec;
    Vector3 dodgeVec;

    Rigidbody rigid;
    Animator anim;

    void Awake()
    {   
        //animator을 자식으로 넣어서 GetComponent만으로는 못 가져옴
        //InChildren도 붙히기
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge(); 
    }

    //코드 기능에 따라 구분되게 함수로 분리하기
    void GetInput()
    {
        //GetAxisRaw() : Axis 값을 정수로 반환하는 함수
        //Editor -> Project Setting -> Input Manager에서 확인 가능
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        //shift는 누를때만 작동되도록 GetButton으로 씀 Down X
        wDown = Input.GetButton("walk");
        jDown = Input.GetButtonDown("Jump");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
        {
            moveVec = dodgeVec;
        }

        //trasnform 이동은 물리 충돌이 일어나서 벽을 뚫을 수도 있음
        //Rigidbody -> constraints -> freeze rotation의 x z를 막아서 쓰러지지않게 고정
        //Rigidbody -> collision Detection을 Continuous로 변경해서 막기

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Turn()
    {
        //LookAt() : 지정된 벡터를 향해서 회전시켜주는 함수
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge) {
            // 점프높이를 AddForce로 힘을 전달해서 Vector3.up으로 방향을 정함. * %d로 힘 양 조절 가능
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;

        }

    }
    void Dodge()
    {
        //액션 도중에 다른 액션이 실행되지 않도록 여러 조건 추가
        if (jDown && moveVec != Vector3.zero && !isJump && !isJump)
        {
            dodgeVec = moveVec;
            // 점프높이를 AddForce로 힘을 전달해서 Vector3.up으로 방향을 정함. * %d로 힘 양 조절 가능
            speed *= 2;   
            anim.SetTrigger("doDodge");
            isDodge = true;
            Invoke("DodgeOut", 0.5f); 
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }

}
