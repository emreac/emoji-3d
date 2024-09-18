using UnityEngine;

public class RoadReturnToPool : MonoBehaviour
{
    public Transform player;
    public float returnDistance = 50f; // Distance after which the road is returned to the pool

    private void Update()
    {
        if (player.position.z - transform.position.z > returnDistance)
        {
            // Return the road segment to the pool
            ObjectPooler.Instance.ReturnObjectToPool(gameObject);
        }
    }
}
