using System;

//c# 9.0
[System.Serializable]
public class LoopNum
{
    public LoopNum(int value)
    {
        _value = value;
    }

    public static int Zero => 0;

    private static int _max { get; set; } = 3;

    public static int CurrentMax
    {
        get 
        {
            if (_max < 3) _max = 3;

            return _max;
        }

        set
        {
            _max = value;
        }
    }

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


    public static implicit operator int(LoopNum value) => value.Value;
    public static implicit operator LoopNum(int value) => new LoopNum(value);


    public static LoopNum operator +(LoopNum add1, LoopNum add2) => Add(add1, add2);
    public static LoopNum operator +(int add1, LoopNum add2) => Add(add1, add2);
    public static LoopNum operator +(LoopNum add1, int add2) => Add(add1, add2);

    public static LoopNum operator -(LoopNum add1, LoopNum add2) => Add(add1, add2);
    public static LoopNum operator -(int add1, LoopNum add2) => Add(add1, add2);
    public static LoopNum operator -(LoopNum add1, int add2) => Add(add1, add2);

    public static bool operator >(LoopNum left, LoopNum right)
    {
        if (left?.Value > right?.Value) return true;

        return false;
    }

    public static bool operator <(LoopNum left, LoopNum right)
    {
        if (left?.Value < right?.Value) return true;

        return false;
    }

    public static bool operator >=(LoopNum left, LoopNum right)
    {
        if (left?.Value >= right?.Value) return true;

        return false;
    }

    public static bool operator <=(LoopNum left, LoopNum right)
    {
        if (left?.Value <= right?.Value) return true;

        return false;
    }

    public static bool operator ==(LoopNum left, LoopNum right)
    {
        if (left is null && right is null) return true;

        if (left?.Value == right?.Value) return true;

        return false;
    }

    public static bool operator !=(LoopNum left, LoopNum right)
    {
        if (left is null && right is null) return false;

        if (left?.Value != right?.Value) return true;

        return false;
    }

    public override string ToString() => Value.ToString(); 

    static LoopNum Add(LoopNum add1, LoopNum add2)
    { 
        var result = Math.Abs( add1.Value + add2.Value);

        if(result > CurrentMax)
            result %= nums;

        return new LoopNum(result);
    }

    static LoopNum Add(int add1, LoopNum add2)
    {
        var result = add1 + add2.Value;

        if (result > CurrentMax)
            result %= nums;
        else if (result < 0)
            result = Math.Abs(result) + 1;

        return new LoopNum(result);
    }

    static LoopNum Add(LoopNum add1, int add2)
    {
        var result = add1.Value + add2;

        if (result > CurrentMax)
            result %= nums;
        else if (result < 0)
            result = Math.Abs(result) + 1;

        return new LoopNum(result);
    }

    public override bool Equals(object obj)
    {
        return obj is LoopNum pson &&
               Value == pson.Value
               && pson is not null;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }
}