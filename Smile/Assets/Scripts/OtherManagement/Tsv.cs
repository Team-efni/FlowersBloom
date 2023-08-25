using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class TSV
{
    private string[] buff;
    private string ppat;
    public TSV(string filePath)
    {
        ppat = Path.Combine(Application.streamingAssetsPath, filePath);

        WWW reader = new WWW(ppat);
        while (!reader.isDone) { }
        buff = reader.text.Split('\n');
    }
    private DataTable LoadAllTSV(string lineName)
    {
        DataTable table=new DataTable();

        bool addColumns = false;

        foreach (string d in buff)
        {
            string[] split = d.Split('\t');

            if (split.Length > 0)
            {
                if (split[0] == lineName)
                {
                    // 헤더 행일 경우, 컬럼을 추가합니다.
                    foreach (string colName in split)
                    {
                        table.Columns.Add(colName);
                    }
                    addColumns = true;
                }
                else if (addColumns)
                {
                    // 컬럼이 추가되었을 경우에만 데이터 행으로 추가합니다.
                    table.Rows.Add(split);
                }
            }
        }

        return table;
    }


    public DataTable limitTSV(string command)
    {
        DataTable tb = LoadAllTSV("Command");
        DataTable limTb = tb.Clone(); // 동일한 스키마를 가진 빈 DataTable을 생성합니다.

        // Linq를 사용하여 선택된 행을 필터링합니다.
        DataRow[] selectedRows = tb.Select("Command = '" + command + "'");
        foreach (DataRow row in selectedRows)
        {
            limTb.ImportRow(row); // 선택된 행을 새 DataTable에 추가합니다.
        }

        return limTb;
    }
}