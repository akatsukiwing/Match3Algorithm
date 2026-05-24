using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class M3System 
{
    private static int _rowNums { get; set; } = 3;

    private static int _colNums { get; set; } = 3;

    public static int RowNums
    {
        get 
        {
            if(_rowNums < 3) _rowNums = 3;
            return _rowNums;
        }
        set
        {
            _rowNums = value;
        }
    }


    public static int ColNums
    {
        get
        {
            if (_colNums < 3) _colNums = 3;
            return _colNums;
        }
        set
        {
            _colNums = value;
        }
    } 

    public static int AllNums => RowNums * ColNums;

    public static List<pson> Stage { get; set; } = new();

    public static void init(int row, int col)
    {
        
        for (var i = 0; i < AllNums; i++)
        {
            var r = new System.Random();
            Stage.Add(r.Next(pson.Zero, pson.nums));
        }

        CheckAllElemtent(row,col);


        var s = MakeASolution();

        //if (s)
        //    Debug.Log("lol");
        //else
        //    Debug.Log("wow");
    }

    private static bool MakeASolution()
    {
        var primitive = IdentifyPrimitive();


        var b1 = Case1Solution(primitive);

        if (b1)
        {
            Debug.Log("a solution");
            return true;
        }
        
        b1 = Case2Solution(primitive);

        if (b1) return true;
        
        b1 = Case3Solution();

        if (b1) return true;
        
        return false;
    }

    private static bool Case3Solution()
    {
        var b = AllOrphan();

        return b;
    }

    private static bool AllOrphan()
    {
        for (var i = 0; i < AllNums; i++)
        {
            var r = i / ColNums;
            var c = i % ColNums;

            var b = CheckUp4(r, c);

            if(b) return true;

            b = CheckLeft4(r, c);

            if (b) return true;

            b = CheckRight4(r, c);

            if (b) return true;

            b = CheckDown4(r, c);

            if (b) return true;
        }

        return false;
    }

    private static bool CheckDown4(int r, int c)
    {
        if (r + 2 > RowNums - 1) return false;

        var index = r * ColNums + c;

        var aimindex = (r + 2) * ColNums + c;

        if (Stage[index] == Stage[aimindex])
        {
            if (c - 1 >= 0)
            {
                var aim2index = (r + 1) * ColNums + c - 1;

                if (Stage[index] == Stage[aim2index]) return true;
                else
                {
                    Stage[aim2index].Value = Stage[index].Value;
                    return true;
                }
            }

            if (c + 1 <= ColNums - 1)
            {
                var aim2index = (r + 1) * ColNums + c + 1;

                if (Stage[index] == Stage[aim2index]) return true;
                else
                {
                    Stage[aim2index].Value = Stage[index].Value;
                    return true;
                }
            }
        }

        return false;
    }

    private static bool CheckRight4(int r, int c)
    {
        if (c + 2 > ColNums - 1) return false;

        var index = r * ColNums + c;

        var aimindex = r * ColNums + c + 2;

        if (Stage[index] == Stage[aimindex])
        {
            if (r - 1 >= 0)
            {
                var aim2index = (r - 1) * ColNums + c + 1;

                if (Stage[index] == Stage[aim2index]) return true;
                else
                {
                    Stage[aim2index].Value = Stage[index].Value;
                    return true;
                }
            }

            if (r + 1 <= RowNums - 1)
            {
                var aim2index = (r + 1) * ColNums + c + 1;

                if (Stage[index] == Stage[aim2index]) return true;
                else
                {
                    Stage[aim2index].Value = Stage[index].Value;
                    return true;
                }
            }
        }

        return false;
    }

    private static bool CheckLeft4(int r, int c)
    {
        if (c - 2 < 0) return false;

        var index = r * ColNums + c;

        var aimindex = r * ColNums + c - 2;

        if (Stage[index] == Stage[aimindex])
        {
            if (r - 1 >= 0)
            {
                var aim2index = (r - 1) * ColNums + c - 1;

                if (Stage[index] == Stage[aim2index]) return true;
                else
                {
                    Stage[aim2index].Value = Stage[index].Value;
                    return true;
                }
            }

            if (r + 1 <= RowNums - 1 )
            {
                var aim2index = (r + 1) * ColNums + c - 1;

                if (Stage[index] == Stage[aim2index]) return true;
                else
                {
                    Stage[aim2index].Value = Stage[index].Value;
                    return true;
                }
            }
        }

        return false;
    }

    private static bool CheckUp4(int r, int c)
    {
        if (r - 2 < 0) return false;
        
        var index = r * ColNums + c;

        var aimindex = (r - 2) * ColNums + c;

        if (Stage[index] == Stage[aimindex])
        {
            if (c - 1 >= 0)
            {
                var aim2index = (r - 1) * ColNums + c - 1;

                if (Stage[index] == Stage[aim2index]) return true;
                else
                {
                    Stage[aim2index].Value = Stage[index].Value;
                    return true;
                }
            }

            if(c + 1 <= ColNums - 1)
            {
                var aim2index = (r - 1) * ColNums + c + 1;

                if (Stage[index] == Stage[aim2index]) return true;
                else
                {
                    Stage[aim2index].Value = Stage[index].Value;
                    return true;
                }
            }
        }

        return false;
    }

    private static bool Case2Solution(List<List<int>> primitive)
    {
        var sub = primitive.Where(e => e.Count > 1).ToList();

        var b1 = false;

        foreach (var item in sub)
        {
            if (item.Count == 2)
            {
                b1 = CheckOneShape(item);

                if (b1) return true;
            }
            else if (item.Count == 3)
            {
                b1 = CheckCornShape(item);

                return true;
            }
            else if (item.Count == 4)
            {
                b1 = CheckSquareShape(item);

                return true;
            }
        }
        
        return false;
    }

    private static bool CheckSquareShape(List<int> primitive)
    {
        Debug.Log("square shape");
        primitive.Sort();

        var r1 = primitive[0] / ColNums;
        var r2 = primitive[1] / ColNums;
        var r3 = primitive[2] / ColNums;
        var r4 = primitive[3] / ColNums;

        var c1 = primitive[0] % ColNums;
        var c2 = primitive[1] % ColNums;
        var c3 = primitive[2] % ColNums;
        var c4 = primitive[3] % ColNums;

        var type = Stage[primitive[0]];

        var b1 = CheckLD(r1, c1, type, primitive);

        if (b1) return true;

        b1 = CheckRU(r2, c2, type, primitive);

        if (b1) return true;

        b1 = CheckLD(r3, c3, type, primitive);

        if (b1) return true;

        b1 = CheckRD(r4, c4, type, primitive);

        if (b1) return true;

        return false;
    }

    private static bool CheckCornShape(List<int> primitive)
    {
        Debug.Log("corn shape");
        primitive.Sort();

        var type = Stage[primitive[0]];

        foreach (var item in primitive)
        {
            var r = item / ColNums;
            var c = item % ColNums;

            var b1 = CheckLU(r, c, type, primitive);

            if (b1) return true;

            b1 = CheckLD(r, c, type, primitive);

            if (b1) return true;

            b1 = CheckRU(r, c, type, primitive);

            if (b1) return true;

            b1 = CheckRD(r, c, type, primitive);

            if (b1) return true;
        }

        return false;
    }

    private static bool CheckOneShape(List<int> primitive)
    {
        Debug.Log("one shape");
        primitive.Sort();

        var r1 = primitive[0] / ColNums;
        var r2 = primitive[1] / ColNums;

        var c1 = primitive[0] % ColNums;
        var c2 = primitive[1] % ColNums;

        var type = Stage[primitive[0]];

        if (r1 == r2)
        {
            var b1 = CheckLU(r1, c1, type, primitive);

            if (b1) return true;

            b1 = CheckLD(r1, c1, type, primitive);

            if (b1) return true;

            var b2 = CheckRU(r2, c2, type, primitive);

            if (b2) return true;

            b2 = CheckRD(r2, c2, type, primitive);

            if (b2) return true;
        }
        else
        {
            var b1 = CheckLU(r1, c1, type, primitive);

            if (b1) return true;

            b1 = CheckRU(r1, c1, type, primitive);

            if (b1) return true;

            var b2 = CheckLD(r2, c2, type, primitive);

            if (b2) return true;

            b2 = CheckRD(r2, c2, type, primitive);

            if (b2) return true;
        }

        return false;
    }

    static bool CheckLU(int r, int c, int type,List<int> primitive)
    {
        if(r - 1 < 0 || c - 1 < 0) return false;

        var lu = (r - 1) * ColNums + c - 1;

        if (primitive.Contains(lu)) return false;
       
        if (Stage[lu] == type)
            return true;
        else
        {
            var rp = 0;
            var cp = 0;

            CheckLeft3(r - 1, c - 1,type, ref rp);
            CheckRight3(r - 1, c - 1, type, ref rp);
            CheckUp3(r - 1, c - 1, type, ref cp);
            CheckDown3(r - 1, c - 1, type, ref cp);

            if (rp < 2 && cp < 2)
            {
                Stage[lu] = type;

                return true;
            }
            else return false;
        }
    }

    static bool CheckLD(int r, int c, int type,List<int> primitive)
    {
        if (r + 1 > RowNums - 1 || c - 1 < 0) return false;

        var ld = (r + 1) * ColNums + c - 1;

        if (primitive.Contains(ld)) return false;

        if (Stage[ld] == type)
            return true;
        else
        {
            var rp = 0;
            var cp = 0;

            CheckLeft3(r + 1, c - 1, type, ref rp);
            CheckRight3(r + 1, c - 1, type, ref rp);
            CheckUp3(r + 1, c - 1, type, ref cp);
            CheckDown3(r + 1, c - 1, type, ref cp);

            if (rp < 2 && cp < 2)
            {
                Stage[ld] = type;

                return true;
            }
            else return false;
        }
    }


    static bool CheckRU(int r, int c, int type,List<int> primitive)
    {
        if (r - 1 < 0 || c + 1 < ColNums - 1) return false;

        var ru = (r - 1) * ColNums + c + 1;

        if (primitive.Contains(ru)) return false;

        if (Stage[ru] == type)
            return true;
        else
        {
            var rp = 0;
            var cp = 0;

            CheckLeft3(r - 1, c + 1, type, ref rp);
            CheckRight3(r - 1, c + 1, type, ref rp);
            CheckUp3(r - 1, c + 1, type, ref cp);
            CheckDown3(r - 1, c + 1, type, ref cp);

            if (rp < 2 && cp < 2)
            {
                Stage[ru] = type;

                return true;
            }
            else return false;
        }
    }


    static bool CheckRD(int r, int c, int type,List<int> primitive)
    {
        if (r + 1 < RowNums - 1 || c + 1 < ColNums - 1) return false;

        var rd = (r + 1) * ColNums + c + 1;

        if (primitive.Contains(rd)) return false;

        if (Stage[rd] == type)
            return true;
        else
        {
            var rp = 0;
            var cp = 0;

            CheckLeft3(r + 1, c + 1, type, ref rp);
            CheckRight3(r + 1, c + 1, type, ref rp);
            CheckUp3(r + 1, c + 1, type, ref cp);
            CheckDown3(r + 1, c + 1, type, ref cp);

            if (rp < 2 && cp < 2)
            {
                Stage[rd] = type;

                return true;
            }
            else return false;
        }
    }


    static void CheckLeft3(int r, int c,int type, ref int counter, int depth = 1)
    {
        if (c < 0) return;

        var dd = c - depth;

        if (dd < 0) return;

        var index = r * ColNums + dd;

        if (index < 0 || index > AllNums - 1) return;

        if (Stage[index] == type)
        {
            counter++;
            depth++;
            CheckLeft3(r, c, type,ref counter, depth);
        }
    }

    static void CheckRight3(int r, int c, int type, ref int counter, int depth = 1)
    {
        if (c > ColNums - 1) return;

        var dd = c + depth;

        if (dd > ColNums - 1) return;

        var index = r * ColNums + dd;

        if (index < 0 || index > AllNums - 1) return;

        if (Stage[index] == type)
        {
            counter++;
            depth++;
            CheckRight3(r, c, type, ref counter, depth);
        }
    }

    static void CheckUp3(int r, int c, int type, ref int counter, int depth = 1)
    {
        if (r < 0) return;

        var dd = r - depth;

        if (dd < 0) return;

        var index = dd * ColNums + c;

        if (index < 0 || index > AllNums - 1) return;

        if (Stage[index] == type)
        {
            counter++;
            depth++;
            CheckUp3(r, c, type, ref counter, depth);
        }
    }

    static void CheckDown3(int r, int c, int type, ref int counter, int depth = 1)
    {
        if (r > RowNums - 1) return;

        var dd = r + depth;

        if (dd > RowNums - 1) return;

        var index = dd * ColNums + c;

        if (index < 0 || index > AllNums - 1) return;

        if (Stage[index] == type)
        {
            counter++;
            depth++;
            CheckDown3(r, c, type, ref counter, depth);
        }
    }


    private static bool Case1Solution(List<List<int>> primitive)
    {
        var sub = primitive.Where(e => e.Count > 3).ToList();

        foreach (var i in sub)
        {
            var r = i.Select(e => e / ColNums).ToList();
            var c = i.Select(e => e % ColNums).ToList();

            r.Sort();
            c.Sort();

            var r1 = r[0];
            var r2 = r[i.Count - 1];

            var c1 = c[0];
            var c2 = c[i.Count - 1];

            if (Math.Abs(r1 - r2) >= 2 || Math.Abs(c1 - c2) >= 2)
            {
                return true;
            }
        }

        return false;
    }

    private static List<List<int>> IdentifyPrimitive()
    {
        var allprimitive = new List<List<int>>();

        var waitcheck = Enumerable.Range(0, AllNums).ToList();

        for (var i = 0; i < AllNums; i++)
        {
            if (!waitcheck.Contains(i)) continue;

            waitcheck.Remove(i);

            var primitive = new List<int>() { i };

            SpawnPrimitive(i, primitive, waitcheck);
            
            allprimitive.Add(primitive);
        }

        foreach (var item in allprimitive)
        {
            Debug.Log(item.Count);
        }

        Debug.Log(waitcheck.Count);

        return allprimitive;
    }

    static void SpawnPrimitive(int index,List<int> primitive,List<int> waitcheck)
    {
        var r = index / ColNums;
        var c = index % ColNums;

        CheckUp2(r, c, primitive, waitcheck);
        CheckLeft2(r, c, primitive, waitcheck);
        CheckRight2(r, c, primitive, waitcheck);
        CheckDown2(r, c, primitive, waitcheck);
    }

    static void CheckUp2(int r, int c, List<int> primitive,List<int> waitcheck)
    {
        
        if (r < 0) return;

        var dd = r - 1;

        if (dd < 0) return;

        var index = dd * ColNums + c;

        if (primitive.Contains(index)) return;
        if (!waitcheck.Contains(index)) return;

        var oriindex = r * ColNums + c;

        if (Stage[oriindex] == Stage[index])
        {
            primitive.Add(index);
            waitcheck.Remove(index);

            SpawnPrimitive(index, primitive, waitcheck);
        }
    }

    static void CheckDown2(int r, int c, List<int> primitive, List<int> waitcheck)
    {
        if (r > RowNums - 1) return;

        var dd = r + 1;

        if (dd > RowNums - 1) return;

        var index = dd * ColNums + c;

        if (primitive.Contains(index)) return;
        if (!waitcheck.Contains(index)) return;

        var oriindex = r * ColNums + c;

        if (Stage[oriindex] == Stage[index])
        {
            primitive.Add(index);
            waitcheck.Remove(index);

            SpawnPrimitive(index, primitive, waitcheck);
        }
    }

    static void CheckLeft2(int r, int c, List<int> primitive, List<int> waitcheck)
    {
        if (c < 0) return;

        var dd = c - 1;

        if (dd < 0) return;

        var index = r * ColNums + dd;

        if (primitive.Contains(index)) return;
        if (!waitcheck.Contains(index)) return;

        var oriindex = r * ColNums + c;
 
        if (Stage[oriindex] == Stage[index])
        {           
            primitive.Add(index);
            waitcheck.Remove(index);

            SpawnPrimitive(index, primitive, waitcheck);
        }
    }

    static void CheckRight2(int r, int c, List<int> primitive, List<int> waitcheck)
    {
        if (c > ColNums - 1) return;

        var dd = c + 1;

        if (dd > ColNums - 1) return;

        var index = r * ColNums + dd;

        if (primitive.Contains(index)) return;
        if (!waitcheck.Contains(index)) return;

        var oriindex = r * ColNums + c;

        if (Stage[oriindex] == Stage[index])
        {
            primitive.Add(index);
            waitcheck.Remove(index);

            SpawnPrimitive(index, primitive, waitcheck);
        }
    }

    private static void CheckAllElemtent(int row, int col)
    {

        for (var i = 0; i < AllNums; i++)
        {
            var r = i / ColNums;
            var c = i % ColNums;

            var rcounter = 0;
            var ccounter = 0;

            CheckLeft(r, c, ref rcounter);
            CheckRight(r, c, ref ccounter);
            CheckUp(r, c, ref ccounter);
            CheckDown(r, c, ref ccounter);

            if (rcounter >= 2 || ccounter >= 2)
            {
                ReRoll(r, c);
            }
        }
    }

    static void CheckUp(int r, int c, ref int counter , int depth = 1)
    {
        if (r == 0) return;

        var dd = r - depth;

        if (dd < 0) return;

        var oriindex = r * ColNums + c;

        var index = dd * ColNums + c;

        if (Stage[oriindex] == Stage[index])
        {
            depth++;
            counter++;

            CheckUp(r, c, ref counter , depth);
        }
    }

    static void CheckDown(int r, int c, ref int counter, int depth = 1)
    {
        if (r == RowNums - 1) return;

        var dd = r + depth;

        if (dd > RowNums - 1) return;

        var oriindex = r * ColNums + c;

        var index = dd * ColNums + c;

        if (Stage[oriindex] == Stage[index])
        {
            depth++;
            counter++;

            CheckDown(r, c, ref counter, depth);
        }
    }

    static void CheckRight(int r, int c, ref int counter, int depth = 1)
    {
        if (c == ColNums - 1) return;

        var dd = c + depth;

        if (dd > ColNums - 1) return;

        var oriindex = r * ColNums + c;

        var index = r * ColNums + dd;

        if (Stage[oriindex] == Stage[index])
        {
            depth++;
            counter++;

            CheckRight(r, c, ref counter, depth);
        }
    }

    static void CheckLeft(int r, int c, ref int counter, int depth = 1)
    {
        if (c == 0) return;

        var dd = c - depth;

        if (dd < 0) return;

        var oriindex = r * ColNums + c;

        var index = r * ColNums + dd;

        if (Stage[oriindex] == Stage[index])
        {
            depth++;
            counter++;

            CheckLeft(r, c, ref counter, depth);
        }
    }

    static void ReRoll(int r, int c)
    {
        var type = Enumerable.Range(pson.Zero, pson.nums)
            .Except(CheckNeighborhood(r, c))
            .FirstOrDefault();

        var index = r * ColNums + c;

        Stage[index] = type;
    }

    static List<int> CheckNeighborhood(int r, int c)
    {
        var list = new List<int>();

        if (r - 1 >= 0)
        {
            list.Add(Stage[(r - 1) * ColNums + c]);
        }
        if (r + 1 <= RowNums - 1)
        {
            list.Add(Stage[(r + 1) * ColNums + c]);
        }
        if (c - 1 >= 0)
        {
            list.Add(Stage[r * ColNums + c - 1]);
        }
        if (c + 1 <= ColNums - 1)
        {
            list.Add(Stage[r * ColNums + c + 1]);
        }

        return list;
    }




    public static void Test1(List<pson> stage)
    {
        for (var i = 0; i < AllNums; i++)
        {
            var r = i / ColNums;
            var c = i % ColNums;

            var rcounter = 0;
            var ccounter = 0;

            CheckLeft(r, c, ref rcounter);
            CheckRight(r, c, ref ccounter);
            CheckUp(r, c, ref ccounter);
            CheckDown(r, c, ref ccounter);

            if (rcounter >= 2 || ccounter >= 2)
            {
                Debug.Log("???" + i);

                M3Test.check = true;

                return;
            }

        }

        Debug.Log("pass");
    }

    public static void Test2(List<pson> stage)
    {
        
    }
}
