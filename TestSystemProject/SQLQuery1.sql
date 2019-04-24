create database TestSystem;

use TestSystem;

CREATE TABLE Answer
(
	[AnswerId] int PRIMARY KEY IDENTITY,
	[Text] nvarchar(50) not null,
	[IsRight] bit not null,
	[QuestionId] int not null,
	CONSTRAINT FK_ANSWER_TO_QUESTIONS FOREIGN KEY ([QuestionId]) REFERENCES Question([QuestionId]),
)

create table Question
(
	[QuestionId] int primary key identity,
	[Text] nvarchar(200) not null,
	[Score] int not null,
	[TestId] int not null,
	constraint FK_Question_To_Test foreign key ([TestId]) references [Test]([TestId])
)

create table [Test]
(
	[TestId] int primary key identity,
	[Name] nvarchar(50) not null,
	[ThemeId] int not null,
	constraint FK_Test_To_Theme foreign key ([ThemeId]) references [Theme]([ThemeId])
)

create table Theme
(
	[ThemeId] int primary key identity,
	[Name] nvarchar(50) not null,
	[Description] nvarchar(200) not null
)

create table [User]
(
	[UserId] int primary key identity,
	[Email] nvarchar(100) not null,
	[Password] nvarchar(100) not null,
	[Role] bit not null
)

create table [Result]
(
	[ResultId] int primary key identity,
	[DateOfTest] date not null,
	[TotalScore] int not null,
	[TestId] int not null,
	[UserId] int not null,
	constraint FK_Result_To_Test foreign key ([TestId]) references [Test]([TestId]),
	constraint FK_Result_To_User foreign key ([UserId]) references [User]([UserId])
)
