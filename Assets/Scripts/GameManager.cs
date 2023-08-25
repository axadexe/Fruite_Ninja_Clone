using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoretxt;
    private int score;

    private Blade blade;
    private Spawner spawner;

    public Image fadeimage;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        NewGame();
    }
    private void NewGame()
    {
        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoretxt.text = score.ToString();
        Time.timeScale = 1f;

        ClearScene();

    }
    private void ClearScene()
    {
        Fruits[] fruits = FindObjectsOfType<Fruits>();

        foreach (Fruits fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }


    public void IncreaseScore()
    {
        score++;
        scoretxt.text = score.ToString();
    }
    public void explode()
    {
        blade.enabled = false;
        spawner.enabled = false;
        StartCoroutine(ExplodSequence());

    }


    private IEnumerator ExplodSequence()
    {
        float elapsed = 0f;
        float duration= 0.05f;
        while(elapsed<duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeimage.color = Color.Lerp(Color.clear, Color.white,t);
            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;


            
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        NewGame();
        elapsed = 0f;
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeimage.color = Color.Lerp(Color.white, Color.clear, t);
     
            elapsed += Time.unscaledDeltaTime;



            yield return null;
        }

    }



}
