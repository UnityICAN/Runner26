using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourControllerGizmoDrawer : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 1000f);
    }
}
