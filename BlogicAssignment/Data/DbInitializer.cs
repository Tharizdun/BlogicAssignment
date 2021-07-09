using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogicAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogicAssignment.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BlogicAssignmentDbContext context) 
        {
            context.Database.EnsureCreated();

            // Look for any clients.
            if (context.Clients.Any())
            {
                return;   // DB has been seeded
            }

            // clients
            var clients = new Client[]
            {
                new Client{FirstName="Carson",LastName="Alexander",BirthNumber="900101/0018",Age=31,Email="Carson.Alexander@text.com",Phone="111111111"},
                new Client{FirstName="Meredith",LastName="Alonso",BirthNumber="900101/0018",Age=31,Email="Meredith.Alonso@text.com",Phone="222222222"},
                new Client{FirstName="Arturo",LastName="Anand",BirthNumber="900101/0018",Age=31,Email="Arturo.Anand@text.com",Phone="333333333"},
                new Client{FirstName="Gytis",LastName="Barzdukas",BirthNumber="900101/0018",Age=31,Email="Gytis.Barzdukas@text.com",Phone="444444444"},
            };

            foreach (Client s in clients)
            {
                context.Clients.Add(s);
            }
            context.SaveChanges();

            // advisors
            var advisors = new Advisor[]
           {
                new Advisor{FirstName="Yan",LastName="Li",BirthNumber="900101/0018",Age=31,Email="Yan.Li@text.com",Phone="555555555"},
                new Advisor{FirstName="Peggy",LastName="Justice",BirthNumber="900101/0018",Age=31,Email="Peggy.Justice@text.com",Phone="666666666"},
                new Advisor{FirstName="Laura",LastName="Norman",BirthNumber="900101/0018",Age=31,Email="Laura.Norman@text.com",Phone="777777777"},
                new Advisor{FirstName="Nino",LastName="Olivetto",BirthNumber="900101/0018",Age=31,Email="Nino.Olivetto@text.com",Phone="888888888"}
           };

            foreach (Advisor a in advisors)
            {
                context.Advisors.Add(a);
            }
            context.SaveChanges();

            // contracts
            var contracts = new Contract[]
           {
                //contract for Carson Alexander
                new Contract
                {
                    EvidenceNumber = "100",
                    Institution = "AIG",
                    ContractEnterDate = new DateTime(2021, 7, 9),
                    ContractValidSinceDate = new DateTime(2021, 7, 10),
                    ContractEndDate = new DateTime(2022, 12, 12),
                    ClientID = context.Clients.Single(c => c.LastName == "Alexander").ClientID,
                    SupervisorID = context.Advisors.Single(a => a.LastName == "Li").AdvisorID
                },

                // contracts for Meredith Alonso
                new Contract
                {
                    EvidenceNumber="101",
                    Institution="Allianz",
                    ContractEnterDate = new DateTime(2021, 7, 9),
                    ContractValidSinceDate = new DateTime(2021, 7, 10),
                    ContractEndDate = new DateTime(2022, 12, 12),
                    ClientID = context.Clients.Single(c => c.LastName == "Alonso").ClientID,
                    SupervisorID = context.Advisors.Single(a => a.LastName == "Li").AdvisorID
                },
                new Contract
                {
                    EvidenceNumber="102",
                    Institution="AXA",
                    ContractEnterDate = new DateTime(2021, 7, 9),
                    ContractValidSinceDate = new DateTime(2021, 7, 10),
                    ContractEndDate = new DateTime(2022, 12, 12),
                    ClientID = context.Clients.Single(c => c.LastName == "Alonso").ClientID,
                    SupervisorID = context.Advisors.Single(a => a.LastName == "Justice").AdvisorID
                },

                // contracts for Arturo Anand
                new Contract
                {
                    EvidenceNumber="103",
                    Institution="ČSOB",
                    ContractEnterDate = new DateTime(2021, 7, 9),
                    ContractValidSinceDate = new DateTime(2021, 7, 10),
                    ContractEndDate = new DateTime(2022, 12, 12),
                    ClientID = context.Clients.Single(c => c.LastName == "Anand").ClientID,
                    SupervisorID = context.Advisors.Single(a => a.LastName == "Li").AdvisorID
                },
                new Contract
                {
                    EvidenceNumber="104",
                    Institution="Česká podnikatelská pojišťovna",
                    ContractEnterDate = new DateTime(2021, 7, 9),
                    ContractValidSinceDate = new DateTime(2021, 7, 10),
                    ContractEndDate = new DateTime(2022, 12, 12),
                    ClientID = context.Clients.Single(c => c.LastName == "Anand").ClientID,
                    SupervisorID = context.Advisors.Single(a => a.LastName == "Justice").AdvisorID
                },
                new Contract
                {
                    EvidenceNumber="105",
                    Institution="Česká pojišťovna",
                    ContractEnterDate = new DateTime(2021, 7, 9),
                    ContractValidSinceDate = new DateTime(2021, 7, 10),
                    ContractEndDate = new DateTime(2022, 12, 12),
                    ClientID = context.Clients.Single(c => c.LastName == "Anand").ClientID,
                    SupervisorID = context.Advisors.Single(a => a.LastName == "Norman").AdvisorID
                },

                // contracts for Gytis Barzdukas
                new Contract
                {
                    EvidenceNumber="106",
                    Institution="Generali",
                    ContractEnterDate = new DateTime(2021, 7, 9),
                    ContractValidSinceDate = new DateTime(2021, 7, 10),
                    ContractEndDate = new DateTime(2022, 12, 12),
                    ClientID = context.Clients.Single(c => c.LastName == "Barzdukas").ClientID,
                    SupervisorID = context.Advisors.Single(a => a.LastName == "Li").AdvisorID
                },
                new Contract
                {
                    EvidenceNumber="107",
                    Institution="Kooperativa",
                    ContractEnterDate = new DateTime(2021, 7, 9),
                    ContractValidSinceDate = new DateTime(2021, 7, 10),
                    ContractEndDate = new DateTime(2022, 12, 12),
                    ClientID = context.Clients.Single(c => c.LastName == "Barzdukas").ClientID,
                    SupervisorID = context.Advisors.Single(a => a.LastName == "Justice").AdvisorID
                },
                new Contract
                {
                    EvidenceNumber="108"
                    ,Institution="Kooperativa",
                    ContractEnterDate = new DateTime(2021, 7, 9),
                    ContractValidSinceDate = new DateTime(2021, 7, 10),
                    ContractEndDate = new DateTime(2022, 12, 12),
                    ClientID = context.Clients.Single(c => c.LastName == "Barzdukas").ClientID,
                    SupervisorID = context.Advisors.Single(a => a.LastName == "Norman").AdvisorID
                },
                new Contract
                {
                    EvidenceNumber="109",
                    Institution="Kooperativa",
                    ContractEnterDate = new DateTime(2021, 7, 9),
                    ContractValidSinceDate = new DateTime(2021, 7, 10),
                    ContractEndDate = new DateTime(2022, 12, 12),
                    ClientID = context.Clients.Single(c => c.LastName == "Barzdukas").ClientID,
                    SupervisorID = context.Advisors.Single(a => a.LastName == "Olivetto").AdvisorID
                }
           };

            foreach (Contract c in contracts)
            {
                context.Contracts.Add(c);
            }
            context.SaveChanges();

            // AdvisorContract this is join table between Advisor and Contract that solves Many-to-Many relationship
            // by creating two one-to-many relationships between Advisor,Contract and AdvisorContract
            var advisorContracts = new AdvisorContract[]
           {
                new AdvisorContract
                {
                    AdvisorID = context.Advisors.Single(a => a.LastName == "Li").AdvisorID,
                    ContractID = context.Clients.Single(c => c.LastName == "Alexander").ClientID
                },
                new AdvisorContract
                {
                    AdvisorID = context.Advisors.Single(a => a.LastName == "Justice").AdvisorID,
                    ContractID = context.Clients.Single(c => c.LastName == "Alexander").ClientID
                },
               new AdvisorContract
                {
                    AdvisorID = context.Advisors.Single(a => a.LastName == "Norman").AdvisorID,
                    ContractID = context.Clients.Single(c => c.LastName == "Alexander").ClientID
                },
                new AdvisorContract
                {
                    AdvisorID = context.Advisors.Single(a => a.LastName == "Olivetto").AdvisorID,
                    ContractID = context.Clients.Single(c => c.LastName == "Alexander").ClientID
                },
           };

            foreach (AdvisorContract ac in advisorContracts)
            {
                context.AdvisorContracts.Add(ac);
            }
            context.SaveChanges();

        }
    }
}
