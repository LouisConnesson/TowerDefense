using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private Transform mobPrefab; //change to list when their will be some differents
    [SerializeField] private Transform hersePrefab; //change to list when their will be some differents
    private List<Transform> mobList = new List<Transform>();
    [SerializeField] bool isPlacingHerse = false;
    [SerializeField] Quaternion herseRotation = Quaternion.identity;

    //private NavMeshSurface surface;

    /*    private NetworkVariable<MyCustomData> newValue = new NetworkVariable<MyCustomData>(
            new MyCustomData { 
                _int= 75,
                _bool = false, 
                _string = "nono",
            }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        public struct MyCustomData : INetworkSerializable
        {
            public int _int;
            public bool _bool;
            public FixedString128Bytes _string;

            public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
            {
                serializer.SerializeValue(ref _int);
                serializer.SerializeValue(ref _bool);
                serializer.SerializeValue(ref _string);
            }
        }

        public override void OnNetworkSpawn()
        {
            newValue.OnValueChanged += (MyCustomData previousValue, MyCustomData newValue) => {
                Debug.Log(OwnerClientId + "; " + newValue._int + "; " + newValue._bool + "; "+ newValue._string);
            };
        }*/

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnMobsServerRPC(GetRandomSpawnPoint(GameManager.Instance.GetSpawnPointList()));
            //TestClientRPC(new ClientRpcParams { Send= new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
            /*            newValue.Value = new MyCustomData
                        {
                            _int = 10,
                            _bool = true,
                            _string = "ouioui",
                        };*/
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (mobList[0])
                Destroy(mobList[0]);
        }

        Vector3 InputVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Z))
        {
            InputVector.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            InputVector.z -= 1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            InputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            InputVector.x += 1;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isPlacingHerse = isPlacingHerse?false:true;
        }

        if (isPlacingHerse)
        {
            herseRotation.eulerAngles = new Vector3(0, ((int)herseRotation.eulerAngles.y + (int) Input.mouseScrollDelta.y),0); //marche po

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    Transform herse = Instantiate(hersePrefab, hit.point, herseRotation, transform.parent);
                    herse.GetComponent<NetworkObject>().Spawn(true);
                    //surface.BuildNavMesh();
                }
            }
        }


        float moveSpeed = 3f;
        transform.position += InputVector * moveSpeed * Time.deltaTime;
    }

    [ServerRpc]
    public void SpawnMobsServerRPC(Vector3 spawnPosition)
    {
        //Debug.Log("TestServerRPC : " + OwnerClientId + " ; " + serverRpcParams.Receive.SenderClientId);

        //SPAWN MOB AND INIT HIS INITIAL POSITION
        Transform spawnedObjectTransform = Instantiate(mobPrefab, spawnPosition, Quaternion.identity, transform.parent);
        spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);

        //INIT HIS DESTINATION
        //spawnedObjectTransform.position = spawnPosition;
        SetDestinationToPosition(spawnedObjectTransform, GameManager.Instance.GetCastlePosition());

        mobList.Add(spawnedObjectTransform);
    }

    public Vector3 GetRandomSpawnPoint(List<Transform> spawnPoints)
    {
        int rand = Random.Range(0, 3);
        return spawnPoints[rand].position;
    }

    public void SetDestinationToPosition(Transform mob, Vector3 destination)
    {
        NavMeshAgent mobAgent = mob.GetComponent<NavMeshAgent>();
        if (mobAgent != null)
            mobAgent.SetDestination(destination);
    }

    [ClientRpc]
    public void TestClientRPC(ClientRpcParams clientRpcParams)
    {
        Debug.Log("TestServerRPC : " + OwnerClientId + " ; " + clientRpcParams.Send.TargetClientIds);
    }
}
