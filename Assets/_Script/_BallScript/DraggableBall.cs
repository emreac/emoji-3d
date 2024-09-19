using UnityEngine;

public class DraggableBall : MonoBehaviour
{
  

    private Vector3 offset;  // Offset between mouse position and ball position
    private float zCoordinate;  // Z-coordinate of the ball in world space
    private ColorBallDestroyer ballDestroyer;  // Reference to the ball destruction script
    private Material originalMaterial;  // Store the original material
    public Material highlightMaterial;  // Reference to the highlight material (glow or outline)

    private Renderer ballRenderer;
    private Rigidbody rb;  // Reference to Rigidbody component
    private bool originalUseGravity;  // Store the original gravity state

    public static bool isGameCompleted = false; // Static flag to track if the game is over

    void Start()
    {
        

        // Get the reference to the ColorBallDestroyer attached to the same object
        ballDestroyer = GetComponent<ColorBallDestroyer>();

        // Get the renderer component
        ballRenderer = GetComponent<Renderer>();

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Store the original material
        if (ballRenderer != null)
        {
            originalMaterial = ballRenderer.material;
        }

        // Store the original gravity state
        if (rb != null)
        {
            originalUseGravity = rb.useGravity;
        }
    }

    void OnMouseDown()
    {
        if (isGameCompleted) return;

        // Highlight the ball by changing its material
        if (highlightMaterial != null && ballRenderer != null)
        {
            ballRenderer.material = highlightMaterial;
        }

        // Store the Z-coordinate of the ball
        zCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;

        // Calculate the offset between the mouse position and the ball's position
        offset = transform.position - GetMouseWorldPosition();

        // Disable gravity during dragging
        if (rb != null)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;  // Stop any velocity to prevent continued movement
        }
    }

    void OnMouseDrag()
    {
        // If game is over, prevent further interactions
        if (isGameCompleted) return;

        // Update the ball's position to follow the mouse position
        Vector3 targetPosition = GetMouseWorldPosition() + offset;

        // Smooth movement with Rigidbody
        if (rb != null)
        {
            rb.MovePosition(targetPosition);
        }
        else
        {
            // Fallback if Rigidbody is not present
            transform.position = targetPosition;
        }

        // Set the hasBeenMoved flag to true if the ball has been moved
        if (ballDestroyer != null)
        {
            ballDestroyer.hasBeenMoved = true;
        }
    }

    void OnMouseUp()
    {
        // If game is over, prevent further interactions
        if (isGameCompleted) return;

        // Revert the ball's material back to the original material when dragging stops
        if (originalMaterial != null && ballRenderer != null)
        {
            ballRenderer.material = originalMaterial;
        }

        // Re-enable gravity when dragging ends
        if (rb != null)
        {
            rb.useGravity = originalUseGravity;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Get the current mouse position in screen space
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;  // Set the Z position to maintain depth in 3D space
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
