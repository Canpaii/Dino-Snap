using UnityEngine;

public class PosePoint : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Photographable _photographable;

    void Awake()
    {
        _photographable = GetComponent<Photographable>();
    }

    void LateUpdate()
    {
        // Late update ot make sure you arent getting the previous frames curve data. 

        float pose = Mathf.Clamp01(animator.GetFloat("Pose"));

        _photographable.posePoints = Mathf.RoundToInt(pose * 100);
    }
}