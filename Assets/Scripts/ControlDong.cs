using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityRandom = UnityEngine.Random;

public class ControlDong : MonoBehaviour {

    public Rigidbody target;
    public float speed;
    public float acclerationIncrease;
    public float acclerationDecrease;
    public float jumpPower = 70f;
    public float jumpCooldown = 0.8f;
    public static bool PlaySound { get; set; }
    private float accleration = 0;    
    private bool canJump = true;
    private float _jumpDist = 0f;
    private bool isJumpInCooldown = false;

	// Use this for initialization
	void Start () {
        PlaySound = true;
        target = GetComponent<Rigidbody>();
        gameObject.GetComponent<PlayerLevel>().LeveledUp += (sender, useless) =>
        {
            var s = (PlayerLevel)sender;
            speed += 0.05f + s.Level / 45f;
            acclerationIncrease += 0.05f + s.Level / 10f;
        };
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        CalculateAccleration(horizontal, vertical);
        string acclerationString = "Accleration : " + accleration;
        GameObject.Find("Acceleration").GetComponent<Text>().text = acclerationString;
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isJumpInCooldown)
        {
            _jumpDist = jumpPower;
            canJump = false;
            Debug.Log("Let's jump !");
            StartCoroutine(JumpCooldown());
        }
        Vector3 move = new Vector3(horizontal, _jumpDist, vertical);
        target.AddForce(move * (speed + accleration + target.mass / 2));
        _jumpDist = 0f;
    }

    private void CalculateAccleration(float horizontal, float vertical)
    {
        if (horizontal != 0 || vertical != 0)
        {
            float velocity = SumVector3(target.velocity);
            float reduce = (float)Math.Pow(2, (accleration * 1.8f) + 0.01f);
            accleration += (acclerationIncrease * (velocity / reduce) * Time.deltaTime) / (accleration + 2f + reduce);
        }
        else if (accleration > 0)
        {
            accleration -= Math.Min(accleration, (acclerationDecrease / SumVector3(target.velocity)) * Time.deltaTime);
        }
    }

    private float SumVector3(Vector3 vector)
    {
        return Math.Abs(vector.x + vector.y + vector.z);
    }
    // OnCollisionEnter est appelé quand ce collider/rigidbody commence à toucher un autre rigidbody/collider french ftw
    private void OnCollisionEnter(Collision collision)
    {
        SetJumpStatus();
        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            // SOUND
            var soundObject = (GameObject)Instantiate(Resources.Load("Sound/FAFSound"));
            if (PlaySound)
            {
                soundObject.GetComponent<FireAndForgetSound>().Play((AudioClip)Resources.Load("Syndra/SyndraDying"));
            }
            gameObject.GetComponent<EntityStat>().KilledSomeone();
            GameObject.Find("AnnouncerManage").GetComponent<AnnouncerManager>().ShowEnemySlain();
            var lvl = gameObject.GetComponent<PlayerLevel>().Level;
            gameObject.GetComponent<PlayerLevel>().AddExperience(UnityRandom.Range(19,UnityRandom.Range(26,30 + lvl * 3))); // range-ception
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        SetJumpStatus();
    }

    private void SetJumpStatus()
    {
        if (!isJumpInCooldown)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }
    
    IEnumerator JumpCooldown()
    {
        canJump = false;
        isJumpInCooldown = true;
        Debug.Log("Cooldown activated");
        yield return new WaitForSeconds(jumpCooldown);
        Debug.Log("Cooldown disabled");
        isJumpInCooldown = false;
    }
}
