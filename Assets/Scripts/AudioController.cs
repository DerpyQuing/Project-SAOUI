using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    public AudioSource audioSource;

    public AudioClip hoverSound;
    public AudioClip pressSound;
    public AudioClip menuCreationSound;
    public AudioClip menuDismissalSound;




	// Use this for initialization
	void Start () {
	
	}

    // But do we need a hover sound. I think it's annoying

    public void playHoverSound() {
        audioSource.PlayOneShot(hoverSound);
    }

    public void playPressSound() {
        audioSource.PlayOneShot(pressSound);
    }

    public void playMenuCreationSound() {
        audioSource.PlayOneShot(menuCreationSound);
    }

    public void playMenuDismissalSound() {
        audioSource.PlayOneShot(menuDismissalSound);
    }

}
