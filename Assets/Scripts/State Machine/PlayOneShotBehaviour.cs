using UnityEngine;

public class PlayOneShotBehaviour : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public float volume = 1f;
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;

    public float playDelay = 0.40f;
    private float timeSinceEntered = 0;
    private bool hasDelayedSoundPlayed = false;
    private Transform audioParent;

    void Awake()
    {
        GameObject audioManager = GameObject.Find("AudioManager");
        if (audioManager == null)
        {
            audioManager = new GameObject("AudioManager");
        }
        audioParent = audioManager.transform;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
        {
            PlaySound(animator.gameObject.transform.position);
        }

        timeSinceEntered = 0f;
        hasDelayedSoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;

            if (timeSinceEntered > playDelay)
            {
                PlaySound(animator.gameObject.transform.position);
                hasDelayedSoundPlayed = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            PlaySound(animator.gameObject.transform.position);
        }
    }

    private void PlaySound(Vector3 position)
    {
        GameObject audioObject = new GameObject("OneShotAudio");
        audioObject.transform.position = position;

        audioObject.transform.parent = audioParent;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = soundToPlay;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(audioObject, soundToPlay.length);
    }
}
