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
        //animator�� �ڽ����� �־ GetComponent�����δ� �� ������
        //InChildren�� ������
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

    //�ڵ� ��ɿ� ���� ���еǰ� �Լ��� �и��ϱ�
    void GetInput()
    {
        //GetAxisRaw() : Axis ���� ������ ��ȯ�ϴ� �Լ�
        //Editor -> Project Setting -> Input Manager���� Ȯ�� ����
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        //shift�� �������� �۵��ǵ��� GetButton���� �� Down X
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

        //trasnform �̵��� ���� �浹�� �Ͼ�� ���� ���� ���� ����
        //Rigidbody -> constraints -> freeze rotation�� x z�� ���Ƽ� ���������ʰ� ����
        //Rigidbody -> collision Detection�� Continuous�� �����ؼ� ����

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Turn()
    {
        //LookAt() : ������ ���͸� ���ؼ� ȸ�������ִ� �Լ�
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge) {
            // �������̸� AddForce�� ���� �����ؼ� Vector3.up���� ������ ����. * %d�� �� �� ���� ����
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;

        }

    }
    void Dodge()
    {
        //�׼� ���߿� �ٸ� �׼��� ������� �ʵ��� ���� ���� �߰�
        if (jDown && moveVec != Vector3.zero && !isJump && !isJump)
        {
            dodgeVec = moveVec;
            // �������̸� AddForce�� ���� �����ؼ� Vector3.up���� ������ ����. * %d�� �� �� ���� ����
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
