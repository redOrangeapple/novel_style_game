using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
   public string name;
   public AudioClip clip;

}

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    [SerializeField] Sound[] effectSounds;

    [SerializeField] AudioSource[] effectPlayer;

    [SerializeField] Sound[] bgmSounds;

    [SerializeField] AudioSource bgmPlayer;

    [SerializeField] AudioSource voicePlayer;
    
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else Destroy(gameObject);
    }

    
    void PlayBGM(string _p_name)
    {
        for(int i = 0 ; i < bgmSounds.Length ; i++)
        {
            if(_p_name == bgmSounds[i].name)
            {

                    bgmPlayer.clip = bgmSounds[i].clip;
                    bgmPlayer.Play();
                    return;          
             }

        }
        Debug.LogError("노래없다");

    }


    void StopBGM()
    {
        bgmPlayer.Stop();

    }

    void PauseBGM()
    {
        bgmPlayer.Pause();
    }

    void UnPausedBGM()
    {
        bgmPlayer.UnPause();
    }

    void PlayeffectSound(string _p_name)
    {
        for(int i = 0 ; i<effectSounds.Length;i++)
        {
            if(_p_name == effectSounds[i].name)
            {   
                for(int j = 0 ; j < effectPlayer.Length ; j++)
                {
                    if(!effectPlayer[j].isPlaying)
                    {
                        effectPlayer[j].clip = effectSounds[i].clip;
                        effectPlayer[j].Play();
                        return;
                    }
                        
                }
                Debug.LogError("모든플레이어 사용중"); return;
            }
        }
        Debug.LogError(_p_name + " 에 해당하는 효과음이 없습니다");

    }

    void StopallEffectSound()
    {
        for(int i = 0 ; i <effectSounds.Length ; i++)
        {
            effectPlayer[i].Stop();

        }


    }


    // void PlayvoiceSound(string _pname)
    // {
    //     AudioClip _clip = Resources.Load<AudioClip>("Sounds/Voices/bongran01") ;
    //     Debug.Log("발견된 이름 "+_pname+" " + _clip);
    //     if(_clip!=null)
    //     {//Assets/Resources/Sounds/Voice
    //         voicePlayer.clip = _clip;
    //         voicePlayer.Play();
            
    //      }
    //      else
    //      { 
    //         Debug.LogError(_pname + " 에 해당하는 효과음이 없습니다");

    //     }


    // }

    /// <summary>
    /// p_type 0 브금재생
    /// p_type 1 효과음 재생"
    /// _p_name 2 보이스 재생"
    /// </summary>
    public void PlaySound(string _p_name , int p_type)
    {
        if(p_type == 0) PlayBGM(_p_name);
        else if(p_type==1) PlayeffectSound(_p_name);
       // else PlayvoiceSound(_p_name);

    }
}
