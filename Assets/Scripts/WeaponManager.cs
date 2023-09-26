using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftGlove;
    [SerializeField] private GameObject rightGlove;

    [SerializeField] private GameObject leftGloveObj;
    [SerializeField] private GameObject rightGloveObj;

    [SerializeField] private XRDirectInteractor leftInteractor;
    [SerializeField] private XRDirectInteractor rightInteractor;
    [SerializeField] private Rigidbody leftHandRb;
    [SerializeField] private Rigidbody rightHandRb;

    public void EquipRightGlove()
    {
        rightHand.SetActive(false);
        rightGlove.SetActive(true);
        rightGloveObj.SetActive(false);
        rightInteractor.enabled = false;
        rightHandRb.mass = 50;
    }

    public void EquipLeftGlove()
    {
        leftHand.SetActive(false);
        leftGlove.SetActive(true);
        leftGloveObj.SetActive(false);
        leftInteractor.enabled = false;
        leftHandRb.mass = 50;
    }
}
