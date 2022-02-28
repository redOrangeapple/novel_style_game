using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] Transform TF_crosshair;

    void Update()
    {
        CrosshairMoving();
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
