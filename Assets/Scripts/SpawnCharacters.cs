using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SpawnCharacters : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjectsToSpawn;

    private ARTrackedImageManager m_TrackedImageManager;
    private Dictionary<string, GameObject> arObjects;

    private void Awake()
    {
       
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        arObjects = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        foreach (var prefab in gameObjectsToSpawn)
        {

            GameObject newArGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            newArGameObject.name = prefab.name;
            arObjects.Add(prefab.name, newArGameObject);

        }
    }

   
    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

  

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
           
            if (newImage != null)
            {
                switch(newImage.referenceImage.name)
                {
                    case "octopus":
                        {
                            
                            arObjects[newImage.referenceImage.name].gameObject.SetActive(true);
                            arObjects[newImage.referenceImage.name].transform.position = newImage.transform.position;
                           
                            Debug.Log("Gameobject name: " + arObjects[newImage.referenceImage.name].name);
                            Debug.Log("Octopus is active: " + arObjects[newImage.referenceImage.name].gameObject.activeSelf);
                            Debug.Log("Octopus Location: " + arObjects[newImage.referenceImage.name].transform.position);
                            break;
                        }
                }
                //foreach (GameObject prefab in gameObjectsToSpawn)
                //{
                   
                //    newArGameObject.gameObject.SetActive(false);
                   
                //}

               
                
            }
            
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            if (updatedImage != null)
            {
                arObjects[updatedImage.referenceImage.name].gameObject.SetActive(true);
                arObjects[updatedImage.referenceImage.name].transform.position = updatedImage.transform.position;
                m_TrackedImageManager.trackedImagePrefab = arObjects[updatedImage.referenceImage.name];
            }
        }

        foreach (var removedImage in eventArgs.removed)
        {
            
        }
    }
}
