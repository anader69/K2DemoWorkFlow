﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Commons.Framework.Extensions
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;



    #endregion

    /// <summary>
    ///     The object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts the anonymous type object to the <c>Dictionary{string, object}</c> type object.
        /// </summary>
        /// <param name="attributes">
        /// Anonymous type object.
        /// </param>
        /// <returns>
        /// Returns the <c>Dictionary{string, object}</c> type object.
        /// </returns>
        public static IDictionary<string, object> ConvertAnonymousObjectToDictionary(object attributes)
        {
            var dictionary = new Dictionary<string, object>();
            if (attributes == null)
            {
                return dictionary;
            }

            dictionary = attributes.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(pi => pi.Name, pi => pi.GetValue(attributes));
            return dictionary;
        }

        /// <summary>
        /// The is null or default.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsNullOrDefault<T>(this object obj)
        {
            if (obj == null)
            {
                return true;
            }

            if (Equals(obj, default(T)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The to.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// </exception>
        public static T To<T>(object obj, T defaultValue, Type type)
        {
            // Place convert to structures types here
            if (type == typeof(short))
            {
                short value;
                if (short.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(ushort))
            {
                ushort value;
                if (ushort.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(int))
            {
                int value;

                if (int.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(uint))
            {
                uint value;

                if (uint.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(long))
            {
                long value;
                if (long.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(ulong))
            {
                ulong value;
                if (ulong.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(float))
            {
                float value;
                if (float.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(double))
            {
                double value;
                if (double.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(decimal))
            {
                decimal value;
                if (decimal.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(bool))
            {
                bool value;
                if (bool.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(DateTime))
            {
                DateTime value;
                if (DateTime.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(DateTimeOffset))
            {
                DateTimeOffset value;
                if (DateTimeOffset.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(byte))
            {
                byte value;
                if (byte.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(Guid))
            {
                const string GuidRegEx =
                    @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$";
                var regEx = new Regex(GuidRegEx);
                if (regEx.IsMatch(obj.ToString()))
                {
                    return (T)(object)new Guid(obj.ToString());
                }

                return defaultValue;
            }

            if (type.GetTypeInfo().IsEnum)
            {
                if (Enum.IsDefined(type, obj))
                {
                    return (T)Enum.Parse(type, obj.ToString());
                }

                return defaultValue;
            }

            throw new NotSupportedException($"Couldn't parse \"{obj}\" as {type} to Type \"{typeof(T)}\"");
        }

        /// <summary>
        /// The to.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T To<T>(this object obj, T defaultValue = default(T))
        {
            if (obj == null)
            {
                return defaultValue;
            }

            if (obj is T)
            {
                return (T)obj;
            }

            var type = typeof(T);

            // Place convert to reference types here
            if (type == typeof(string))
            {
                return (T)(object)obj.ToString();
            }

            var underlyingType = Nullable.GetUnderlyingType(type);

            if (underlyingType != null)
            {
                return To(obj, defaultValue, underlyingType);
            }

            return To(obj, defaultValue, type);
        }

        /// <summary>
        /// object the json string. using JSON.Net Library
        /// </summary>
        /// <param name="o">
        /// The o.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToJson(this object o)
        {
            return JsonConvert.SerializeObject(o, JsonDotNetSerializer.SerializerSettings);
        }

        /// <summary>
        /// The to json.
        /// </summary>
        /// <param name="o">
        /// The o.
        /// </param>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToJson(this object o, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(o, settings);
        }

    }
}