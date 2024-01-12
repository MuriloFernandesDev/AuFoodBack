using AuFood.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace AuFood.Auxiliary
{
    public static class Functions
    {
        public static int? HeaderStoreId(this HttpRequest pRequest, string pDefault = "")
        {
            string id = "";
            pRequest.Headers.TryGetValue("store_id", out var data);

            if (data.Count > 0)
                id = data.FirstOrDefault();

            if (id.IsNullOrEmpty())
                return null;

            try
            {
                return int.Parse(id);
            }
            catch 
            {
                return null;
            }
        }

        public static async Task<List<int>> getStores(_DbContext _context, string? email, int? Store_id)
        {
            if (email.IsNullOrEmpty())
                return new List<int>();

            var login = await _context.Login
                .Include(w => w.Store_login)
                .Where(w => w.Email == email)
                .FirstOrDefaultAsync();

            if (login == null)
                return new List<int>();

            var stores_permission = login.Store_login
                .Select(w => w.Store_id)
                .ToList();

            if(login.Profile == "adm")
                stores_permission = await _context.Store.Select(w => w.Id).ToListAsync();

            if (Store_id.HasValue)
            {
                var stores_filter = await _context.Store_login
                    .Where(w => w.Store_id == Store_id)
                    .Select(w => w.Store_id)
                    .ToListAsync();

                if(stores_filter.Count > 0)
                    stores_permission = stores_permission.Where(w => stores_filter.Contains(w)).ToList();
            }

            return stores_permission;
        }

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
