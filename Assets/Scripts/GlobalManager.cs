using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;

    public bool is3D;
    public Action<bool> TogglePerspective; // true when transitioning to 3d
    public Action TogglePerspectiveFinished; // called when transitioning ended

    public Action<bool, float> GroundedChanged; // true + fall speed when touched down, false when left ground
    public Action Jumped; // called when jumped
    
    public int killCount;
    public Action TrpaslikKilled;

    public int apologizeCount;
    public Action TrpaslikApologized;

    public Action<CharacterControllerSwitcher> PlayerSpawned;
    public Action PlayerKilled;

    public DialogWindow DialogWindow;

    void Awake()
    {
        Instance = this;
        is3D = false;
        
        PlayerKilled += OnPlayerKilled;
    }

    private void OnPlayerKilled()
    {
        StartCoroutine(OnPlayerKilledCoroutine());
    }

    private IEnumerator OnPlayerKilledCoroutine()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            is3D = !is3D;
            TogglePerspective?.Invoke(is3D);
        }
    }
}
