using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManage : MonoSingleton<WeaponManage>
{
    List<GameObject> weaponSlots;
    Dictionary<string,BaseWeapon> CurrentWeapon;
    private void Awake() {
        weaponSlots = new List<GameObject>();
        CurrentWeapon = new Dictionary<string, BaseWeapon>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        

    }
    

    public void addWeapon(string name)
    {
        Debug.Log(2);
        WeaponCfg cfg = WeaponDate.Instance.weaponContainer[name];
        var prefab = ResourceLoading.Instance.Load<GameObject>(name);
        GameObject weapon = GameObject.Instantiate<GameObject>(prefab);
        weapon.transform.localScale = Vector3.one;
        weapon.transform.parent = transform;
        weapon.name = name;
        weapon.transform.position = transform.position;
        weapon.GetComponent<BaseWeapon>().InitCfg(cfg);
        //TODO 根据数量进行位置调整
    }
}
