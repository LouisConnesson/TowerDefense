using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;

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
    public GameObject RightHandGameObject;
    private InputDevice targetDeviceRight;
    private InputDevice targetDeviceLeft;
    private bool isControllerRightFound = false;
    private bool isControllerLeftFound = false;
    private float constructCD;

    [SerializeField] int currentClassIndex;
    [SerializeField] private GameObject skill;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private int gameModeId;

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
        gameModeId = dropdown.value;

        if (!isControllerRightFound || !isControllerLeftFound)
            GetController();

        if (isControllerRightFound && isControllerLeftFound)
        {

            if (gameModeId == 0)
            {
                if (targetDeviceRight.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue && isConstructAvailable)
                {
                    isConstructAvailable = false;
                    ConstructBuilding();
                    StartCoroutine("ConstructCD");
                    Debug.Log("primary button pressed");

                }

                targetDeviceLeft.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
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
            if (gameModeId == 1)
            {
                targetDeviceRight.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
                if (triggerValue > 0.3f)
                {
                    if (!isCasting)
                    {
                        skill = Instantiate(classMagics[currentClassIndex].magics[currentMagicIndex], magicSpawner.transform);
                        skill.transform.SetParent(RightHandGameObject.transform);
                        isCasting = true;

                    }
                    if (isCasting && skill)
                    {
                        skill.transform.position = RightHandGameObject.transform.position;
                        skill.transform.rotation = RightHandGameObject.transform.rotation;
                    }
                    Debug.Log(triggerValue);

                }
                else
                {
                    if (isCasting)
                    {
                        isCasting = false;
                        skill.transform.SetParent(null);

                        skill.GetComponent<Rigidbody>().velocity = skill.transform.forward * 20;
                    }

                }
            }

        }
    }
    private void GetController()
    {

        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        InputDeviceCharacteristics righControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(righControllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        if (devices.Count > 0)
        {
            targetDeviceRight = devices[0];
            isControllerRightFound = true;
            Debug.Log(string.Format("Device name '{0}' has role '{1}'", targetDeviceRight.name, targetDeviceRight.role.ToString()));
        }
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDeviceLeft = devices[0];
            isControllerLeftFound = true;
        }

    }
    public int GetgameMod()
    {
        return gameModeId;
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
