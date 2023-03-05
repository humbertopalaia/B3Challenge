CREATE DATABASE B3Challenge

GO

USE B3Challenge

GO

CREATE TABLE dbo.TaskStatus
(
    [Id] int not null primary key,
    [Name] varchar(200) not null
)

GO


CREATE  TABLE [dbo].[Task](
	[Id] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Description] varchar(200) not null,
    [Date] DateTime not null,
	[TaskStatusId] [int] NULL
	   CONSTRAINT FK_TaskTaskStatus FOREIGN KEY (TaskStatusId)
        REFERENCES [dbo].[TaskStatus] (Id)
) ON [PRIMARY]


GO


INSERT dbo.TaskStatus
VALUES
    (1, 'Pendente'),
    (2, 'Em andamento'),
    (3, 'Parada'),
    (4, 'Concluída')
