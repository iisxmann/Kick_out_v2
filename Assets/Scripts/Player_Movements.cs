using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class Player_Movements : MonoBehaviour
{

    [SerializeField] private Camera Camera;
    [SerializeField] private NavMeshAgent Agent;
    public GameObject GameoverPanel;
    public GameObject WinnerPanel;
    public GameObject GamePanel;
    float geriSayim = 60.0f;

    public Text skor;
    public Text geriSayimSkor;

    private RaycastHit[] Hits = new RaycastHit[1];
    // Start is called before the first frame update
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //E�er oyun bir durumdan dolay� durmu�sa burada ba�lat�yoruz.
        Time.timeScale = 1;
        GameoverPanel.SetActive(false);
        WinnerPanel.SetActive(false);
        GamePanel.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        //T�klan�lan yere Player �n y�nlendirilmesini sa�l�yoruz.
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.RaycastNonAlloc(ray, Hits) > 0)
            {
                Agent.SetDestination(Hits[0].point += Vector3.forward);
            }
        }

        var dusman = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (dusman == 0)
        {
            //Oyunu durduruyoruz
            Time.timeScale = 0;

            //Panelleri akti�le�tiriyoruz
            WinnerPanel.SetActive(true);
            GamePanel.SetActive(false);
        }

        //Oyundaki dusmanlar� say�p d��enleri skor olarak ekliyoruz. Toplam dusman say�s� - dusan d��man = Skor.
        dusman = 10 - dusman; 
        skor.text = dusman.ToString();

        geriSayim -= Time.deltaTime;
        geriSayimSkor.text = "" + (int)geriSayim;
        if (geriSayim < 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Saha kenarlar�na �arp�nca gameobject yok ediliyor.
        Agent.enabled = false;

        //Suya d��me efekti g�r�ns�n diye gecikme veriyoruz.
        Invoke("Die", 2f);
    }

    void Die()
    {        
        gameObject.SetActive(false);

        //Oyunu durduruyoruz
        Time.timeScale = 0;
        
        //Panelleri akti�le�tiriyoruz
        GameoverPanel.SetActive(true); 
        GamePanel.SetActive(false); 
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
