using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectControl : MonoBehaviour
{
    public Camera cam;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    //Alla oleva antaa boolean arvon (true = raycast on osunut) koskien osumaa 3D collideriin.
    //Tätä metodia voidaan kutsua, kun halutaan tietää, onko osuttu johonkin annetussa vector3
    //pisteessä rayPosition. Tämä ei havaitse osumaa 2D collideriin.
    public bool DetectCollisionAtPosition (Vector3 rayPosition)
    {
        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(cam.WorldToScreenPoint(rayPosition)), out hit))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

