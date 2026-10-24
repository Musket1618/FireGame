using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr I;
    public float fireGenerater = 0;
    public GameObject Spark;
    public GameObject Smoke;  
    public float dust;
    // Start is called before the first frame update
    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        Spark.SetActive(false);
        Smoke.SetActive(false);
    }
    // Update is called once per frame
    public void Update()
        {
            if(fireGenerater >= 1 && dust >= 1f)
            {
              Fire();
            }
        if (fireGenerater >= 0.3f && dust >= 0.5f)
        {
            getSmoke();
        }
        else
        {
            Smoke.SetActive(false);
        }
    }


    public void getSmoke()
    {
        Smoke.SetActive(true);
    }
    public void Fire()
    {
        Spark.SetActive(true);
    }
   }
