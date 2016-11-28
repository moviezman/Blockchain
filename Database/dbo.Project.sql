CREATE TABLE [dbo].[Project] (
    [UniekeCode]    NVARCHAR (50) NOT NULL,
    [Naam]          NVARCHAR (50) NULL,
    [AantalStemmen] INT           NULL,
    [StemmingsNaam] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([UniekeCode] ASC), 
    CONSTRAINT StemmingFK FOREIGN KEY (StemmingsNaam) REFERENCES Stemming(Naam)
);

