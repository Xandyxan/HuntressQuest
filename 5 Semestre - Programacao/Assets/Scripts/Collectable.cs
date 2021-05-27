using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour, ISelectable, IInteractable
{
    [SerializeField] private string _objectDescription;

    public bool loadLevel;


    public virtual void Interact()
    {
        if (loadLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else this.gameObject.SetActive(false);
    }

    public string objectDescription { get => _objectDescription; set => _objectDescription = value; }

}
