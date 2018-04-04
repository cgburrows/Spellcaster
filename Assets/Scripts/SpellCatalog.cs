using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpellCatalogue : MonoBehaviour
{
    public void FireBeam(GameObject beamLineRendererPrefab, Transform beamStartPosition, Vector3 beamEndPosition)
    {
        GameObject beam = null;

        if (Input.GetMouseButtonDown(0))
        {
            beam = Instantiate(beamLineRendererPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(beam);
        }
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
