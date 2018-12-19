using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footfall : MonoBehaviour {
    [SerializeField] GameObject m_footprint;
    [SerializeField] AudioClip m_audioClip;

    private enum Direction { right, left };
    private AudioSource m_audioSource;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void MakeFootprint(int scale)
    {

        Direction footDirection;

        if (scale > 0)
        {
            footDirection = Direction.left;
        }
        else
        {
            footDirection = Direction.right;
        }

        PlaySound(footDirection);
    }

    private void PlaySound(Direction footDirection)                                 //only want to play sound so rest of footprint isnt needed
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
