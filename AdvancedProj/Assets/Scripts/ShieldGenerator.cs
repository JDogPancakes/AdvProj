using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : MonoBehaviour
{
    private float hp = 3;

    private GameObject boss;
    // Start is called before the first frame update
    void Awake()
    {
        boss = GameObject.FindGameObjectWithTag("Enemy");
    }


    public void Damage(int dmg) 
    {
        hp--;
        if(hp<=0)
        {
            Destroy(gameObject);
            BossController bossController= boss.GetComponent<BossController>();
            bossController.StartCoroutine(bossController.ShieldTurretDied());
        }
    } 
}
