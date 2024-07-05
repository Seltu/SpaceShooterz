using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameVariableContainer
{
    [SerializeReference] public List<GameVariable> list;
}

[Serializable]
public abstract class GameVariable
{
#if UNITY_EDITOR
    [Multiline]
    public string developerDescription = "";
#endif
    public virtual void SetValue(object value)
    {
    }

    public virtual void SetValue(GameVariable value)
    {
    }
}

public abstract class GameVariable<T> : GameVariable
{
    public T value;

    public override void SetValue(object value)
    {
        this.value = (T)value;
    }

    public override void SetValue(GameVariable value)
    {
        this.value = ((GameVariable<T>)value).value;
    }
}

[Serializable]
public class IntVariable : GameVariable<int> { }
[Serializable]
public class FloatVariable : GameVariable<float> { }
[Serializable]
public class ShipVariable : GameVariable<ShipStats> { }
[Serializable]
public class PlayerVariable : GameVariable<PlayerStats> { }
[Serializable]
public class RoundVariable : GameVariable<Round> { }