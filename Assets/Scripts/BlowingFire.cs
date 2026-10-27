using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float blowResistance;
    private float mouseMoveAmout;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {          
            if (GameMgr.I.canBlowFire)
            {
                GameMgr.I.BlowAmount = mouseMoveAmout;
                mouseMoveAmout = Mathf.Clamp(mouseMoveAmout, 0, 100);
                mouseMoveAmout = Input.GetAxis("Mouse Y") * blowResistance;
                GameMgr.I.BlowAmount = mouseMoveAmout;
            }
        }
    }
}
