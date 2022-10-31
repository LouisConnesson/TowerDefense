using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private Transform spawnObjectPrefab;
    private Transform spawnedObjectTransform;

    private NetworkVariable<MyCustomData> newValue = new NetworkVariable<MyCustomData>(
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
    }

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnMobsServerRPC(new ServerRpcParams());
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
            Destroy(spawnObjectPrefab);
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

        float moveSpeed = 3f;
        transform.position += InputVector * moveSpeed * Time.deltaTime;
    }

    [ServerRpc]
    public void SpawnMobsServerRPC(ServerRpcParams serverRpcParams)
    {
        Debug.Log("TestServerRPC : " + OwnerClientId + " ; " + serverRpcParams.Receive.SenderClientId);
        spawnedObjectTransform = Instantiate(spawnObjectPrefab);
        spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);

        spawnedObjectTransform.position = GameManager.Instance.GetRandomSpawnPoint();
    }

    [ClientRpc]
    public void TestClientRPC(ClientRpcParams clientRpcParams)
    {
        Debug.Log("TestServerRPC : " + OwnerClientId + " ; " + clientRpcParams.Send.TargetClientIds);
    }
}
