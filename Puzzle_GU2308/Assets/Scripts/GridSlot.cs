
using UnityEditor;
using UnityEngine;

public class GridSlot : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Handles.Label(transform.position + Vector3.up * 0.3f, $"{transform.position.x},{transform.position.y}");
    }
}
