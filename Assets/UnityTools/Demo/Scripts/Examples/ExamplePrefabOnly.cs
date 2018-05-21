using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools;

public class ExamplePrefabOnly : MonoBehaviour
{
    [PrefabOnly]
    public GameObject prefab;

    [PrefabOnly]
    public GameObject[] prefabs = new GameObject[5];

    [PrefabOnly(true)]
    public GameObject prefabRequired;

    public GameObject instance;
}
