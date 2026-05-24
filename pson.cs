using System;

//c# 9.0
public class pson
{
    public pson(int value)
    {
        _value = value;
    }

    public static int Zero => 0;

    public static int CurrentMax { get; set; } = 3;

    public static int nums => CurrentMax + 1;

    private int _value { get; set; }

    public int Value
    {
        get
        {
            var e = Math.Abs(_value);
            if (e > CurrentMax)
            {
                e %= nums;
            }
            return e;
        }

        set
        {
            _value = value;
        }
    }


    public static implicit operator int(pson value) => value.Value;
    public static implicit operator pson(int value) => new pson(value);


    public static pson operator +(pson add1, pson add2) => Add(add1, add2);
    public static pson operator +(int add1, pson add2) => Add(add1, add2);
    public static pson operator +(pson add1, int add2) => Add(add1, add2);

    public static pson operator -(pson add1, pson add2) => Add(add1, add2);
    public static pson operator -(int add1, pson add2) => Add(add1, add2);
    public static pson operator -(pson add1, int add2) => Add(add1, add2);

    public static bool operator >(pson left, pson right)
    {
        if (left.Value > right.Value) return true;

        return false;
    }

    public static bool operator <(pson left, pson right)
    {
        if (left.Value < right.Value) return true;

        return false;
    }

    public static bool operator >=(pson left, pson right)
    {
        if (left.Value >= right.Value) return true;

        return false;
    }

    public static bool operator <=(pson left, pson right)
    {
        if (left.Value <= right.Value) return true;

        return false;
    }

    public static bool operator ==(pson left, pson right)
    {
        if (left.Value == right.Value) return true;

        return false;
    }

    public static bool operator !=(pson left, pson right)
    {
        if (left.Value != right.Value) return true;

        return false;
    }

    public override string ToString() => Value.ToString(); 

    static pson Add(pson add1, pson add2)
    { 
        var result = Math.Abs( add1.Value + add2.Value);

        if(result > CurrentMax)
            result %= nums;

        return new pson(result);
    }

    static pson Add(int add1, pson add2)
    {
        var result = Math.Abs(add1 + add2.Value);

        if (result > CurrentMax)
            result %= nums;

        return new pson(result);
    }

    static pson Add(pson add1, int add2)
    {
        var result = Math.Abs(add1.Value + add2);

        if (result > CurrentMax)
            result %= nums;

        return new pson(result);
    }

    public override bool Equals(object obj)
    {
        return obj is pson pson &&
               Value == pson.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }
}