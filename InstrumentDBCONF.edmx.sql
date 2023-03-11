
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/10/2023 09:46:40
-- Generated from EDMX file: C:\c#tmp\source\repos\WindowsFormsApp1\InstrumentDBCONF.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SignalTypeInstrument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstrumentSet] DROP CONSTRAINT [FK_SignalTypeInstrument];
GO
IF OBJECT_ID(N'[dbo].[FK_AnalogRangeInstrument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstrumentSet] DROP CONSTRAINT [FK_AnalogRangeInstrument];
GO
IF OBJECT_ID(N'[dbo].[FK_SignalTypeMeasurment_]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Measurment] DROP CONSTRAINT [FK_SignalTypeMeasurment_];
GO
IF OBJECT_ID(N'[dbo].[FK_Measurment_Instrument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstrumentSet] DROP CONSTRAINT [FK_Measurment_Instrument];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[InstrumentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InstrumentSet];
GO
IF OBJECT_ID(N'[dbo].[SignalTypeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SignalTypeSet];
GO
IF OBJECT_ID(N'[dbo].[Measurment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Measurment];
GO
IF OBJECT_ID(N'[dbo].[AnalogRangeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnalogRangeSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'InstrumentSet'
CREATE TABLE [dbo].[InstrumentSet] (
    [Instrument_Id] int IDENTITY(1,1) NOT NULL,
    [InstrumentName] nvarchar(max)  NOT NULL,
    [SerialNo] nvarchar(max)  NOT NULL,
    [Comments] nvarchar(max)  NULL,
    [RegisterDate] datetime  NOT NULL,
    [SignalType_SignalType_Id] int  NOT NULL,
    [AnalogRange_Id] int  NOT NULL,
    [Measurment__Measurement_Id] int  NOT NULL
);
GO

-- Creating table 'SignalTypeSet'
CREATE TABLE [dbo].[SignalTypeSet] (
    [SignalType_Id] int IDENTITY(1,1) NOT NULL,
    [SignalTypeName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Measurment'
CREATE TABLE [dbo].[Measurment] (
    [Measurement_Id] int IDENTITY(1,1) NOT NULL,
    [MeasrumentType] nvarchar(max)  NOT NULL,
    [SignalType_SignalType_Id] int  NOT NULL
);
GO

-- Creating table 'AnalogRangeSet'
CREATE TABLE [dbo].[AnalogRangeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Lrv] float  NULL,
    [Urv] float  NULL,
    [AlramLow] float  NULL,
    [AlramHigh] float  NULL,
    [Span] float  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Instrument_Id] in table 'InstrumentSet'
ALTER TABLE [dbo].[InstrumentSet]
ADD CONSTRAINT [PK_InstrumentSet]
    PRIMARY KEY CLUSTERED ([Instrument_Id] ASC);
GO

-- Creating primary key on [SignalType_Id] in table 'SignalTypeSet'
ALTER TABLE [dbo].[SignalTypeSet]
ADD CONSTRAINT [PK_SignalTypeSet]
    PRIMARY KEY CLUSTERED ([SignalType_Id] ASC);
GO

-- Creating primary key on [Measurement_Id] in table 'Measurment'
ALTER TABLE [dbo].[Measurment]
ADD CONSTRAINT [PK_Measurment]
    PRIMARY KEY CLUSTERED ([Measurement_Id] ASC);
GO

-- Creating primary key on [Id] in table 'AnalogRangeSet'
ALTER TABLE [dbo].[AnalogRangeSet]
ADD CONSTRAINT [PK_AnalogRangeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SignalType_SignalType_Id] in table 'InstrumentSet'
ALTER TABLE [dbo].[InstrumentSet]
ADD CONSTRAINT [FK_SignalTypeInstrument]
    FOREIGN KEY ([SignalType_SignalType_Id])
    REFERENCES [dbo].[SignalTypeSet]
        ([SignalType_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SignalTypeInstrument'
CREATE INDEX [IX_FK_SignalTypeInstrument]
ON [dbo].[InstrumentSet]
    ([SignalType_SignalType_Id]);
GO

-- Creating foreign key on [AnalogRange_Id] in table 'InstrumentSet'
ALTER TABLE [dbo].[InstrumentSet]
ADD CONSTRAINT [FK_AnalogRangeInstrument]
    FOREIGN KEY ([AnalogRange_Id])
    REFERENCES [dbo].[AnalogRangeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnalogRangeInstrument'
CREATE INDEX [IX_FK_AnalogRangeInstrument]
ON [dbo].[InstrumentSet]
    ([AnalogRange_Id]);
GO

-- Creating foreign key on [SignalType_SignalType_Id] in table 'Measurment'
ALTER TABLE [dbo].[Measurment]
ADD CONSTRAINT [FK_SignalTypeMeasurment_]
    FOREIGN KEY ([SignalType_SignalType_Id])
    REFERENCES [dbo].[SignalTypeSet]
        ([SignalType_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SignalTypeMeasurment_'
CREATE INDEX [IX_FK_SignalTypeMeasurment_]
ON [dbo].[Measurment]
    ([SignalType_SignalType_Id]);
GO

-- Creating foreign key on [Measurment__Measurement_Id] in table 'InstrumentSet'
ALTER TABLE [dbo].[InstrumentSet]
ADD CONSTRAINT [FK_Measurment_Instrument]
    FOREIGN KEY ([Measurment__Measurement_Id])
    REFERENCES [dbo].[Measurment]
        ([Measurement_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Measurment_Instrument'
CREATE INDEX [IX_FK_Measurment_Instrument]
ON [dbo].[InstrumentSet]
    ([Measurment__Measurement_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------