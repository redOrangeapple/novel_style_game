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

    bool isDialogue = false;

    InteractionController theIC;

    private void Start() {
        theIC = FindObjectOfType<InteractionController>();
    }   



    public void ShowDialogue()
    {
        txt_Dialgoue.text="";
        txt_Name.text="";

        theIC.HideUI();

        SettingUI(true);

    }

    void SettingUI(bool _p_flag)
    {
        go_DialogueBar.SetActive(_p_flag);
        go_DialogueNameBar.SetActive(_p_flag);

    }

}