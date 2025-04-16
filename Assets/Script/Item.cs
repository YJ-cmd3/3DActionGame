using UnityEngine;

public class Item : MonoBehaviour
{
    //enum : 열거형 타입 (1,2,3,4,5,6,7,8,....)
    public enum Type { Ammo, Coin, Grenade, Heart, Weapon};
    public Type type;
    public int value;


    Rigidbody rigid;
    SphereCollider sphereCollider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }
    void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;
            sphereCollider.enabled = false;
        }
    }
}
