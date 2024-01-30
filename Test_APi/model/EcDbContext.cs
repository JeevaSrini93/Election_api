namespace Test_APi.model
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    // Enum to represent states in India
    public class ElectionManagerContext : DbContext
    {

        public ElectionManagerContext(DbContextOptions<ElectionManagerContext> options) : base(options)
        {

        }

        public DbSet<State> states { get; set; }
        public DbSet<MPSeat> MPSeats { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<VotingSystem> VotingSystems { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public DbSet<Ec> Ecs { get; set; }
    }

}
