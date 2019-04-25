create database TestSystem;

use TestSystem;

CREATE TABLE Answer
(
	[AnswerId] int PRIMARY KEY IDENTITY,
	[Text] nvarchar(50) not null,
	[IsRight] bit not null,
	[QuestionId] int not null,
	CONSTRAINT FK_ANSWER_TO_QUESTIONS FOREIGN KEY ([QuestionId]) REFERENCES Question([QuestionId]) on delete cascade
)

create table Question
(
	[QuestionId] int primary key identity,
	[Text] nvarchar(200) not null,
	[Score] int not null,
	[TestId] int not null,
	constraint FK_Question_To_Test foreign key ([TestId]) references [Test]([TestId]) on delete cascade
)

create table [Test]
(
	[TestId] int primary key identity,
	[Name] nvarchar(50) not null,
	[ThemeId] int not null,
	constraint FK_Test_To_Theme foreign key ([ThemeId]) references [Theme]([ThemeId]) on delete cascade
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
	constraint FK_Result_To_Test foreign key ([TestId]) references [Test]([TestId]) on delete cascade,
	constraint FK_Result_To_User foreign key ([UserId]) references [User]([UserId])on delete cascade
)

drop table [User];
drop table [Result];
drop table [Theme];
drop table [Test];
drop table [Question];
drop table [Answer];

select * from Test
join Question on Test.TestId = Question.TestId
join Answer on Question.QuestionId = Answer.QuestionId
where Test.TestId = 1;