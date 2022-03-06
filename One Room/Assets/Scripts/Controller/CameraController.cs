using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    Vector3 OriginPos;

    Quaternion OriginRot;

    InteractionController theIC ;

    Playercontroller thePlayer;

    Coroutine coroutine;

    private void Start() {
        theIC = FindObjectOfType<InteractionController>();
        thePlayer = FindObjectOfType<Playercontroller>();
    }


    public void CameraOrginSetting()
    {
        OriginPos = transform.position;
        Debug.Log("Orginpos 는  " +OriginPos +" 입니다" );
        OriginRot = Quaternion.Euler(0,-90,0);
    }


    public void CameraTargeting(Transform P_Target,float P_CamSpeed=0.02f,bool p_isReset = false, bool p_isFinish = false)
    {
        

        if(!p_isFinish)
        {
            if(P_Target != null)
            {
                StopAllCoroutines(); 
                coroutine =  StartCoroutine(CameraTargetingCoroutine(P_Target,P_CamSpeed));
            }

        }
        else 
        {
            if(coroutine!= null)
            {
                StopCoroutine(coroutine);
            }
              StartCoroutine(CameraResetCoroutine(P_CamSpeed,p_isFinish));

        }
       
    }

    IEnumerator CameraTargetingCoroutine(Transform P_Target,float P_CamSpeed=0.05f)
    {
        Vector3 t_TargetPos = P_Target.position;

        Vector3 t_TargetFrontPos = t_TargetPos + P_Target.forward*2;

        Vector3 t_Direction = (t_TargetPos-t_TargetFrontPos).normalized;


        while(transform.position != t_TargetFrontPos || Quaternion.Angle(transform.rotation,Quaternion.LookRotation(t_Direction))>=0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position,t_TargetFrontPos,P_CamSpeed);
            //Rotation 관리시 대체적으로 Qauaternion 이용
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(t_Direction),P_CamSpeed);
            yield return null;


        }

    }

    IEnumerator CameraResetCoroutine(float P_CamSpeed= 0.1f,bool p_isFinish =false)
    {
        yield return new WaitForSeconds(0.5f);

          while(transform.position != OriginPos || Quaternion.Angle(transform.rotation,OriginRot)>=0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position,OriginPos,P_CamSpeed);
            //Rotation 관리시 대체적으로 Qauaternion 이용
            transform.rotation = Quaternion.Lerp(transform.rotation,OriginRot,P_CamSpeed);
            yield return null;


        }


        transform.position = OriginPos;

        if(p_isFinish)
        {
            // 모든 대화가 끝났으면 Reset



            thePlayer.ResetAngle();
            theIC.SettingUI(true);

        }

    }
}
