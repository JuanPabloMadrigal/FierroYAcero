
using System.Text;
using System.Security.Cryptography;
using System;
using System.IO;
using UnityEngine;
using System.Linq.Expressions;

public class CryptoHandler
{

    private const int keysize = 256; // Tamaño de la llave encriptada


    public static string EncryptStringAlt(string plainText, string passPhrase, string initV)
    {
        try
        {

            Debug.Log(initV);
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initV);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            Aes aesAlg = Aes.Create();
            aesAlg.Key = keyBytes;
            aesAlg.IV = initVectorBytes;
            aesAlg.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            cryptoStream.Close();
            memoryStream.Close();
            encryptor.Dispose();
            aesAlg.Dispose();
            password.Dispose();
            return Convert.ToBase64String(cipherTextBytes);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static string DecryptStringAlt(string cipherText, string passPhrase, string initV)
    {
        try
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initV);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);

            Aes aesAlg = Aes.Create();
            aesAlg.Key = keyBytes;
            aesAlg.IV = initVectorBytes;
            aesAlg.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            decryptor.Dispose();
            aesAlg.Dispose();
            password.Dispose();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
