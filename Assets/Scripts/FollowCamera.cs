using UnityEngine;

public class FollowCamera : MonoBehaviour {
    [SerializeField] Transform Player;

    float CameraSmooth = 5f;
    Vector3 cameraOffset;

    void Start() {
        cameraOffset = new Vector3(0.0f, 1.36f, -10.0f); //camera Offset for adjusting the position
    }

    void Update() {
        //To check if player was distroyed
        if (Player) { 
            Vector3 targetPosition = Player.position + cameraOffset; // adding an offset so that the camera is away from player
            Vector3 SmoothedVector = Vector3.Lerp(transform.position, targetPosition, CameraSmooth * Time.deltaTime);
            transform.position = SmoothedVector;
        }
    }
}
