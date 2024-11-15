using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var connectionString = "mongodb://localhost:27017";
        var databaseName = "MemberDatabase";
        var collectionName = "Members";

        var memberService = new MemberService(connectionString, databaseName, collectionName);

        while (true)
        {
            Console.WriteLine("1. List all members");
            Console.WriteLine("2. Add a new member");
            Console.WriteLine("3. Update a member");
            Console.WriteLine("4. Delete a member");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    var members = await memberService.GetAllMembersAsync();
                    Console.WriteLine("Members:");
                    foreach (var member in members)
                    {
                        Console.WriteLine($"{member.Id}: {member.FirstName} {member.LastName}, {member.Address}, {member.PostalCode}, {member.Phone}, {member.Email}, {member.MembershipStartDate}");
                    }
                    break;

                case "2":
                    var newMember = new Member
                    {
                        FirstName = Prompt("First Name: "),
                        LastName = Prompt("Last Name: "),
                        Address = Prompt("Address: "),
                        PostalCode = Prompt("Postal Code: "),
                        Phone = Prompt("Phone: "),
                        Email = Prompt("Email: "),
                        MembershipStartDate = DateTime.Now
                    };
                    await memberService.CreateMemberAsync(newMember);
                    Console.WriteLine("Member created.");
                    break;

                case "3":
                    var updateId = Prompt("Enter the ID of the member to update: ");
                    var memberToUpdate = await memberService.GetMemberByIdAsync(updateId);
                    if (memberToUpdate != null)
                    {
                        memberToUpdate.FirstName = Prompt("First Name: ", memberToUpdate.FirstName);
                        memberToUpdate.LastName = Prompt("Last Name: ", memberToUpdate.LastName);
                        memberToUpdate.Address = Prompt("Address: ", memberToUpdate.Address);
                        memberToUpdate.PostalCode = Prompt("Postal Code: ", memberToUpdate.PostalCode);
                        memberToUpdate.Phone = Prompt("Phone: ", memberToUpdate.Phone);
                        memberToUpdate.Email = Prompt("Email: ", memberToUpdate.Email);
                        await memberService.UpdateMemberAsync(updateId, memberToUpdate);
                        Console.WriteLine("Member updated.");
                    }
                    else
                    {
                        Console.WriteLine("Member not found.");
                    }
                    break;

                case "4":
                    var deleteId = Prompt("Enter the ID of the member to delete: ");
                    await memberService.DeleteMemberAsync(deleteId);
                    Console.WriteLine("Member deleted.");
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static string Prompt(string message, string defaultValue = "")
    {
        Console.Write(message);
        var input = Console.ReadLine();
        return string.IsNullOrEmpty(input) ? defaultValue : input;
    }
}

