using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoSingleton<AudioManage>
{
    [Range(0,1)]
    public float AllVolume = 1;
    Dictionary<GameObject,AudioSource> AllObjectSoundDic;
    //需要记录当前正在播放的音乐
    string SoundPath;

    private void Awake() {
        SoundPath = "Sound/";
        AllObjectSoundDic = new Dictionary<GameObject, AudioSource>();
    }

    public void PlaySound(GameObject g,string SoundName,bool loop = false)
    {
        
        if(AllObjectSoundDic.ContainsKey(g))//物体有音频播放组件
        {
            AudioSource sound = AllObjectSoundDic[g];
            if(sound.clip.name == SoundName)//正在获已经播放音频文件
            {
                sound.loop = loop;
                sound.volume = AllVolume;
                sound.Play();
            }else
            {
                var s = ResourceLoading.Instance.Load<AudioClip>(SoundPath+SoundName);//换音频
                sound.clip = s;
                sound.loop = loop;
                sound.volume = AllVolume;
                sound.Play();
            }
        }else
        {
            AudioSource sound =  g.AddComponent<AudioSource>();//物体没有音频播放组件
            var s = ResourceLoading.Instance.Load<AudioClip>(SoundPath+SoundName);
            sound.clip = s;
            sound.loop = loop;
            sound.volume = AllVolume;
            sound.playOnAwake = false;
            sound.Play();
            AllObjectSoundDic.Add(g,sound);
        }
    }
}
