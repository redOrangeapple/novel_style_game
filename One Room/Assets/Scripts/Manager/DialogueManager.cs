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

        //txt_Dialgoue.text = t_ReplaceText;

        txt_Name.text = dialogues[lineCount].name; 

        for(int i = 0 ; i< t_ReplaceText.Length;i++)
        {
            txt_Dialgoue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);

        }

        isNext = true;

        // yield return null;

    }




    void SettingUI(bool _p_flag)
    {
        go_DialogueBar.SetActive(_p_flag);
        go_DialogueNameBar.SetActive(_p_flag);

    }

}
