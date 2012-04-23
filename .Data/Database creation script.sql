IF EXISTS(SELECT name FROM sys.databases WHERE name = 'VX')     
	DROP DATABASE VX;
CREATE DATABASE VX COLLATE SQL_Latin1_General_CP1_CI_AS;
GO
USE VX;
GO
CREATE SCHEMA domain;
GO

CREATE TABLE VX.domain.WORDS
(
	[PK_ID] int primary key clustered IDENTITY (1,1),
	[SPELLING] nvarchar(50),
	[TRANSCRIPTION] nvarchar(50),
	[LANGUAGE_ID] int NOT NULL
);

CREATE TABLE VX.domain.LANGUAGES
(
	[PK_ID] int primary key clustered IDENTITY(1,1),
	[NAME] nvarchar(50),
	[ABBREVIATION] varchar(2)
);

CREATE TABLE VX.domain.TRANSLATIONS
(
	[PK_ID] int primary key clustered IDENTITY(1,1),
	[SOURCE_ID] int NOT NULL,
	[TARGET_ID] int NOT NULL
)

ALTER TABLE VX.domain.WORDS
	ADD FOREIGN KEY ([LANGUAGE_ID]) REFERENCES VX.domain.LANGUAGES([PK_ID]);
	
ALTER TABLE VX.domain.TRANSLATIONS
	ADD FOREIGN KEY ([SOURCE_ID]) REFERENCES VX.domain.WORDS([PK_ID]);
	
ALTER TABLE VX.domain.TRANSLATIONS
	ADD FOREIGN KEY ([TARGET_ID]) REFERENCES VX.domain.WORDS([PK_ID]);
	
INSERT INTO VX.domain.LANGUAGES (NAME, ABBREVIATION) VALUES('english', 'en');
INSERT INTO VX.domain.LANGUAGES (NAME, ABBREVIATION) VALUES('russian', 'ru');

DECLARE @LangRu_ID int,
		@LangEn_ID int;
SELECT @LangEn_ID = PK_ID FROM VX.domain.LANGUAGES WHERE ABBREVIATION = 'en';
SELECT @LangRu_ID = PK_ID FROM VX.domain.LANGUAGES WHERE ABBREVIATION = 'ru';

DECLARE @SourceId int,
		@TargetId int,
		@TargetId2 int,
		@targetId3 int;
--ambitious
INSERT INTO VX.domain.WORDS VALUES('ambitious', N'æm''bɪʃəs', @LangEn_ID);
SELECT @SourceId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'честолюбивый', N'ч''истал''уб''`ивый''', @LangRu_ID);
SELECT @TargetId = @@IDENTITY;
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId);
--arrogant
INSERT INTO VX.domain.WORDS VALUES('arrogant', N'''ærəgənt', @LangEn_ID);
SELECT @SourceId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'высокомерный', N'высакам''`эрный''', @LangRu_ID);
SELECT @TargetId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'заносчивый', N'зан`ощ''ивый''', @LangRu_ID);
SELECT @TargetId2 = @@IDENTITY;
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId);
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId2);
--assertive
INSERT INTO VX.domain.WORDS VALUES('assertive', N'ə''sɜːtɪv', @LangEn_ID);
SELECT @SourceId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'напористый', N'нап`ор''истый''', @LangRu_ID);
SELECT @TargetId = @@IDENTITY;
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId);
--bad-tempered
INSERT INTO VX.domain.WORDS VALUES('bad-tempered', N'ˌbæd''tempəd', @LangEn_ID);
SELECT @SourceId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'вздорный', N'взд`орный''', @LangRu_ID);
SELECT @TargetId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'раздражительный', NULL, @LangRu_ID);
SELECT @TargetId2 = @@IDENTITY;
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId);
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId2);
--Calm
INSERT INTO VX.domain.WORDS VALUES('calm', N'kɑːm', @LangEn_ID);
SELECT @SourceId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'спокойный', N'спак`ой''ный''', @LangRu_ID);
SELECT @TargetId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'невозмутимый', N'н''ивазмут''`имый''', @LangRu_ID);
SELECT @TargetId2 = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'мирный', N'м''`ирный''', @LangRu_ID);
SELECT @targetId3 = @@IDENTITY;
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId);
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId2);
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @targetId3);
--cheerful
INSERT INTO VX.domain.WORDS VALUES('cheerful', N'''ʧɪəf(ə)l], [-ful', @LangEn_ID);
SELECT @SourceId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'весёлый', N'в''ис''олый''', @LangRu_ID);
SELECT @TargetId = @@IDENTITY;
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId);
--concientious
INSERT INTO VX.domain.WORDS VALUES('conscientious', N'ˌkɔn(t)ʃɪ''en(t)ʃəs', @LangEn_ID);
SELECT @SourceId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'добросовестный', N'дабрас`ов''издный''', @LangRu_ID);
SELECT @TargetId = @@IDENTITY;
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId);
--easy-going
INSERT INTO VX.domain.WORDS VALUES('easy-going', N'''iːzɪˌgəuɪŋ', @LangEn_ID);
SELECT @SourceId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'естественный', N'й''эст''`эздв''инный''', @LangRu_ID);
SELECT @TargetId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'легкий', N'л''окк''ий''', @LangRu_ID);
SELECT @TargetId2 = @@IDENTITY;
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId);
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId2);
--eccentric
INSERT INTO VX.domain.WORDS VALUES('eccentric', N'ɪk''sentrɪk], [ek-', @LangEn_ID);
SELECT @SourceId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'эксцентричный', N'эксциндр''`ич''ный''', @LangRu_ID);
SELECT @TargetId = @@IDENTITY;
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId);
--funny
INSERT INTO VX.domain.WORDS VALUES('funny', N'''fʌnɪ', @LangEn_ID);
SELECT @SourceId = @@IDENTITY;
INSERT INTO VX.domain.WORDS VALUES(N'забавный', N'заб`авный''', @LangRu_ID);
SELECT @TargetId = @@IDENTITY;
INSERT INTO VX.domain.TRANSLATIONS(SOURCE_ID, TARGET_ID) VALUES(@SourceId, @TargetId);

--INSERT INTO VX.domain.WORDS VALUES('immature', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('impulsive', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('insecure', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('insincere', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('loyal', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('open-minded', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('optimistic', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('possessive', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('reserved', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('self-confident', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('stubborn', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('vain', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('well-balanced', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);
--INSERT INTO VX.domain.WORDS VALUES('wise', N'', @LangEn_ID);
--INSERT INTO VX.domain.WORDS VALUES('--emptyRUS--', N'', @LangRu_ID);




