using TMPro;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public Interactable focus;

    [Header("Layer")]
    public LayerMask interactableMask;
    [Header("Mouse info")]
    public Transform mouseInfoBox;
    public TextMeshProUGUI mouseInfoText;
    public string onRayCastText = "Interactable";
    public Interactable onFocusInteractable;

    private Camera cam;
    private bool isButtonDownOnInteractable = false;

    void Awake()
    {
        cam = Camera.main;
    }
    void Update()
    {
        mouseInfoBox.gameObject.SetActive(false);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, interactableMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null && Mathf.Abs(hit.point.y - transform.position.y) < 15f)
            {
                mouseInfoBox.position = Input.mousePosition + new Vector3(10, 10, 0);
                mouseInfoBox.gameObject.SetActive(true);
                SetMouseInfo(onRayCastText);
            }
        }

        if (Input.GetMouseButton(0))
        {

            if (Physics.Raycast(ray, out hit, 100, interactableMask))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                    isButtonDownOnInteractable = true;
                    onFocusInteractable.Interact(transform);
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && !isButtonDownOnInteractable)
        {
            isButtonDownOnInteractable = false;
        }
    }

    void SetFocus(Interactable newFocus)
    {
        // If our focus has changed
        if (newFocus != focus)
        {
            // Defocus the old one
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;   // Set our new focus
            // motor.FollowTarget(newFocus);   // Follow the new focus
        }

        newFocus.OnFocused(transform);
    }

    public void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
    }
    void SetMouseInfo(string text)
    {
        mouseInfoText.text = text;
    }
}
