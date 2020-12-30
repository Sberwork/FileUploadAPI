using System;
using System.Collections.Generic;
using System.Linq;
using CommandDAL.Models;

namespace CommandDAL.Data
{
    public class SqlCommandAPIRepo : ICommandAPIRepo
    {
        private readonly ApplicationContext _context;
        public SqlCommandAPIRepo(ApplicationContext context)
        {
            _context = context;
        }
        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.CommandItems.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.CommandItems.Remove(cmd);
        }
        public IEnumerable<Command> GetAllCommands()
        {
            return _context.CommandItems.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.CommandItems.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {

        }
    }
}