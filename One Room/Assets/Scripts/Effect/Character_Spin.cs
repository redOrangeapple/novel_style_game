using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Spin : MonoBehaviour
{
    [SerializeField] Transform tf_Target;

    private void Start() {
        
    }


    private void Update() {
        Quaternion t_Rotation = Quaternion.LookRotation(tf_Target.position);
        Vector3 t_Euler = new Vector3(0,t_Rotation.eulerAngles.y+90,0);       
        transform.eulerAngles = t_Euler ;                                                                 
    }
}
