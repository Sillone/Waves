using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class GetMatrix : MonoBehaviour
{
    public LinkedList<int[,]> MatrixList = new LinkedList<int[,]>();
    public int LvlNumber =2;
    public string HashString;
    public string[] lines, columns;
    public int Floor, StringNumber, columnNumber, post, I, J, NowFloor;
    int[][,] value;
    void Start()
    {
        FromFile();
        GetMatrixList();

        var lc = gameObject.GetComponent<LevelCreator>();
        lc.I = I-1; lc.J = J;
        lc.Data = MatrixList;

        if (MatrixList == null)
            print("pizdec");
        else
            print(MatrixList.Count.ToString() + "= levels");


        /*    var List = MatrixList.First;
            for (int k = 0; k < Floor - 1; k++)
            {
                var matrix = List.Value;
                for (int i = 0; i < I - 1; i++)
                {
                    string s = string.Empty;
                    for (int j = 0; j < J; j++)
                        s = s + " " + matrix[i, j].ToString();
                    print(i.ToString() + ")  " + s);
                }
                List = List.Next;
            }*/
        //Matrixlist—то что тебе нужно:)
        //Передавай куда хочешь.
    }
    void FromFile()
    {
        HashString = File.ReadAllText(Environment.CurrentDirectory + @"\Assets\LvlMatrix\" + LvlNumber.ToString() + ".txt");
        columns = HashString.Split(' ');
        Floor = int.Parse(columns[0]);
        StringNumber = int.Parse(columns[1]);
        columnNumber = int.Parse(columns[2]);
        HashString = HashString.Replace("\r", "");
        post = HashString.Length - HashString.IndexOf('n');
        HashString = HashString.Substring(HashString.IndexOf('n'), post);
        HashString = HashString.Replace("n\n", "");

        post = HashString.Length - HashString.IndexOf('t');
        lines = HashString.Split(new Char[] { '\n' });
        value = new int[Floor][,];
    }

    void GoTheNextFloor()
    {
        MatrixList.AddLast(value[NowFloor]);
        HashString = HashString.Substring(HashString.IndexOf('t'), post);
        HashString = HashString.Replace("t\n", "");
        lines = HashString.Split('\n');
        I = 0; J = -1;
        value[NowFloor] = new int[StringNumber, columnNumber];
        columns = lines[I].Split(' ');

    }
    void GetMatrixList()
    {
        for (NowFloor = 0; NowFloor < Floor - 1; NowFloor++)
        {
            value[NowFloor] = new int[StringNumber, columnNumber];
            for ( I = 0; I < StringNumber +1 ; I++)
            {
                if (lines[I] != "end")
                {
                    columns = lines[I].Split(' ');
                    for ( J = 0; J < columnNumber; J++)
                    {
                        if (columns[J] != "t")
                        {
                            value[NowFloor][I, J] = int.Parse(columns[J]);
                        }
                        else
                            GoTheNextFloor();
                    }
                }
            }
            MatrixList.AddLast(value[NowFloor]);
        }
    }
}

