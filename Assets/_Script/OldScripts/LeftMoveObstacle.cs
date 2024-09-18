using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LeftMoveObstacle : MonoBehaviour
{
    private SideRunnerPlayerController controller;
    [SerializeField] private float obstacleSpeed = 3.0f;
    private float leftBoud = -30f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<SideRunnerPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.gameOver)
        {
            transform.Translate(Vector3.back * obstacleSpeed * Time.deltaTime);
            if (transform.position.z < leftBoud)
            {
                Destroy(gameObject);
            }

        }

    }
}
