using UnityEngine;

public class Modifier : MonoBehaviour
{
    public float newMoveSpeed;
    public float newJumpForce;
    public float newGravity;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movement playerMovement = other.GetComponent<Movement>();
            if (playerMovement != null)
            {
                playerMovement.moveSpeed = newMoveSpeed;
                playerMovement.jumpForse = newJumpForce;
                playerMovement.g = newGravity;
            }
        }
    }
}
 