using UnityEngine;

public class OrbitalCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float mouseSensibility = 0.5f;
    [SerializeField] private float frozenCameraMouseAreaLength = 0.5f;


    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraTPS;
    [SerializeField] private Transform cameraFPS;

    private float _xRotation = 30f;
    private float _yRotation = 0f;

    private void Update()
    {
        //todo : zoom logic

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenSize = new Vector3(Screen.width, Screen.height, 0);
        
        mousePos -= screenSize / 2;
        mousePos = mousePos.normalized * mouseSensibility;
        float d = Vector3.Distance(Input.mousePosition, screenSize / 2);
        if (d <= frozenCameraMouseAreaLength)
            mousePos *= (d/frozenCameraMouseAreaLength);
        
        _xRotation = Mathf.Clamp(_xRotation - mousePos.y * Mathf.Abs(mousePos.y) * rotationSpeed * Time.deltaTime, -45, 20);
        _yRotation = _yRotation + mousePos.x * Mathf.Abs(mousePos.x) * rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);

        if (!IsPlayerVisible() || cameraTransform.position.y <= 0.1f) ZoomIn();
        else ZoomOut();
    }

    private void ZoomIn()
    {
        if (Vector3.Distance(cameraTransform.position, cameraFPS.position) >= 0.5f)
        {
            cameraTransform.position += zoomSpeed * (cameraFPS.position - cameraTPS.position).normalized * Time.deltaTime;
        }
    }
    private void ZoomOut()
    {
        if (Vector3.Distance(cameraTransform.position, cameraTPS.position) >= 0.5f)
        {
            cameraTransform.position -= zoomSpeed * (cameraFPS.position - cameraTPS.position).normalized * Time.deltaTime;
        }
    }

    private bool IsPlayerVisible()
    {
        RaycastHit hit;
        Vector3 start = cameraTransform.position;
        Vector3 dir = Player.playerHitBox.position - start;

        Ray ray = new Ray(start, dir);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(start, hit.point, Color.magenta, 0.0f, true);
            bool b = (Vector3.Distance(hit.point, Player.playerHitBox.position) <= 5f);
            return b;
        }
        else
        {
            return false;
        }
    }

}








