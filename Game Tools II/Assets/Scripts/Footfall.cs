using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footfall : MonoBehaviour {
    [SerializeField] GameObject m_footprint;
    [SerializeField] AudioClip m_audioClip;

    private enum Direction { right, left };

    private Animator m_animator;
    private Transform m_rightFootTransform;
    private Transform m_leftFootTransform;

    private AudioSource m_audioSource;



    private void Start()
    {
        if (GetComponent<Animator>())
        {
            m_animator = GetComponent<Animator>();

            m_rightFootTransform = m_animator.GetBoneTransform(HumanBodyBones.RightFoot);
            m_leftFootTransform = m_animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        }
        m_audioSource = GetComponent<AudioSource>();
    }

    public void MakeFootprint(int scale)
    {
        Debug.Log("Footprint");

        Direction footDirection;

        if (scale > 0) // left foot
        {
            footDirection = Direction.left;
            //Instantiate(m_footprint, m_leftFootTransform.position, m_leftFootTransform.rotation);
        }
        else // right foot
        {
            footDirection = Direction.right;
            //Instantiate(m_footprint, m_rightFootTransform.position, m_rightFootTransform.rotation);
        }

        PlaySound(footDirection);
    }

    private void PlaySound(Direction footDirection)
    {

        if (m_audioSource != null)
        {
            if (footDirection == Direction.left)
            {

                m_audioSource.panStereo = -0.05f;
                m_audioSource.pitch = Random.Range(0.5f, 1.0f);
            }

            if (footDirection == Direction.right)
            {

                m_audioSource.panStereo = +0.05f;
                m_audioSource.pitch = Random.Range(1.5f, 2.0f);
            }

            m_audioSource.PlayOneShot(m_audioClip);
        }
    }


}
