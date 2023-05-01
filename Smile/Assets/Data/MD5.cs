/*
*MD5 알고리즘 스크립트
*
*구현 목표
*-string 텍스트를 Hash화 시키는 기능
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System;
using System.Security.Cryptography;
using System.Text;

public static class MD5_Algo
{
    //사용 예제
    /*static void Main(string[] args)
    {
        string input = "Hello World";
        string hash = GetMD5Hash(input);
        Console.WriteLine(hash);
    }*/

    public static string GetMD5Hash(string input)
    {
        using (MD5 md5Hash = MD5.Create())
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}