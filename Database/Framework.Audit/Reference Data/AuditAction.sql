EXEC dbo.AuditActionInsert 
				@AuditActionId	= 1	
			,	@Name			= 'Insert'
			,	@Description	= 'Insert'		  
			,	@SortOrder		= 100	

EXEC dbo.AuditActionInsert 
				@AuditActionId	= 2	
			,	@Name			= 'Update'
			,	@Description	= 'Update'		  
			,	@SortOrder		= 100	

EXEC dbo.AuditActionInsert 
				@AuditActionId	= 3	
			,	@Name			= 'Details'
			,	@Description	= 'Details'		  
			,	@SortOrder		= 100	

EXEC dbo.AuditActionInsert 
				@AuditActionId	= 4
			,	@Name			= 'Delete'
			,	@Description	= 'Delete'		  
			,	@SortOrder		= 100	

EXEC dbo.AuditActionInsert 
				@AuditActionId	= 5	
			,	@Name			= 'Search'
			,	@Description	= 'Search'		  
			,	@SortOrder		= 100
			
EXEC dbo.AuditActionInsert 
				@AuditActionId	= 6
			,	@Name			= 'Clone'
			,	@Description	= 'Clone'		  
			,	@SortOrder		= 100	
			
EXEC dbo.AuditActionInsert 
				@AuditActionId	= 7
			,	@Name			= 'DoesExist'
			,	@Description	= 'DoesExist'		  
			,	@SortOrder		= 100	
			
EXEC dbo.AuditActionInsert 
				@AuditActionId	= 8	
			,	@Name			= 'DeleteHard'
			,	@Description	= 'DeleteHard'		  
			,	@SortOrder		= 100	
			
EXEC dbo.AuditActionInsert 
				@AuditActionId	= 9
			,	@Name			= 'List'
			,	@Description	= 'List'		  
			,	@SortOrder		= 100	