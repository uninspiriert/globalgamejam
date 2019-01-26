using System.Linq;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerOne;

    public GameObject playerTwo;

    public float cameraMinClamp = 20f;

    public float cameraMaxClamp = 50f;

    private void Update()
    {
        var avgX = (playerOne.transform.position.x + playerTwo.transform.position.x) / 2f;
        var position = transform.position;

        var aspectRatio = Screen.width / Screen.height;
        var tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);

        var playerDist = Mathf.Abs(playerOne.transform.position.x - playerTwo.transform.position.x) / 2.0f;
        var camDist = Mathf.Max(playerDist / 2.0f / aspectRatio / tanFov, cameraMinClamp);
        camDist = Mathf.Min(camDist, cameraMaxClamp);

        position = new Vector3(avgX, position.y, position.z);

        Camera.main.orthographicSize = camDist;
        transform.position = position;
    }
}