using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace AuFood.Auxiliary
{
    public static class Functions
    {
        public static void SerializeProps<T, TU>(this T source, ref TU pDestino, bool pIgnoreNull = false)
        {
            var destProps = typeof(TU).GetProperties().Where(x => x.CanWrite && !Attribute.IsDefined(x, typeof(JsonIgnoreAttribute))).ToList();
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead && !Attribute.IsDefined(x, typeof(JsonIgnoreAttribute))).ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    {
                        if (!(pIgnoreNull && sourceProp.GetValue(source, null) == null))
                            p.SetValue(pDestino, sourceProp.GetValue(source, null), null);
                    }
                }
            }
        }

        public static string CripterString(string pSenha)
        {
            string vSenhaCript = "";
            using (SHA512 sha512Hash = SHA512.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(pSenha);
                byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                vSenhaCript = hash.ToLower();
            }
            return vSenhaCript;
        }

        public static byte[] CripterByte(string pSenha)
        {
            return Encoding.ASCII.GetBytes(CripterString(pSenha));
        }
    }
}
