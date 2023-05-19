using SFC_DataAccess.Data;
using SFC_DataAccess.Repository.Contracts;
using SFC_DataEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC_DataAccess.Repository
{

    public class FileContentRepository : Repository<FileContent>, IFileContentRepository
    {
        readonly SFCDBContext _context;

        public FileContentRepository(SFCDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
