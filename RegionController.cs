using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionController : MonoBehaviour {
    private class FishRarityComparer : IComparer<GameObject> {
        public int Compare(GameObject x, GameObject y) {
            Fish a = x.GetComponent<Fish>();
            Fish b = y.GetComponent<Fish>();

            return a.rarity.CompareTo(b.rarity);
        }
    }

    public GameObject[] fish;

    public void Start() {
        // idk if we want to sort or not
        System.Array.Sort(fish, new FishRarityComparer());
	}

    public GameObject getFish() {
        int index = (int)Mathf.Ceil(Random.Range(0, 1000)) % fish.Length;
        return fish[index];
	} 
}
