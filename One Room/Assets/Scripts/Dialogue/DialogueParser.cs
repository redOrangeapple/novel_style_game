using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _csvFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); // 대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_csvFileName); // csv 파일을 가져옴

        string[] data = csvData.text.Split(new char[]{'\n'});
        
        for(int i = 1;i<data.Length;)
        {
            //Debug.Log(data[i]);
            string[] row =data[i].Split(new char[]{','});
       
            Dialogue dialogue = new Dialogue(); // 대사 리스트를 생성

            dialogue.name = row[1];
           // Debug.Log(row[1]);

            List<string> contextList = new List<string>();
            List<string> spriteList = new List<string>();
            List<string> VoiceList =  new List<string>();

            do{
                contextList.Add(row[2]);
                // Debug.Log(row[2]);
                spriteList.Add(row[3]);
                VoiceList.Add(row[4]);
                Debug.Log(row[4]);
                if(++i<data.Length)
                {
                    row =data[i].Split(new char[]{','});
                }
                else
                {
                    break;
                }

            }while(row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();
            dialogue.spriteName = spriteList.ToArray();
            dialogue.VoiceName = VoiceList.ToArray();

            dialogueList.Add(dialogue);


        }
        return dialogueList.ToArray();
    }


//     void Start()
//     {
//       Parse("prologue"); 

//     }
}
