using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MemberService
{
    private readonly IMongoCollection<Member> _members;

    public MemberService(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _members = database.GetCollection<Member>(collectionName);
    }

    public async Task<List<Member>> GetAllMembersAsync()
    {
        return await _members.Find(member => true).ToListAsync();
    }

    public async Task<Member> GetMemberByIdAsync(string id)
    {
        return await _members.Find<Member>(member => member.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateMemberAsync(Member member)
    {
        await _members.InsertOneAsync(member);
    }

    public async Task UpdateMemberAsync(string id, Member memberIn)
    {
        await _members.ReplaceOneAsync(member => member.Id == id, memberIn);
    }

    public async Task DeleteMemberAsync(string id)
    {
        await _members.DeleteOneAsync(member => member.Id == id);
    }
}
