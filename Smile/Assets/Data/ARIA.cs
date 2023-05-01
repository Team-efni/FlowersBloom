/*
*ARIA 알고리즘 스크립트
*
*구현 목표
*-데이터 암/복호화 작업 수행
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System;
using System.IO;
using System.Security.Cryptography;

public static class ARIA
{
    public static byte[] Encrypt(byte[] data, byte[] key)
    {
        using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
        {
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Key = key;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.Zeros;
            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            {
                return encryptor.TransformFinalBlock(data, 0, data.Length);
            }
        }
    }

    public static byte[] Decrypt(byte[] data, byte[] key)
    {
        using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
        {
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Key = key;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.Zeros;
            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            {
                return decryptor.TransformFinalBlock(data, 0, data.Length);
            }
        }
    }
}
