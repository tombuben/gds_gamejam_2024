using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;
    
    public bool is3D;
    public Action<bool> OnTogglePerspective; // true when transitioning to 3d
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
    
    public Action OnDialogShown;
    public Action OnDialogClosed;

    public Action OnHatPickedUp;
    public Action OnHatLost;
    public bool PickedUpHat = false;

    public bool SmudlaIsFollowing;
    public Action SmudlaStartedFollowing;
    public Action StopSmudlaFollowing;

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

    public void TogglePerspective()
    {
        is3D = !is3D;
        OnTogglePerspective?.Invoke(is3D);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TogglePerspective();
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
