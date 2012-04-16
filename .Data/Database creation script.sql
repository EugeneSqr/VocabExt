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
	PK_ID int primary key clustered IDENTITY (1,1),
	SPELLING nvarchar(50),
	TRANSCRIPTION nvarchar(50),
	LANGUAGE_ID int NOT NULL
);

CREATE TABLE VX.domain.LANGUAGES
(
	PK_ID int primary key clustered IDENTITY(1,1),
	NAME nvarchar(50),
	ABBREVIATION varchar(2)
);

ALTER TABLE VX.domain.WORDS
	ADD FOREIGN KEY (LANGUAGE_ID) REFERENCES VX.domain.LANGUAGES(PK_ID)
	
INSERT INTO VX.domain.LANGUAGES (NAME, ABBREVIATION) VALUES('english', 'en');
INSERT INTO VX.domain.LANGUAGES (NAME, ABBREVIATION) VALUES('russian', 'ru');

DECLARE @LangRu_ID int,
		@LangEn_ID int;
SELECT @LangEn_ID = PK_ID FROM VX.domain.LANGUAGES WHERE ABBREVIATION = 'en';
SELECT @LangRu_ID = PK_ID FROM VX.domain.LANGUAGES WHERE ABBREVIATION = 'ru';

INSERT INTO VX.domain.WORDS (SPELLING, TRANSCRIPTION, LANGUAGE_ID) VALUES('cat', N'kæt', @LangEn_ID);
INSERT INTO VX.domain.WORDS (SPELLING, TRANSCRIPTION, LANGUAGE_ID) VALUES('dog', N'dɔɡ', @LangEn_ID);
INSERT INTO VX.domain.WORDS (SPELLING, TRANSCRIPTION, LANGUAGE_ID) VALUES('cow', N'kau', @LangEn_ID);



