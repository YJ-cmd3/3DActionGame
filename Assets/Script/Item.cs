using UnityEngine;

public class Item : MonoBehaviour
{
    //enum : 열거형 타입 (1,2,3,4,5,6,7,8,....)
    public enum Type { Ammo, Coin, Grenade, Heart, Weapon};
    public Type type;
    public int value;

    void Update()
    {
        transform.Rotate(Vector3.up * 10 * Time.deltaTime);
    }
}
