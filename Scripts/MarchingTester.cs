using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class MarchingTester : MonoBehaviour
{
    public GameObject brush;
    public float stenght;

    float radius;


    public bool create;
    public MarchingManager manager;

    void Update()
    {
        radius = brush.transform.localScale.x / 2;

        if (create){
            radius = brush.transform.localScale.x;
            manager.terraform(brush.transform.position, radius, stenght);
        }

        if(Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit)){
                manager.terraform(hit.point , radius, stenght);
            }
        }

        if (Input.GetMouseButton(1)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit)){
                manager.terraform(hit.point, radius, -stenght);
            }
        }
  

    }

}
