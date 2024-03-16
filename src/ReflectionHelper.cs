using System.Reflection;

namespace GlowingArrows;

public static class ReflectionHelper {
    public static T? GetField<T>(this object obj, string name) where T : class {
        return obj.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(obj) as T;
    }
}
