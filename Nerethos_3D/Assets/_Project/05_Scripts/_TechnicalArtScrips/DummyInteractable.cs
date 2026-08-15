using UnityEngine.SceneManagement;

public class DummyInteractable : BaseInteractable
{
    public override void Interact()
    {
        SceneManager.LoadScene("BattleScene");
    }
}