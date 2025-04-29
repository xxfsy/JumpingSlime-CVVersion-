using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownPlatform : MonoBehaviour
{
    private bool _isFalling;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(_isFalling)
        {
            transform.position -= new Vector3(0, 0.05f, 0);
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a - 0.1f);  
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.attachedRigidbody.velocity.y < 0)
        {
            SoundManager.Instance.PlayBrownPlatformSound();
            _isFalling = true;
        }
    }
}
