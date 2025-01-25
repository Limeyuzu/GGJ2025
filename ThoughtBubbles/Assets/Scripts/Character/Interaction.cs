using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Interaction
{
    public string Prompt;
    public List<Dialogue> Responses;

    public List<Dialogue> GetResponses()
    {
        return Responses.OrderBy(_ => Random.value).ToList();
    }
}
