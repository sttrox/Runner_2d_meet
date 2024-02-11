using System.Collections;
using UnityEngine;

namespace Core
{
	public interface ICoroutineRunner
	{
		Coroutine RunCoroutine(IEnumerator enumerator);
	}
}