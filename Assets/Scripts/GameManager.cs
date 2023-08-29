using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int pilCount = 0;
    public GameObject[] pils; 
    private float currentHueShift = 0f;
    public float valueChangeEveryUpdate = 0.01f;
    public VolumeProfile volume;
    public TextMeshProUGUI scoreText;
    public GameObject[] doors;
    private SoundEffectManager soundEffectManager;
    public GameObject bequilleItem;
    public GameObject pillsBg;
    public GameObject blockNoteImage;
    public GameObject lifeBar;
    public GameObject postProcessingVolume;
    private PlayerMovement playerMovement;
    public bool isEndScene = false;
    public GameObject ZombieOutro;
    public GameObject MedecinOutro;
    public GameObject OutroGameObject;
    public GameObject AudioManager;

    private void Start() {
        AudioManager.SetActive(false);
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        postProcessingVolume.gameObject.SetActive(false);
        lifeBar.SetActive(false);
        blockNoteImage.SetActive(false);
        soundEffectManager = GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>();
        scoreText.text = "0";
        pillsBg.SetActive(false);
        bequilleItem.SetActive(false);
        foreach(GameObject pil in pils)
        {
            pil.SetActive(false);
        }

        ZombieOutro.SetActive(false);
        MedecinOutro.SetActive(false);
        volume.TryGet<Vignette>(out Vignette vignette);
        vignette.intensity.value = 0.18f;
    }
    public void OpenCloseDoor(int index)
    {
        doors[index].SetActive(!doors[index].activeSelf);
    }
    
    private void Update() {
        if(volume.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments))
        {
            currentHueShift += valueChangeEveryUpdate;
            colorAdjustments.hueShift.value = currentHueShift;

            if(currentHueShift >= 180f || currentHueShift <= -180f)
            {
                valueChangeEveryUpdate *= -1;
            }
        }
        if(volume.TryGet<LensDistortion>(out LensDistortion lensDistortion))
        {
            lensDistortion.intensity.value = -Mathf.Abs(0.75f*Mathf.Sin(1.3f*Time.time));
        }

    }
    public void PickUpPil()
    {
        soundEffectManager.PlaySoundEffect(3);
        OpenCloseDoor(pilCount+1);
        pils[pilCount].SetActive(true);
        pilCount++;
        if(pilCount == 1)
        {
            StartCoroutine(GameObject.Find("Player").GetComponent<PlayerMovement>().FromSwordToPistol());
        }
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
    }
    public void EarnScorePoints(int points)
    {
        scoreText.text = (int.Parse(scoreText.text) + points).ToString();
    }
    public void StartEndGame()
    {
        soundEffectManager.PlaySoundEffect(3);
        OutroGameObject.SetActive(true);
        volume.TryGet<Vignette>(out Vignette vignette);
        vignette.intensity.value = 0.5f;
    }
    public void PickUpBlocNote()
    {
        blockNoteImage.SetActive(true);
        bequilleItem.SetActive(true);
        pillsBg.SetActive(true);

    }
    public IEnumerator GetDrunk()
    {
        AudioManager.SetActive(true);
        float currentIntensity = 0.18f;
        for(int i = 0; i < 5; i++)
        {
            postProcessingVolume.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.25f-0.03f*i);
            postProcessingVolume.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.25f-0.03f*i);
        }
        yield return new WaitForSeconds(0.25f);
        
        postProcessingVolume.gameObject.SetActive(true);
        while(currentIntensity < 1f)
        {
            currentIntensity += 0.01f;
            volume.TryGet<Vignette>(out Vignette vignette);
            vignette.intensity.value = currentIntensity;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        while(currentIntensity > 0.18f)
        {
            currentIntensity -= 0.01f;
            volume.TryGet<Vignette>(out Vignette vignette);
            vignette.intensity.value = currentIntensity;
            yield return new WaitForSeconds(0.01f);
        }

        OpenCloseDoor(0);
        playerMovement.canMove = true;
        playerMovement.canHit = true;
        lifeBar.SetActive(true);
    }
    public void StartOutro()
    {
        // playerMovement.canMove = false;
        // playerMovement.canHit = false;
        // playerMovement.canShoot = false;
        StartCoroutine(Outro());
    }
    private IEnumerator Outro()
    {
        MusicFadeAway();
        MedecinOutro.transform.position = ZombieOutro.transform.position + new Vector3(0,0.76f,0);
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < 10; i++)
        {

            postProcessingVolume.gameObject.SetActive(false);
            if(i!=0)
            {
                MedecinOutro.SetActive(true);
                MedecinOutro.transform.position = ZombieOutro.transform.position + new Vector3(0,0.76f,0);
                ZombieOutro.SetActive(false);
            }
            yield return new WaitForSeconds(0.33f-0.03f*i);
            if(i!=0)
            {
                MedecinOutro.SetActive(false);
                ZombieOutro.SetActive(true);
            }
            postProcessingVolume.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.33f-0.03f*i);
        }
        yield return new WaitForSeconds(0.25f);

        float currentIntensity = 0.18f;
        while(currentIntensity < 1f)
        {
            currentIntensity += 0.01f;
            volume.TryGet<Vignette>(out Vignette vignette);
            vignette.intensity.value = currentIntensity;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        while(currentIntensity > 0.18f)
        {
            currentIntensity -= 0.01f;
            volume.TryGet<Vignette>(out Vignette vignette);
            vignette.intensity.value = currentIntensity;
            yield return new WaitForSeconds(0.01f);
        }
        postProcessingVolume.gameObject.SetActive(false);
        MedecinOutro.SetActive(true);
        MedecinOutro.transform.position = ZombieOutro.transform.position + new Vector3(0,0.76f,0);
        ZombieOutro.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Credits");
    }
    private void MusicFadeAway()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().FadeAway();
    }

}
