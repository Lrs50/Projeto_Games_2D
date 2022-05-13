using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sun : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public Sprite sunBreak1;
    public Sprite sunBreak2;
    public Sprite sunBreak3;
    public PlayerStateManager player;
    private float shakeMagnitude = 0.7f;
    
    Vector3 initialPosition;

    public float health = 100f;

    private bool broken = false;

    public Image overlay;

    void Awake() {

        initialPosition = transform.localPosition;
    }

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update() {
        if (!broken) {
            if (health <= 0){
                broken = true;
                StartCoroutine(BreakSun());
            }
            else if (health <= 25){
                spriteRenderer.sprite = sunBreak3;
            }
            else if (health <= 50){
                spriteRenderer.sprite = sunBreak2;
            }
            else if (health <= 75) {
                spriteRenderer.sprite = sunBreak1;
            }
        }
    }

    void FixedUpdate() {
        if (broken) {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player Attack"){
            health -= 10f;
            StartCoroutine(DamageAnimation());
            Destroy(other.gameObject);
        }
    }

    IEnumerator DamageAnimation(){    
        spriteRenderer.color=new Vector4(255/255f, 0/255f, 0/255f,0.7f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color=Color.white;
    }

    IEnumerator BreakSun(){
        yield return new WaitForSeconds(2f);

        for (var i = 0; i <= 100; i++){
            overlay.color = new Vector4(255/255f, 255/255f, 255/255f, i/100);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.5f);

        for (var i = 255; i >= 0; i-=10){
            overlay.color = new Vector4(i/255f, i/255f, i/255f, 1f);
            yield return new WaitForSeconds(0.1f);
        }
        player.nextStage = true;
    }

}
