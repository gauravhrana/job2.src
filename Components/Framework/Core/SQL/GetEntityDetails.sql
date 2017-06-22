

DECLARE @NoOfRecords AS INT

SELECT @NoOfRecords=Count(AuditHistoryId) FROM CommonServices.dbo.AuditHistory 
WHERE SystemEntityId = @SystemEntityTypeId@ 

SELECT @NoOfRecords




