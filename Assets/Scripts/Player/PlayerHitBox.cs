using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    internal PlayerHealthController healthController;

    public void Impact()
    {
        healthController.TakeDamage(damage);
    }
}
