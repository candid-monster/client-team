using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject angle;
    public float moveSpeed;
    public float weaponSpeed = 10f;
    public List<GameObject> weaponPrefabs;
    public List<GameObject> weapons;
    public GameObject weaponSpawnPos;
    private GameObject weapon;
    public float attackCoolDown = 0.5f;
    private float nextAttack;
    Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        for (int i = 0; i < 4; i++)
        {
            GameObject tmp = Instantiate(weaponPrefabs[i]);
            tmp.transform.position = new Vector2(1000, 1000);
            weapons.Add(tmp);
        }
        weapon = weapons[0];
    }
    private void Update()
    {
        LookAt();
        WeaponSwap();
        Attack();
    }
    void FixedUpdate()
    {
        float xinput = Input.GetAxis("Horizontal");
        float yinput = Input.GetAxis("Vertical");
        Vector2 newVelocity = new Vector2(xinput, yinput);
        gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity*moveSpeed;
    }
    void LookAt()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dirVec = mousePos - (Vector2)transform.position;
        transform.right = dirVec.normalized;
    }
    void WeaponSwap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("무기1번");
            weapons[0].gameObject.SetActive(true);
            weapons[1].gameObject.SetActive(false);
            weapons[2].gameObject.SetActive(false);
            weapons[3].gameObject.SetActive(false);
            weapon = weapons[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("무기2번");
            weapons[1].gameObject.SetActive(true);
            weapons[0].gameObject.SetActive(false);
            weapons[2].gameObject.SetActive(false);
            weapons[3].gameObject.SetActive(false);
            weapon = weapons[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("무기3번");
            weapons[2].gameObject.SetActive(true);
            weapons[0].gameObject.SetActive(false);
            weapons[1].gameObject.SetActive(false);
            weapons[3].gameObject.SetActive(false);
            weapon = weapons[2];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("무기4번");
            weapons[3].gameObject.SetActive(true);
            weapons[0].gameObject.SetActive(false);
            weapons[1].gameObject.SetActive(false);
            weapons[2].gameObject.SetActive(false);
            weapon = weapons[3];
        }
        weapon.transform.position = weaponSpawnPos.transform.position;
        weapon.transform.rotation = weaponSpawnPos.transform.rotation;
    }
    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackCoolDown;
            StartCoroutine(Swing());
            if (weapon != weapons[0])
            {
                Vector2 shootingDir = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
                shootingDir.Normalize();
                GameObject shootingObject = Instantiate(weapon);
                shootingObject.transform.position = weaponSpawnPos.transform.position;
                shootingObject.transform.right = shootingDir;
                shootingObject.GetComponent<Rigidbody2D>().velocity = shootingDir * weaponSpeed;
            }
        }
    }
    private IEnumerator Swing()
    {
        for (int i = 90; i >= -90; i--)
        {
            angle.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z+i);
            yield return new WaitForSeconds(0.002f);
        }
        angle.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
    }

    [SerializeField]
    private Inventory theInventory;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item.Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null)
            {
                Debug.Log(hitObject.transform.GetComponent<Consumable>().item.itemName + " 획득 했습니다.");
                theInventory.AcquireItem(hitObject.transform.GetComponent<Consumable>().item);
                collision.gameObject.SetActive(false);
            }
        }
    }
}
