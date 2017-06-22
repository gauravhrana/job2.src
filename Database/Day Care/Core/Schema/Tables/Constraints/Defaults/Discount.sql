IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'Description'
)
ALTER TABLE Discount
ADD CONSTRAINT DF_Discount_Description        DEFAULT ''    FOR Description                
GO


IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name			  = 'SortOrder'
)
ALTER TABLE Discount
ADD CONSTRAINT DF_Discount_SortOrder DEFAULT 1000   FOR SortOrder  
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name			  = 'Amount'
)
ALTER TABLE Discount
ADD CONSTRAINT DF_Discount_Amount DEFAULT 1000   FOR Amount  
GO

-- Confirmation
SELECT name, * 
FROM   sys.objects 
WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
AND       name   LIKE 'DF_Discount%'

