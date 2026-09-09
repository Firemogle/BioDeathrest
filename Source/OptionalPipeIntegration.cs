using System;
using System.Reflection;
using PipeSystem;
using Verse;

namespace DeathrestBiosculptor
{
    public static class OptionalPipeIntegration
    {
        public static float TryPullNutrition(Thing parent, float amount)
        {
            if (parent == null || amount <= 0f) return 0f;
            CompResource resource = parent.TryGetComp<CompResource>();
            if (resource == null || resource.PipeNet == null) return 0f;

            object net = resource.PipeNet;
            float drawn = InvokeDraw(net, "DrawResource", amount);
            if (drawn > 0f) return drawn;
            return InvokeDraw(net, "DrawAmongStorage", amount);
        }

        public static bool IsConnected(Thing parent)
        {
            if (parent == null) return false;
            CompResource resource = parent.TryGetComp<CompResource>();
            return resource != null && resource.PipeNet != null;
        }

        private static float InvokeDraw(object net, string methodName, float amount)
        {
            foreach (MethodInfo method in net.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (method.Name != methodName) continue;
                ParameterInfo[] ps = method.GetParameters();
                object[] args = new object[ps.Length];
                bool usable = true;
                int floatInputs = 0;
                for (int i = 0; i < ps.Length; i++)
                {
                    Type t = ps[i].ParameterType;
                    if (t.IsByRef)
                    {
                        Type et = t.GetElementType();
                        if (et == typeof(float)) args[i] = 0f;
                        else if (et == typeof(double)) args[i] = 0d;
                        else { usable = false; break; }
                    }
                    else if (t == typeof(float)) { args[i] = amount; floatInputs++; }
                    else if (t == typeof(double)) { args[i] = (double)amount; floatInputs++; }
                    else if (t == typeof(bool)) args[i] = true;
                    else if (!t.IsValueType) args[i] = null;
                    else { usable = false; break; }
                }
                if (!usable || floatInputs == 0) continue;
                try
                {
                    object result = method.Invoke(net, args);
                    float drawn = result is float f ? f : result is double d ? (float)d : 0f;
                    for (int i = 0; i < ps.Length; i++)
                    {
                        if (!ps[i].ParameterType.IsByRef) continue;
                        Type et = ps[i].ParameterType.GetElementType();
                        if (et == typeof(float) && args[i] is float of) drawn = Math.Max(drawn, of);
                        else if (et == typeof(double) && args[i] is double od) drawn = Math.Max(drawn, (float)od);
                    }
                    if (drawn > 0f) return drawn;
                }
                catch { }
            }
            return 0f;
        }
    }
}
