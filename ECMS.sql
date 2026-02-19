USE ECMS;

-- 1. Groups table
CREATE TABLE Groups (
    GroupID INT IDENTITY(1,1) PRIMARY KEY,
    GroupName NVARCHAR(255) NOT NULL,
    CreatedDate DATETIME2 DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1 NOT NULL
);

CREATE NONCLUSTERED INDEX IX_Groups_GroupName ON Groups (GroupName);
CREATE NONCLUSTERED INDEX IX_Groups_IsActive ON Groups (IsActive);

-- 2. Clients table
CREATE TABLE Clients (
    ClientID INT IDENTITY(1,1) PRIMARY KEY,
    ClientName NVARCHAR(255) NOT NULL,
    Email VARCHAR(254) NOT NULL,
    CreatedDate DATETIME2 DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1 NOT NULL
);

CREATE NONCLUSTERED INDEX IX_Clients_Email ON Clients (Email);
CREATE NONCLUSTERED INDEX IX_Clients_ClientName ON Clients (ClientName);
CREATE NONCLUSTERED INDEX IX_Clients_IsActive ON Clients (IsActive);

-- 3. ClientGroups table (Many-to-Many)
CREATE TABLE ClientGroups (
    ClientGroupID INT IDENTITY(1,1) PRIMARY KEY,
    ClientID INT NOT NULL FOREIGN KEY REFERENCES Clients(ClientID),
    GroupID INT NOT NULL FOREIGN KEY REFERENCES Groups(GroupID),
    UNIQUE(ClientID, GroupID)
);

CREATE NONCLUSTERED INDEX IX_ClientGroups_ClientID ON ClientGroups (ClientID);
CREATE NONCLUSTERED INDEX IX_ClientGroups_GroupID ON ClientGroups (GroupID);

-- 4. Campaigns table
CREATE TABLE Campaigns (
    CampaignID INT IDENTITY(1,1) PRIMARY KEY,
    CampaignName NVARCHAR(500) NOT NULL,
    Subject NVARCHAR(1000) NOT NULL,
    HtmlBody NVARCHAR(MAX) NOT NULL,
    Status INT NOT NULL DEFAULT 0 CHECK (Status IN (0,1,2,3)),
    CreatedDate DATETIME2 DEFAULT GETDATE(),
    ScheduledDate DATETIME2 NULL
);

CREATE NONCLUSTERED INDEX IX_Campaigns_Status ON Campaigns (Status);
CREATE NONCLUSTERED INDEX IX_Campaigns_ScheduledDate ON Campaigns (ScheduledDate);
CREATE NONCLUSTERED INDEX IX_Campaigns_CreatedDate ON Campaigns (CreatedDate);

-- 5. CampaignGroups table (Many-to-Many)
CREATE TABLE CampaignGroups (
    CampaignGroupID INT IDENTITY(1,1) PRIMARY KEY,
    CampaignID INT NOT NULL FOREIGN KEY REFERENCES Campaigns(CampaignID),
    GroupID INT NOT NULL FOREIGN KEY REFERENCES Groups(GroupID),
    UNIQUE(CampaignID, GroupID)
);

CREATE NONCLUSTERED INDEX IX_CampaignGroups_CampaignID ON CampaignGroups (CampaignID);
CREATE NONCLUSTERED INDEX IX_CampaignGroups_GroupID ON CampaignGroups (GroupID);

-- 6. CampaignLogs table
CREATE TABLE CampaignLogs (
    LogID BIGINT IDENTITY(1,1) PRIMARY KEY,
    CampaignID INT NOT NULL FOREIGN KEY REFERENCES Campaigns(CampaignID),
    ClientID INT NOT NULL FOREIGN KEY REFERENCES Clients(ClientID),
    Status INT NOT NULL CHECK (Status IN (0,1,2)),
    SentDate DATETIME2 NULL,
    ErrorMessage NVARCHAR(1000) NULL
);
