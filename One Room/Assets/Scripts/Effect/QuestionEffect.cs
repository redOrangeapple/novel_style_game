using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionEffect : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Vector3 targetPos = new Vector3();

    [SerializeField] ParticleSystem ps_effect;

    public static bool isCollide = false;

    public void SetTartget(Vector3 _target)
    {
        targetPos = _target ;

    }


    // Update is called once per frame
    void Update()
    {
        if(targetPos != Vector3.zero)
        {
            if((transform.position - targetPos).sqrMagnitude >=0.1f)
            transform.position = Vector3.Lerp(transform.position,targetPos,moveSpeed); 
            else{
                ps_effect.gameObject.SetActive(true);
                ps_effect.transform.position = transform.position;
                ps_effect.Play();
                isCollide = true;
                targetPos = Vector3.zero;
                gameObject.SetActive(false);
            }
        }
    }
}
