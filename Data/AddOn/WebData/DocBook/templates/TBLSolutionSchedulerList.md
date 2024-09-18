
 IF OBJECT_ID('[dbo].[SolutionSchedulerList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionSchedulerList] 
 GO
 CREATE TABLE [dbo].[SolutionSchedulerList] ( 
 [Id]                     INT              IDENTITY(1,1)          NOT NULL,
 [InheritedGroupName]     VARCHAR(50)                             NOT NULL,
 [Name]                   VARCHAR(255)                            NOT NULL,
 [Sequence]               INT                                     NOT NULL,
 [Email]                  VARCHAR(255)                                NULL,
 [Data]                   TEXT                                    NOT NULL,
 [Description]            TEXT                                        NULL,
 [StartNowOnly]           BIT                                     NOT NULL,
 [StartAt]                DATETIME2                                   NULL,
 [FinishAt]               DATETIME2                                   NULL,
 [Interval]               INT                                     NOT NULL,
 [InheritedIntervalType]  VARCHAR(50)                             NOT NULL,
 [UserId]                 INT                                     NOT NULL,
 [Active]                 BIT                                     NOT NULL  CONSTRAINT [DF_GlobalAutoSchedulerList_Active] DEFAULT ((1)),
 [TimeStamp]              DATETIME2                               NOT NULL  CONSTRAINT [DF_GlobalAutoSchedulerList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SolutionSchedulerList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_SolutionSchedulerList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_GlobalAutoSchedulerList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 