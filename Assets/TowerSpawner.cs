using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tower;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask mask = LayerMask.GetMask("Path");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                Transform objectHit = hit.transform;
                Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green, 60f);


                Waypoint waypoint = hit.transform.GetComponent<Waypoint>();
                if (waypoint && waypoint.AllowsTowerPlacement)
                {
                    Instantiate(tower, waypoint.transform.position, Quaternion.identity, transform);
                }
            }
        }
    }
}
