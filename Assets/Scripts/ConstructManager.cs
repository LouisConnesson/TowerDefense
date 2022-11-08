using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[System.Serializable]
public class Buildings
{
    public List<GameObject> buildings;
}

[System.Serializable]
public class Magic
{
    public List<GameObject> magics;
}
public class ConstructManager : MonoBehaviour
{
    public InputDevice targetDevice;
    private bool isControllerFound = false;
    private float constructCD;

    [SerializeField] int currentClassIndex;
    [SerializeField] private GameObject skill;

    #region general

    void Start()
    {
        currentClassIndex = 0;
        currentBuildingIndex = 0;
        constructCD = 3.0f;
        isConstructAvailable = true;

        GetController();

    }
    void Update()
    {
        if (!isControllerFound)
            GetController();

        if (isControllerFound)
        {
            currentClassIndex = PlayerMenuController.Instance.GetGamemode();

            if (currentClassIndex == 0)
            {
                if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue && isConstructAvailable)
                {
                    isConstructAvailable = false;
                    ConstructBuilding();
                    StartCoroutine("ConstructCD");
                    Debug.Log("primary button pressed");

                }

                targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
                if (triggerValue > 0.3f)
                {
                    //GetComponent<CanvasController>().SetCanvas(true);
                    Debug.Log(triggerValue);

                }
                else
                {
                    //GetComponent<CanvasController>().SetCanvas(false);

                }
            }
            if (currentClassIndex == 1)
            {
                targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
                if (triggerValue > 0.3f)
                {
                    if (!isCasting)
                    {
                        skill = Instantiate(classMagics[currentClassIndex].magics[currentMagicIndex], magicSpawner.transform);
                        skill.transform.SetParent(null);
                        isCasting = true;

                    }
                    Debug.Log(triggerValue);

                }
                else
                {
                    if (isCasting)
                    {
                        isCasting = false;
                        skill.GetComponent<Rigidbody>().velocity = Vector3.forward*20;
                    }

                }
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
    public int GetClass()
    {
        return currentClassIndex;
    }
    public void SetClass(int newClassIndex)
    {
        currentClassIndex = newClassIndex;
        currentBuildingIndex = 0;
        return;
    }
    #endregion

    #region ConstructManager
    [Header("Construct Manager")]
    [SerializeField] int currentBuildingIndex;
    [SerializeField] GameObject currentSpawner;
    [SerializeField] bool isConstructAvailable;

    public List<Buildings> classBuildings = new List<Buildings>();
    [SerializeField] private bool isCasting = false;
    [SerializeField] private GameObject magicSpawner;

    public void SetBuilding(int newBuildingIndex)
    {
        currentBuildingIndex = newBuildingIndex;
        return;
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
    #endregion

    #region MagicManager
    [Header("Magic Manager")]
    [SerializeField] int currentMagicIndex;
    public List<Magic> classMagics = new List<Magic>();
    public void SetMagic(int newMagicIndex)
    {
        currentMagicIndex = newMagicIndex;
        return;
    }

    #endregion
}
