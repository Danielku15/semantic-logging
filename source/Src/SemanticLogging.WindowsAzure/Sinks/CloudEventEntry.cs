﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks.WindowsAzure
{
    /// <summary>
    /// Represents a log entry in a Windows Azure Table.
    /// </summary>
    public sealed class CloudEventEntry
    {
        private const string RowKeyFormat = "{0}_{1}_{2:X5}";

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudEventEntry"/> class.
        /// </summary>
        public CloudEventEntry()
        {
            this.Payload = new Dictionary<string, object>();
            this.EventDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>
        /// The event id.
        /// </value>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the event date.
        /// </summary>
        /// <value>
        /// The event date.
        /// </value>
        public DateTime EventDate { get; set; }

        /// <summary>
        /// Gets or sets the keywords for the event.
        /// </summary>
        /// <value>
        /// The keywords.
        /// </value>
        public long Keywords { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the provider, which is typically the class derived from <see cref="System.Diagnostics.Tracing.EventSource"/>.
        /// </summary>
        /// <value>
        /// The provider ID.
        /// </value>
        public Guid ProviderId { get; set; }

        /// <summary>
        /// Gets or sets the friendly name of the class that is derived from the event source.
        /// </summary>
        /// <value>
        /// The name of the event source.
        /// </value>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the instance name where the entries are generated from.
        /// </summary>
        /// <value>
        /// The name of the instance.
        /// </value>
        public string InstanceName { get; set; }

        /// <summary>
        /// Gets or sets the level of the event.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the message for the event.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the operation code for the event.
        /// </summary>
        /// <value>
        /// The operation code.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Opcode", Justification = "Uses casing from EventWrittenEventArgs.Opcode")]
        public int Opcode { get; set; }

        /// <summary>
        /// Gets or sets the task for the event.
        /// </summary>
        /// <value>
        /// The task code.
        /// </value>
        public int Task { get; set; }

        /// <summary>
        /// Gets or sets the version of the event.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the payload for the event.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public Dictionary<string, object> Payload { get; set; }

        /// <summary>
        /// Gets or sets the process id.
        /// </summary>
        /// <value>The process id.</value>
        public int ProcessId { get; set; }

        /// <summary>
        /// Gets or sets the thread id.
        /// </summary>
        /// <value>The thread id.</value>
        public int ThreadId { get; set; }

        /// <summary>
        /// Gets or sets the activity id for the event.
        /// </summary>
        /// <value>
        /// The activity id.
        /// </value>
        public Guid ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the related activity id for the event.
        /// </summary>
        /// <value>
        /// The related activity id.
        /// </value>
        public Guid RelatedActivityId { get; set; }

        /// <summary>
        /// Gets or sets the entity's partition key.
        /// </summary>
        /// <value>
        /// The partition key.
        /// </value>
        internal string PartitionKey { get; set; }

        /// <summary>
        /// Gets or sets the entity's partition key.
        /// </summary>
        /// <value>
        /// The row key.
        /// </value>
        internal string RowKey { get; set; }

        /// <summary>
        /// Create a key for the entity.
        /// </summary>
        /// <param name="sortKeysAscending"><see langword="true" /> generates WAD-style keys, otherwise it uses an key generated from a reversed tick value that is sorted from newest to oldest.</param>
        /// <param name="salt">The salt for the key.</param>
        public void CreateKey(bool sortKeysAscending, int salt)
        {
            this.PartitionKey = sortKeysAscending ? this.EventDate.GeneratePartitionKey() : this.EventDate.GeneratePartitionKeyReversed();

            this.RowKey = string.Format(
                CultureInfo.InvariantCulture,
                RowKeyFormat,
                this.InstanceName,
                sortKeysAscending ? this.EventDate.GetTicks() : this.EventDate.GetTicksReversed(),
                salt);
        }
    }
}
