using UnityEngine;

public class NPCSetting : MonoBehaviour
{


    [Header("NPC")]
    public Transform target;

    [Header("Camera")]
    public Camera mainCamera;
    

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null) { 
            target.transform.rotation = mainCamera.transform.rotation;
        }

    }
}
