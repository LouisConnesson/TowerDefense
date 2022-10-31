using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[System.Serializable]
public class Buildings
{
    public List<GameObject> buildings;
}
public class ConstructManager : MonoBehaviour
{
    public InputDevice targetDevice;
    private bool isControllerFound = false;
    public List<Buildings> classBuildings = new List<Buildings>();
    [SerializeField] bool isConstructAvailable;

    private float constructCD;

    [SerializeField] int currentClassIndex;
    [SerializeField] int currentBuildingIndex;
    [SerializeField] GameObject currentSpawner;
    // Start is called before the first frame update

    void Start()
    {
        currentClassIndex = 0;
        currentBuildingIndex = 0;
        constructCD = 3.0f;
        isConstructAvailable = true;

        GetController();

    }

    // Update is called once per frame
    void Update()
    {
        if(!isControllerFound)
            GetController();

        if (isControllerFound)
        {


            if(targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue && isConstructAvailable)
            {
                isConstructAvailable = false;
                ConstructBuilding();
                StartCoroutine("ConstructCD");
                Debug.Log("primary button pressed");

            }

            targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
            if (triggerValue > 0.1f)
            {
                Debug.Log("Trigger pressed + ");
                Debug.Log(triggerValue);

            }


        }
    }
    private void GetController()
    {

        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        InputDeviceCharacteristics righControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(righControllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            isControllerFound = true;
            Debug.Log(string.Format("Device name '{0}' has role '{1}'", targetDevice.name, targetDevice.role.ToString()));
        }
    }

   
    public void OnSpawnerEnter(GameObject spawn)
    {
        currentSpawner = spawn;
        Debug.Log("enterspawn");
    }
    public void OnSpawnerExit()
    {
        Debug.Log("exitspawn");

        currentSpawner = null;
    }

    private void ConstructBuilding()
    {
        Debug.Log("Construct building 0 ");

        if (classBuildings.Count >= currentClassIndex && currentSpawner)
        {
            Debug.Log("Construct building 2");

            GameObject newObj = Instantiate(classBuildings[currentClassIndex].buildings[currentBuildingIndex],currentSpawner.transform.position,currentSpawner.transform.rotation);
            newObj.transform.parent = currentSpawner.transform;
        }
    }

     private IEnumerator ConstructCD()
    {
        
        yield return new WaitForSeconds(constructCD);
        isConstructAvailable = true;
        print("WaitAndPrint ");
        
    }
}
