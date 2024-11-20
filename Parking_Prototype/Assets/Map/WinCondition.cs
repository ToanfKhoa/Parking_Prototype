using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{

    public Collider2D thisSlot;

    public void OnTriggerStay2D(Collider2D collision)
    {
        thisSlot = collision;
        if (IsPlayerCompletelyInsideSlot())
        {
            GameManager.Instance.NextLevel();
        }
        Debug.Log("Da vao" + collision.name);
    }

    private bool IsPlayerCompletelyInsideSlot()
    {
        
        Vector2 a = new Vector2(this.transform.localPosition.x + 0.3f, this.transform.localPosition.y + 0.5f);
        Vector2 b = new Vector2(this.transform.localPosition.x + 0.3f, this.transform.localPosition.y - 0.5f);
        Vector2 c = new Vector2(this.transform.localPosition.x - 0.3f, this.transform.localPosition.y + 0.5f);
        Vector2 d = new Vector2(this.transform.localPosition.x - 0.3f, this.transform.localPosition.y - 0.5f);

        return (thisSlot.bounds.Contains(a) && thisSlot.bounds.Contains(b) && thisSlot.bounds.Contains(c) && thisSlot.bounds.Contains(d));
    }
}