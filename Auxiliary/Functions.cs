using Newtonsoft.Json;

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
    }
}
