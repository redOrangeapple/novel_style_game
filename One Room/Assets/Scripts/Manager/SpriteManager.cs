using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;

    bool CheckSameSprite(SpriteRenderer p_SpriteRenderer,Sprite p_Sprite)
    {
        if(p_SpriteRenderer.sprite == p_Sprite)  
        return true;
        else return false;

    }

    public IEnumerator SpriteChangeCoroutine(Transform p_Target,string p_SpriteName)
    {
        SpriteRenderer[] t_sprite_renderer = p_Target.GetComponentsInChildren<SpriteRenderer>();
        
        Sprite t_sprtie = Resources.Load("Characters/" + p_SpriteName,typeof(Sprite)) as Sprite;


       if(CheckSameSprite(t_sprite_renderer[0],t_sprtie) == false)
       {
            Color t_color = t_sprite_renderer[0].color;
            Color t_ShadowColor = t_sprite_renderer[1].color;
            t_color.a = 0;
            t_ShadowColor.a = 0;
            t_sprite_renderer[0].color = t_color;
            t_sprite_renderer[1].color = t_ShadowColor ;
        
            t_sprite_renderer[0].sprite = t_sprtie;
            t_sprite_renderer[1].sprite = t_sprtie;



            while(t_color.a <1)
            {
                t_color.a += fadeSpeed;
                t_ShadowColor.a += fadeSpeed;


                t_sprite_renderer[0].color = t_color;
                t_sprite_renderer[1].color = t_ShadowColor;

                yield return null;
            }

       }
    }


}
