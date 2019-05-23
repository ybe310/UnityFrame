using System.Collections;
using System.Collections.Generic;

public interface ISystem
{
	void Init();

	void Update();

	void LateUpdate();

	void FixedUpdate();

	void AddListener();

	void RemoveListener();
}
