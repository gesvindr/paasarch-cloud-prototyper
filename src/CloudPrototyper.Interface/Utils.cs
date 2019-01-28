using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CloudPrototyper.Interface
{
    /// <summary>
    /// Provides functionality for working with types.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Loading all types takes many seconds and can be done only once.
        /// </summary>
        private static readonly Dictionary<string, List<Type>> TypeCache = new Dictionary<string, List<Type>>();

        /// <summary>
        /// Load all types recursively from folder specified by path parameter.
        /// </summary>
        /// <param name="path">Folder where to find libraries.</param>
        /// <returns></returns>
        public static List<Type> LoadTypes(string path)
        {
            if (TypeCache.ContainsKey(path))
            {
                return TypeCache[path];
            }
            var output = new List<Type>();
            List<string> allAssemblies = Directory.GetFiles(path, "*.dll").ToList();


            foreach (var assemblyPath in allAssemblies)
            {
                try
                {
                    var assembly = Assembly.LoadFile(assemblyPath);
                    output.AddRange(assembly.GetTypes());
                }
                catch (ReflectionTypeLoadException e)
                {
                     output.AddRange(e.Types.Where(t => t != null));
                }
                catch (Exception)
                {
                    // ignore
                }
            }

            TypeCache.Add(path, output);
            return output;
        }
        
        /// <summary>
        /// Finds all instances of T type withtin the value object.
        /// </summary>
        /// <typeparam name="T">Searched type.</typeparam>
        /// <param name="value">Root object.</param>
        /// <returns>List of all T instances in value.</returns>
        public static List<T> FindAllInstances<T>(object value) where T : class
        {

            HashSet<object> exploredObjects = new HashSet<object>();
            List<T> found = new List<T>();

            FindAllInstances(value, exploredObjects, found);

            return found;
        }

        private static void FindAllInstances<T>(object value, HashSet<object> exploredObjects, List<T> found) where T : class
        {
            if (value == null)
                return;

            if (exploredObjects.Contains(value))
                return;

            exploredObjects.Add(value);

            IEnumerable enumerable = value as IEnumerable;

            if (enumerable != null)
            {
                foreach (object item in enumerable)
                {
                    FindAllInstances<T>(item, exploredObjects, found);
                }
            }
            else
            {
                T possibleMatch = value as T;

                if (possibleMatch != null)
                {
                    found.Add(possibleMatch);
                }

                Type type = value.GetType();

                var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                                    BindingFlags.GetProperty);

                foreach (var property in properties)
                {
                    object propertyValue = property.GetValue(value, null);

                    FindAllInstances<T>(propertyValue, exploredObjects, found);
                }

            }
        }
    }
}
