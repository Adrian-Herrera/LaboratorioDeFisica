﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    // Start is called before the first frame update

    void Awake()
    {
        Time.timeScale = 1;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextLevel(string LevelName)
    {
        StartCoroutine(LoadLevel(LevelName));
    }

    IEnumerator LoadLevel(string LevelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(LevelName);
    }


}
