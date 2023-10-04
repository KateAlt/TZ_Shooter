using UnityEngine;

[CreateAssetMenu(fileName="BaseData", menuName = "ScriptableObjects/", order = 1)]
public class BaseData : ScriptableObject
{
    [SerializeField, Range(0, 100)]
    private float HealthPlayer;
    public float healthPlayer
    {
        get { return HealthPlayer; }
        set { if(value > 100)
                HealthPlayer = 100;
                else if (value < 0)
                HealthPlayer = 0;
                else
                HealthPlayer = value;
            }
    }

    [SerializeField, Range(0, 100)]
    private float HealthEnemy;
    public float healthEnemy
    {
        get { return HealthEnemy; }
        set { if(value > 100)
                HealthEnemy = 100;
                else if (value < 0)
                HealthEnemy = 0;
                else
                HealthEnemy = value;
            }
    }
}


// [SerializeField, Min(1)]
//     private int Level;
//     public int level
//     {
//         get { return Level; }
//         set { Level = value; }
//     }