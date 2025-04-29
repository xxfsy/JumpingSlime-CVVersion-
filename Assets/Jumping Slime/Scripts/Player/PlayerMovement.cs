using System.Collections;
using UnityEngine;
using YG;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedOfMovementXAxis = 5f;
    [SerializeField] private float _maxNegativeYVelocity; 

    [SerializeField] private Joystick _joystickForMobile; 
    public bool isPC; 

    private Rigidbody2D rb;
    private float _inputMovement;

    private Vector3 _lowerLeftCorner;
    private Vector3 _playerLocalScale; 

    internal bool IsDead = false;

    private IEnumerator Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        _lowerLeftCorner = _lowerLeftCorner = Camera.main.ScreenToWorldPoint(Vector3.zero);
        _playerLocalScale = transform.localScale;

        while (!YandexGame.SDKEnabled) 
        {
            yield return new WaitForSeconds(0.2f);
        }

        if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
        {
            isPC = false;
            _joystickForMobile.gameObject.SetActive(true);
        }
        else
        {
            isPC = true;
        }
    }

    private void Update()
    {
        if (isPC) 
            _inputMovement = Input.GetAxis("Horizontal");
        else
            _inputMovement = _joystickForMobile.Horizontal;

        if (_inputMovement > 0) 
        {
            transform.localScale = new Vector3(_playerLocalScale.x, _playerLocalScale.y);
        }
        else if (_inputMovement < 0)
        {
            transform.localScale = new Vector3(-_playerLocalScale.x, _playerLocalScale.y);
        }
    }

    private void FixedUpdate()
    {
        XMovement();
        NegativeYVelocityBlock();
        CheckingScreenEdge();
    }

    private void NegativeYVelocityBlock()
    {
        if (rb.velocity.y < -6) 
        {
            rb.velocity = new Vector2(rb.velocity.x, _maxNegativeYVelocity);
        }
    }

    private void XMovement()
    {
        if (!IsDead)
        {
            Vector2 velocity = rb.velocity;
            velocity.x = _inputMovement * _speedOfMovementXAxis;
            rb.velocity = velocity;
        }
    }

    private void CheckingScreenEdge() 
    {
        if(transform.position.x > -_lowerLeftCorner.x) 
        {
            transform.position = new Vector3(_lowerLeftCorner.x, transform.position.y); 
        }
        else if (transform.position.x < _lowerLeftCorner.x) 
        {
            transform.position = new Vector3(-_lowerLeftCorner.x, transform.position.y); 
        }
    }

}
