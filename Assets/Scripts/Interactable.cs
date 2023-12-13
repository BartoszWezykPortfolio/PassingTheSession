using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //Add or remove an InteractionEvent component to gameobject
    public bool useEvents;
    [SerializeField]
    //message shown when looking at object
    public string promptMessage;
    //tihs function will be called from our player

    public virtual string OnLook()
    {
        return promptMessage;
    }
    public void BaseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();

    }
    protected virtual void Interact()
    {
        //no code here, just a function to be overriden by subclasses
    }
}
