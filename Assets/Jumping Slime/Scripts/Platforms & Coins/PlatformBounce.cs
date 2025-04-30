using UnityEngine;

public class PlatformBounce : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    private Vector2 _playerVelocity;

    [SerializeField] private int _indexInSoundManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.relativeVelocity.y <= 0)
        {
            SoundManager.Instance.PlayObjectSound(_indexInSoundManager);

            ParticleSystem particleSystem = collision.gameObject.GetComponentInChildren<ParticleSystem>();
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particleSystem.Play();

            _playerVelocity = collision.rigidbody.velocity;
            _playerVelocity.y = _jumpForce;
            collision.rigidbody.velocity = _playerVelocity;
        }
    }
}
