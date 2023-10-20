using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHitTest : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var screenPoint = Input.mousePosition;
            var worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);

            int layerMask = 1 << 7;
            layerMask = ~layerMask;
            RaycastHit hit;
            if (Physics.Raycast(worldPoint, Camera.main.transform.forward, out hit, 1000, layerMask))
            {
                var rb = hit.transform.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(Vector3.up, ForceMode.Impulse);
                }
            }
        }
    }
}
