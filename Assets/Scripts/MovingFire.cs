using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFire : MonoBehaviour
{
    public Transform Plank;

    [Header("회전 속도")]
    public float rotationSpeed = 5f;

    // 각도가 튀는 것을 방지하기 위해 누적 값을 저장할 변수
    private float currentZAngle = 0f;

    void Start()
    {
        if (Plank != null)
        {
            // 시작할 때 오브젝트의 현재 Z축 각도를 가져와서 설정합니다.
            currentZAngle = Plank.localEulerAngles.z;
            if (currentZAngle > 180f) currentZAngle -= 360f;
        }
    }

    void Update()
    {
        // 1. 마우스 왼쪽 버튼을 누르고 있는 동안
        if (Input.GetMouseButton(0))
        {
            if (GameMgr.I.canMoveFire)
            {
                // 2. 마우스의 좌우 이동량(Mouse X)을 가져옵니다.
                float mouseX = Input.GetAxis("Mouse X");

                // 3. 이동량에 회전 속도를 곱해 Z축 각도를 누적합니다.
                currentZAngle += mouseX * rotationSpeed;

                // 4. 회전 범위를 -120도에서 0도 사이로 제한합니다.
                currentZAngle = Mathf.Clamp(currentZAngle, 0f, 120f);

                // 5. Plank 오브젝트에 최종 Z축 회전 값을 적용합니다.
                Vector3 targetRotation = Plank.localEulerAngles;
                targetRotation.z = currentZAngle;
                Plank.localEulerAngles = targetRotation;
            }
        }
    }
}