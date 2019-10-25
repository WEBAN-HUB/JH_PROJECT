using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
public enum CMVIEW
{
    SIDE = 0,

    TOP = 1
};

public class CActor : MonoBehaviour
{

    public float mSpeed = 0.25f;

  

    public CMVIEW PLAYERVIEW = CMVIEW.TOP;
    bool Isground = true;
    bool RayShielder = false;
    int RScalar = 1;
    int LREdge = 0;
    float JumpScalar = 15f; 

    CCamera mCamera = null;
    Rigidbody mRigidbody;
    public GameObject mBody = null;
    public GameObject DeadParticle = null;

    bool IsAlive = true;
        
    void Start()
    {
        mCamera = FindObjectOfType<CCamera>();
        mRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
  
        if(Physics.Raycast(this.transform.position,Vector3.right,0.6f))
        {
            DoDead();
        }


        
        if (mCamera.IsCameraMove == false && IsAlive == true) 
        {
            switch (PLAYERVIEW)
            {
                case CMVIEW.SIDE:

                    if (Input.GetKeyDown(KeyCode.Space))
                    {

                        DoJump();
                    }
                    break;

                case CMVIEW.TOP:

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        DoMove();
                    }
                    break;
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftControl) && Isground == true)
        {
            cameraSwitch();
        }
    }


    public void cameraSwitch()
    {
        if (PLAYERVIEW == CMVIEW.TOP)
        {
            PLAYERVIEW = CMVIEW.SIDE;
            Debug.Log("SIDE VIEW");
        }
        else if (PLAYERVIEW == CMVIEW.SIDE)
        {
            PLAYERVIEW = CMVIEW.TOP;
            Debug.Log("TOP VIEW");
        }

        mCamera.CameraSwitch();

    }

    public void DoJump()
    {
        Debug.Log("INPUT SPACE1");
        if (Isground == true)
        {
            mBody.transform.localScale = Vector3.one;

            Debug.Log("JUMP");
            Isground = false;

            //mRigidbody.AddForce(Vector3.up * JumpScalar,ForceMode.Impulse);

            this.transform.DOMove(this.transform.position + (Vector3.up * 3), mSpeed * 2, false).SetEase(Ease.InOutQuart);

            if (mBody.transform.localScale == Vector3.one)
            {
                mBody.transform.DOScaleY(1.25f, 0.25f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
                mBody.transform.DOScaleZ(0.75f, 0.25f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
                mBody.transform.DOScaleX(0.75f, 0.25f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
            }

        }
    }

    public void DoMove()
    {
        Vector3 MoveVector = Vector3.zero;

        MoveVector.y = this.transform.position.y;

        Debug.Log("INPUT SPACE2");
        LREdge = LREdge + RScalar;
        if (LREdge >= 1)
        {
            RScalar = -1;
        }
        else if (LREdge <= -1)
        {
            RScalar = 1;
        }
        switch (LREdge)
        {

            case -1:
                MoveVector = MoveVector + (Vector3.forward * 2);
                this.transform.DOMove(MoveVector, mSpeed, false).SetEase(Ease.InOutQuart);

                break;

            case 0:

                this.transform.DOMove(MoveVector, mSpeed, false).SetEase(Ease.InOutQuart);


                break;

            case 1:
                MoveVector = MoveVector + (Vector3.forward * -2);
                this.transform.DOMove(MoveVector, mSpeed, false).SetEase(Ease.InOutQuart);

                break;
        }
        if (mBody.transform.localScale == Vector3.one)
        {
            mBody.transform.DOScaleZ(1.4f, 0.1f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
            mBody.transform.DOScaleY(0.6f, 0.1f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
            mBody.transform.DOScaleX(0.6f, 0.1f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
        }
        Debug.Log("TOP VIEW POSITION: " + LREdge);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tagGround"))
        {
            if (RayShielder == false)
            {
                Debug.Log("Ground");
                Isground = true;
                RayShielder = true;
            }
        }


    }

   
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("tagGround"))
        {
            Debug.Log("Not Ground");
            RayShielder = false;
        }

    }


    public void DoDead()
    {
        Debug.Log("으앙쥬금");
        IsAlive = false;
        mBody.SetActive(false);
        DeadParticle.SetActive(true);
    }
}
