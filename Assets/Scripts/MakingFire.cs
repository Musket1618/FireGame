using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MakingFire : MonoBehaviour
{
    public bool isReadytoFire = false;
    public Transform Stick;
    public float moveSpeed;
    public float minY;
    public float maxY;
    public float resistance;
    public float decreaseRate;
    public float decreaseTime;  
    public float mouseMoveSpeed;
    public float woodfriction;
    public float woodfrictiondecrese;

    private bool wasAtMaxLimit = false;

    void Update()
    {
        GameMgr.I.fireGenerater = Mathf.Clamp(GameMgr.I.fireGenerater, minY, maxY);
        if (Input.GetMouseButton(0))
        {
            isReadytoFire = true;

            // Stick 움직이기
            float mouseStickY = Input.GetAxis("Mouse Y") * moveSpeed;
            Vector3 targetPosition = Stick.position;
            targetPosition.z += mouseStickY;
            targetPosition.z = Mathf.Clamp(targetPosition.z, minY, maxY);
            Stick.position = targetPosition;

            // Dust 효과
            float moveY = Input.GetAxis("Mouse Y");
            mouseMoveSpeed = moveY / Time.deltaTime;

            bool isAtMaxLimit = Stick.position.z >= maxY;
            if (isAtMaxLimit && !wasAtMaxLimit)
            {
                if(mouseMoveSpeed > 450)
                {
                    float dustAmount = (mouseMoveSpeed == 0) ? 0 : 10 / mouseMoveSpeed;                  
                    GameMgr.I.dust += dustAmount;
                }               
            }

            wasAtMaxLimit = isAtMaxLimit;

            // 불 생성량 증가
            if (mouseStickY > 0f && !isAtMaxLimit)
            {
                float moveAmount = Mathf.Clamp(mouseStickY, minY, maxY); ;
                woodfriction = Mathf.Clamp(woodfriction, 0, 1);
                GameMgr.I.fireGenerater += moveAmount * resistance * woodfriction;
                woodfriction = woodfriction - moveAmount / woodfrictiondecrese;
            }
            else
            {
                GameMgr.I.fireGenerater -= decreaseRate * decreaseTime;
            }
        }
        else
        {
            GameMgr.I.fireGenerater -= decreaseRate * decreaseTime;
            isReadytoFire = false;
        }
    }
}

