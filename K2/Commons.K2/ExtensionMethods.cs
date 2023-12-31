﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="SURE International Technology">
//   Copyright © 2015 All Right Reserved
// </copyright>
// <summary>
//   The extension methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.K2
{
    using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
    using SourceCode.Workflow.Client;
    using SourceCode.Workflow.Management;
    using System.IO;
    using System.Linq;
    using static System.Net.Mime.MediaTypeNames;

    /// <summary>
    /// The extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the k2 work list.
        /// </summary>
        /// <param name="worklist">The work list.</param>
        /// <returns>K2 work list</returns>        
        public static K2Worklist GetK2Worklist(this Worklist worklist)
        {
            var list = new K2Worklist();
            list.AddRange(worklist.Cast<SourceCode.Workflow.Client.WorklistItem>().Select(worklistItem => worklistItem.GetK2WorklistItem()));
            return list;
        }

        /// <summary>
        /// Gets the k2 work list item.
        /// </summary>
        /// <param name="worklistItem">The work list item.</param>
        /// <returns>K2 work list item.</returns>
        public static K2WorklistItem GetK2WorklistItem(this SourceCode.Workflow.Client.WorklistItem worklistItem)
        {
         //List<IWorkflowAttachment>  c = worklistItem.ProcessInstance.Attachments.ToList();
         //List<IWorkflowComment> c = worklistItem.ProcessInstance.Comments.ToList();
            var item = new K2WorklistItem
            {
                Id = worklistItem.ID,
                ProcessFullName = worklistItem.ProcessInstance.FullName,
                Folio = worklistItem.ProcessInstance.Folio,
                ActivityName = worklistItem.ActivityInstanceDestination.Name,
                Url = worklistItem.Data,
                OriginatorName = worklistItem.ProcessInstance.Originator.Name,
                SerialNumber = worklistItem.SerialNumber,
                CreatedOn = worklistItem.ActivityInstanceDestination.StartDate,
                AllocatedUser = worklistItem.AllocatedUser,
                EventName = worklistItem.EventInstance.Name,
                ProcessInstanceId = worklistItem.ProcessInstance.ID,
                TaskDate = worklistItem.ActivityInstanceDestination.StartDate,
                UserName = worklistItem.ProcessInstance.Originator.Name,
                Comment = worklistItem.ProcessInstance.Comments.Any()? worklistItem.ProcessInstance.Comments.Select(x=>x.Message).ToList():null,
                Attachment = worklistItem.ProcessInstance.Attachments.Any()? worklistItem.ProcessInstance.Attachments.Select(x=>
                {
                    var filestream= x.GetFile();
                    byte[] bytes;
                    using (var binaryReader = new BinaryReader(filestream))
                    {
                        bytes = binaryReader.ReadBytes((int)filestream.Length);
                    }
                   // StreamReader reader = new StreamReader(filestream);
                    return  Convert.ToBase64String(bytes); 

                }).ToList():null,

            };
           
            return item;
        }

        public static K2ManagerWorklist GetK2ManagerWorklist(this SourceCode.Workflow.Management.WorklistItems worklist)
        {
            var list = new K2ManagerWorklist();
            list.AddRange(worklist.Cast<SourceCode.Workflow.Management.WorklistItem>().Select(worklistItem => worklistItem.GetK2ManagerWorklistItem()));
            return list;
        }

        public static K2WorklistItem GetK2ManagerWorklistItem(this SourceCode.Workflow.Management.WorklistItem worklistItem)
        {
            var item = new K2WorklistItem
            {
                Id = worklistItem.ID,
                ProcessFullName = worklistItem.ProcName,
                Folio = worklistItem.Folio,
                ActivityName = worklistItem.ActivityName,
                Url = "",
                OriginatorName = worklistItem.Users?.Count > 0 ? worklistItem.Users[0].UserName : "",
                SerialNumber = worklistItem.ProcInstID + "_" + worklistItem.ActInstDestID,
                CreatedOn = worklistItem.StartDate,
                AllocatedUser = worklistItem.Destination,
                EventName = worklistItem.EventName,
                ProcessInstanceId = worklistItem.ProcInstID,
                TaskDate = worklistItem.StartDate,
                UserName = worklistItem.Actioner.Name,
                ActionName = worklistItem.ActionName,
                ProcessInstanceStatus = (int)worklistItem.ProcessInstanceStatus,
                WorklistItemStatus = (int)worklistItem.Status,
                ActInstDestID = worklistItem.ActInstDestID,
                EventID = worklistItem.EventID


            };

            return item;
        }

        #endregion
    }
}