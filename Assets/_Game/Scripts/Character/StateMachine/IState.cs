using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T>
{
    void OnEnter(T enemy);
    void OnExcute(T enemy);
    void OnExit(T enemy);
}
