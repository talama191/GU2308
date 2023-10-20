

using UnityEngine;

public class GasPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other.gameObject);
            var rc = other.GetComponent<RobotController>();
            rc.PickUpGas();
            Destroy(gameObject);
        }

    }
}