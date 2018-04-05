using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpellCatalogue : MonoBehaviour
{
    public void FireBeam(GameObject beamLineRendererPrefab, Transform beamStartPosition, Vector3 beamEndPosition)
    {
        
    }

    public void FireMissile(Rigidbody spellPrefab, Transform firePosition)
    {
        Rigidbody spellInstance;

        spellInstance = Instantiate(spellPrefab, firePosition.position, firePosition.rotation) as Rigidbody;
        spellInstance.AddForce(firePosition.forward * 2000);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
