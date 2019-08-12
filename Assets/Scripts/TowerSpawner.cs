using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;

    Queue<Tower> towers = new Queue<Tower>();
    [SerializeField] private int maxTowers = 3;

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
                Debug.DrawRay(ray.origin, ray.direction * Camera.main.farClipPlane, Color.green, 60f);


                Waypoint waypoint = hit.transform.GetComponent<Waypoint>();
                if (waypoint && waypoint.AllowsTowerPlacement)
                {
                    AddTower(waypoint);
                }
            }
        }
    }

    private void AddTower(Waypoint waypoint)
    {
        Tower tower;

        if (towers.Count < maxTowers)
        {
            tower = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity, transform).GetComponent<Tower>();
        }
        else
        {
            tower = towers.Dequeue();
            tower.waypoint.EnableTowerPlacement(false);
            tower.transform.position = waypoint.transform.position;
        }

        tower.waypoint = waypoint;
        waypoint.DisableTowerPlacement(false);
        towers.Enqueue(tower);
    }
}
