using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaPlant : EnemyBehaviour
{

    protected BoxCollider2D boxCollider;
    public float waitTime;
    private float timeToMove;
    private Vector2 dest;
    private Vector2 origin;
    public LayerMask toHit;

    // Start is called before the first frame update
    protected void Awake()
    {
        base.Awake();
        attackDistance = 2f;
        m_FacingRight = true;
        curHealth = maxHealth;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
        boxCollider = GetComponent<BoxCollider2D>();
        origin = transform.position;
        dest = new Vector2(transform.position.x, transform.position.y + 2f);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (curHealth <= 0) {
            m_dead = true;
            boxCollider.isTrigger = true;
            FadeOut(0, 25f);
            Destroy(gameObject, 1f);
        } else {
            if (Time.time > timeToMove) {
                if (Vector2.Distance(transform.position, dest) <= 0.01) {
                    Vector2 temp = origin;
                    origin = dest;
                    dest = temp;
                    timeToMove = Time.time + waitTime;
                }
                transform.position = Vector2.MoveTowards(transform.position, dest, maxSpeed * Time.deltaTime);
            }
            float range = Vector2.Distance(transform.position, Player.position);
            if (range <= attackDistance) {
                Vector2 playerPosition = new Vector2(Player.position.x, Player.position.y);
                Vector2 curPosition = new Vector2(transform.position.x, transform.position.y);
                RaycastHit2D hitInfo = Physics2D.Raycast(curPosition, playerPosition - curPosition, attackDistance, toHit);
                if (hitInfo) {
                    if (hitInfo.transform.gameObject.tag == "Player") {
                        if (Time.time > timeToFire)
                        {
                            timeToFire = Time.time + 1 / attackRate;
                            float[] array = {attackPower, 0 };
                            Player.SendMessage("Damage", array);
                        }
                    }
                }
            }
            flipCheck();
        }
    }
}
