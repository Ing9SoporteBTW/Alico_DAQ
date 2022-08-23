
namespace IntegrationEventLog.Utilities
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public class ResilientTransaction
    {
        private DbContext _context;
        private ILogger _log { get; set; }
        private ResilientTransaction(DbContext context, ILogger log) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _log = log ?? throw new ArgumentNullException(nameof(log));
            
        }

        public static ResilientTransaction New(DbContext context, ILogger log)=>
            new ResilientTransaction(context,log);
        


        public async Task ExecuteAsync(Func<Task> action)
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
            try
            {
                var strategy = _context.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    //_log.LogDebug("***** BeginTransaction {0}", _context.Database.BeginTransaction());
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        _log.LogInformation("----- ExecuteAsync Method {0}", transaction);
                        await action();
                        transaction.Commit();
                    }
                });
            }
            catch (Exception ex)
            {
                _log.LogError("**** Error In Process {0} with exception {1}", ex.Message, ex.InnerException);
                throw;
            }

        }
    }
}
