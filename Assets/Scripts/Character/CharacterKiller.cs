using DG.Tweening;
using UnityEngine;

public class CharacterKiller : MonoBehaviour
{
    public void Kill()
    {
        Camera.main.transform.SetParent(null, worldPositionStays: true);
        
        GetComponent<CharacterController>().enabled = false;
        GetComponent<CharacterController2D>().enabled = false;
        GetComponent<CharacterController3D>().enabled = false;
        transform.Translate(Vector3.back * 10f);
        transform.DORotate(new Vector3(0,0,90f), 3f);
        transform.DOMoveY(transform.position.y - 20f, 3f);
        transform.DOMoveX(transform.position.x + 5f, 3f);
        
        GlobalManager.Instance.PlayerKilled?.Invoke();
    }
}
