CREATE TABLE [dbo].[users] (
    [userId]                 INT           IDENTITY (1, 1) NOT NULL,
    [firstname]              VARCHAR (20)  NOT NULL,
    [surname]                VARCHAR (20)  NULL,
    [username]               VARCHAR (15)  NULL,
    [email]                  VARCHAR (25)  NOT NULL,
    [password]               VARCHAR (100) NOT NULL,
    [confirmationCode]       VARCHAR (16)  NOT NULL,
    [status]                 INT           DEFAULT ((0)) NOT NULL,
    [creation]          DATETIME      NULL,
    [last_modification] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([userId] ASC)
);

CREATE TABLE [dbo].[notifications] (
    [notificationId] INT IDENTITY (1, 1) NOT NULL,
    [fk_users_userId] INT NOT NULL,
    [notificationTitle] VARCHAR (50) NOT NULL,
    [notificationMessage]   VARCHAR (200) NULL,
    [notificationStatus]  INT NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([notificationId] ASC),
    FOREIGN KEY ([fk_users_userId]) REFERENCES users([userId])    
);

CREATE TABLE [dbo].[chats] (
    [chatId] INT IDENTITY (1, 1) NOT NULL,
    [chatTitle] VARCHAR (50) NOT NULL,
    [chatStatus]  INT NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([chatId] ASC)   
);

CREATE TABLE [dbo].[usersChats] (
    [usersChatsId] INT IDENTITY (1, 1) NOT NULL,
    [fk_users_userId] INT NOT NULL,
    [fk_chats_chatId] INT NOT NULL,
    [usersChatsStatus]  INT NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([usersChatsId] ASC),
    FOREIGN KEY ([fk_users_userId]) REFERENCES users([userId]),
    FOREIGN KEY ([fk_chats_chatId]) REFERENCES chats([chatId])
);

CREATE TABLE [dbo].[messages] (
    [messageId] INT IDENTITY (1, 1) NOT NULL,
    [messageText] VARCHAR(256) NOT NULL,
    [fk_chats_chatId] INT NOT NULL,
    [fk_users_userId] INT NOT NULL,
    [messageStatus]  INT NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([messageId] ASC),
    FOREIGN KEY ([fk_users_userId]) REFERENCES users([userId]),
    FOREIGN KEY ([fk_chats_chatId]) REFERENCES chats([chatId])
);

CREATE TABLE [dbo].[projects] (
    [projectId] INT IDENTITY (1, 1) NOT NULL,
    [projectTitle] VARCHAR (50) NOT NULL,
    [projectDescription] VARCHAR (100) NULL,
    [prjectBody] VARCHAR (1000) NOT NULL,
    [projectKeywords] VARCHAR (500) NOT NULL,
    [projectStatus]  INT NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([projectId] ASC)
);

CREATE TABLE [dbo].[usersProjects] (
    [usersProjectsId] INT IDENTITY (1, 1) NOT NULL,
    [fk_projects_projectId] INT NOT NULL,
    [fk_users_userId] INT NOT NULL,
    [usersProjectsStatus]  INT NOT NULL DEFAULT 0,
    PRIMARY KEY CLUSTERED ([usersProjectsId] ASC),
    FOREIGN KEY ([fk_users_userId]) REFERENCES users([userId]),
    FOREIGN KEY ([fk_projects_projectId]) REFERENCES projects([projectId])
);

CREATE TABLE [dbo].[topics] (
    [topicId] INT IDENTITY (1, 1) NOT NULL,
    [topicTitle] VARCHAR (50) NOT NULL,
    [topicDescription]   VARCHAR (100) NULL,
    [topicBody]  VARCHAR (1000) NOT NULL,
    [topicKeywords]     VARCHAR (500) NOT NULL,
    [topicStatus]  INT NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([topicId] ASC)
);

CREATE TABLE [dbo].[topicsProjects] (
    [topicsProjectsId] INT IDENTITY (1, 1) NOT NULL,
    [fk_topics_topicId] INT NOT NULL,
    [fk_projects_projectId] INT NOT NULL,
    [topicsProjectsStatus]  INT NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([topicsProjectsId] ASC),
    FOREIGN KEY ([fk_topics_topicId]) REFERENCES topics([topicId]),
    FOREIGN KEY ([fk_projects_projectId]) REFERENCES projects([projectId])
);

