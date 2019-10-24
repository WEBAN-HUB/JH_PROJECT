using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
public class CCamera : MonoBehaviour
{
    Vector3 TopVec = Vector3.zero;
    Vector3 SideVec = Vector3.zero;
    Vector3 TopRot = Vector3.zero;
    Vector3 SideRot = Vector3.zero;

    Vector3 CMoveV = Vector3.zero;

    public GameObject SideObject = null;
    CActor mPlayer = null;
    public GameObject LookPos = null;
    float CameraSpeed = 0.5f;
    
    CMVIEW CAMERAVIEW = CMVIEW.TOP;
    void Start()
    {
        TopVec.x = -3;
        TopVec.y = 3;
        TopVec.z = 0;

        //SideVec.x = -3.5f;
        //SideVec.y = 0.85f;
        //SideVec.z = -2.2f;

        SideVec.x = -0.7f;
        SideVec.y = 1.15f;
        SideVec.z = -6.5f;

        TopRot.x = 150;
        TopRot.y = -90;
        TopRot.z = -180;

        SideRot.x = 6;
        SideRot.y = 65;
        SideRot.z = 8;

        CMoveV.x = 24f;
        CMoveV.y = -25;
        CMoveV.z = -8;



        mPlayer = FindObjectOfType<CActor>();

        CameraSwitch();
    }

   
    void Update()
    {
        this.transform.LookAt(LookPos.transform.position);

    }

    public void CameraSwitch()
    {
        CAMERAVIEW = mPlayer.PLAYERVIEW;

        switch (CAMERAVIEW)
        {
            case CMVIEW.SIDE:
                //this.transform.DOLocalRotate(SideRot, CameraSpeed, RotateMode.FastBeyond360);
                this.transform.SetParent(SideObject.transform);
                this.transform.DOMove(SideVec, CameraSpeed, false);
                

                break;

            case CMVIEW.TOP:
                //this.transform.DOLocalRotate(TopRot, CameraSpeed, RotateMode.FastBeyond360);
                this.transform.SetParent(mPlayer.transform);

                this.transform.DOMove(mPlayer.transform.position + TopVec, CameraSpeed, false);
        

                break;
        }

    }
}
