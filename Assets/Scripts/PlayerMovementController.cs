using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public CharacterController characterController;
    public float blastForceMultiplier = 1.0f;
    private Vector3 blastDirection;
    private float blastUpwardsSpeed;
    private float blastRemainingTime;
    public float blastDuration = 2f;
    public float upwardForce = 1.5f;
    public float upwardDuration = 0.5f; // Duration of the upward force

    [Space(20)]
    public bool respawn;
    public float yRespawnDis;
    public Vector3 respawnPoint;

    void Update()
    {
        if (gameObject.transform.position.y < yRespawnDis)
            gameObject.transform.position = respawnPoint;
        if (blastRemainingTime > 0)
        {
            Vector3 horizontalMovement = new Vector3(blastDirection.x, 0, blastDirection.z) * Time.deltaTime * blastForceMultiplier;
            characterController.Move(horizontalMovement);

            if (upwardDuration > 0)
            {
                // Apply upward force separately
                characterController.Move(Vector3.up * blastUpwardsSpeed * Time.deltaTime);
                upwardDuration -= Time.deltaTime;
            }

            // Apply gravity manually if you're using a CharacterController
            blastDirection += Physics.gravity * Time.deltaTime;
            blastRemainingTime -= Time.deltaTime;
        }
    }

    public void GetBlasted(Vector3 explosionPosition, float force, float radius)
    {
        Vector3 explosionDirection = transform.position - explosionPosition;
        float explosionDistance = explosionDirection.magnitude;

        // Remove the y component from explosionDirection for horizontal blast only
        Vector3 horizontalExplosionDirection = new Vector3(explosionDirection.x, 0f, explosionDirection.z);

        // Normalize the horizontal explosion direction vector and scale the force based on the distance
        blastDirection = horizontalExplosionDirection.normalized * (force / explosionDistance) * (1 - (explosionDistance / radius));

        // Set the upwards speed separately
        blastUpwardsSpeed = upwardForce;

        // Reset timers
        blastRemainingTime = blastDuration;
        upwardDuration = blastDuration; // Set upward force duration equal to the blast duration or another value
    }
}
