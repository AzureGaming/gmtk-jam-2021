using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    public delegate void TriggerMonster();
    public static TriggerMonster OnTriggerMonster;

    public List<AudioSource> sounds = new List<AudioSource>();
    public AudioSource crunch;
    public GameObject blood;

    Transform player;
    SpriteRenderer spriteR;
    Collider2D collider2d;
    AudioSource chosenAudio;
    float speed = 0.005f;

    private void OnEnable() {
        OnTriggerMonster += StartRoutine;
    }

    private void OnDisable() {
        OnTriggerMonster -= StartRoutine;
    }

    private void Awake() {
        spriteR = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>().transform;
        collider2d = GetComponent<Collider2D>();
    }


    void Start() {
        Color invis = spriteR.color;
        invis.a = 0f;
        spriteR.color = invis;
        collider2d.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Instantiate(blood, transform.position, Quaternion.identity);
            crunch.Play();
            Destroy(collision.gameObject);
        }
        StopAllCoroutines();
        StartCoroutine(FadeOut());
        GameManager.OnPlayerDeath?.Invoke();
    }

    void StartRoutine() {
        StartCoroutine(AIRoutine());
    }

    IEnumerator AIRoutine() {
        for (; ; ) {
            Color invis = spriteR.color;
            invis.a = 0f;
            spriteR.color = invis;
            collider2d.enabled = false;

            TeleportNearPlayer();
            StartCoroutine(FadeIn());

            float chaseTime = Random.Range(3f, 8f);
            float timeElapsed = 0f;

            collider2d.enabled = true;

            // Play a random sound
            float audioChance = Random.Range(0f, 1f);
            if (audioChance >= 0.3f) {
                int index = Random.Range(0, sounds.Count);
                chosenAudio = sounds[index];
                Invoke("PlaySound", Random.Range(0f, chaseTime));
            }

            // Chase for defined time
            while (timeElapsed < chaseTime) {
                ChasePlayer();
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            collider2d.enabled = false;
            StartCoroutine(FadeOut());

            float delay = Random.Range(5f, 8f);
            yield return new WaitForSeconds(delay);
        }
    }

    void PlaySound() {
        chosenAudio.Play();
    }

    Vector2 GetTeleportPos() {
        return Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
    }

    void TeleportNearPlayer() {
        Vector2 randomScreenPos = GetTeleportPos();
        while(Vector2.Distance(player.position, randomScreenPos) < 3f) {
            randomScreenPos = GetTeleportPos();
        }

        transform.position = randomScreenPos;
    }

    void ChasePlayer() {
        Vector2 playerPos = player.position;
        Vector2 nextPos = Vector2.MoveTowards(transform.position, playerPos, speed);
        Vector2 direction = ( nextPos - (Vector2)transform.position ).normalized;

        if (direction.x < 0) {
            spriteR.flipX = true;
        } else {
            spriteR.flipX = false;
        }
        transform.position = nextPos;
    }

    IEnumerator FadeIn() {
        float animationDuration = 1.5f;
        float timeElapsed = 0f;
        Color start = spriteR.color;

        while (timeElapsed < animationDuration) {
            Color newColor = spriteR.color;
            newColor.a = Mathf.Lerp(start.a, 1f, ( timeElapsed / animationDuration ));

            spriteR.color = newColor;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut() {
        float animationDuration = 1f;
        float timeElapsed = 0f;
        Color start = spriteR.color;

        while (timeElapsed < animationDuration) {
            Color newColor = spriteR.color;
            newColor.a = Mathf.Lerp(start.a, 0f, ( timeElapsed / animationDuration ));

            spriteR.color = newColor;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
