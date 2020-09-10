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
}

namespace DemoTarget
{
    public class PersonWithEmail
    {
        // contains only characters from english alphabet
        // (a-z, both uppercase and lowercase) and numbers 0-9
        public string SanitizedNameWithId { get; set; }

        // to be formatteed based on email and email type
        public string FormattedEmail { get; set; }

    }
}

namespace DemoImplementation
{
    public static class Demo
    {
        /// <summary>
        /// Takie mapowanie może zostać użyte w przypadku, gdy chcemy wydobyć np. z bazy danych z jednej tabeli rekordy z odpowiadającymi dla nich rekordami z drugiej tabeli. Należałoby wtedy użyć klucza obcego,
        /// w celu powiązania tych tabel. W tym przypadku, występuje relacja jeden do wielu. W takim przypadku tabela przechowująca adresy mailowe, powinna zawierać pole "userId".
        /// Propercje "SanitizedNameWithId" oraz "FormattedEmail" zdefiniowałem w postaci stringa przechowującego propercje z odpowiednich klas, gdyż nie było informacji jak konkretnie powinien być zformatowany "FormattedEmail",
        /// w przypadku "SanitizedNameWithId" pewnie należałoby użyć jakiejś zewnętrznej biblioteki. Z powodu braku informacji jak te propercje miałyby być zformatowane, zrobiłem to w taki sposób jak w metodzie poniżej.
        /// </summary>
        /// <param name="people">IEnumerable of Person</param>
        /// <returns>IEnumerable of flatten people</returns>
        public static IEnumerable<DemoTarget.PersonWithEmail> Flatten(IEnumerable<DemoSource.Person> people)
        {
            foreach (var person in people)
            {
                foreach (var email in person.Emails)
                {
                    yield return new DemoTarget.PersonWithEmail
                    {
                        SanitizedNameWithId = $"Sanitized name: {person.Name}, with id: {person.Id}",
                        FormattedEmail = $"Formatted email: {email.Email} {email.EmailType}"
                    };
                }
            }
        }


        static void Main(string[] args)
        {
            //Testowanie zaimplementowanej metody Flatten

            var email1 = new DemoSource.EmailAddress
            {
                Email = "em1@a2.pl",
                EmailType = "1"
            };
            var email2 = new DemoSource.EmailAddress
            {
                Email = "drugi@a2.pl",
                EmailType = "2"
            };
            var email3 = new DemoSource.EmailAddress
            {
                Email = "em3@a2.pl",
                EmailType = "3"
            };

            var emails1 = new List<DemoSource.EmailAddress>();
            var emails2 = new List<DemoSource.EmailAddress>();
            var emails3 = new List<DemoSource.EmailAddress>();

            emails1.Add(email1);
            emails1.Add(email2);
            emails1.Add(email3);
            emails2.Add(email1);
            emails2.Add(email2);
            emails3.Add(email3);

            var p1 = new DemoSource.Person
            {
                Id = "123asf",
                Name = "Adam",
                Emails = emails1
            };

            var p2 = new DemoSource.Person
            {
                Id = "bbbaa124",
                Name = "Michał",
                Emails = emails2
            };

            var p3 = new DemoSource.Person
            {
                Id = "sfda1",
                Name = "Karol",
                Emails = emails3
            };

            var people = new List<DemoSource.Person>();
            people.Add(p1);
            people.Add(p2);
            people.Add(p3);

            var result = Flatten(people).ToList();

            result.ForEach(p => Console.WriteLine($"{p.SanitizedNameWithId}\n{p.FormattedEmail}"));

            Console.ReadKey();
        }
    }
}