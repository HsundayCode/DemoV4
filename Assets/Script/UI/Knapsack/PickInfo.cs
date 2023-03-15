using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickInfo : MonoBehaviour
{
    public Text BgText;
    public Text Info;
    public void SetInfo(string info,Vector2 Pos)
    {
        BgText.text = info;
        Info.text = info;
        transform.position = Pos+new Vector2(0.8f,0);
        ShowInfo();
    }
    public void ShowInfo()
    {
        gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        gameObject.SetActive(false);
    }
}
