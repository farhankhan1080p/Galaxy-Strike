using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xClamp = 15f;
    [SerializeField] float yClamp = 7f;
    [SerializeField] float topPadding;
    [SerializeField] float bottomPadding;
    [SerializeField] float leftPadding;
    [SerializeField] float rightPadding;

    [SerializeField] float rollSpeed = 10;
    [SerializeField] float controllRollFactor = 15;

    [SerializeField] float pitchSpeed = 10;
    [SerializeField] float controllPitchFactor = 15;
    Vector3 rollRotation;
    Vector3 pitchRotation;
    static PlayerMovement instance;

    [SerializeField] PlayableDirector masterTimelines;
    [SerializeField] float ripoffLastSec = 7.8f; // Strict Dafault Value 7.8
    float exactEndPointSec;
    void Start()
    {
        rollRotation = new Vector3(0, 0, controllRollFactor);
        pitchRotation = new Vector3(controllPitchFactor, 0, 0);
        exactEndPointSec = (float) masterTimelines.duration - ripoffLastSec;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        if(exactEndPointSec < masterTimelines.time) // when player reach near to end/perticular timeline added in "exactEndPointSec", then controll disabled and Ship Positioned at Vecotr.zero
        {
            resetShipPositionAtZero();
        } else
        {
            PlayerControl();
            RollShip();
            PitchShip();
        }
    }

    private void resetShipPositionAtZero()
    {
        transform.localPosition = Vector3.Slerp(transform.localPosition, Vector3.zero, Time.deltaTime);
    }
    private void PlayerControl()
    { 
        float offset = controlSpeed * Time.deltaTime;
        Vector3 movement = new Vector3(offset * moveInput.x, offset * moveInput.y, 0);
        Vector3 newPos = transform.localPosition + movement;
        newPos.x = Mathf.Clamp(newPos.x, -xClamp + leftPadding, xClamp - rightPadding);
        newPos.y = Mathf.Clamp(newPos.y, -yClamp + bottomPadding, yClamp - topPadding);
        transform.localPosition = newPos;
    }
    
    private void RollShip()
    {
        /* if(moveInput.x > 0)
        {
            Quaternion targetRotation = Quaternion.Euler(-rollRotation);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rollSpeed * Time.deltaTime);
        } else if (moveInput.x < 0)
        {
            Quaternion targetRotation = Quaternion.Euler(rollRotation);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rollSpeed * Time.deltaTime);
        } */                    
                                // OR
        Quaternion targetRotation = Quaternion.Euler(-rollRotation * moveInput.x);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rollSpeed * Time.deltaTime);
    }

    private void PitchShip()
    {
        /* if(moveInput.y > 0)
        {
            Quaternion targetRotation = Quaternion.Euler(-pitchRotation);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, pitchSpeed * Time.deltaTime);
        } else if (moveInput.y < 0)
        {
            Quaternion targetRotation = Quaternion.Euler(pitchRotation);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, pitchSpeed * Time.deltaTime);
        } */
                        // OR
        Quaternion targetRotation = Quaternion.Euler(-pitchRotation * moveInput.y);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, pitchSpeed * Time.deltaTime);
    }
}