using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<WinCondition>())
        {
            Destroy(this.gameObject);
            GameManager.Instance.realTime += GameManager.Instance.timeBonus;
        }
    }
}
