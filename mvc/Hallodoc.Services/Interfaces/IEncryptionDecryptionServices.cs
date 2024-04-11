using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IEncryptionDecryptionServices
    {
        string Encrypt(int id);
        int Decrypt(string cipherText);
    }
}
