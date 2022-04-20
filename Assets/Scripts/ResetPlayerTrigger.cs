using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        SimpleCharacterControl player = other.gameObject.GetComponent<SimpleCharacterControl>();
        if (player != null) player.resetPlayer();
    }
}
