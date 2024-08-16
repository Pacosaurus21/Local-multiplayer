using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public float speed, rotationSpeed, jumpForce, shootForce;
    public Vector3 moveDir;
    private Rigidbody rigid;
    public GameObject missile;
    public Transform otherPlayer;
    public Transform shootPoint, shootRotation;
    public string inputCode, enemyInputCode;

    public int lifes;
    public int score;
    public int rounds;

    public TextMeshProUGUI lifesText;
    public TextMeshProUGUI scoreText;

    public Vector3 originalPoint, originalOtherPoint;
    public Quaternion originalRotation, originalOtherRotation;

    public RoundControl _roundControl;

    public TextMeshProUGUI Win;
    public TextMeshProUGUI Lose;

    public GameObject otherPlayerObject;
    public PlayerControl otherPlayerControl;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        originalPoint = transform.position;
        originalRotation = transform.rotation;
        rounds = 1;
        Canvas.ForceUpdateCanvases();
        Win.gameObject.SetActive(false);
        Lose.gameObject.SetActive(false);
        otherPlayerControl = otherPlayerObject.GetComponent<PlayerControl>();
        
    }
    public void Update()
    {
        if (Input.GetButtonDown("Fire1" + inputCode))
        {
            GameObject newMissile = Instantiate(missile, shootPoint.position, shootRotation.rotation);
            newMissile.GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
            newMissile.tag = "Bullet" + inputCode;
            Destroy(newMissile, 5);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        moveDir = new Vector3(0, 0, Input.GetAxis("Vertical" + inputCode));
        moveDir = transform.TransformDirection(moveDir);
        transform.Rotate(Vector3.up * rotationSpeed * Input.GetAxis("Horizontal" + inputCode) * Time.deltaTime);
        rigid.MovePosition(rigid.position + moveDir * speed * Time.deltaTime);
        lifesText.text = lifes.ToString();
        scoreText.text = score.ToString();
        
    }
    //Void for bullets and scores, and transforms
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet" + enemyInputCode)
        {
            Destroy(other.gameObject);
            GameObject[] clones = GameObject.FindGameObjectsWithTag("Bullet" + enemyInputCode);
            foreach (GameObject clone in clones)
            {
                Destroy(clone);
            }
            transform.position = originalPoint;
            transform.rotation = originalRotation;
            otherPlayer.transform.position = originalOtherPoint;
            otherPlayer.transform.rotation = originalOtherRotation;
            lifes--;
            if(lifes >= 0)
            {
                score++;
                _roundControl.rounds++;
                
            }
            if (lifes <= 0)
            {
                print("GameOver");
                LosePlayer();
                lifes = 0;
                otherPlayerControl.WinPlayer();
            }
            
        }
    }

    //Void win, know that the player won
    public void WinPlayer()
    {
        Win.gameObject.SetActive(true);
    }
    //Void lose, know that the player lost
    public void LosePlayer()
    {
        Lose.gameObject.SetActive(true);
    }
}
