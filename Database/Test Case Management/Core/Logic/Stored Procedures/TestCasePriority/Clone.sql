			IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCasePriorityClone')
			BEGIN
				PRINT 'Dropping Procedure TestCasePriorityClone'
				DROP  Procedure TestCasePriorityClone
			END 
			GO

			PRINT 'Creating Procedure TestCasePriorityClone'
			GO

			/*********************************************************************************************
			**		File: 
			**		Name: TestCasePriorityClone
			**		Desc: 
			**
			**		This template can be customized:
			**              
			**		Return values:
			** 
			**		Called by:   
			**              
			**		Parameters:
			**		Input							Output
			**     ----------							-----------
			**
			**		Auth: 
			**		Date: 
			*********************************************************************************************
			**		Change History
			*********************************************************************************************
			**		Date:		Author:				Description:
			**		--------	--------			------------------------------------------------------
			**		
			**********************************************************************************************/

			CREATE Procedure dbo.TestCasePriorityClone
			(
					@TestCasePriorityId			INT			= NULL 	OUTPUT	
				,	@ApplicationId			INT			= NULL	
				,	@Name					VARCHAR(50)						
				,	@Description			VARCHAR(100)						
				,	@SortOrder				INT								
				,	@AuditId				INT									
				,	@AuditDate				DATETIME	= NULL				
				,	@SystemEntityType		VARCHAR(50) = 'TestCasePriority'			
			)
			AS
			BEGIN		

				SELECT	@ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
					,	@Description		= ISNULL(@Description, Description)
					,	@SortOrder			= ISNULL(@SortOrder, SortOrder)				
				FROM	dbo.TestCasePriority
				WHERE	TestCasePriorityId		= @TestCasePriorityId

				EXEC dbo.TestCasePriorityInsert 
						@TestCasePriorityId		=	NULL
					,	@ApplicationId		=	@ApplicationId
					,	@Name				=	@Name
					,	@Description		=	@Description
					,	@SortOrder			=	@SortOrder
					,	@AuditId			=	@AuditId

				-- Create Audit Record
				EXEC dbo.AuditHistoryInsert
						@SystemEntityType		= @SystemEntityType
					,	@EntityKey				= @TestCasePriorityId
					,	@AuditAction			= 'Clone'
					,	@CreatedDate			= @AuditDate
					,	@CreatedByPersonId		= @AuditId	

			END	
			GO
