using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController m_character;
    public float m_sensitivity = 15f;
    public float m_moveSpeed = 10f;
    public bool m_pause = false;
    // Start is called before the first frame update
    void Start()
    {
        m_character = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_pause)
        {
            float mouseX = Input.GetAxis("Mouse X") * m_sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * m_sensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up * mouseX);
            Camera.main.transform.Rotate(Vector3.right * -mouseY);

            float forward = Input.GetAxis("Vertical") * m_moveSpeed * Time.deltaTime;
            float sideways = Input.GetAxis("Horizontal") * m_moveSpeed * Time.deltaTime;


            m_character.Move(transform.forward * forward  + transform.right * sideways);

            if(Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.RightControl)) 
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void SetPause(bool pause)
    {
        m_pause = pause;
        if(pause) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
        }
    }
}
