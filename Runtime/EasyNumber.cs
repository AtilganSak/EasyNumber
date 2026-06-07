using System;
using System.Collections.Generic;
using UnityEngine;

public class EasyNumberComparer : IComparer<EasyNumber>
{
    public int Compare(EasyNumber x, EasyNumber y)
    {
        return x.Value.CompareTo(y.Value);
    }
}

[System.Serializable]
public struct EasyNumber
{
    [SerializeField] double[] steps;
    [SerializeField, HideInInspector] int _decimals;

    public int Decimals
    {
        get => _decimals;
        set => _decimals = Math.Max(0, value);
    }

    bool isCombined;

    [SerializeField, HideInInspector] double _value;

    public double Value
    {
        get
        {
            if (!isCombined)
                Combine();
            return _value;
        }
        set
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                return;
            _value = value;
            isCombined = true;
        }
    }

    public static EasyNumber Zero => Create(0);

    public override string ToString() => Necessary.Convert(Value, _decimals);
    public string ToString(int decimals) => Necessary.Convert(Value, decimals);

    public double Percent(EasyNumber max) => max.Value > 0 ? Value / max.Value * 100.0 : 0;
    public double Percent(double max) => max > 0 ? Value / max * 100.0 : 0;

    void Combine()
    {
        isCombined = true;
        if (steps != null && steps.Length > 0)
        {
            _value = 0;
            for (int i = 0; i < steps.Length; i++)
                _value += Math.Max(0, steps[i]) * Math.Pow(1000, i);
            steps = null;
        }
    }

    public void Clear()
    {
        isCombined = false;
        steps = null;
        _value = 0;
    }

    #region Static Helpers

    public static EasyNumber Clamp(EasyNumber value, EasyNumber min, EasyNumber max)
    {
        if (value.Value < min.Value)
        {
            var r = min;
            r.Decimals = value.Decimals;
            return r;
        }

        if (value.Value > max.Value)
        {
            var r = max;
            r.Decimals = value.Decimals;
            return r;
        }

        return value;
    }

    public static EasyNumber Lerp(EasyNumber a, EasyNumber b, double t)
    {
        t = Math.Max(0, Math.Min(1, t));
        return Create(a.Value + (b.Value - a.Value) * t, a.Decimals);
    }

    #endregion

    #region Factory

    public static EasyNumber Create(double value, int decimals = 1)
    {
        var n = new EasyNumber();
        n.Value = value;
        n.Decimals = decimals;
        return n;
    }

    #endregion

    #region Implicit Casts

    public static implicit operator EasyNumber(double value) => Create(value);
    public static implicit operator EasyNumber(float value) => Create(value);
    public static implicit operator EasyNumber(int value) => Create(value);
    public static implicit operator double(EasyNumber n) => n.Value;

    #endregion

    #region Operators

    public static EasyNumber operator -(EasyNumber a)
    {
        a.Value = -a.Value;
        return a;
    }

    public static EasyNumber operator +(EasyNumber a, double b)
    {
        a.Value += b;
        return a;
    }

    public static EasyNumber operator -(EasyNumber a, double b)
    {
        a.Value -= b;
        return a;
    }

    public static EasyNumber operator *(EasyNumber a, double b)
    {
        a.Value *= b;
        return a;
    }

    public static EasyNumber operator /(EasyNumber a, double b)
    {
        a.Value /= b;
        return a;
    }

    public static EasyNumber operator +(EasyNumber a, EasyNumber b)
    {
        a.Value += b.Value;
        return a;
    }

    public static EasyNumber operator -(EasyNumber a, EasyNumber b)
    {
        a.Value -= b.Value;
        return a;
    }

    public static EasyNumber operator *(EasyNumber a, EasyNumber b)
    {
        a.Value *= b.Value;
        return a;
    }

    public static EasyNumber operator /(EasyNumber a, EasyNumber b)
    {
        a.Value /= b.Value;
        return a;
    }

    public static bool operator ==(EasyNumber a, double b) => a.Value == b;
    public static bool operator !=(EasyNumber a, double b) => a.Value != b;
    public static bool operator >(EasyNumber a, double b) => a.Value > b;
    public static bool operator <(EasyNumber a, double b) => a.Value < b;
    public static bool operator >=(EasyNumber a, double b) => a.Value >= b;
    public static bool operator <=(EasyNumber a, double b) => a.Value <= b;

    public static bool operator ==(EasyNumber a, EasyNumber b) => a.Value == b.Value;
    public static bool operator !=(EasyNumber a, EasyNumber b) => a.Value != b.Value;
    public static bool operator >(EasyNumber a, EasyNumber b) => a.Value > b.Value;
    public static bool operator <(EasyNumber a, EasyNumber b) => a.Value < b.Value;
    public static bool operator >=(EasyNumber a, EasyNumber b) => a.Value >= b.Value;
    public static bool operator <=(EasyNumber a, EasyNumber b) => a.Value <= b.Value;

    public override bool Equals(object obj) => obj is EasyNumber other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();

    #endregion
}