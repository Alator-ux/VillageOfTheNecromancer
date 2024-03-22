using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

public class Statable : MonoBehaviour
{
    [SerializeField]
    const int maxState = 2;

    [SerializeField]
    [Range(0, maxState)]
    private int currentState;

    public int CurrentState
    {
        get {return currentState;}
        set {
            currentState = value;
            if(currentState >= maxState || filteredSprites[currentState].Count == 0){
                Destroy();
                return;
            }
            var t = filteredSprites[currentState];
            sr.sprite = t[hash % t.Count];
        }
    }

    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    int[] spriteStates;

    private SpriteRenderer sr;
    private int hash;
    private List<List<Sprite>> filteredSprites;

    private void Start()
    {
        Assert.AreEqual(sprites.Length, spriteStates.Length);

        hash = Math.Abs(GetHashCode() >> 2);

        filteredSprites = new List<List<Sprite>>(maxState);
        for(int i = 0; i < maxState; i++) filteredSprites.Add(new List<Sprite>(5));
        for (uint i = 0; i < sprites.Length; i++)
        {
            filteredSprites[spriteStates[i]].Add(sprites[i]);
        }

        sr = GetComponent<SpriteRenderer>();
        CurrentState = currentState;
    }

    private void Destroy() {
        
    }

}
