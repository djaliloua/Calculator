﻿using Calculator.DataAccessLayer.Abstractions;
using Calculator.DataAccessLayer.Contexts;
using Calculator.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculator.DataAccessLayer.Implementations
{
    public class Repository : GenericRepository<Operation>, IRepository
    {
        public Repository():base()
        {
            
        }
        public async void DeleteAllAsync()
        {
            await _context.Operations.ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public override void Delete(Operation item)
        {
            _context.Operations
                .FromSql($"delete from OperationsTable\r\nwhere JULIANDAY(date('now')) - JULIANDAY(date(OperationDate)) > 10;");
        }
    }
}
