using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Fall : MonoBehaviour
{
    public float speed;

    void Start()
    {
        if (speed <= 0)
        {
            throw new Exception("Go up?");
        }

    }

    void Update()
    {
        gameObject.transform.position += new Vector3(0, -speed);

        var screenCoordinates = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        if (screenCoordinates.y < Screen.safeArea.yMin)
        {
            Controller.INSTANCE.UnRegister(gameObject);
            Destroy(gameObject);
        }
    }
}
