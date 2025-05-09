using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using Matsuri;

public class PlayerController : MonoBehaviour
{
    //public Transform cameraTransform;
    public int canloadlocate { get; set; }
    public float speed;
    public bool canmove;
    public bool faceingRight = true;
    public Animator animate;

    [Header("Skin")]
    public ImageAsset[] SkinMaterials;
    // Start is called before the first frame update
    void Start()
    {
        //cameraTransform = Camera.main.transform;
        Debug.Log(canloadlocate);
        canloadlocate = PlayerPrefs.GetInt("PlayerHaveLocate");
        Debug.Log(canloadlocate);
        if(canloadlocate == 1)
        {
            Vector3 lastlocate = PlayerDataManager.inter.LoadLocate();
            transform.position = lastlocate;
#if UNITY_EDITOR
            Debug.Log($"||RingToss: {PlayerDataManager.inter.ScoreRingToss} || KnockOut: {PlayerDataManager.inter.ScoreKnockOut} || GunBlast: {PlayerDataManager.inter.ScoreGunBlast}");
            Debug.Log($"||Coin: {PlayerDataManager.inter.gold}");
#endif
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log($"||Coin: {PlayerDataManager.inter.gold}");
            Debug.Log("Not Save");
#endif
            //canmove = true;
        }
        Debug.Log(canloadlocate);
        canmove = true;
        SetSkin(SkinMaterials[PlayerDataManager.skinPickedID]);
    }

    public void SetSkin(ImageAsset skin)
    {
        //GetComponent<SpriteRenderer>().sprite = SkinMaterials[PlayerDataManager.skinPickedID];
        foreach (Transform child in transform)
        {
            if(child.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                switch (child.name)
                {
                    case "Head":
                        child.gameObject.GetComponent<SpriteRenderer>().sprite = skin.head;
                        break;

                    case "RHand":
                        child.gameObject.GetComponent<SpriteRenderer>().sprite = skin.rhand;
                        break;

                    case "LHand":
                        child.gameObject.GetComponent<SpriteRenderer>().sprite = skin.lhand;
                        break;

                    case "Cloth":
                        child.gameObject.GetComponent<SpriteRenderer>().sprite = skin.body;
                        break;

                    case "RLeg":
                        child.gameObject.GetComponent<SpriteRenderer>().sprite = skin.rpant;
                        break;

                    case "LLeg":
                        child.gameObject.GetComponent<SpriteRenderer>().sprite = skin.lpant;
                        break;
                }
            }
            else
            {
                break;
            }
            /*if(skin.head != null)
                child.gameObject.GetComponent<SpriteRenderer>().sprite = skin.head;*/
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManagerWorld.instance.gameHasStarted)
        {
            if(canmove)
            {
                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");
                Vector2 direction = new Vector2(x, y).normalized;
                Move(direction);
                animate.SetFloat("Speed",Mathf.Abs(x));
                if (x == 0 && y == 0)
                {
                    GetComponent<AudioSource>().mute = true;
                }
                else
                {
                    GetComponent<AudioSource>().mute = false;
                }

                if (x > 0 && !faceingRight)
                {
                    Flip();
                }
                if (x < 0 && faceingRight)
                {
                    Flip();
                }

                if(y > 0 || y < 0)
                {
                    animate.SetFloat("Speed", Mathf.Abs(y));
                }
            }
        }
        else 
        {
            if(GameManagerWorld.instance.introStart)
            {
                Move(new Vector2(0.75f, 0f));
                animate.SetFloat("Speed", Mathf.Abs(0.75f));
            }

        }

    }

    void Move(Vector2 direction)
    {
        /*Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.25f;
        min.x = min.x + 0.25f;

        max.y = max.y - 0.5f;
        min.y = min.y + 0.5f;*/

        Vector2 pos = transform.position;
        pos += direction * speed * Time.deltaTime;

        //pos.x = Mathf.Clamp(pos.x,min.x,max.x);
        pos.y = Mathf.Clamp(pos.y, -7.25f, -3.25f);

        transform.position = pos;
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        faceingRight = !faceingRight;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetString("QuitTime", "The application last closed at: " + System.DateTime.Now);
    }
}
