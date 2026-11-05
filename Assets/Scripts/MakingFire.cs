using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;


public class MakingFire : MonoBehaviour
{  
    public Transform Stick;
    public float moveSpeed;
    public float minY;
    public float maxY;
    public float resistance;
    public float decreaseRate;
    public float decreaseTime;
    public float dustDecreaseRate;
    public float dustDecreaseTime;
    public float mouseMoveSpeed;   
    public float woodfrictiondecrese;

    private bool wasAtMaxLimit = false;
    private bool isAtMaxLimit;

    void Update()
    {
        GameMgr.I.dust = Mathf.Clamp(GameMgr.I.dust, 0, 100);
        GameMgr.I.fireGenerater = Mathf.Clamp(GameMgr.I.fireGenerater, minY, maxY);                      
        if (Input.GetMouseButton(0))
        {
            float mouseStickY = Input.GetAxis("Mouse Y") * moveSpeed;
            if (GameMgr.I.canMakeFire)
            {
                // Stick 움직이기
                
                Vector3 targetPosition = Stick.position;
                targetPosition.z += mouseStickY;
                targetPosition.z = Mathf.Clamp(targetPosition.z, minY, maxY);
                Stick.position = targetPosition;
                if (mouseStickY > 0f && !isAtMaxLimit)
                {
                    float moveAmount = Mathf.Clamp(mouseStickY, minY, maxY);
                    GameMgr.I.woodfriction = Mathf.Clamp(GameMgr.I.woodfriction, 0, 1);
                    GameMgr.I.fireGenerater += moveAmount * resistance * GameMgr.I.woodfriction;
                    GameMgr.I.woodfriction = GameMgr.I.woodfriction - moveAmount / woodfrictiondecrese;
                }
                else
                {
                    GameMgr.I.fireGenerater -= decreaseRate * decreaseTime;
                }                
            }

            // Dust 효과
            float moveY = Input.GetAxis("Mouse Y");
            mouseMoveSpeed = moveY / Time.deltaTime;

            isAtMaxLimit = Stick.position.z >= maxY;
            if (isAtMaxLimit && !wasAtMaxLimit)
            {
                if(mouseMoveSpeed > 450)
                {
                    float dustAmount = (mouseMoveSpeed == 0) ? 0 : 30 / mouseMoveSpeed;                  
                    GameMgr.I.dust += dustAmount;
                    
                }               
            }

            wasAtMaxLimit = isAtMaxLimit;                 
        }
        else
        {
            GameMgr.I.fireGenerater -= decreaseRate * decreaseTime;
            
        }
        if (GameMgr.I.isFire)
        {
            GameMgr.I.dust -= dustDecreaseRate * dustDecreaseTime;
        }

    }
}

