using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class StartSequence : MonoBehaviour
{  
    public Transform TinderDefaultPosition;
    public Transform TinderBlowPosition;
    public Transform TinderMovePosition;
    public Transform CameraDefaultPosition;
    public Transform CameraMovePosition;
    public Transform CameraBlowPosition;
    public Transform PlankDefaultPosition;
    public Transform PlankMovePosition;
    public Transform StickDefaultPosition;
    public GameObject Tinder;
    public GameObject Camera;
    public GameObject Plank;
    public GameObject Stick;

    public void MakeFire()
    {
        StartCoroutine(co_MakeFire());
    }

    public IEnumerator co_MakeFire()
    {
        //RArm.transform.position = GuideMgr.GetGuidePos(guideIndex);
        Sequence MakeFire = DOTween.Sequence();
        MakeFire
             .AppendCallback(() =>
             {
                 GameMgr.I.canMakeFire = false;
                 GameMgr.I.canBlowFire = false;
                 GameMgr.I.isMove = false;
             })
            .Append(Tinder.transform.DOMove(TinderDefaultPosition.position, 0.3f))
            .Join(Camera.transform.DOMove(CameraDefaultPosition.position, 0.3f))
            .Join(Camera.transform.DORotateQuaternion(CameraDefaultPosition.rotation, 0.3f))          
            .AppendCallback(() =>
            {
                GameMgr.I.canMakeFire = true;
                GameMgr.I.canBlowFire = false;
                GameMgr.I.isMove = true;
            });
        yield return MakeFire.WaitForCompletion();
    }

    public void BlowFire()
    {
        StartCoroutine(co_BlowFire());
    }

    public IEnumerator co_BlowFire()
    {
        //RArm.transform.position = GuideMgr.GetGuidePos(guideIndex);
        Sequence BlowFire = DOTween.Sequence();
        BlowFire
            .AppendCallback(() =>
            {
                GameMgr.I.canMakeFire = false;
                GameMgr.I.canBlowFire = false;
                GameMgr.I.isMove = false;
            })
            .Append(Tinder.transform.DOMove(TinderBlowPosition.position, 0.6f))          
            .Join(Camera.transform.DOMove(CameraBlowPosition.position, 0.4f))
            .Join(Camera.transform.DORotateQuaternion(CameraBlowPosition.rotation, 0.4f))
            .SetEase(Ease.InOutSine)
            .AppendCallback(() =>
            {
                GameMgr.I.canMakeFire = false;
                GameMgr.I.canBlowFire = true;
                GameMgr.I.isMove = true;
            });
        yield return BlowFire.WaitForCompletion();
    }

    public void Reloading()
    {
        StartCoroutine(co_Reloading());
    }

    public IEnumerator co_Reloading()
    {
        //RArm.transform.position = GuideMgr.GetGuidePos(guideIndex);
        Sequence Reloading = DOTween.Sequence();
        Reloading
            .AppendCallback(() =>
            {
                GameMgr.I.canMakeFire = false;
                GameMgr.I.canBlowFire = false;
                GameMgr.I.isMove = false;
            })
            .Join(Camera.transform.DOMove(CameraDefaultPosition.position, 0.5f))
            .Join(Camera.transform.DORotateQuaternion(CameraDefaultPosition.rotation, 0.5f))
            .SetEase(Ease.InOutSine)
            .Append(Tinder.transform.DOMoveZ(-2, 1.2f))
            .Join(Plank.transform.DOMoveZ(-2, 1.2f))
            .Join(Stick.transform.DOMoveZ(-2, 1.2f))
            .SetEase(Ease.InOutCubic)
            .Append(Tinder.transform.DOMove(TinderDefaultPosition.position, 0.3f))
            .Join(Plank.transform.DOMove(PlankDefaultPosition.position, 0.3f))
            .Join(Stick.transform.DOMove(StickDefaultPosition.position, 0.3f))
            .SetEase(Ease.InOutSine)
            .AppendCallback(() =>
            {
                GameMgr.I.canMakeFire = true;
                GameMgr.I.canBlowFire = false;
                GameMgr.I.isMove = true;
            });
        yield return Reloading.WaitForCompletion();
        GameMgr.I.ChangeState(GameMgr.State.MakingFire);
    }

    public void MoveFire()
    {
        StartCoroutine(co_MoveFire());
    }

    public IEnumerator co_MoveFire()
    {
        //RArm.transform.position = GuideMgr.GetGuidePos(guideIndex);
        Sequence BlowFire = DOTween.Sequence();
        BlowFire
            .AppendCallback(() =>
            {
                GameMgr.I.isMove = false;
                GameMgr.I.canMakeFire = false;
                GameMgr.I.canBlowFire = false;
            })
            .Append(Stick.transform.DOMoveZ(-2, 0.5f))
            .Join(Tinder.transform.DOMove(TinderMovePosition.position, 0.5f))
            .SetEase(Ease.InOutCubic)
            .Append(Plank.transform.DOMove(PlankMovePosition.position, 1))           
            .Join(Plank.transform.DORotateQuaternion(PlankMovePosition.rotation, 1))   
            .SetEase(Ease.InOutSine)
            .Join(Camera.transform.DOMove(CameraMovePosition.position, 0.3f))
            .Join(Camera.transform.DORotateQuaternion(CameraMovePosition.rotation, 0.3f))
            .Append(Plank.transform.DOMove(PlankDefaultPosition.position, 0.5f))
            .Join(Plank.transform.DORotateQuaternion(PlankDefaultPosition.rotation, 0.5f))
            .SetEase(Ease.InOutSine)
            .AppendCallback(() =>
            {
                GameMgr.I.canMakeFire = false;
                GameMgr.I.canBlowFire = true;
                GameMgr.I.isMove = true;
            });
        yield return BlowFire.WaitForCompletion();
        StartCoroutine(co_BlowFire());
    }
}
