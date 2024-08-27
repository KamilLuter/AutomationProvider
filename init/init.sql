-- restore-database.sql

RESTORE DATABASE [AutomationProviderDb]
FROM DISK = '/var/opt/mssql/backup/AutomationProviderDb.bak'
WITH
    MOVE 'AutomationProviderDb' TO '/var/opt/mssql/data/AutomationProviderDb.mdf',
    MOVE 'AutomationProviderDb_log' TO '/var/opt/mssql/data/AutomationProviderDb_log.ldf',
    REPLACE;
GO
