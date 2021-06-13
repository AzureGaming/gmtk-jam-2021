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
    GameObject fuelDropOffText;

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

        FuelDropoff fuelDropOff = FindObjectOfType<FuelDropoff>();
        if (fuelDropOff != null && fuelDropOffText != null) {
            if (fuelDropOff.isActive) {
                fuelDropOffText.GetComponent<TextMeshProUGUI>().color = Color.red;
            } else {
                fuelDropOffText.GetComponent<TextMeshProUGUI>().color = Color.green;
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

        FuelDropoff fuelDropOff = FindObjectOfType<FuelDropoff>();
        GameObject obj2 = Instantiate(taskItemPrefab, transform, false);
        fuelDropOffText = obj2;
        TextMeshProUGUI text2 = obj2.GetComponent<TextMeshProUGUI>();
        text2.SetText(GetTaskFromTag(fuelDropOff.gameObject.tag));
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
