/*
 * 
 * This is the torso class. It has the condition to check if player
 * has the torso or not. Players are able to detach or attach the 
 * torso depending on the condition.
 * 
 * 
 * */


using UnityEngine;
using System.Collections;

public class Torso : MonoBehaviour
{


    public bool hasTorso;
    public bool torsoCollide;
    public Arms arm;
    public LegObject leg;
    public Sprite head;
    public Sprite torso;
    GameObject bodyObj;
    GameObject sub;
    bool nearTorso;
    GameObject[] goWithTag;
    public SpriteRenderer sprRend;
    Transform player;
    Vector3 pos;


    private KeyCode[] keyCodes = {
        KeyCode.Alpha1,
    };



    // Use this for initialization
    void Start()
    {
        hasTorso = false;
        arm = GetComponent<Arms>();
        leg = GetComponent<LegObject>();
        sprRend = GetComponent<SpriteRenderer>();
        sprRend.sprite = head;
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.position;
        attachTorso();
        detachTorso();

        nearTorso = CheckCloseToTag("torso", 1.5f);

        //sprRend.sprite = test;
    }

    //Checking if the player is near the torso body part
    bool CheckCloseToTag(string tag, float minimumDistance)
    {
        goWithTag = GameObject.FindGameObjectsWithTag("torso");

        for (int i = 0; i < goWithTag.Length; ++i)
        {
            if (Vector3.Distance(transform.position, goWithTag[i].transform.position) <= minimumDistance)
            {
                bodyObj = goWithTag[i];

                Debug.Log("Press X to pick up");
                return true;
            }
        }
        return false;
    }

    //Attach the torso to the head.
    void attachTorso()
    {
        if (nearTorso && Input.GetKeyDown(KeyCode.X))
        {
            //Speed changes
            sub = bodyObj;
            Debug.Log("testing in attaching");
            bodyObj.SetActive(false);
            hasTorso = true;
            sprRend.sprite = head;
        }
    }

    //Detach the torso if players want to. 
    void detachTorso()
    {
        Vector3 pos = player.position;
        //pos.x += 4;
        if (hasTorso && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Testing in detach");
            //sprite changes to head
            sprRend.sprite = head;
            //speed changes.
            bodyObj.SetActive(true);
            hasTorso = false;
            Instantiate(bodyObj, pos, Quaternion.Euler(0f, 0f, 0f));
            Destroy(bodyObj);
            oneArm();
            twoArms();
            oneLeg();
            twoLegs();
            oneArmOneLeg();
            oneArmTwoLegs();
            twoArmsOneLeg();
            twoArmsTwoLegs();
        }
    }
    void oneArm()
    {
        if (hasTorso && Input.GetKeyDown(KeyCode.Alpha1) && arm.armCount == 1)
        {
            arm.armObj.SetActive(true);
            Instantiate(arm.armObj, pos, Quaternion.Euler(0f, 0f, 0f));
            Destroy(arm.armObj);
            sprRend.sprite = head;
            arm.armCount = 0;
        }

    }
    void twoArms()
    {
        if (hasTorso && Input.GetKeyDown(KeyCode.Alpha1) && arm.armCount == 2)
        {
            for (int i = 0; i < arm.goWithTag.Length; i++)
            {
                arm.goWithTag[i].SetActive(true);
                Instantiate(arm.goWithTag[i], pos, Quaternion.Euler(0f, 0f, 0f));
                Destroy(arm.goWithTag[i]);
                arm.armCount = 0;
            }
            sprRend.sprite = head;
        }
    }
    void oneLeg()
    {
        if (hasTorso && Input.GetKeyDown(KeyCode.Alpha1) && arm.armCount == 0 && leg.legCount == 1)
        {
            leg.legObj.SetActive(true);
            Instantiate(leg.legObj, pos, Quaternion.Euler(0f, 0f, 0f));
            Destroy(leg.legObj);
            sprRend.sprite = head;
            leg.legCount = 0;
        }
    }
    void twoLegs()
    {
        if (hasTorso && Input.GetKeyDown(KeyCode.Alpha1) && arm.armCount == 0 && leg.legCount == 2)
        {
            for (int i = 0; i < leg.goWithTag.Length; i++)
            {
                leg.goWithTag[i].SetActive(true);
                Instantiate(leg.goWithTag[i], pos, Quaternion.Euler(0f, 0f, 0f));
                Destroy(leg.goWithTag[i]);
                leg.legCount = 0;
            }
            sprRend.sprite = head;
        }
    }

    void oneArmOneLeg()
    {
        if (hasTorso && Input.GetKeyDown(KeyCode.Alpha1) && arm.armCount == 1 && leg.legCount == 1)
        {
            leg.legObj.SetActive(true);
            arm.armObj.SetActive(true);
            Instantiate(leg.legObj, pos, Quaternion.Euler(0f, 0f, 0f));
            Instantiate(arm.armObj, pos, Quaternion.Euler(0f, 0f, 0f));
            Destroy(leg.legObj);
            Destroy(arm.armObj);
            sprRend.sprite = head;
        }
        leg.legCount = 0;
        arm.armCount = 0;
    }
    void oneArmTwoLegs()
    {
        if (hasTorso && Input.GetKeyDown(KeyCode.Alpha1) && arm.armCount == 1 && leg.legCount == 2)
        {

            arm.armObj.SetActive(true);
            Instantiate(arm.armObj, pos, Quaternion.Euler(0f, 0f, 0f));
            Destroy(arm.armObj);
            leg.legObj.SetActive(true);
            for (int i = 0; i < leg.goWithTag.Length; i++)
            {
                leg.goWithTag[i].SetActive(true);
                Instantiate(leg.goWithTag[i], pos, Quaternion.Euler(0f, 0f, 0f));
                Destroy(leg.goWithTag[i]);
            }
            sprRend.sprite = head;
        }
        arm.armCount = 0;
        leg.legCount = 0;
    }
    void twoArmsOneLeg()
    {
        if (hasTorso && Input.GetKeyDown(KeyCode.Alpha1) && arm.armCount == 2 && leg.legCount == 1)
        {

            for (int i = 0; i < arm.goWithTag.Length; i++)
            {
                arm.goWithTag[i].SetActive(true);
                Instantiate(arm.goWithTag[i], pos, Quaternion.Euler(0f, 0f, 0f));
                Destroy(arm.goWithTag[i]);
            }

            leg.legObj.SetActive(true);
            Instantiate(leg.legObj, pos, Quaternion.Euler(0f, 0f, 0f));
            //Instantiate (leg.legObj, pos, Quaternion.Euler(0f,0f,0f));
            Destroy(leg.legObj);
            sprRend.sprite = head;
        }
        arm.armCount = 0;
        leg.legCount = 0;
    }

    void twoArmsTwoLegs()
    {
        if (hasTorso && Input.GetKeyDown(KeyCode.Alpha1) && arm.armCount == 2 && leg.legCount == 1)
        {

            for (int i = 0; i < arm.goWithTag.Length; i++)
            {
                arm.goWithTag[i].SetActive(true);
                Instantiate(arm.goWithTag[i], pos, Quaternion.Euler(0f, 0f, 0f));
                Destroy(arm.goWithTag[i]);
            }

            for (int i = 0; i < leg.goWithTag.Length; i++)
            {
                leg.goWithTag[i].SetActive(true);
                Instantiate(leg.goWithTag[i], pos, Quaternion.Euler(0f, 0f, 0f));
                Destroy(leg.goWithTag[i]);
            }
            sprRend.sprite = head;
        }
        arm.armCount = 0;
        leg.legCount = 0;
    }
}