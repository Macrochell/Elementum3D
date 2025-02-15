using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    private List<string> worldEvents = new List<string>();
    [SerializeField] private TextMeshProUGUI worldText;

    public void AddEvent(string newEvent)
    {
        if (worldEvents.Count >= 1)
        {
            worldEvents.RemoveAt(0);
        }

        worldEvents.Add(newEvent);
        UpdateWorldText();
    }

    private void UpdateWorldText()
    {
        worldText.text = string.Join("\n", worldEvents);
    }
}
