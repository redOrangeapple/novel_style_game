using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("대사를 하는 캐릭터의 이름")]
    public string name;

    [Tooltip("대사")]
    public string[] contexts;
}

[System.Serializable]
public class DialogueEvent
{ 
    public string name;

    public Vector2 line;

    //클래스를 배열로 만듬
    public Dialogue[] dialogues;
 
}
