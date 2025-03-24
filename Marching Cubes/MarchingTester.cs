using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class MarchingTester : MonoBehaviour
{
    public float stenght;
    public float radius;
    public MarchingManager manager = FindFirstObjectByType<MarchingManager>();

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                manager.terraform(hit.point, radius, stenght);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                manager.terraform(hit.point, radius, -stenght);
            }
        }


    }

}
