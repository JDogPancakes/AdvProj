using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Develop_ValueBarInfo : MonoBehaviour
{
    [Header("Health Info")]
    [SerializeField]
    private float currrent_Health = 100;
    [SerializeField]
    private float max_Health = 100;
    [Space(10)]
    
    [Header("Mana Info")]
    [SerializeField]
    private float currrent_Mana = 100;
    [SerializeField]
    private float max_Mana = 100;
    [Space(10)]
    
    [Header("Value Bar Info")]
    [SerializeField]
    private UI_ValueBar healthBar;
    [SerializeField]
    private UI_ValueBar manaBar;

    private void Start()
    {
        if (!healthBar || !manaBar)
        {
            Debug.Log("No health bar and mana bar manual assignment, set to seach children component for health and mana bar reference.");
            foreach (var bar in gameObject.GetComponentsInChildren<UI_ValueBar>())
            {
                if (bar.tag == "HealthBarObjects")
                {
                    healthBar = bar;
                    continue;
                }
                if (bar.tag == "ManaBarObjects")
                {
                    manaBar = bar;
                    break; 
                }
            } 
        }
        healthBar.SetUpPlayerValue(max_Health, currrent_Health);
        manaBar.SetUpPlayerValue(max_Mana, currrent_Mana);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetHit(10.5f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LoseMana(10.5f);
        }
    }

    /// <summary>
    /// Simulate if player get hit.
    /// </summary>
    /// <param name="damage">the value of been damaged</param>
    private void GetHit(float damage)
    {
        currrent_Health -= damage;
        //Update the value of health bar at the same time as current health been decrease
        healthBar.DecreaseCurrentValue(damage);
        //Or set the current value as the current health of health bar
        //..
        //healthBar.SetCurrentHealth(currrent_Health);
    }

    /// <summary>
    /// Simulate if player losing mana.
    /// </summary>
    /// <param name="lose">the value of been lost</param>
    private void LoseMana(float lose)
    {
        currrent_Mana -= lose;
        //Update the value of health bar at the same time as current health been decrease
        manaBar.DecreaseCurrentValue(lose);
        //Or set the current value as the current Mana of mana bar
        //..
        //manaBar.SetCurrentValue(currrent_Mana);
    }
}
