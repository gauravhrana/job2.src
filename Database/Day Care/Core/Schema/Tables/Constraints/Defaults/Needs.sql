IF NOT EXISTS 
(
    SELECT	name 
    FROM	sys.objects 
    WHERE	type_desc       = 'DEFAULT_CONSTRAINT' 
    AND     name            = 'RequestDate'
)
ALTER TABLE NeedItem
	ADD CONSTRAINT DF_NeedItem_RequestDate		DEFAULT ''  FOR RequestDate               
GO

IF NOT EXISTS 
(
    SELECT name 
    FROM   sys.objects 
    WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
    AND       name			  = 'RecievedDate'
)
ALTER TABLE NeedItem
	ADD CONSTRAINT DF_NeedItem_RecievedDate		DEFAULT 1000   FOR RecievedDate  
GO

SELECT	name 
    FROM	sys.objects 
    WHERE	type_desc       = 'DEFAULT_CONSTRAINT' 
    AND     name            = 'NeedItemStatus'

ALTER TABLE NeedItem
	ADD CONSTRAINT DF_NeedItem_NeedItemStatus		DEFAULT ''  FOR NeedItemStatus              
GO

SELECT	name 
    FROM	sys.objects 
    WHERE	type_desc       = 'DEFAULT_CONSTRAINT' 
    AND     name            = 'NeedItemBy'

ALTER TABLE NeedItem
	ADD CONSTRAINT DF_NeedItem_NeedItemBy	DEFAULT ''  FOR NeedItemBy              
GO



-- Confirmation
SELECT	name, * 
FROM	sys.objects 
WHERE	type_desc           = 'DEFAULT_CONSTRAINT' 
AND		name   LIKE 'DF_NeedItem%'



