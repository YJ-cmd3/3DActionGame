using JetBrains.Annotations;
using UnityEngine;

public class follow : MonoBehaviour
{
    //���� ��ǥ�� ��ġ �������� public ������ ����
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}
