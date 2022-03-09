using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] float HPCurrent;
    [SerializeField] FloatReference HPMax;
    [SerializeField] FloatReference globalDamage;
    [SerializeField] GameEvent unitDies;
    [SerializeField] Slider slider;
    public ObjectPool pool;

    private void Start()
    {
        HPCurrent = HPMax;
    }

    private void OnEnable()
    {
        HPCurrent = HPMax;
    }

    private void Update()
    {
        slider.value = HPCurrent / HPMax;
    }

    public void GlobalDamage()
    {
        HPCurrent -= globalDamage;
        if (HPCurrent <= 0)
        {
            pool.ReturnPoolObject(this.gameObject);
            unitDies.Raise();
        }
    }
}
