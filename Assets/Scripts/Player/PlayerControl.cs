using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;  // �ƶ��ٶ�
    public float rotationSpeed = 700f;  // ��ת�ٶ�

    private void Update()
    {
        // ��ȡ����
        float horizontal = Input.GetAxis("Horizontal"); // A/D �� ��/�Ҽ�ͷ
        float vertical = Input.GetAxis("Vertical"); // W/S �� ��/�¼�ͷ

        // �����ƶ�����
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // �ƶ���ɫ
        if (moveDirection.magnitude >= 0.1f)
        {
            // ����Ŀ����ת
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // �ƶ���ɫ
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
