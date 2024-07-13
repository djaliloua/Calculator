﻿using Calculator.DataAccessLayer.Abstractions;
using Calculator.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculator.DataAccessLayer.Implementations
{
    public class Repository : GenericRepository<Operation>, IRepository
    {
        public async void DeleteAllAsync()
        {
            await _entities.ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public override void Delete(Operation item)
        {
            _entities
                .FromSql($"delete from OperationsTable\r\nwhere JULIANDAY(date('now')) - JULIANDAY(date(OperationDate)) > 31;");
        }
        private static bool Diff(DateTime t1, DateTime t2)
        {
            TimeSpan difference = t1 - t2;
            return difference.Days > 31;
        }
        

    }
}