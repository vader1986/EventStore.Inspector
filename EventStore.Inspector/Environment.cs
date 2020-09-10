using System.Linq;
using System.Reflection;
using CommandLine;

namespace EventStore.Inspector
{
    public class Environment
    {
        public static void BindOptions(object obj)
        {
            if (obj == null)
            {
                return;
            }

            var setters = obj
                .GetType()
                .GetProperties()
                .ToDictionary(pi => pi, pi => pi.GetSetMethod());

            foreach (var (propertyInfo, methodInfo) in setters)
            {
                if (methodInfo == null) continue;

                var option = propertyInfo.GetCustomAttribute(typeof(OptionAttribute)) as OptionAttribute;
                if (option == null) continue;

                var key = option.LongName ?? propertyInfo.Name;
                var value = System.Environment.GetEnvironmentVariable(key);

                if (!string.IsNullOrEmpty(value) && propertyInfo.PropertyType == typeof(string))
                {
                    methodInfo.Invoke(obj, new object?[]{value});
                }
            }
        }
    }
}
