using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPSlider : MonoBehaviour
{
    private Slider slider;
    private float bossHP;

    public GameObject boss;
    void Awake()
    {
        slider = GetComponent<Slider>(); 
        bossHP = boss.GetComponent<BossController>().hp;
        slider.value = bossHP;
    }


    public void SetHp(float hp)
    {
        slider.value = hp/boss.GetComponent<BossController>().maxHP; 
    }
}
