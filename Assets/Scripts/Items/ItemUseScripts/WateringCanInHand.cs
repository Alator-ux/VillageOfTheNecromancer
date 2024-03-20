using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanInHand : MonoBehaviour
{
    public WateringCanUse WateringCanUseScript;

    public void OnWateringAnimationEnded() {
        WateringCanUseScript.OnWateringAnimationEnded();
    }
}
