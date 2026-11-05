using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowingFire : MonoBehaviour
{
    public float blowResistance;
    private float mouseMoveAmout;
    public Transform FireOnTinder;
    public float FireSize;
    // Start is called before the first frame update
    void Start()
    {
        FireOnTinder.localScale = new Vector3(FireSize, FireSize, FireSize);
    }

    // Update is called once per frame
    void Update()
    {
        if(FireSize >= 0.7f)
        {
            GameMgr.I.ClearText.gameObject.SetActive(true);
        }
        FireSize = Mathf.Clamp(FireSize, 0, 0.7f);
        GameMgr.I.BlowAmount = Mathf.Clamp(GameMgr.I.BlowAmount, 0, 100);
        if (Input.GetMouseButton(0))
        {
            mouseMoveAmout = Input.GetAxis("Mouse Y");
            if (GameMgr.I.canBlowFire)
            {
                float blowAmout = Mathf.Clamp(mouseMoveAmout, 0, 100) * blowResistance;               
                GameMgr.I.BlowAmount += blowAmout;
                if(GameMgr.I.isFire)
                {
                    FireSize += blowAmout;
                    FireOnTinder.localScale = new Vector3(FireSize, FireSize, FireSize);
                }               
            }
        }       
    }
}
