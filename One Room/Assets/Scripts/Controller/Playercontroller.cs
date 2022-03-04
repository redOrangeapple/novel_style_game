using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] Transform TF_crosshair;

    [SerializeField] Transform TF_Cam;

    [SerializeField] Vector2 CamBoundary; // 캠의 가두기 영역

    [SerializeField] float sightMoveSpeed; // 좌우 움직임 스피드

    [SerializeField] float sightSensitivity; // 고개움직임 속도

    [SerializeField] float lookLimit_X;
    [SerializeField] float lookLimit_Y;

    float CurrentAngle_X;
    float CurrentAngle_Y;

    [SerializeField] GameObject go_NotCamDown;
    [SerializeField] GameObject go_NotCamUp;
    [SerializeField] GameObject go_NotCamLeft;
    [SerializeField] GameObject go_NotCamRight;

    float OriginPosY;

    public void ResetAngle()
    {
        CurrentAngle_X = 0 ; 
        CurrentAngle_Y = 0 ;
    }

    private void Start() {
        OriginPosY = TF_Cam.localPosition.y;
    }

    void Update()
    {
        if(!InteractionController.isInteract)
        {
            CrosshairMoving();
            ViewMoving();
            KeyViewMoving();
            CameraLimit();
            NotCamUI();
        }

    }

    void NotCamUI()
    {
        go_NotCamDown.SetActive(false);
        go_NotCamUp.SetActive(false);
        go_NotCamLeft.SetActive(false);
        go_NotCamRight.SetActive(false);

        if(CurrentAngle_Y >= lookLimit_X)
            go_NotCamRight.SetActive(true);
        else if(CurrentAngle_Y<= -lookLimit_X)
            go_NotCamLeft.SetActive(true);    
        else if(CurrentAngle_X<=-lookLimit_Y)
            go_NotCamUp.SetActive(true);
        else if(CurrentAngle_X>=lookLimit_Y)
            go_NotCamDown.SetActive(true);        

        

    }

    void CameraLimit()
    {
        if(TF_Cam.localPosition.x >= CamBoundary.x)
        {
            TF_Cam.localPosition = new Vector3(CamBoundary.x
                                              ,TF_Cam.localPosition.y
                                              ,TF_Cam.localPosition.z);
        }

        else if(TF_Cam.localPosition.x <= -CamBoundary.x)
        {
            TF_Cam.localPosition = new Vector3(-CamBoundary.x
                                              ,TF_Cam.localPosition.y
                                              ,TF_Cam.localPosition.z);
        }

        
        if(TF_Cam.localPosition.y >= CamBoundary.y+1)
        {
            TF_Cam.localPosition = new Vector3(TF_Cam.localPosition.x
                                        ,CamBoundary.y+OriginPosY
                                        ,TF_Cam.localPosition.z);

        }

        else if(TF_Cam.localPosition.y <= 1-CamBoundary.y)
        {
            TF_Cam.localPosition = new Vector3(TF_Cam.localPosition.x
                                        ,OriginPosY-CamBoundary.y
                                        ,TF_Cam.localPosition.z);

        }


    }

    void KeyViewMoving()
    {
        if(Input.GetAxisRaw("Horizontal") !=0)
        {
            CurrentAngle_Y += sightSensitivity *Input.GetAxisRaw("Horizontal");

            CurrentAngle_Y = Mathf.Clamp(CurrentAngle_Y,-lookLimit_X ,lookLimit_X);

            TF_Cam.localPosition =
             new Vector3(TF_Cam.localPosition.x + sightMoveSpeed*Input.GetAxisRaw("Horizontal")
                        ,TF_Cam.localPosition.y
                        ,TF_Cam.localPosition.z);
        }

        if(Input.GetAxisRaw("Vertical") !=0)
        {
            CurrentAngle_X += sightSensitivity * -Input.GetAxisRaw("Vertical");

            CurrentAngle_X = Mathf.Clamp(CurrentAngle_X,-lookLimit_Y ,lookLimit_Y);

            TF_Cam.localPosition =
             new Vector3(TF_Cam.localPosition.x 
                        ,TF_Cam.localPosition.y + sightMoveSpeed*Input.GetAxisRaw("Vertical")
                        ,TF_Cam.localPosition.z);
        }

        TF_Cam.localEulerAngles = new Vector3(CurrentAngle_X,CurrentAngle_Y,TF_Cam.localEulerAngles.z);

    }

    void ViewMoving()
    {
        if(TF_crosshair.localPosition.x > (Screen.width/2-100) 
        || TF_crosshair.localPosition.x < (-Screen.width/2+100))
        {
            CurrentAngle_Y += (TF_crosshair.localPosition.x > 0) ? sightSensitivity : -sightSensitivity;

            CurrentAngle_Y = Mathf.Clamp(CurrentAngle_Y,-lookLimit_X,lookLimit_X);


            float t_applyspeed = (TF_crosshair.localPosition.x >0) ? sightMoveSpeed : -sightMoveSpeed;
            TF_Cam.localPosition = new Vector3(TF_Cam.localPosition.x+t_applyspeed,
                                               TF_Cam.localPosition.y,TF_Cam.localPosition.z);

        }


        if(TF_crosshair.localPosition.y > (Screen.height/2-100) 
        || TF_crosshair.localPosition.y < (-Screen.height/2+100))
        {
            CurrentAngle_X += (TF_crosshair.localPosition.y > 0) ? -sightSensitivity : sightSensitivity;

            CurrentAngle_X = Mathf.Clamp(CurrentAngle_X,-lookLimit_Y,lookLimit_Y);


            float t_applyspeed = (TF_crosshair.localPosition.y >0) ? sightMoveSpeed : -sightMoveSpeed;
            TF_Cam.localPosition = new Vector3(TF_Cam.localPosition.x,
                                               TF_Cam.localPosition.y+t_applyspeed,TF_Cam.localPosition.z);


        }




        TF_Cam.localEulerAngles = new Vector3(CurrentAngle_X,CurrentAngle_Y,TF_Cam.localEulerAngles.z);

    }


    void CrosshairMoving()
    {
        TF_crosshair.localPosition = new Vector2(Input.mousePosition.x - (Screen.width/2)
                                     , Input.mousePosition.y -(Screen.height/2));


        float t_cursorPosX = TF_crosshair.localPosition.x;
        float t_cursorPosY = TF_crosshair.localPosition.y;
        t_cursorPosX = Mathf.Clamp(t_cursorPosX,(-Screen.width/2+50),(Screen.width/2-50));
        t_cursorPosY = Mathf.Clamp(t_cursorPosY,(-Screen.height)/2+50,(Screen.height/2-50));

        TF_crosshair.localPosition = new Vector2(t_cursorPosX,t_cursorPosY);

    }
}
