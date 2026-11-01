using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr I;
    public float fireGenerater = 0;
    public float BlowAmount = 0;
    public StartSequence StartSequence;
    public GameObject Spark;
    public GameObject Smoke;
    public float dust;
    public bool canMakeFire = true;
    public bool canMoveFire = false;
    public bool canBlowFire = false;
    public bool isFire = false;
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
        MovingFire,
        BlowingFire,
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
            ChangeState(State.MovingFire);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(State.BlowingFire);
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
                canMakeFire = true;
                canMoveFire = false;
                canBlowFire = false;
                StartSequence.MakeFire();
                break;

            case State.MovingFire:
                canMakeFire = false;
                canMoveFire = true;
                canBlowFire = false;
                StartSequence.MoveFire();
                break;

            case State.BlowingFire:
                canMakeFire = false;
                canMoveFire = false;
                canBlowFire = true;
                StartSequence.BlowFire();
                break;
        }
    }
}
