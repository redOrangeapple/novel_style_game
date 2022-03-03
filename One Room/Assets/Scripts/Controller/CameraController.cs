using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void CameraTargeting(Transform P_Target,float P_CamSpeed=0.05f)
    {
        if(P_Target!= null)
        {
            StopAllCoroutines();
            StartCoroutine(CameraTargetingCoroutine(P_Target,P_CamSpeed));
        }
    }

    IEnumerator CameraTargetingCoroutine(Transform P_Target,float P_CamSpeed=0.05f)
    {
        Vector3 t_TargetPos = P_Target.position;

        Vector3 t_TargetFrontPos = t_TargetPos+P_Target.forward;

        Vector3 t_Direction = (t_TargetPos-t_TargetFrontPos).normalized;


        while(transform.position != t_TargetFrontPos || Quaternion.Angle(transform.rotation,Quaternion.LookRotation(t_Direction))>=0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position,t_TargetFrontPos,P_CamSpeed);
            //Rotation 관리시 대체적으로 Qauaternion 이용
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(t_Direction),P_CamSpeed);
            yield return null;


        }

    }
}
