using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
   
    public List<GameObject> objTrigger;
    public List<GameObject> objButten;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject obj1 in objTrigger)
        {
            if (other.gameObject == obj1)
            {
                int index = objTrigger.IndexOf(obj1);
                if (index < objButten.Count)
                {
                    objButten[index].SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject obj1 in objTrigger)
        {
            if (other.gameObject == obj1)
            {
                int index = objTrigger.IndexOf(obj1);
                if (index < objButten.Count)
                {
                    objButten[index].SetActive(false);
                }
            }
        }
    }
}
