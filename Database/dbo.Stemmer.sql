CREATE TABLE [dbo].[Stemmer] (
    [UniekeCode]    NVARCHAR (50) NOT NULL,
    [HeeftStem]     BIT           NOT NULL,
    [StemmingsNaam] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([UniekeCode] ASC), 
    CONSTRAINT StemmingFK FOREIGN KEY (StemmingsNaam) REFERENCES Stemming(Naam)
);

