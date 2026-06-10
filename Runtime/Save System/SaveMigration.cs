using System;
using System.Collections.Generic;
using UnityEngine;

namespace KH
{
    public interface IKHSaveMigration<T>
    {
        int Version { get; }

        T Migrate(T data, int fromVersion, int toVersion);
    }

    public static class KHSaveMigrationSystem
    {
        private static Dictionary<Type, object> migrations = new();

        public static void Register<T>(IKHSaveMigration<T> migration)
        {
            migrations[typeof(T)] = migration;
        }

        public static T Migrate<T>(T data, int fromVersion, int toVersion)
        {
            if (!migrations.TryGetValue(typeof(T), out var migObj))
                return data;

            var migration = (IKHSaveMigration<T>)migObj;
            return migration.Migrate(data, fromVersion, toVersion);
        }
    }
}