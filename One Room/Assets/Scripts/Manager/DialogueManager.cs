using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;

    [SerializeField] Text txt_Dialgoue;
    [SerializeField] Text txt_Name;

    Dialogue[] dialogues;

    bool isDialogue = false;
    bool isNext = false; // 특정키 입력 대기

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;
    
    int lineCount = 0 ; // 대화 카운트
    int ConTextCount = 0 ; // 대사카운트 같은 사람이 여러번 걸쳐 말할때 카운트 됨

    InteractionController theIC;

    private void Start() {
        theIC = FindObjectOfType<InteractionController>();
    }   

    private void Update() {
         if(isDialogue)
         {
             if(isNext)
             {
                 if(Input.GetKeyDown(KeyCode.Space))
                 {
                     isNext = false;
                     txt_Dialgoue.text ="";
                     if(++ConTextCount < dialogues[lineCount].contexts.Length)
                     {
                        StartCoroutine(TypeWriter());
                     }
                     else
                     {
                         ConTextCount = 0 ;
                         if(++lineCount < dialogues.Length)
                         {

                            StartCoroutine(TypeWriter());
                         }
                         else
                         {
                             EndDialogue();

                         }

                     }

                    

                 }

             }
         }   
    }   


    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_Dialgoue.text="";
        txt_Name.text="";

        theIC.SettingUI(false);

        dialogues = p_dialogues;

        StartCoroutine(TypeWriter());
    }

    void EndDialogue()
    {
        isDialogue = false;
        ConTextCount = 0;
        lineCount =0 ;
        dialogues = null;
        isNext = false;
        theIC.SettingUI(true);
        SettingUI(false);
    }

     IEnumerator TypeWriter()
    {
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[ConTextCount]; // '`' --> ',' 로 바꿔주는놈 

        t_ReplaceText = t_ReplaceText.Replace("`",",");
         t_ReplaceText = t_ReplaceText.Replace("\\n","\n");

        //txt_Dialgoue.text = t_ReplaceText;

        

        bool t_white = false;
        bool t_yellow = false;
        bool t_cyan = false;


        bool t_ignore= false;


        for(int i = 0 ; i< t_ReplaceText.Length;i++)
        {
            switch(t_ReplaceText[i])
            {
                case 'ⓦ': t_white = true;
                           t_yellow = false;
                           t_cyan = false;
                           t_ignore = true;
                break;

                case 'ⓨ': t_white = false;
                           t_yellow = true;
                           t_cyan  = false;
                           t_ignore = true;
                break; 
                case 'ⓒ': t_white  = false;
                           t_yellow = false;
                           t_cyan   = true;
                           t_ignore = true;
                break; 
            }
            //https://docs.google.com/spreadsheets/d/1tP7aNJ2_vkWzmn3UsT2KTdrw5uykZc_0mzFbRVMRx5c/edit#gid=0
            string t_letter = t_ReplaceText[i].ToString();

            if(!t_ignore)
            {
                if(t_white == true)
                {t_letter= "<color=#ffffff>"+ t_letter +"</color>";}
                else if(t_yellow == true){t_letter = "<color=#FFFF00>"+ t_letter +"</color>";}
                else if(t_cyan == true) {t_letter = "<color=#5DDED4>"+ t_letter +"</color>";}


                txt_Dialgoue.text += t_letter;

            }
            t_ignore = false;
            //txt_Dialgoue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);

        }

        isNext = true;

        // yield return null;

    }




    void SettingUI(bool _p_flag)
    {
        go_DialogueBar.SetActive(_p_flag);

        if(_p_flag)
        {
            if(dialogues[lineCount].name=="")
            {
                 go_DialogueNameBar.SetActive(false);
            }
            else
            {
                go_DialogueNameBar.SetActive(true);
                txt_Name.text = dialogues[lineCount].name; 
            }

        }
     

    }

}
