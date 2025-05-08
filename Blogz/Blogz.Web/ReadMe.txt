CREATE LOGIN myuser WITH PASSWORD = 'StrongPass123!';
CREATE USER myuser FOR LOGIN myuser;
ALTER ROLE db_owner ADD MEMBER myuser;
ALTER ROLE db_owner ADD MEMBER myuser;

SELECT name, is_disabled FROM sys.sql_logins WHERE name = 'myuser';

ALTER LOGIN myuser ENABLE;


## Right Click Server and Restart

Open Nuget Package Manage Console

-> Add-migration -context "dbcontext"
->update-database -context "dbcontext"