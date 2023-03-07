using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash Chip", menuName = "ScriptableObjects/Chips/Dash Chip")]
public class DashChipObject : ChipObject
{
    public float dashStrength = 150f;
    public float dashCooldown = 3f;
    bool canDash = true;
    public override IEnumerator Activate(GameObject player)
    {
        if (canDash)
        {
            Debug.Log("Dashing");
            canDash = false;
            Rigidbody2D rb = player.transform.GetComponent<Rigidbody2D>();
            rb.AddForce(rb.velocity * dashStrength);
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }
    }
}
