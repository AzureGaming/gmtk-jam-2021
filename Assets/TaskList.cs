using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskList : MonoBehaviour {
    public delegate void UpdateList();
    public static UpdateList OnUpdateList;

    public GameObject taskItemPrefab;

    Dictionary<Fixable, GameObject> taskDictionary = new Dictionary<Fixable, GameObject>();

    private void OnEnable() {
        OnUpdateList += UpdateStatuses;
    }

    private void OnDisable() {
        OnUpdateList -= UpdateStatuses;
    }

    private void Start() {
        GetTasks();
        UpdateStatuses();
    }

    void UpdateStatuses() {
        foreach (KeyValuePair<Fixable, GameObject> task in taskDictionary) {
            if (task.Key.source.value == 100) {
                task.Value.GetComponent<TextMeshProUGUI>().color = Color.green;
            } else {
                task.Value.GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
    }

    void GetTasks() {
        taskDictionary.Clear();
        Fixable[] tasks = FindObjectsOfType<Fixable>();

        foreach (Fixable task in tasks) {
            GameObject obj = Instantiate(taskItemPrefab, transform, false);
            TextMeshProUGUI text = obj.GetComponent<TextMeshProUGUI>();

            text.SetText(GetTaskFromTag(task.gameObject.tag));
            taskDictionary.Add(task, obj);
        }
    }

    string GetTaskFromTag(string tag) {
        switch (tag) {
            case "Engine":
                return "Critical engine failure detected.";
            case "FuelDropOff":
                return "Fuel is low. Start the refueling process.";
            case "HullBreach":
                return "Detected a breach in the hull.";
            case "HyperDrive":
                return "Enable the Hyper Drive capacitors.";
            case "Thruster":
                return "Reconnect thrusters.";
            default:    
                return "INVALID TASK";
        }
    }
}
