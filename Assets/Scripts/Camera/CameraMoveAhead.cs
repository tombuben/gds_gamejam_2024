using UnityEngine;


public class CameraMoveAhead : MonoBehaviour
{
    public CharacterController characterController;
    

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = characterController.velocity * 0.1f;
        var localPosition = transform.localPosition;
        transform.localPosition = Vector3.MoveTowards(
            localPosition, 
            Vector3.ClampMagnitude(movement, 4),
            Time.deltaTime * 2f );
    }
}
