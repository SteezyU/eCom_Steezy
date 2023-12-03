﻿using System.Reflection;
using FluentMigrator;

namespace Nop.Data.Migrations
{
    /// <summary>
    /// Represents a migration manager
    /// </summary>
    public interface IMigrationManager
    {
        /// <summary>
        /// Executes an Up for all found unapplied migrations
        /// </summary>
        /// <param name="assembly">Assembly to find migrations</param>
        /// <param name="migrationProcessType">Type of migration process</param>
        /// <param name="commitVersionOnly">Commit only version information</param>
        void ApplyUpMigrations(Assembly assembly, MigrationProcessType migrationProcessType = MigrationProcessType.Installation, bool commitVersionOnly = false);

        /// <summary>
        /// Executes a Down for all found (and applied) migrations
        /// </summary>
        /// <param name="assembly">Assembly to find the migration</param>
        void ApplyDownMigrations(Assembly assembly);

        /// <summary>
        /// Executes down expressions for the passed migration
        /// </summary>
        /// <param name="migration">Migration to rollback</param>
        void DownMigration(IMigration migration);

        /// <summary>
        /// Executes up expressions for the passed migration
        /// </summary>
        /// <param name="migration">Migration to apply</param>
        void UpMigration(IMigration migration);
    }
}