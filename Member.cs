﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class Member
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime MembershipStartDate { get; set; }
}