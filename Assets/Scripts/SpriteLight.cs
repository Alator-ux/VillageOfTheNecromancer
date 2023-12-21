using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Color baseColor;
    public Color dawnColor;
    public Color NightColor;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    float time = 0;
    void Update()
    {
        time += 0.003f;
        if(time >= 14){
            time = 0;
        }

        if(time >= 12 && time < 14 && sr.color != dawnColor){
            sr.color = dawnColor;
            return;
        }
        if(time >= 7  && time < 12 && sr.color != baseColor){
            sr.color = baseColor;
            return;
        }
        if(time >= 5 && time < 7 && sr.color != dawnColor){
            sr.color = dawnColor;
            return;
        }
        if(time >= 0 && time < 5 && sr.color != NightColor){
            sr.color = NightColor;
            return;
        }
    }
}
