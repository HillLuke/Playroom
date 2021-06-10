using Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
public class CameraRig : MonoBehaviour
{
    public Transform Follow;
    public Transform LookAt;
    public CinemachineFreeLook FreeLookMovement;

    void Start()
    {
        FollowAndLookCheck();
        UpdateSettings();
    }

    public void UpdateSettings()
    {
        FreeLookMovement.Follow = Follow;
        FreeLookMovement.LookAt = LookAt;
    }

    /// <summary>
    /// Auto follow and look at the player if not set
    /// </summary>
    private void FollowAndLookCheck()
    {
        if (Follow == null && LookAt == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementBase>();

            if (player != null)
            {
                Follow = player.gameObject.transform;
                LookAt = player.LookAt.transform;
            }
        }
    }
}
