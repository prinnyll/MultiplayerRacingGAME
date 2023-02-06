using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TakeDamage : MonoBehaviourPun
{
    public float starthHealth = 100f;
    private float health;
    public Image healthBar;

    Rigidbody rb;

    public GameObject PlayerGraphics;
    public GameObject PlayerUI;
    public GameObject PlayerWeaponHolder;
    public GameObject DeathPanelUIPrefab;
    private GameObject DeathPanelUIGameObject;

    // Start is called before the first frame update
    void Start()
    {

        health = starthHealth;

        healthBar.fillAmount = health / starthHealth;

        rb = GetComponent<Rigidbody>();

    }


    [PunRPC]
    public void DoDamage(float _damage)
    {
        health -= _damage;
        Debug.Log(health);

        healthBar.fillAmount = health / starthHealth;

        if (health <= 0f)
        {
            //Die
            Die();
        }
    }

    void Die()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;


        PlayerGraphics.SetActive(false);
        PlayerUI.SetActive(false);
        PlayerWeaponHolder.SetActive(false);


        if (photonView.IsMine)
        {
            //respawn
            StartCoroutine(ReSpawn());
        }
    }

    IEnumerator ReSpawn()
    {
        GameObject canvasGameObject = GameObject.Find("Canvas");

        if (DeathPanelUIGameObject==null)
        {
            DeathPanelUIGameObject = Instantiate(DeathPanelUIPrefab, canvasGameObject.transform);

        }
        else
        {
            DeathPanelUIGameObject.SetActive(true);
        }

        Text respawnTimeText = DeathPanelUIGameObject.transform.Find("RespawnTimeText").GetComponent<Text>();

        float respawnTime = 8.0f;

        respawnTimeText.text = respawnTime.ToString(".00");

        while (respawnTime> 0.0f)
        {
            yield return new WaitForSeconds(1.0f);
            respawnTime -= 1.0f;
            respawnTimeText.text = respawnTime.ToString(".00f");


            GetComponent<CarMovement>().enabled = false;
            GetComponent<Shooting>().enabled = false;
        }


        DeathPanelUIGameObject.SetActive(false);

        GetComponent<CarMovement>().enabled = true;
        GetComponent<Shooting>().enabled = true;


        int randomPoint = Random.Range(-20,20);
        transform.position = new Vector3(randomPoint,0,randomPoint);

        photonView.RPC("Reborn",RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void Reborn()
    {
        health = starthHealth;
        healthBar.fillAmount = health / starthHealth;

        PlayerGraphics.SetActive(true);
        PlayerUI.SetActive(true);
        PlayerWeaponHolder.SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
