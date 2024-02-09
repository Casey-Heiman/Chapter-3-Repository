using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI()
    {
        int size = 12;
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");

        // Display coordinates of the raycast hit
        if (hitPoint != Vector3.zero)
        {
            Vector2 hitPointPos = cam.WorldToScreenPoint(hitPoint);
            GUI.Label(new Rect(hitPointPos.x, cam.pixelHeight - hitPointPos.y, 100, 50), "Hit Point: " + hitPoint.ToString());
        }
    }

    private Vector3 hitPoint = Vector3.zero; // Store the hit point
    private GameObject hitObject = null; // Store the hit object

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                hitPoint = hit.point; // Store the hit point
                hitObject = hit.transform.gameObject;

                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }

            // Draw the ray in the scene view
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}

