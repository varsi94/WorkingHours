﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Client.Interfaces
{
    public interface IIssueManager : IManager
    {
        Task CreateIssueForProject(int projectId, IssueHeader issue);
    }
}
