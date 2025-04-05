using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Example
{
    public class Collections : MonoBehaviour
    {
        public TMP_InputField inputField;
        public List<GameObject> gameObjects;
        
        public void RemoveCubeByIndex()
        {
            var index = int.Parse(inputField.text);
            Destroy(gameObjects[index]);
            gameObjects.RemoveAt(index);
        }
    }
}