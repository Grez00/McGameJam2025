using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private GameObject crate;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("OnClick");
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (rayHit.collider != null && rayHit.collider.gameObject == crate)
        {
            var myCrate = crate.GetComponent<Crate>();
            if (myCrate != null)
            {
                Debug.Log("myCrate.clicked()");
                myCrate.clicked();
            }
            else
            {
                Debug.Log("Crate bad");
            }
        }
    }
}
