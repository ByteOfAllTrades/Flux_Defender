﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    public float WalkSpeed;
    public float JumpForce;
    public AnimationClip _walk, _jump;
    public Animation _Legs;
    public Transform _Blade, _GroundCast;
    public Camera cam;
    public bool mirror;


    private bool _canJump, _canWalk;
    private bool _isWalk, _isJump;
    private float rot, _startScale;
    private Rigidbody2D rig;
    private Vector2 _inputAxis;
    private RaycastHit2D _hit;

	void Start ()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        _startScale = transform.localScale.x;
	}

    void Update()
    {
        if (_hit = Physics2D.Linecast(new Vector2(_GroundCast.position.x, _GroundCast.position.y + 0.2f), _GroundCast.position))
        {
            if (!_hit.transform.CompareTag("Player"))
            {
                _canJump = true;
                _canWalk = true;
            }
        }
        else _canJump = false;

        _inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (_inputAxis.y > 0 && _canJump)
        {
            _canWalk = false;
            _isJump = true;
        }

        //Consume Potions
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (PlayerPrefs.GetInt("potionTotal") != 0)
            {
                PlayerPrefs.SetInt("potionTotal", (PlayerPrefs.GetInt("potionTotal")-1));
                if (PlayerPrefs.GetFloat("currentHealth") > (PlayerPrefs.GetFloat("maxHealth") * 0.9f))
                {
                    PlayerPrefs.SetFloat("currentHealth", PlayerPrefs.GetFloat("maxHealth"));
                    Debug.Log("Already Maxed");
                }
                else
                {
                    PlayerPrefs.SetFloat("currentHealth", (PlayerPrefs.GetFloat("currentHealth") + (PlayerPrefs.GetFloat("maxHealth") * 0.1f)));
                    Debug.Log("Slurp");
                }
            }
            else
            {
                PlayerPrefs.SetString("popMsg", "No More Potions!");
                StartCoroutine("displayPopup");
            }
        }
        //Check for death
        if (PlayerPrefs.GetFloat("currentHealth") <= 0)
        {
            StartCoroutine("gameOver");
        }
    }

    void FixedUpdate()
    {
        Vector3 dir = cam.ScreenToWorldPoint(Input.mousePosition) - _Blade.transform.position;
        dir.Normalize();

        if (cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x + 0.2f)
            mirror = false;
        if (cam.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x - 0.2f)
            mirror = true;

        if (!mirror)
        {
            rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localScale = new Vector3(_startScale, _startScale, 1);
            _Blade.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }
        if (mirror)
        {
            rot = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
            transform.localScale = new Vector3(-_startScale, _startScale, 1);
            _Blade.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }

        if (_inputAxis.x != 0)
        {
            rig.velocity = new Vector2(_inputAxis.x * WalkSpeed * Time.deltaTime, rig.velocity.y);

            if (_canWalk)
            {
                _Legs.clip = _walk;
                _Legs.Play();
            }
        }

        else
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
        }

        if (_isJump)
        {
            rig.AddForce(new Vector2(0, JumpForce));
            _Legs.clip = _jump;
            _Legs.Play();
            _canJump = false;
            _isJump = false;
        }
    }

    public bool IsMirror()
    {
        return mirror;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, _GroundCast.position);
    }

    IEnumerator displayPopup()
    {
        yield return new WaitForSeconds(2);
        PlayerPrefs.SetString("popMsg", "");
    }
    IEnumerator gameOver()
    {
        PlayerPrefs.SetString("popMsg", "You Died");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("game_over");
    }
}
