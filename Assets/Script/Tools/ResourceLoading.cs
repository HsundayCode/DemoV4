using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResourceLoading : BaseSingleton<ResourceLoading>
{
    Dictionary<string,object> ResContainer = new Dictionary<string, object>();
    Dictionary<string,Sprite> IconContaniner = new Dictionary<string, Sprite>();

    public T Load<T>(string resPath) where T :Object
    {
        if(IconContaniner.ContainsKey(resPath))
        {
            return IconContaniner[resPath] as T;
        }
        
        if(ResContainer.ContainsKey(resPath))
        {
            return ResContainer[resPath] as T;
        }
        T t = Resources.Load<T>(resPath);
        ResContainer.Add(resPath,t);
        return t;
    }

    public Sprite LoadSpriteInAtlas(string AtlasPath,string name)
    {
        if(ResContainer.ContainsKey(name))
        {
            return ResContainer[name] as Sprite;
        }
        Sprite[] sprites = Resources.LoadAll<Sprite>(AtlasPath);
        foreach (Sprite item in sprites)
        {
            ResContainer.Add(item.name,item);
        }

        return (Sprite)ResContainer[name];
    }

    public void LoadAllSpriteInAtlas()
    {
        
        DirectoryInfo directoryInfo = new DirectoryInfo(@"Assets/Resources/Atlas");
        FileInfo[] AtlasPaths = directoryInfo.GetFiles("*.png");
       
        foreach(FileInfo AtlasPath in AtlasPaths)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Atlas/"+AtlasPath.Name.Replace(".png",""));
            foreach(Sprite s in sprites)
            {
                IconContaniner.Add(s.name,s);
            }
        }
        LoadAllSpriteIcon();
    }
    public void LoadAllSpriteIcon()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(@"Assets/Resources/Icon");
        FileInfo[] AtlasPaths = directoryInfo.GetFiles("*.png");
        foreach(FileInfo AtlasPath in AtlasPaths)
        {
            
            Sprite[] sprites = Resources.LoadAll<Sprite>("Icon/"+AtlasPath.Name.Replace(".png",""));
            foreach(Sprite s in sprites)
            {
                IconContaniner.Add(s.name,s);
            }
        }
        LoadAllSpriteIcon1();
    }

    public void LoadAllSpriteIcon1()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(@"Assets/Resources/Icon");
        FileInfo[] AtlasPaths = directoryInfo.GetFiles("*.PNG");
        foreach(FileInfo AtlasPath in AtlasPaths)
        {
            
            Sprite[] sprites = Resources.LoadAll<Sprite>("Icon/"+AtlasPath.Name.Replace(".PNG",""));
            foreach(Sprite s in sprites)
            {
                IconContaniner.Add(s.name,s);
            }
        }
    }
    
}
