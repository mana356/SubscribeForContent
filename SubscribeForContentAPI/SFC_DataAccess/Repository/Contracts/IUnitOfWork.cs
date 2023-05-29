﻿using Microsoft.EntityFrameworkCore;
using SFC_DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC_DataAccess.Repository.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        public SFCDBContext DbContext { get; }

        public IPostRepository PostRepository { get; }
        public IFileContentRepository FileContentRepository { get; }
        public IUserProfileRepository UserProfileRepository { get; }

        Task SaveAsync();
    }
}
