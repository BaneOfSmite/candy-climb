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
        transform.position += dir.normalized * flySpeed * Time.deltaTime; //Move the bullet to the enemy
    }

    IEnumerator hitObject() {
        yield return new WaitForSeconds(Vector3.Distance(target.transform.position, transform.position) / flySpeed); //Wait for the bullet to fly to the enemy
        if (target != null) {
            GameManager.instance.GetComponent<AchievementManager>().toSave.setEnemiesKilled(GameManager.instance.GetComponent<AchievementManager>().toSave.getEnemiesKilled()+1); //Increase achievement count of enemies killed.
            Destroy(target); //Delete enemy if it hasn't been deleted yet
        }
        Destroy(gameObject); //Destroy bullet
    }
}
