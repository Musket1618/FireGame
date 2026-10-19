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
    public float _decreaseTime;

    void Update()
    {
        GameMgr.I.fireGenerater = Mathf.Clamp(GameMgr.I.fireGenerater, minY, maxY);
        if (Input.GetMouseButton(0))
        {
            isReadytoFire = true;
            float mouseY = Input.GetAxis("Mouse Y")* moveSpeed;
            if (Mathf.Abs(mouseY) > 0.001f)
            {
                float moveAmount = Mathf.Abs(mouseY);
                GameMgr.I.fireGenerater += moveAmount * resistance;
            }
            else
            {
                GameMgr.I.fireGenerater -= decreaseRate * _decreaseTime;
            }          
            Vector3 targetPosition = Stick.position;
            targetPosition.z += mouseY;
            targetPosition.z = Mathf.Clamp(targetPosition.z, minY, maxY);
            Stick.position = targetPosition;
        }
        else
        {
            GameMgr.I.fireGenerater -= decreaseRate * _decreaseTime;
            isReadytoFire = false;
        }
    }
}
