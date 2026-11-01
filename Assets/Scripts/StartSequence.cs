using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class StartSequence : MonoBehaviour
{
    public Transform TinderMovePosition;
    public Transform TinderDefaultPosition;
    public Transform TinderBlowPosition;
    public Transform CameraMovePosition;
    public Transform CameraDefaultPosition;
    public Transform PlankDefaultRotation;
    public Transform CameraBlowPosition;   
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
            .Join(Tinder.transform.DOMove(TinderDefaultPosition.position, 0.3f))
            .Join(Camera.transform.DOMove(CameraDefaultPosition.position, 0.3f))
            .Join(Camera.transform.DORotateQuaternion(CameraDefaultPosition.rotation, 0.3f))
            .Join(Plank.transform.DORotateQuaternion(PlankDefaultRotation.rotation, 0.3f))
            .AppendCallback(() =>
            {

            });
        yield return MakeFire.WaitForCompletion();
    }

    public void MoveFire()
    {
        StartCoroutine(co_MoveFire());
    }

    public IEnumerator co_MoveFire()
    {
        //RArm.transform.position = GuideMgr.GetGuidePos(guideIndex);
        Sequence MoveFire = DOTween.Sequence();
        MoveFire
            .Join(Tinder.transform.DOMove(TinderMovePosition.position, 0.3f))
            .Join(Camera.transform.DOMove(CameraMovePosition.position, 0.3f))
            .Join(Camera.transform.DORotateQuaternion(CameraMovePosition.rotation, 0.3f))
            .AppendCallback(() =>
            {

            });
        yield return MoveFire.WaitForCompletion();
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
            .Join(Tinder.transform.DOMove(TinderBlowPosition.position, 0.3f))
            .Join(Camera.transform.DOMove(CameraBlowPosition.position, 0.3f))
            .Join(Camera.transform.DORotateQuaternion(CameraBlowPosition.rotation, 0.3f))
            .Join(Plank.transform.DORotateQuaternion(PlankDefaultRotation.rotation, 0.3f))
            .AppendCallback(() =>
            {

            });
        yield return BlowFire.WaitForCompletion();
    }

    public void BiggerFire()
    {
        StartCoroutine(co_BiggerFire());
    }

    public IEnumerator co_BiggerFire()
    {
        //RArm.transform.position = GuideMgr.GetGuidePos(guideIndex);
        Sequence BiggerFire = DOTween.Sequence();
        BiggerFire
            //.Join(Tinder.transform.DOMove(TinderMovePosition.position, 0.3f))
            .AppendCallback(() =>
            {

            });
        yield return BiggerFire.WaitForCompletion();
    }
}
