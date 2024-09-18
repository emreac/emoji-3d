using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private new ConstantForce constantForce;

    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float maxForce = 500f;
    [SerializeField] private float forceIncrementSpeed = 300f; // Speed at which the force increases
    private float currentForceZ = 0f;

    // Lane Change
    public float xSpeed = 5f; // Speed of lane change
    public float xRange = 5f; // The range the object can move on the X-axis

    // Reference to the UI Slider
    public Slider forceMeterSlider;

    // Reference to the TextMeshProUGUI for displaying force value
    public TextMeshProUGUI forceValueText;

    private float initialXPosition;

    void Start()
    {
        // Get the ConstantForce component
        constantForce = GetComponent<ConstantForce>();

        // Store the initial X position of the object
        initialXPosition = transform.position.x;

        // Initialize the slider's max value to match the max force
        if (forceMeterSlider != null)
        {
            forceMeterSlider.maxValue = maxForce;
            forceMeterSlider.value = 0f; // Start the slider at 0
        }

        // Initialize the TextMeshProUGUI to display the initial force value
        if (forceValueText != null)
        {
            forceValueText.text = "0 mph";
        }
    }

    void Update()
    {
     

        // Check if the screen is being held (for mobile) or mouse button is pressed (for testing on PC)
        if (Input.GetMouseButton(0))
        {
            // Increase the Z force while holding, clamped to the maxForce
            currentForceZ += forceIncrementSpeed * Time.deltaTime;
            currentForceZ = Mathf.Clamp(currentForceZ, 0f, maxForce);

            // Apply the current force to the ConstantForce component
            constantForce.force = new Vector3(constantForce.force.x, constantForce.force.y, currentForceZ);

            // Update the force meter slider
            if (forceMeterSlider != null)
            {
                forceMeterSlider.value = currentForceZ;
            }

            // Update the TextMeshProUGUI with the current force value
            if (forceValueText != null)
            {
                forceValueText.text = Mathf.RoundToInt(currentForceZ / 15).ToString() + " mph";
            }

            // Move the object along the X-axis based on touch/mouse position
            float horizontalInput = Input.GetAxis("Mouse X"); // Detect horizontal swipe or mouse movement

            // Move left or right based on input
            Vector3 newPosition = transform.position + Vector3.right * horizontalInput * xSpeed * Time.deltaTime;

            // Clamp the X position within the specified range
            newPosition.x = Mathf.Clamp(newPosition.x, initialXPosition - xRange, initialXPosition + xRange);

            // Apply the new position to the object
            transform.position = newPosition;
        }
        else
        {
            // Reset the force if the screen is not held
            currentForceZ = 0f;
            constantForce.force = new Vector3(constantForce.force.x, constantForce.force.y, currentForceZ);

            // Reset the slider value
            if (forceMeterSlider != null)
            {
                forceMeterSlider.value = 0f;
            }

            // Reset the TextMeshProUGUI text
            if (forceValueText != null)
            {
                forceValueText.text = "0 mph";
            }
        }
    }
}
