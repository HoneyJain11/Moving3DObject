using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

    GameObject movingObject = null;
    Vector3 offset;

    private void Update()
    {
        MoveObjet();
    }

    Ray CreateMouseRay()
    {
        Vector3 mousePositionFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 mousePositionNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 takeMousePositionFar = Camera.main.ScreenToWorldPoint(mousePositionFar);
        Vector3 takeMousePositionNear = Camera.main.ScreenToWorldPoint(mousePositionNear);

        Ray ray = new Ray(takeMousePositionNear, takeMousePositionFar - takeMousePositionNear);
        return ray;
    }

    void MoveObjet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseClickRay = CreateMouseRay();
            RaycastHit hit;

            if (Physics.Raycast(mouseClickRay.origin, mouseClickRay.direction, out hit))
            {
                movingObject = hit.transform.gameObject;
                offset = hit.transform.position - hit.point;
            }
        }

        else if (Input.GetMouseButton(0) && movingObject)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movingObject.transform.position = new Vector3(pos.x + offset.x, pos.y + offset.y, movingObject.transform.position.z);
        }
        else if (Input.GetMouseButtonUp(0) && movingObject)
        {
            movingObject = null;
        }
    }

}
