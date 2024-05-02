--Table for Crime
CREATE TABLE Crime (
 CrimeID INT PRIMARY KEY,
 IncidentType VARCHAR(255),
 IncidentDate DATE,
 Location VARCHAR(255),
 Description TEXT,
 Status VARCHAR(20)
);


--table for Victim
CREATE TABLE Victim (
 VictimID INT PRIMARY KEY,
 CrimeID INT,
 Name VARCHAR(255),
 ContactInfo VARCHAR(255),
 Injuries VARCHAR(255),
 FOREIGN KEY (CrimeID) REFERENCES Crime(CrimeID)
);

--Table for Suspect
CREATE TABLE Suspect (
 SuspectID INT PRIMARY KEY,
 CrimeID INT,
 Name VARCHAR(255),
 Description TEXT,
 CriminalHistory TEXT,
 FOREIGN KEY (CrimeID) REFERENCES Crime(CrimeID)
);

-- Insert sample data
INSERT INTO Crime (CrimeID, IncidentType, IncidentDate, Location, Description, Status)
VALUES
 (1, 'Robbery', '2023-09-15', '123 Main St, Cityville', 'Armed robbery at a convenience store', 'Open'),
 (2, 'Homicide', '2023-09-20', '456 Elm St, Townsville', 'Investigation into a murder case', 'Under
Investigation'),
 (3, 'Theft', '2023-09-10', '789 Oak St, Villagetown', 'Shoplifting incident at a mall', 'Closed');


INSERT INTO Victim (VictimID, CrimeID, Name, ContactInfo, Injuries)
VALUES
 (1, 1, 'John Doe', 'johndoe@example.com', 'Minor injuries'),
 (2, 2, 'Jane Smith', 'janesmith@example.com', 'Deceased'),
 (3, 3, 'Alice Johnson', 'alicejohnson@example.com', 'None');

INSERT INTO Suspect (SuspectID, CrimeID, Name, Description, CriminalHistory)
VALUES
 (1, 1, 'Robber 1', 'Armed and masked robber', 'Previous robbery convictions'),
 (2, 2, 'Unknown', 'Investigation ongoing', NULL),
 (3, 3, 'Suspect 1', 'Shoplifting suspect', 'Prior shoplifting arrests');

-- 1. Select all open incidents

select CrimeID,IncidentType from Crime where status='Open';

--2.Find the total number of incidents

select Count(*) as total from Crime;

--3. List all unique incident types.

select DISTINCT IncidentType from Crime ;

--4.Retrieve incidents that occurred between '2023-09-01' and '2023-09-10'.

select * from Crime where IncidentDate between '2023-09-01' and '2023-09-10';

-- 5.List persons involved in incidents in descending order of age.

alter table Victim add Age int; 
SELECT Name, Age
FROM Victim
ORDER BY Age DESC;


--6. Find the average age of persons involved in incidents.
SELECT AVG(age) AS averageAge
FROM (
    SELECT Age FROM Victim
    UNION ALL
    SELECT Age FROM Suspect
) AS AllPersons;



--7. List incident types and their counts, only for open cases.

select COUNT(IncidentType) from Crime where status='open';

--8.Find persons with names containing 'Doe'.

select * from Victim where name like '%Doe';

--9.Retrieve the names of persons involved in open cases and closed cases.

select v.name from Victim v
join Crime c
on v.CrimeID=c.CrimeID
where c.status IN ('open','close');

--.10. List incident types where there are persons aged 30 or 35 involved.

select c.IncidentType  from Crime c
join Victim v
on v.CrimeID=c.CrimeID
where v.age in(30,35);

--11. Find persons involved in incidents of the same type as 'Robbery'
insert into Crime values (4, 'Robbery', '2023-09-19', '123 Main St, newyork', 'jewel robbery at a jewelry store', 'Open')

Select v.name from Victim  v
join Crime c
on v.CrimeID=c.CrimeID
where c.IncidentType='Robbery';

--12. List incident types with more than one open case.

select IncidentType from crime where status =('open')
group by IncidentType
having count(*)>1

--13. List all incidents with suspects whose names also appear as victims in other incidents.
SELECT c.*
FROM Crime c
JOIN (
    SELECT Name
    FROM Victim
    UNION
    SELECT Name
    FROM Suspect
) AS VictimsAndSuspects ON c.CrimeID IN (
    SELECT  CrimeID
    FROM Victim
    WHERE Name IN (SELECT Name FROM Suspect)
);

--14. Retrieve all incidents along with victim and suspect details.

SELECT c.IncidentType, v.Name , v.ContactInfo  , v.Injuries ,
       s.Name , s.Description , s.CriminalHistory 
FROM Crime c
JOIN Victim v ON c.CrimeID = v.CrimeID
JOIN Suspect s ON c.CrimeID = s.CrimeID;

--15. Find incidents where the suspect is older than any victim.

alter table Suspect add Age int;
ALTER TABLE SUSPECT DROP COLUMN Ages;

SELECT c.*
FROM Crime c
JOIN (
    SELECT CrimeID, MAX(Age) AS MaxVictimAge
    FROM Victim
    GROUP BY CrimeID
) MaxVictimAgePerCrime ON c.CrimeID = MaxVictimAgePerCrime.CrimeID
JOIN (
    SELECT CrimeID, MAX(Age) AS MaxSuspectAge
    FROM Suspect
    GROUP BY CrimeID
) MaxSuspectAgePerCrime ON c.CrimeID = MaxSuspectAgePerCrime.CrimeID
WHERE MaxSuspectAgePerCrime.MaxSuspectAge > MaxVictimAgePerCrime.MaxVictimAge;


--16. Find suspects involved in multiple incidents

SELECT s.Name AS SuspectName, 
COUNT(s.CrimeID) AS IncidentCount
FROM Suspect s
GROUP BY s.Name
HAVING COUNT(s.CrimeID) > 1;

--17. List incidents with no suspects involved.


select c.IncidentType from Crime c
left join Suspect s
on s.CrimeID=c.CrimeID
where s.CrimeID is NULL

--18. List all cases where at least one incident is of type 'Homicide' and all other incidents are of type 'Robbery'.

SELECT CrimeID,IncidentType
FROM Crime
WHERE IncidentType = 'Homicide'
Union
SELECT CrimeID,IncidentType
FROM Crime
WHERE CrimeID NOT IN (SELECT CrimeID FROM Crime WHERE IncidentType <> 'Robbery');

--19. Retrieve a list of all incidents and the associated suspects, showing suspects for each incident, or 'No Suspect' if there are none.

select c.CrimeID, c.IncidentType, COALESCE(s.Name, 'No Suspect') AS SuspectName
from Crime c
left join Suspect s
on c.crimeID=s.crimeID


--20. List all suspects who have been involved in incidents with incident types 'Robbery' or 'Assault'

select s.* from Suspect s
join Crime c
on s.CrimeID=c.CrimeID
where c.IncidentType in ('Robbery','Assault');


