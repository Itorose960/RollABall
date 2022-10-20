using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class randomGenerator : MonoBehaviour
{
    public Vector2 positionMax, positionMin;
    public float amount;
    public GameObject prefab;

    int attempts = 0;

    void Start()
    {
        StartCoroutine(generateCubes());
    }

    IEnumerator generateCubes()
    {
        for (int i = 0; i < amount && attempts < 100; i++)
        {
            // Generamos la posicion y tama�o
            float posX = Random.Range(positionMin.x, positionMax.x);
            float posZ = Random.Range(positionMin.y, positionMax.y);
            Vector3 position = new Vector3(posX, 0.6f, posZ);

            Collider[] checkIfOverlaps = Physics.OverlapBox(position, prefab.transform.localScale/2, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);  
            // Si no hay nada en medio lo generamos
            if (checkIfOverlaps.Length != 0)
            {
                attempts++;
                i--;
            }
            else
            {
                attempts = 0;
                Instantiate(prefab, position, Quaternion.identity);
            }
        }

        if(attempts >= 99)
        {
            Debug.LogWarning("Couldn't generate all objects");
            attempts = 0;
        }

        yield return null;
    }
}
