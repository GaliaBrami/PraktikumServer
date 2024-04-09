﻿using Solid.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Solid.Data.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly DataContext _context;
        public WorkerRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Worker>> GetMembersAsync()
        {
            return await _context.Workers.Include(u => u.Roles).ThenInclude(r=>r.Name).ToListAsync();
        }
        public async Task<Worker> GetByIdAsync(int id)
        {
            return await _context.Workers.Include(u => u.Roles).ThenInclude(r => r.Name).FirstAsync(b => b.Id == id);
        }

        public async Task<Worker> AddAsync(Worker m)
        {
            _context.Workers.Add(m);
            await _context.SaveChangesAsync();
            return m;
        }


        public async Task<Worker> PutAsync(int id, Worker value)
        {
            Worker m = _context.Workers.Find(id);
            if (m != null)
            {
                m.FirstName = value.FirstName;
                m.LastName = value.LastName;
                m.Status = value.Status;
                m.StartDate = value.StartDate;
                m.BirthDate = value.BirthDate;
                m.Gender = value.Gender;
                m.Identity = value.Identity;
                m.Roles = value.Roles;
            }
            await _context.SaveChangesAsync();
            return m;

        }

        public async Task<Worker> PutStatusAsync(int id)
        {
            Worker m = _context.Workers.Find(id);
            m.Status = !m.Status;
            await _context.SaveChangesAsync();
            return m;

        }

        public async Task<Worker> DeleteAsync(int id)
        {
            Worker m = _context.Workers.Find(id);
            if (m != null)
                _context.Workers.Remove(m);
            await _context.SaveChangesAsync();
            return m;
        }

    }
}
