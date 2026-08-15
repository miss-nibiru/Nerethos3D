using UnityEngine;
using UnityEngine.SceneManagement;

public class IDummy : BaseInteractable
{
    //when the player interacts with this object, it will call the Interact() method and will change to battle secne
    
    public override void Interact()
    {
        SceneManager.LoadScene("BattleScene");
    }
    
}
