EXEC dbo.AuditAction_Insert 
				@AuditActionId = 100	
			,	@Name			= 'Insert'
			,	@Description	= 'Insert'		  
			,	@SortOrder		= 100	
EXEC dbo.AuditAction_Insert 
				@AuditActionId = 200	
			,	@Name			= 'Update'
			,	@Description	= 'Update'		  
			,	@SortOrder		= 200	
EXEC dbo.AuditAction_Insert 
				@AuditActionId = 300	
			,	@Name			= 'Delete'
			,	@Description	= 'Delete'		  
			,	@SortOrder		= 300	
EXEC dbo.AuditAction_Insert 
				@AuditActionId = 400	
			,	@Name			= 'Details'
			,	@Description	= 'Details'		  
			,	@SortOrder		= 400
EXEC dbo.AuditAction_Insert 
				@AuditActionId = 500	
			,	@Name			= 'Search'
			,	@Description	= 'Search'		  
			,	@SortOrder		= 500

			