using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

public class UIController : MonoBehaviour
{
    public UIDocument document;
    public BaseData baseData;

    public void Awake()
    {
        UpdateHealthData();
        UpdateEnemyHealthData();
    }

    public void UpdateHealthData(){

        var root = document.rootVisualElement;
        var healthLine = root.Q<VisualElement>("HealthLine");

        healthLine.style.width = Length.Percent(baseData.healthPlayer);
    }

    public void UpdateEnemyHealthData(){

        var root = document.rootVisualElement;
        var enemyHealthLine = root.Q<VisualElement>("EnemyHealthLine");

        enemyHealthLine.style.width = Length.Percent(baseData.healthEnemy);
    }
}
