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
    public float dust;
    public float mouseMoveSpeed;
    public float woodfriction;
    public float woodfrictiondecrese;

    void Update()
    {
        GameMgr.I.fireGenerater = Mathf.Clamp(GameMgr.I.fireGenerater, minY, maxY);
        if (Input.GetMouseButton(0))
        {
            isReadytoFire = true;
            mouseMoveSpeed = Input.GetAxis("Mouse Y");
            float mouseY = Input.GetAxis("Mouse Y")* moveSpeed;
            bool isAtMaxLimit = Stick.position.z >= maxY && mouseY > 0;
            if (mouseY > 0f && !isAtMaxLimit)
            {
                float moveAmount = Mathf.Clamp(mouseY, minY, maxY); ;
                if (isAtMaxLimit)
                {
                    print("aa");
                }
                woodfriction = Mathf.Clamp(woodfriction, 0, 1);
                GameMgr.I.fireGenerater += moveAmount * resistance * woodfriction;
                woodfriction = woodfriction - moveAmount/ woodfrictiondecrese;               
            }
            else
            {
                GameMgr.I.fireGenerater -= decreaseRate * decreaseTime;
            }          
            Vector3 targetPosition = Stick.position;
            targetPosition.z += mouseY;
            targetPosition.z = Mathf.Clamp(targetPosition.z, minY, maxY);
            Stick.position = targetPosition;            
        }
        else
        {
            GameMgr.I.fireGenerater -= decreaseRate * decreaseTime;
            isReadytoFire = false;
        }
    }
}
