using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solidoc.Utility
{
    public static class TypeExtensions
    {
        public static IEnumerable<T> GetTypeMembers<T>(this Type iType)
        {
            var members = new List<T>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes();

                    var membersInAssembly = types.Where(x => iType.IsAssignableFrom(x) && !x.IsInterface).Select(Activator.CreateInstance).Cast<T>();

                    members.AddRange(membersInAssembly);
                }
                catch (Exception ex)
                {
                    if (ex is ReflectionTypeLoadException loadException)
                    {
                        var typeLoadException = loadException;
                        var loaderExceptions = typeLoadException.LoaderExceptions;

                        foreach (var exception in loaderExceptions)
                        {
                            ConsoleUtility.WriteException(exception.Message);
                        }
                    }
                }
            }

            return members;
        }

        public static IEnumerable<T> GetTypeMembersNotAbstract<T>(this Type iType)
        {
            var members = new List<T>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes();

                    var membersInAssembly = types.Where(x => iType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<T>();

                    members.AddRange(membersInAssembly);
                }
                catch (Exception ex)
                {
                    if (ex is ReflectionTypeLoadException loadException)
                    {
                        var typeLoadException = loadException;
                        var loaderExceptions = typeLoadException.LoaderExceptions;

                        foreach (var exception in loaderExceptions)
                        {
                            ConsoleUtility.WriteException(exception.Message);
                        }
                    }
                }
            }

            return members;
        }
    }
}