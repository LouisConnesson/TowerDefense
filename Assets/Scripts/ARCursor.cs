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
                        GameObject.Instantiate(Map, hits[0].pose.position, hits[0].pose.rotation);
                        MapSpawned = true;
                        textComponent.text = "Tu Fait spawn la map";
                    }
                }
                else
                {
                    textComponent.text = "Tu Veux faire spawn";
                    RaycastHit hit;
                    if (Physics.Raycast(arCam.transform.position, arCam.transform.forward, out hit, Mathf.Infinity))
                    {
                        textComponent.text = "Ton rayon a touché";
                        textComponent.text = hit.collider.gameObject.name;
                        if (hit.collider.gameObject.name == "Terrain")
                        {
                            GameObject.Instantiate(mobToPlace[PlayerPrefs.GetInt("typeOfMob")], hit.point, transform.rotation);
                            textComponent.text = "BG";
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