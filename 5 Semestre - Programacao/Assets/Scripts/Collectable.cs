using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ISelectable, IInteractable
{
    [SerializeField] private string _objectDescription;


    public virtual void Interact()
    {
        this.gameObject.SetActive(false);
    }

    public string objectDescription { get => _objectDescription; set => _objectDescription = value; }
    
}
