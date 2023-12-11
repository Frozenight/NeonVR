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

    [SerializeField] private GameObject[] glovesRb;
    private void Start()
    {
        //Invoke("EquipRightGlove", 5);
    }

    public void EquipRightGlove()
    {
        rightHand.SetActive(false);
        rightGlove.SetActive(true);
        rightGloveObj.SetActive(false);
        rightInteractor.enabled = false;
        ActivateGloveMode(true);
    }

    public void EquipLeftGlove()
    {
        leftHand.SetActive(false);
        leftGlove.SetActive(true);
        leftGloveObj.SetActive(false);
        leftInteractor.enabled = false;
        ActivateGloveMode(true);
    }

    private void ActivateGloveMode(bool activate)
    {
        if (activate)
            foreach (var item in glovesRb)
                item.GetComponent<Rigidbody>().mass = 1;
        else
            foreach (var item in glovesRb)
                item.GetComponent<Rigidbody>().mass = 10;
    }
}
