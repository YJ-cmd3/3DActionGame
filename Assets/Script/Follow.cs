using JetBrains.Annotations;
using UnityEngine;

public class follow : MonoBehaviour
{
    //따라갈 목표와 위치 오프셋을 public 변수로 선언
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}
