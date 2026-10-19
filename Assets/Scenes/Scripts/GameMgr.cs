using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr I;
    public float fireGenerater = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        I = this;
    }
        // Update is called once per frame
        public void Update()
        {
            if(fireGenerater >= 1)
            {
              Fire();
            }
        }

    public void Fire()
    {
        
    }
   }
