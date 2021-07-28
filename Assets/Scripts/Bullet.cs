using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject target;
    private Vector3 dir;
    public float flySpeed;
    void Start() {
        dir = target.transform.position - transform.position;
        transform.up = new Vector3(dir.x, dir.y, dir.z);
        StartCoroutine("hitObject");
    }
    void Update() {
        transform.position += dir.normalized * flySpeed * Time.deltaTime;
    }

    IEnumerator hitObject() {
        yield return new WaitForSecondsRealtime(Vector3.Distance(target.transform.position, transform.position) / flySpeed);
        if (target != null) {
            GameManager.instance.GetComponent<AchievementManager>().toSave.setEnemiesKilled(GameManager.instance.GetComponent<AchievementManager>().toSave.getEnemiesKilled()+1);
            Destroy(target);
        }
        Destroy(gameObject);
    }
}
