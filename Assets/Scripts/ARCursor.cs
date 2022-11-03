using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class ARCursor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    public GameObject cursorChildObject;
    public List<GameObject> mobToPlace;
    public ARRaycastManager raycastManager;
    [SerializeField]  public GameObject ARPlaneObject;

    public Camera arCam;

    public bool useCursor = false;

    private bool MapSpawned;
    [SerializeField] private GameObject Map;

    void Start()
    {
        cursorChildObject.SetActive(useCursor);
        MapSpawned = false;
    }

    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (useCursor)
            {
                GameObject.Instantiate(mobToPlace[PlayerPrefs.GetInt("typeOfMob")], transform.position, transform.rotation);
            }
            else
            {
                if (!MapSpawned)
                {
                    List<ARRaycastHit> hits = new List<ARRaycastHit>();
                    raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                    if (hits.Count > 0)
                    {
                        GameObject.Instantiate(Map, hits[0].pose.position + new Vector3(-2.562f, 0, -2.223f), hits[0].pose.rotation);
                        //ARPlaneObject.GetComponent<ARPlaneMeshVisualizer>().enabled = false;
                        //ARPlaneObject.SetActive(false);

                        ARPlaneObject.GetComponent<ARPlaneManager>().requestedDetectionMode = 0;

                        MapSpawned = true;
                    }
                }
                else
                {
                    RaycastHit hit;
                    if (Physics.Raycast(arCam.transform.position, arCam.transform.forward, out hit, Mathf.Infinity))
                    {
                        if (hit.collider.gameObject.name == "Terrain(Clone)")
                        {
                            GameObject.Instantiate(mobToPlace[PlayerPrefs.GetInt("typeOfMob")], hit.point, transform.rotation);
                        }
                    }
                }
            }
        }
    }


    void UpdateCursor()
    {
        Vector2 screenPosition = arCam.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}