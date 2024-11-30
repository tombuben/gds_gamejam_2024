using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(TrpaslikMovement))]
public class TrpaslikKillScript : MonoBehaviour
{
    private bool isDead = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.gameObject.tag == "Player")
        {
            var cont = other.gameObject.GetComponent<CharacterController>();
            if (cont.velocity.y < -0.2 &&
                other.bounds.center.y > transform.position.y
                )
            {
                KillTrpaslik(cont);
            }
            else
            {
                other.GetComponent<CharacterKiller>().Kill();
            }
        }
    }

    private void KillTrpaslik(CharacterController other)
    {
        GetComponent<TrpaslikMovement>().enabled = false;
        transform.Translate(Vector3.back * 10f);
        transform.DORotate(new Vector3(0,0,90f), 3f);
        transform.DOMoveY(transform.position.y - 10f, 2f);
        transform.DOMoveX(transform.position.x + other.velocity.x, 2f);
        
        GlobalManager.Instance.killCount += 1;
        GlobalManager.Instance.TrpaslikKilled?.Invoke();
        isDead = true;
        
        Destroy(gameObject, 2f);
    }

}
