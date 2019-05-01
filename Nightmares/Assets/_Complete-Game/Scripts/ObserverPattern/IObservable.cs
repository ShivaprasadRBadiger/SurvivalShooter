using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable<T>
{
	void Add(IObserver<T> observer);
	void Remove(IObserver<T> observer);

	void Notify();
}
