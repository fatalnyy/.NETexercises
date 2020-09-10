using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSource
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EmailAddress> Emails { get; set; }
    }

    public class EmailAddress
    {
        public string Email { get; set; }
        public string EmailType { get; set; }
    }

    public class Account
    {
        public string Id { get; set; }
        public EmailAddress EmailAddress { get; set; }
    }

    public class Group
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public IEnumerable<Person> People { get; set; }
    }
}

namespace DemoTarget
{
    internal class ToDo
    {
        public IEnumerable<(DemoSource.Account, DemoSource.Person)> 
            MatchPersonToAccount(
                IEnumerable<DemoSource.Group> groups,
                IEnumerable<DemoSource.Account> accounts,
                IEnumerable<string> emails)
        {
            foreach (var group in groups)
                foreach (var person in group.People)
                    foreach (var email in person.Emails)
                        yield return (accounts.Where(p => p.EmailAddress.Email == email.Email).FirstOrDefault(), person);
        }
    }
}
