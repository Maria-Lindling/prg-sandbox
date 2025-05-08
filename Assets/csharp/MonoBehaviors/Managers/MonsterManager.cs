using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private static MonsterManager _instance;
    public static MonsterManager Instance { get => _instance; private set => _instance = value; }


    private List<Actor> _monsterManual;
    public List<Actor> MonsterManual => _monsterManual;


    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            _monsterManual = new();

            _monsterManual.Append(new Actor("Slug"));
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
