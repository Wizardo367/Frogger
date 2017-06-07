using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Variables
	private Animator _animator;

	private void Start()
	{
		// Cache components
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		// Check for input
		if (Input.GetKeyDown(KeyCode.W))
		{
			_animator.Play("Jump Up");
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			_animator.Play("Jump Down");
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
			_animator.Play("Jump Left");
		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			_animator.Play("Jump Right");
		}
	}
}