using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public void OnClick()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero);

        if (hitInfo.collider)
        {
            Debug.Log(hitInfo.transform.gameObject.GetComponent<TileColour>().Coordinates);
        }
        else
        {
            Debug.Log("Nothing hit");
        }
    }
}
