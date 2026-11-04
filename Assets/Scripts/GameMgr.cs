using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr I;
    public float fireGenerater = 0;
    public float BlowAmount = 0;
    public StartSequence StartSequence;
    public MakingFire MakingFire;
    public GameObject Spark;
    public GameObject Smoke;
    public float dust;
    public float woodfriction;
    public bool canMakeFire = true;
    public bool canBlowFire = false;
    public bool isFire = false;
    public bool isMove = false;
    private State _state;
    public State currentState;
    // Start is called before the first frame update
    private void Awake()
    {
        I = this;
    }
    public enum State
    {
        /// <summary>
        /// 
        /// </summary>
        MakingFire,
        BlowingFire,
        Reloading,
    }


    private void Start()
    {
        ChangeState(currentState);
        Spark.SetActive(false);
        Smoke.SetActive(false);
    }
    // Update is called once per frame
    public void Update()
    {
        if (fireGenerater >= 1 && dust >= 1f)
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
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState(State.MakingFire);
        }      
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState(State.BlowingFire);
        }
        if (Input.GetKeyDown(KeyCode.R) && isMove)
        {
            ChangeState(State.Reloading);
        }
        if (dust <= 0.9f)
        {
            Spark.SetActive(false);
            isFire = false;
        }
    }


    public void getSmoke()
    {
        Smoke.SetActive(true);
    }
    public void Fire()
    {
        Spark.SetActive(true);
        isFire = true;
    }
    public void ChangeState(State state)
    {
        _state = state;
        switch (_state)
        {
            case State.MakingFire:
                StartSequence.MakeFire();
                break;         
            case State.BlowingFire:
                StartSequence.MoveFire();
                break;
            case State.Reloading:
                StartSequence.Reloading();
                dust = 0;
                woodfriction = 1;
                fireGenerater = 0;
                BlowAmount = 0;
                break;
        }
    }
}
