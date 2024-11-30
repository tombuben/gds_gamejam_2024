using UnityEngine;


public class CameraMoveAhead : MonoBehaviour
{
    public CharacterController characterController;
    

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.ClampMagnitude(characterController.velocity * 0.025f, 4);
    }
}
