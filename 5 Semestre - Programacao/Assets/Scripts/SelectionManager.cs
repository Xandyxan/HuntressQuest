using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : FirstPersonView
{
    public static SelectionManager instance; //singleton
    private string selectableTag = "Selectable";

    private Transform selectionTransform;

    private void Start()
    {
        maxDistance = 20f;
    }

    private void Update()
    {
        if (selectionTransform != null)
        {
            selectionTransform = null;
            selectionText.gameObject.SetActive(false);
        }

        Ray ray = playerCamera.ScreenPointToRay(centerPointOfView);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Transform selection = hit.transform;

            if (selection.gameObject.CompareTag(selectableTag))
            {
                var selectable = selection.GetComponent<ISelectable>();

                if (selectable != null)
                {
                    selectionText.text = selectable.objectDescription;
                    selectionText.gameObject.SetActive(true);
                }

                if (Input.GetButtonDown("TakeItem"))
                {
                    var interactable = selection.GetComponent<IInteractable>();
                    if (interactable != null) interactable.Interact();
                }

                selectionTransform = selection;

            }
        }


    }
}
