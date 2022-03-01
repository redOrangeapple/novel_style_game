using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Type : MonoBehaviour
{
    public bool isDoor;
    public bool isObj;

    [SerializeField] string interActionName;
    // Start is called before the first frame update

    public string GetName()
    {
        return interActionName;
    }

}
