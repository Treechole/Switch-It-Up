using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodGenerator : MonoBehaviour {
    private ParticleSystem bloodGenerator;
    private bool bloodSpurted = false;

    private void Awake() {
        bloodGenerator = GetComponent<ParticleSystem>();
    }

    private void Update() {
        if (bloodGenerator.isPlaying) {
            bloodSpurted = true;
        } else {   
            if (bloodSpurted) {
                Destroy(gameObject);
            }
        }
    }

    public void BloodSpurt(Transform player) {
        transform.parent = null;
        bloodGenerator.Play();

        Transform newbloodGenerator = Instantiate(gameObject, player).transform;
        newbloodGenerator.name = "Blood Particle System";
        newbloodGenerator.localPosition = Vector3.zero;
    }
}