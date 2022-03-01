using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera cam;


    RaycastHit hitinfo;
    [SerializeField] GameObject go_noraml_crosshair;
    [SerializeField] GameObject go_Interactive_crosshair;

    bool isContact = false;


    void Update()
    {
        CheckObj();
    }

    void CheckObj()
    {
        Vector3 t_mousePos =  new Vector3(Input.mousePosition.x
                                         ,Input.mousePosition.y
                                         ,0);   

        if(Physics.Raycast(cam.ScreenPointToRay(t_mousePos),out hitinfo, 100))
        {
            //Debug.Log(hitinfo.transform.name);
            Contact();

        }
        else
        {
            NotContact();

        }



    }

    void Contact()
    {
        if(hitinfo.transform.CompareTag("Interaction"))
        {
            if(!isContact)
            {
                isContact = true;
                go_Interactive_crosshair.SetActive(true);
                go_noraml_crosshair.SetActive(false);
            }
        }
        else 
        {
            NotContact();
        }

    }

    void NotContact()
    {
        if(isContact)
        {
            isContact = false;
            go_Interactive_crosshair.SetActive(false);
            go_noraml_crosshair.SetActive(true);
        }

    }


}
