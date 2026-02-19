USE ECMS;

INSERT INTO Groups (GroupName)
SELECT Name FROM (VALUES 
    (N'VIP Customers'),
    (N'Newsletter Subscribers'), 
    (N'High Value Customers'),
    (N'Abandoned Cart'),
    (N'New Subscribers'),
    (N'Loyal Customers')
) AS t(Name);

INSERT INTO Clients (ClientName, Email)
VALUES 
    (N'John Doe', 'john.doe@email.com'),
    (N'Jane Smith', 'jane.smith@email.com'),
    (N'Bob Johnson', 'bob.j@email.com'),
    (N'Alice Brown', 'alice.brown@email.com');

-- Insert multiple mappings at once
INSERT INTO ClientGroups (ClientID, GroupID)
SELECT ClientID, GroupID FROM (
    VALUES 
        (1,1), (1,2),  -- John: VIP, Newsletter
        (2,1), (2,2),  -- Jane: VIP, Newsletter
        (3,2),         -- Bob: Newsletter  
        (4,1)          -- Alice: VIP
) AS NewMappings(ClientID, GroupID);

-- All clients with their groups
SELECT 
    c.ClientID, 
    c.ClientName, 
    c.Email,
    STRING_AGG(g.GroupName, ', ') AS Groups
FROM Clients c
LEFT JOIN ClientGroups cg ON c.ClientID = cg.ClientID
LEFT JOIN Groups g ON g.GroupID = cg.GroupID
WHERE c.IsActive = 1
GROUP BY c.ClientID, c.ClientName, c.Email
ORDER BY c.ClientID;