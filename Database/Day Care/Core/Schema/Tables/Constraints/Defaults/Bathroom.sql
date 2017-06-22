IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'StudentId'
)
ALTER TABLE Bathroom
ADD CONSTRAINT DF_Bathroom_StudentId        DEFAULT 1  FOR StudentId                
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'TimeIn'
)
ALTER TABLE Bathroom
ADD CONSTRAINT DF_Bathroom_TimeIn        DEFAULT ''    FOR TimeIn                
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'DiaperStatusId'
)
ALTER TABLE Bathroom
ADD CONSTRAINT DF_Bathroom_DiaperStatusId        DEFAULT 1    FOR DiaperStatusId                
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'DiaperCream'
)
ALTER TABLE Bathroom
ADD CONSTRAINT DF_Bathroom_DiaperCream        DEFAULT ''    FOR DiaperCream                
GO



IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name			  = 'PottyStatus'
)
ALTER TABLE Bathroom
ADD CONSTRAINT DF_Bathroom_PottyStatus DEFAULT ''  FOR PottyStatus  
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name			  = 'TeacherId'
)
ALTER TABLE Bathroom
ADD CONSTRAINT DF_Bathroom_TeacherId DEFAULT 1  FOR TeacherId  
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name			  = 'TeacherNotes'
)
ALTER TABLE Bathroom
ADD CONSTRAINT DF_Bathroom_TeacherNotes DEFAULT 1  FOR TeacherNotes  
GO



-- Confirmation
SELECT name, * 
FROM   sys.objects 
WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
AND       name   LIKE 'DF_Bathroom%'

