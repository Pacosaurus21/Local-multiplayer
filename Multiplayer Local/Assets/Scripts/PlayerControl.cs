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
    public Transform shootPoint, shootRotation;
    public string inputCode, enemyInputCode;

    public int lifes;
    public int score;

    public TextMeshProUGUI lifesText;
    public TextMeshProUGUI scoreText;

    public Vector3 originalPoint;
    public Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        originalPoint = transform.position;
        originalRotation = transform.rotation;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet" + enemyInputCode)
        {
            Destroy(other.gameObject);
            transform.position = originalPoint;
            transform.rotation = originalRotation;
            lifes--;
            if(lifes >= 0)
            {
                score++;
            }
            if (lifes <= 0)
            {
                print("GameOver");
                lifes = 0;
            }
            
        }
    }
}
