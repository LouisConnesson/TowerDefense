using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;


public class MobManager : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.transform.tag == "Herse")
        {
            //mob stop walking

            //mob is beating

            //damaging the fence collision

        }
    }
}
