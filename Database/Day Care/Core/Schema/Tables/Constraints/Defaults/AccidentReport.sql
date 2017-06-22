IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'Date'
)
ALTER TABLE AccidentReport
ADD CONSTRAINT DF_AccidentReport_Date        DEFAULT ''    FOR Date                
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'AccidentPlace'
)
ALTER TABLE AccidentReport
ADD CONSTRAINT DF_AccidentReport_AccidentPlace        DEFAULT ''    FOR AccidentPlace                
GO
IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'Description'
)
ALTER TABLE AccidentReport
ADD CONSTRAINT DF_AccidentReport_Description        DEFAULT ''    FOR Description                
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'Remedy'
)
ALTER TABLE AccidentReport
ADD CONSTRAINT DF_AccidentReport_Remedy DEFAULT ''    FOR Remedy               
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'SignoffParent'
)
ALTER TABLE AccidentReport
ADD CONSTRAINT DF_AccidentReport_SignoffParent DEFAULT '' FOR SignoffParent               
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'SignoffTeacher'
)
ALTER TABLE AccidentReport
ADD CONSTRAINT DF_AccidentReport_SignoffTeacher DEFAULT '' FOR SignoffTeacher              
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'SignoffAdmin'
)
ALTER TABLE AccidentReport
ADD CONSTRAINT DF_AccidentReport_SignoffAdmin DEFAULT '' FOR SignoffAdmin              
GO

-- Confirmation
SELECT name, * 
FROM   sys.objects 
WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
AND       name   LIKE 'DF_AccidentReport%'

