// --------------------------------------------------------------------------------------------------------------------
// <copyright file="K2WorklistItem.cs" company="SURE International Technology">
//   Copyright © 2015 All Right Reserved
// </copyright>
// <summary>
//   Defines K2 Work List Item
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.K2
{
    using System;

    /// <summary>
    /// K2 Work list Item
    /// </summary>


    public class K2WorklistItem
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the folio.
        /// </summary>
        public string Folio { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        public string OriginatorName { get; set; }

        /// <summary>
        /// Gets or sets the process name.
        /// </summary>
        public string ProcessFullName { get; set; }

        /// <summary>
        /// Gets or sets the activity name.
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the serial number.
        /// </summary>
        /// <value>
        /// The serial number.
        /// </value>        
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the allocated user.
        /// </summary>
        /// <value>
        /// The allocated user.
        /// </value>
        public string AllocatedUser { get; set; }

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        /// <value>
        /// The name of the event.
        /// </value>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the process instance identifier.
        /// </summary>
        /// <value>
        /// The process instance identifier.
        /// </value>
        public int ProcessInstanceId { get; set; }

        /// <summary>
        /// Gets or sets the task date.
        /// </summary>
        /// <value>
        /// The task date.
        /// </value>
        /// <created>7/14/2018</created>
        public DateTime TaskDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        /// <created>7/14/2018</created>
        public string UserName { get; set; }


        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the process instance status.
        /// Error = 0,
        /// Running = 1,
        /// Active = 2,
        /// Completed = 3,
        /// Stopped = 4,
        /// Deleted = 5,
        /// Undefined = 99
        /// </summary>
        /// <value>
        /// The process instance status.
        /// </value>
        /// <created>10/14/2018</created>
        public int ProcessInstanceStatus { get; set; }

        /// <summary>
        /// Gets or sets the worklist item status.
        /// Available = 0,
        /// Open = 1,
        /// Allocated = 2,
        /// Sleep = 3,
        /// Completed = 4
        /// </summary>
        /// <value>
        /// The worklist item status.
        /// </value>
        /// <created>10/14/2018</created>
        public int WorklistItemStatus { get; set; }

        public int ActInstDestID { get; set; }

        public int EventID { get; set; }

        #endregion
    }
}