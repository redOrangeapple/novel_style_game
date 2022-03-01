using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

    [SerializeField] float SpinSpeed;

    [SerializeField] Vector3 spinDir;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinDir*SpinSpeed*Time.deltaTime);
    }
}
