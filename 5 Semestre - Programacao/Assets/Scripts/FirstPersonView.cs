using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class FirstPersonView : MonoBehaviour
{
    protected Vector3 centerPointOfView;
    protected Camera playerCamera;

    [Header("Crosshair")]
    protected Image crosshair;

    [SerializeField] protected float maxDistance = 20f;

    [Tooltip("Reference to the text component from the HUD Canvas")]
    protected Text selectionText;

    protected virtual void Awake()
    {
        centerPointOfView = new Vector3(Screen.width / 2, Screen.height / 2);
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        selectionText = GameObject.FindGameObjectWithTag("SelectionText").GetComponent<Text>();
    }
}
