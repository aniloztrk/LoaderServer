using System.Security.Cryptography;
using System.Text;

namespace LoaderServer.Utils;

public class AesEncryption
{
    public static byte[] EncryptBytes(byte[] assembly)
    {
        using (var encryptor = Aes.Create())
        {
            var pdb = new Rfc2898DeriveBytes("Xj!685f8z!?h1+2F8T4HBaYFd4?4F30J(dNUL21NM{|xR5/M)ZWC%nht£x7D@mO=2Z{Evp#'e43Y3v!F", new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x31 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(assembly, 0, assembly.Length);
                    cs.Close();
                }
                assembly = ms.ToArray();
            }
        }
        return assembly;
    }
    public static string EncryptString(string text)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(text);
        using (var encryptor = Aes.Create())
        {
            var pdb = new Rfc2898DeriveBytes("Xj!685f8z!?h1+2F8T4HBaYFd4?4F30J(dNUL21NM{|xR5/M)ZWC%nht£x7D@mO=2Z{Evp#'e43Y3v!F", new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x31 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                text = Convert.ToBase64String(ms.ToArray());
            }
        }
        return text;
    }
}