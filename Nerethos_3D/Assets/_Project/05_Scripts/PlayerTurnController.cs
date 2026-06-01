using UnityEngine;


/// <summary>
/// Controls the player's turn in the battle - what can facey do and not do, what weapon shes carrying
/// </summary>

public class PlayerTurnController : MonoBehaviour
{
    
    [SerializeField] private WeaponData currentWeapon; // store the current weapon data to know what attacks are available to player
    
    public WeaponData CurrentWeapon => currentWeapon;



}
