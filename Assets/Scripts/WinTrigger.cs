using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{

    [SerializeField] private GameObject gameObjektLogik;
    private TipToeLogic logik;
    // Start is called before the first frame update
    void Start()
    {
        logik = gameObjektLogik.GetComponent<TipToeLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        SimpleCharacterControl player = other.gameObject.GetComponent<SimpleCharacterControl>();
        if (player != null) player.resetPlayer();
        logik.resetGame();
    }
}
