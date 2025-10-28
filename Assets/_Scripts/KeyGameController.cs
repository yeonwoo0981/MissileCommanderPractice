using UnityEngine;

public class KeyGameController : IGameController
{
    public bool FireButtonPressed()
    {
       return Input.GetKeyDown(KeyCode.Space);
    }
}
