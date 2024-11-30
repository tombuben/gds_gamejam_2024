using UnityEngine;

public class Killfloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<CharacterKiller>().Kill();
        }
    }

}
