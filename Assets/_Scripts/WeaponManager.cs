using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the weapon in the ship.
/// </summary>
public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private List<LaserGun> laserGuns;

    [SerializeField]
    private List<MissileLauncher> missileLaunchers;

    [SerializeField]
    private TMP_Text munitionUI;

    private int totalAmmo;

    private void Start()
    {
        foreach (var ml in missileLaunchers)
        {
            ml.munitionValueChanged.AddListener(UpdateTotalAmmo);
        }

        UpdateTotalAmmo();
    }


    /// <summary>
    /// Update the total value of remaining munition
    /// for all <see cref="MissileLauncher"/>.
    /// </summary>
    private void UpdateTotalAmmo()
    {
        int total = 0;

        foreach (var ml in missileLaunchers)
        {
            total += ml.CurrentAmmo;
        }

        totalAmmo = total;

        UpdateMunitionUI();
    }

    /// <summary>
    /// Update the Munition UI.
    /// </summary>
    private void UpdateMunitionUI()
    {
        if (munitionUI != null)
        {
            munitionUI.text = "Munition: " + totalAmmo;
        }
    }
}
