using UnityEngine.EventSystems;
using UnityEngine;

public class SelectionMarker : MonoBehaviour
{
    private void Update()
    {
        transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }
}
