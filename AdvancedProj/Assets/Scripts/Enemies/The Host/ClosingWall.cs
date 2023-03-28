using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingWall : MonoBehaviour
{
    public GameObject wall;
    public GameObject boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        wall.SetActive(true);
        boss.GetComponent<BossController>().SetCanAttack(true);
        Destroy(gameObject);
    }
}
