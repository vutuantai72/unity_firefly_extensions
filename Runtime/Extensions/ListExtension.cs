using System;
using System.Collections.Generic;
using Unity.Collections;
using Random = UnityEngine.Random;

namespace FireflyExtensions
{
    public static class ListExtension
    {
        // Fisher-Yates shuffle algorithm
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        public static NativeArray<TDest> ToNativeArray<TSource, TDest>(
            this List<TSource> sourceList,
            Func<TSource, TDest> convertFunc,
            Allocator allocator)
            where TDest : struct
        {
            var nativeArray =
                new NativeArray<TDest>(sourceList.Count, allocator, NativeArrayOptions.UninitializedMemory);
            for (int i = 0; i < sourceList.Count; i++)
            {
                nativeArray[i] = convertFunc(sourceList[i]);
            }

            return nativeArray;
        }
        
        public static NativeList<TDest> ToNativeList<TSource, TDest>(
            this List<TSource> sourceList,
            Func<TSource, TDest> convertFunc,
            Allocator allocator)
            where TDest : unmanaged
        {
            var nativeList = new NativeList<TDest>(allocator);

            foreach (var s in sourceList)
            {
                nativeList.Add(convertFunc(s));
            }

            return nativeList;
        }
        public static NativeList<T> ToNativeList<T>(
            this NativeList<T> sourceList,
            Func<T, bool> condition,
            Allocator allocator)
            where T : unmanaged
        {
            var result = new NativeList<T>(sourceList.Length, allocator);

            foreach (var s in sourceList)
            {
                if (condition(s))
                    result.Add(s);
            }

            return result;
        }
    }
}