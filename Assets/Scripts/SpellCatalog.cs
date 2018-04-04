using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCatalogue : MonoBehaviour
{
    public void FireBeam(GameObject lineRendererPrefab, Transform firePosition)
    {
        LineRenderer line;

        line = Instantiate;
    }

    public void FireMissile(Rigidbody spellPrefab, Transform firePosition)
    {
        Debug.Log("Fire");
        Rigidbody spellInstance;

        spellInstance = Instantiate(spellPrefab, firePosition.position, firePosition.rotation) as Rigidbody;
        spellInstance.AddForce(firePosition.forward * 2000);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
