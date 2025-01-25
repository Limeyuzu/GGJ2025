using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] Image Sprite;
    [SerializeField] Image AltSprite;
    [SerializeField] List<Interaction> Interactions;

    public List<Interaction> GetInteractions() => Interactions;
    public Sprite GetMainSprite() => Sprite.sprite;
    public Sprite GetAltSprite() => AltSprite.sprite;
}
