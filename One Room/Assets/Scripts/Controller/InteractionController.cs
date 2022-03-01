using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera cam;


    RaycastHit hitinfo;
    [SerializeField] GameObject go_noraml_crosshair;
    [SerializeField] GameObject go_Interactive_crosshair;
    [SerializeField] GameObject go_Crosshair;
    [SerializeField] GameObject go_Cursor;

    [SerializeField] GameObject go_TargetNamebar;

    [SerializeField] Text txt_TargetName;

    bool isContact = false;

    public static bool isInteract = false;

    [SerializeField] ParticleSystem ps_question_Effect;

    [SerializeField] Image img_Interaction;
    [SerializeField] Image img_InteractionEffect;
    
    DialogueManager theDm;

    public void HideUI()
    {
        go_Crosshair.SetActive(false);
        go_Cursor.SetActive(false);
        go_TargetNamebar.SetActive(false);

    }


    private void Start() {
        theDm = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if(!isInteract)
        {
            CheckObj();
            ClickLeftBtn();
        }
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
            go_TargetNamebar.SetActive(true);
            txt_TargetName.text = hitinfo.transform.GetComponent<Interaction_Type>().GetName();

            if(!isContact)
            {
                isContact = true;
                go_Interactive_crosshair.SetActive(true);
                go_noraml_crosshair.SetActive(false);
                StopCoroutine("InteractionCoroutine");
                StopCoroutine("InteractionEffectCoroutine");
                StartCoroutine("InteractionCoroutine",true);
                StartCoroutine("InteractionEffectCoroutine");
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
            go_TargetNamebar.SetActive(false);
            isContact = false;
            go_Interactive_crosshair.SetActive(false);
            go_noraml_crosshair.SetActive(true);
            StopCoroutine("InteractionCoroutine");
            StartCoroutine("InteractionCoroutine",false);
        }

    }

    IEnumerator InteractionCoroutine(bool p_Appear)
    {
        Color color = img_Interaction.color;
        
        if(p_Appear)
        {
            color.a = 0 ;
            
            while(color.a<1)
            {
               color.a+=0.1f; 
               img_Interaction.color = color;
               yield return null; //1프레임 대기
            }

        }
        else{

            
            while(color.a >0)
            {
                color.a -= 0.1f;
                img_Interaction.color = color;
                yield return null;
            }

        }
    }

    IEnumerator InteractionEffectCoroutine()
    {
        while(isContact && !isInteract)
        {
            Color color = img_InteractionEffect.color;
            color.a = 0.5f;

            img_InteractionEffect.transform.localScale = new Vector3(1,1,1);
            Vector3 t_scale = new Vector3(1,1,1);

            while(color.a >0)
            {
                color.a -= 0.001f;
                img_InteractionEffect.color = color;
                t_scale.Set(t_scale.x+Time.deltaTime
                           ,t_scale.y+Time.deltaTime
                           ,t_scale.z+Time.deltaTime);

                img_InteractionEffect.transform.localScale = t_scale;     
                yield return null;

            }
            yield return null;

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

        StopCoroutine("InteractionCoroutine");
        Color color = img_Interaction.color;
        color.a = 0;
        img_Interaction.color = color;

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
