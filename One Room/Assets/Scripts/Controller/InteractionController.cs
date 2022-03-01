using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera cam;


    RaycastHit hitinfo;
    [SerializeField] GameObject go_noraml_crosshair;
    [SerializeField] GameObject go_Interactive_crosshair;
    [SerializeField] GameObject go_Crosshair;
    [SerializeField] GameObject go_Cursor;

    bool isContact = false;

    public static bool isInteract = false;

    [SerializeField] ParticleSystem ps_question_Effect;
    
    DialogueManager theDm;

    public void HideUI()
    {
        go_Crosshair.SetActive(false);
        go_Cursor.SetActive(false);

    }


    private void Start() {
        theDm = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        CheckObj();
        ClickLeftBtn();
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


    void ClickLeftBtn()
    {   
        if(!isInteract)
        {
                    //마우스 좌클릭 감지
            if(Input.GetMouseButtonDown(0))
            {
                if(isContact)
                {
                    Interact();
                }
            }
        }
    }

    void Interact()
    {
        isInteract = true;

        ps_question_Effect.gameObject.SetActive(true);

        Vector3 t_targetPos = hitinfo.transform.position;
        ps_question_Effect.GetComponent<QuestionEffect>().SetTartget(t_targetPos);

        ps_question_Effect.transform.position = cam.transform.position;

        StartCoroutine(waitCollision());
       

    }

    IEnumerator waitCollision()
    {
        yield return new WaitUntil(()=>QuestionEffect.isCollide);
        QuestionEffect.isCollide = false;

        theDm.ShowDialogue();
    }


}