CREATE TABLE [dbo].[shares] (
    [shareId] INT IDENTITY (1, 1) NOT NULL,
    [shareTitle] VARCHAR(50) NOT NULL,
    [shareText] VARCHAR(256) NOT NULL,
    [fk_users_userId] INT NOT NULL,
    [fk_projects_projectId] INT NOT NULL,
    [fk_topics_topicId] INT NOT NULL,
    [shareStatus]  INT NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([shareId] ASC),
    FOREIGN KEY ([fk_users_userId]) REFERENCES users([userId]),
    FOREIGN KEY ([fk_projects_projectId]) REFERENCES projects([projectId]),
    FOREIGN KEY ([fk_topics_topicId]) REFERENCES topics([topicId])
);

CREATE TABLE [dbo].[documentTypes] (
    [documentTypeId] INT IDENTITY (1, 1) NOT NULL,
    [documentTypeNname] VARCHAR(50) NOT NULL,
    [documentTypeDescription] VARCHAR(256) NOT NULL,
    [documentTypeStatus]  INT NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([documentTypeId] ASC)
);

CREATE TABLE [dbo].[documents] (
    [documentId] INT IDENTITY (1, 1) NOT NULL,
    [documentTitle] VARCHAR(50) NOT NULL,
    [documentDescription] VARCHAR(256) NOT NULL,
    [documentLocation] VARCHAR(256) NOT NULL,
    [fk_documentTypes_documentTypeId] INT NOT NULL,
    [fk_users_userId] INT NOT NULL,
    [fk_projects_projectId] INT,
    [fk_shares_shareId] INT,
    [fk_topics_topicId] INT NULL,
    [documentStatus]  INT NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([documentId] ASC),
    FOREIGN KEY ([fk_documentTypes_documentTypeId]) REFERENCES documentTypes([documentTypeId]),
    FOREIGN KEY ([fk_users_userId]) REFERENCES users([userId]),
    FOREIGN KEY ([fk_projects_projectId]) REFERENCES projects([projectId]),
    FOREIGN KEY ([fk_shares_shareId]) REFERENCES shares([shareId]),
    FOREIGN KEY ([fk_topics_topicId]) REFERENCES topics([topicId])  
);

CREATE INDEX [idx_chats_chatTitle] ON [dbo].[chats] ([chatTitle]);
CREATE INDEX [idx_documents_documentTitle] ON [dbo].[documents] ([documentTitle]);
CREATE INDEX [idx_notifications_notificationTitle] ON [dbo].[notifications] ([notificationTitle]);
CREATE INDEX [idx_usersChats_fk_userId_chatId] ON [dbo].[usersChats] ([fk_users_userId], [fk_chats_chatId]);
CREATE INDEX [idx_usersProjects_fk_userId_projectId] ON [dbo].[usersProjects] ([fk_users_userId], [fk_projects_projectId]);

CREATE INDEX [idx_projects_projectTitle] ON [dbo].[projects] ([projectTitle]);
CREATE INDEX [idx_projects_projectKeywords] ON [dbo].[projects] ([projectKeywords]);
CREATE INDEX [idx_topics_topicTitle] ON [dbo].[topics] ([topicTitle]);
CREATE INDEX [idx_topics_topicKeywords] ON [dbo].[topics] ([topicKeywords]);

CREATE INDEX [idx_shares_fk_userId_projectId_topicId] ON [dbo].[shares] ([fk_users_userId], [fk_projects_projectId], [fk_topics_topicId]);

CREATE INDEX [idx_topicsProjects_fk_projectsId_topicId] ON [dbo].[shares] ([fk_projects_projectId], [fk_topics_topicId]);




GO

CREATE TRIGGER [dbo].[tgr_set_modification_date]
    ON [dbo].[users]
    FOR UPDATE
    AS
    BEGIN
        SET NoCount ON
		DECLARE @userid AS INT
		SET @userid = (SELECT userId FROM inserted)

		UPDATE users SET last_modification = SYSDATETIME()
		WHERE userId = @userid
    END

	GO

CREATE TRIGGER [dbo].[tgr_set_creation_date]
    ON [dbo].[users]
    FOR INSERT
    AS
    BEGIN
        SET NoCount ON
		DECLARE @userid AS INT
		SET @userid = (SELECT userId FROM inserted)

		UPDATE users SET creation = SYSDATETIME()
		WHERE userId = @userid
    END