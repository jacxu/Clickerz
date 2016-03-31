using UnityEngine;
using System.Collections;

public class ChooseSide : MonoBehaviour {

    public GameObject redBox;
    public GameObject myWorld;

 

    void OnMouseOver()
    {
        redBox.transform.position = transform.position;
    }

    void OnMouseDown()
    {
        MyController.GetGameController().SetNewGame(myWorld);
    }
}
