using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr I;
    public float fireGenerater = 0;
    public float BlowAmount = 0;
    public StartSequence StartSequence;
    public MakingFire MakingFire;
    public BlowingFire BlowingFire;
    public GameObject Spark;
    public GameObject Smoke;
    public GameObject Dustpile;
    public TextMeshProUGUI ClearText;
    public float dust;
    public float woodfriction;
    public float dustpileScaleMin = 0.01f;
    public float dustpileScaleMax = 0.24f;
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
        SetDust(dust);
        ClearText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        if (fireGenerater >= 0.2f && dust >= 0.8f)
        {
            Fire();
        }
        if (fireGenerater >= 0.15f && dust >= 0.5f)
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
        if (dust <= 0.6f)
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
    public void SetDust(float dust)
    {
        this.dust = dust;
        if (dust >= 0.4f)
        {
            Dustpile.GetComponent<MeshRenderer>().material.color = Color.black;
        }
        else
        {
            Dustpile.GetComponent<MeshRenderer>().material.color = Color.gray;
        }

        if (dust <= 0.01)
        {
            Dustpile.transform.localScale = Vector3.zero;
        }
        else
        {
            Dustpile.transform.localScale = Vector3.one * Mathf.Clamp(dust, dustpileScaleMin, dustpileScaleMax);
        }
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
                SetDust(0);
                woodfriction = 1;
                fireGenerater = 0;
                BlowAmount = 0;
                Smoke.SetActive(false);
                Spark.SetActive(false);
                isFire = false;
                ClearText.gameObject.SetActive(false);
                BlowingFire.Reload();
                break;
        }
    }
}
