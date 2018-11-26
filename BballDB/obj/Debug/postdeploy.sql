﻿/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF '$(LoadTestData)' = 'true'

DELETE FROM GameDay;
DELETE FROM Player;

BEGIN

--Approved = 0
--Pending = 1
--Rejected = 2
INSERT INTO Player(ID, FirstName, LastName, PendingApproval) VALUES
('11', 'Klay', 'Thompson', 0),
('7', 'Kyle', 'Lowry', 0),
('34', 'Giannis', 'Antetokounmpo', 0),
('2', 'Kawhi', 'Leonard', 0),
('23', 'Anthony', 'Davis', 0),
('45', 'Lou', 'Williams', 0),
('33', 'Marc', 'Gasol', 0),
('9', 'Rajon', 'Rondo', 0),
('6', 'Lebron', 'James', 1);

--Not Upcoming = 0
INSERT INTO GameDay([Date], [Time], Venue, PassedGame, PaidBy, PaidAmount) VALUES
('28/10/2018', '9:30 PM', 'Staples Center', 0, '11', 55.00),
('04/11/2018', '9:30 PM', 'Staples Center', 0, '11', 55.00),
('11/11/2018', '10:00 PM', 'Madison Square Garden', 0, '34', 55.00),
('18/11/2018', '9:00 PM', 'Oracle Arena', 0, '2', 55.00),
('25/11/2018', '10:00 PM', 'Staples Center', 1, NULL, NULL);

END;
GO
