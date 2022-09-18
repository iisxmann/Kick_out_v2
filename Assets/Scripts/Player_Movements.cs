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
        //Eðer oyun bir durumdan dolayý durmuþsa burada baþlatýyoruz.
        Time.timeScale = 1;
        GameoverPanel.SetActive(false);
        WinnerPanel.SetActive(false);
        GamePanel.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        //Týklanýlan yere Player ýn yönlendirilmesini saðlýyoruz.
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

            //Panelleri aktiþleþtiriyoruz
            WinnerPanel.SetActive(true);
            GamePanel.SetActive(false);
        }

        //Oyundaki dusmanlarý sayýp düþenleri skor olarak ekliyoruz. Toplam dusman sayýsý - dusan düþman = Skor.
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
        //Saha kenarlarýna çarpýnca gameobject yok ediliyor.
        Agent.enabled = false;

        //Suya düþme efekti görünsün diye gecikme veriyoruz.
        Invoke("Die", 2f);
    }

    void Die()
    {        
        gameObject.SetActive(false);

        //Oyunu durduruyoruz
        Time.timeScale = 0;
        
        //Panelleri aktiþleþtiriyoruz
        GameoverPanel.SetActive(true); 
        GamePanel.SetActive(false); 
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
