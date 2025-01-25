using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] List<Interaction> Interactions;

    public List<Interaction> GetInteractions() => Interactions;
}
